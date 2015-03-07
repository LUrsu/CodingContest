using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace CC.Domain.CodeCompilers
{
    public class CodeCompiler
    {
        private const string JVM = "\"" + @"C:\Program Files\Java\jdk1.7.0_03\bin\java.exe" + "\"";
        private const string RunJavaBatch = @"C:\compiler\runJava.bat";
        private const string JavaBatchFileDirectory = @"C:\compiler\compileJava.bat";
        private const string JavaCompilerFileDirectory = "C:\\\"Program Files\"\\Java\\jdk1.7.0_03\\bin\\javac.exe";
        private const string CSharpBatchFileDirectory = @"C:\compiler\compileCSharp.bat";
        private const string CSharpCompilerFileDirectory = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe";
        private const string CPPBatchFileDirectory = @"C:\compiler\compileCPP.bat";
        private const string CPPCompilerFileDirectory = @"C:\Dev-Cpp\bin\c++.exe";
        private const string ServerDirectory = @"C:\fakeserver";

        private static Process CompileCSharpFile(string outputFile, string inputFile)
        {
            //outputFile - where to save the exe
            //intputFile - where the source code file is
            var dir = CSharpBatchFileDirectory;
            var param = CSharpCompilerFileDirectory + " " + outputFile + " " + inputFile;
            var info = new ProcessStartInfo(dir, param) { WindowStyle = ProcessWindowStyle.Hidden };
            return Process.Start(info);
        }

        private static Process CompileJavaFile(string outputDirectory, string inputFile)
        {
            //outputFile - where to save the exe
            //intputFile - where the source code file is
            var dir = JavaBatchFileDirectory;
            var param = JavaCompilerFileDirectory + " " + outputDirectory + " " + inputFile;
            var info = new ProcessStartInfo(dir, param) { };
            return Process.Start(info);
        }

        private static Process CompileCPPFile(string outputFile, string inputFile)
        {
            //outputFile - where to save the exe
            //intputFile - where the source code file is
            var dir = CPPBatchFileDirectory;
            var param = CPPCompilerFileDirectory + " " + outputFile + " " + inputFile;
            var info = new ProcessStartInfo(dir, param) { WindowStyle = ProcessWindowStyle.Hidden };
            return Process.Start(info);
        }

        private static Process CompileCFile(string outputFile, string inputFile)
        {
            return CompileCPPFile(outputFile, inputFile);
        }

        private static List<string> ReadInSourceFileAsList(string inputFile)
        {
            var code = new List<string>();
            using (var sr = new StreamReader(inputFile))
                while (!sr.EndOfStream)
                    code.Add(sr.ReadLine());
            return code;
        }

        private static bool CompileIndividualFile(SolutionModel model, string outputDirectory, Process process)
        {
            while (!process.HasExited) ;
            if (!File.Exists(outputDirectory + "\\" + model.ProblemShortName + ".exe"))
            {
                model.Result = "Failed";
                model.ResultDescription = "File did not compile";
                model.JudgeTime = DateTime.Now;
                return false;
            }
            return true;
        }

        private static bool CompileIndividualJavaFile(SolutionModel model, string outputDirectory, Process process)
        {
            while (!process.HasExited) ;
            if (!File.Exists(outputDirectory + "\\" + model.ProblemShortName + ".class"))
            {
                model.Result = "Failed";
                model.ResultDescription = "File did not compile";
                model.JudgeTime = DateTime.Now;
                return false;
            }
            return true;
        }

        private static bool RunJavaCode(SolutionModel model, string classDirectory, string className)
        {
            var info = new ProcessStartInfo(RunJavaBatch);
            info.Arguments = JVM + " " + classDirectory + " " + className;
            var code = Process.Start(info);

            var timeout = DateTime.Now.AddSeconds(model.SubmissionTimeout);
            while (DateTime.Now <= timeout)
            {
                Thread.Sleep(100);
                if (code.HasExited)
                    break;
            }

            if (!code.HasExited)
            {
                model.Result = "Failed";
                model.ResultDescription = "Runtime of submission timed out";
                model.JudgeTime = DateTime.Now;
                return false;
            }
            return true;
        }

        private static bool RunExeCode(SolutionModel model, string compiledFile , string outputDirectory)
        {
            var info = new ProcessStartInfo(compiledFile);
            info.WorkingDirectory = outputDirectory;
            var code = Process.Start(info);
            

            var timeout = DateTime.Now.AddSeconds(model.SubmissionTimeout);
            while (DateTime.Now <= timeout)
            {
                Thread.Sleep(100);
                if (code.HasExited)
                    break;
            }

            if (!code.HasExited)
            {
                model.Result = "Failed";
                model.ResultDescription = "Runtime of submission timed out";
                model.JudgeTime = DateTime.Now;
                code.Kill();
                return false;
            }
            return true;
        }

        private static bool CheckGeneratedOutputAgainstExpectedInput(SolutionModel model, string outputDirectory)
        {
            if (!File.Exists(outputDirectory + "\\" + model.ProblemShortName + ".out"))
            {
                model.Result = "Failed";
                model.ResultDescription = "File did not generate " + model.ProblemShortName + ".out";
                model.JudgeTime = DateTime.Now;
                return false;
            }

            File.WriteAllText(outputDirectory + "\\" + model.ProblemShortName + ".act", model.ExpectedOutput);
            model.GeneratedOutput = File.ReadAllText(outputDirectory + "\\" + model.ProblemShortName + ".out").Replace("\r","").TrimEnd();
            model.ExpectedOutput = model.ExpectedOutput.Replace("\r", "").TrimEnd();
            

            if (model.GeneratedOutput != model.ExpectedOutput)
            {
                model.Result = "Failed";
                model.ResultDescription = "File did not generate the correct output";
                model.JudgeTime = DateTime.Now;
                return false;
            }

            model.Result = "Passed";
            model.ResultDescription = "File generated the correct output";
            model.JudgeTime = DateTime.Now;
            return true;
        }

        private static void ScoreSubmission(SolutionModel model)
        {
            var time = (int)(DateTime.Now - model.CompetitionStartTime).TotalMinutes;
            var penalty = model.PenaltyAmount*model.TimesAttemped;

            model.Score = time + penalty;
        }

        private static void RunFile(SolutionModel model, string compiledFile, string outputDirectory)
        {
            var succeededRunning = compiledFile.EndsWith(".exe") ? RunExeCode(model, compiledFile, outputDirectory) : RunJavaCode(model, outputDirectory, model.ProblemShortName);
            if (!succeededRunning) return;

            var correctSubmission = CheckGeneratedOutputAgainstExpectedInput(model, outputDirectory);
            
            if (correctSubmission)
                ScoreSubmission(model);
        }

        public static void CompileAndRunFile(SolutionModel model)
        {
            string inputFile = ServerDirectory + @"\fileuploads\" + model.ProblemId + "\\" + model.TeamId + "\\" + model.FileName;
            string outputDirectory = ServerDirectory + @"\compiled\" + model.ProblemId + "\\" + model.TeamId;
            Directory.CreateDirectory(outputDirectory);
            string compiledFile;

            if (model.ProblemShortName != model.FileName.Split('.')[0])
            {
                model.Result = "Failed";
                model.ResultDescription = "Incorrect File Name";
                model.JudgeTime = DateTime.Now;
                File.Delete(inputFile);
                return;
            }

            if (model.FileName.EndsWith(".cs"))
            {
                compiledFile = outputDirectory + "\\" + model.ProblemShortName + ".exe";
                var process = CompileCSharpFile(compiledFile, inputFile);
                if (!CompileIndividualFile(model, outputDirectory, process))
                {
                    File.Delete(inputFile);
                    return;
                }
            }
            else if (model.FileName.EndsWith(".java"))
            {
                compiledFile = outputDirectory + "\\" + model.ProblemShortName;
                var process = CompileJavaFile(outputDirectory, inputFile);
                if (!CompileIndividualJavaFile(model, outputDirectory, process))
                {
                    File.Delete(inputFile);
                    return;
                }
            }
            else if (model.FileName.EndsWith(".c"))
            {
                compiledFile = outputDirectory + "\\" + model.ProblemShortName + ".exe";
                var process = CompileCFile(compiledFile, inputFile);
                if (!CompileIndividualFile(model, outputDirectory, process))
                {
                    File.Delete(inputFile);
                    return;
                }
            }
            else if (model.FileName.EndsWith(".cpp"))
            {
                compiledFile = outputDirectory + "\\" + model.ProblemShortName + ".exe";
                var process = CompileCPPFile(compiledFile, inputFile);
                if (!CompileIndividualFile(model, outputDirectory, process))
                {
                    File.Delete(inputFile);
                    return;
                }
            }
            else
            {
                model.Result = "Failed";
                model.ResultDescription = "Incorrect File Extension";
                model.JudgeTime = DateTime.Now;
                File.Delete(inputFile);
                return;
            }

            File.WriteAllText(outputDirectory + "\\" + model.ProblemShortName + ".in", model.ActualInput);
            model.SourceCode = File.ReadAllText(inputFile);
            File.Delete(inputFile);

            RunFile(model, compiledFile, outputDirectory);
            var files = Directory.GetFiles(outputDirectory);

            foreach (var f in files)
            {
                File.Delete(f);
            }
        }
    }
}

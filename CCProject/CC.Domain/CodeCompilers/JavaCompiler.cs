using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CC.Domain.CodeCompilers
{
    public class JavaCompiler
    {
        private const string BatchFileDirectory = @"C:\compileJava.bat";
        private const string CompilerFileDirectory = "C:\\\"Program Files\"\\Java\\jdk1.7.0_03\\bin\\javac.exe";

        public static void CompileJavaFile(string outputDirectory, string inputFile)
        {
            //outputFile - where to save the exe
            //intputFile - where the source code file is
            //resultsFile - where to save the output of the 
            var dir = BatchFileDirectory;
            var param = CompilerFileDirectory + " " + outputDirectory + " " + inputFile;
            var info = new ProcessStartInfo(dir, param) { };
            Process.Start(info);
        }

        public static List<string> ReadInSourceFileAsList(string inputFile)
        {
            var code = new List<string>();
            using (var sr = new StreamReader(inputFile))
                while (!sr.EndOfStream)
                    code.Add(sr.ReadLine());
            return code;
        }

        public static bool IsSubmittedFileCorrect(string fileName, string expectedFileName)
        {
            var isJavaFile = fileName.EndsWith(".java");
            var fileNameAccurate = fileName.StartsWith(expectedFileName);
            return isJavaFile && fileNameAccurate;
        }
    }
}

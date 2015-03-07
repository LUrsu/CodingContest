using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CC.Domain.CodeCompilers
{
    public class CSharpCompiler
    {
        private const string BatchFileDirectory = @"C:\compileCSharp.bat";
        private const string CompilerFileDirectory = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe";

        public static void CompileCSharpFile(string outputFile, string inputFile, string resultsFile)
        {
            //outputFile - where to save the exe
            //intputFile - where the source code file is
            //resultsFile - where to save the output of the 
            var dir = BatchFileDirectory;
            var param = CompilerFileDirectory + " " + outputFile + " " + inputFile + " " + resultsFile;
            var info = new ProcessStartInfo(dir, param) { WindowStyle = ProcessWindowStyle.Hidden };
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
            var isCSharpFile = fileName.EndsWith(".cs");
            var fileNameAccurate = fileName.StartsWith(expectedFileName);
            return isCSharpFile && fileNameAccurate;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CC.Domain.CodeCompilers
{
    class Compiler
    {
        private const string BatchFileDirectory = @"C:\compileC.bat";
        private const string CppCompiler = @"C:\Dev-Cpp\bin\g++";
        private const string CCompiler = @"C:\Dev-Cpp\bin\gcc";

        public static void Compile(string compiler, string CodeFileName)
        {
            var destination = CodeFileName.Split('.').GetValue(0) + ".exe";
            var info = new ProcessStartInfo(compiler, "-o " + destination + " " + CodeFileName) { WindowStyle = ProcessWindowStyle.Hidden };
            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;

            Process Process;
            Process = Process.Start(info);
            System.IO.StreamReader myOutput = Process.StandardOutput;
            Process.WaitForExit(2000);
        }
    }
}

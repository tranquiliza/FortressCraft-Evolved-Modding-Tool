using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Common
{
    public static class Error
    {
        private static uint ErrorCount = 0;
        private static string Path = "Output_Log.txt";
        public static void Log(string ErrorMessage)
        {
            if (!File.Exists(Path))
            {
                File.WriteAllText(Path, "Welcome!" + Environment.NewLine);
            }
            if (ErrorCount < 1)
            {
                File.WriteAllText(Path, "Welcome!" + Environment.NewLine);
            }
            ErrorCount += 1;
            string lNewLine = ErrorMessage + Environment.NewLine;
            File.AppendAllText(Path, lNewLine);
        }
    }
}

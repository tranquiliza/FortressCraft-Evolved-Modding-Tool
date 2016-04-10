using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ModWriter
{
    public static class DirectoryCreator
    {
        private static string MainFolder;
        private static string Version;
        private static string Xml = "Xml";
        private static string GenericAutoCrafterFolder = "GenericAutoCrafter";
        static public void SetMainFolderName(string AuthorID, string ModName)
        {
            MainFolder = AuthorID + "." + ModName;
        }
        static public void SetVersion(string Number)
        {
            Version = Number;
        }
        static public void GenerateDirectory(string OutputPath)
        {
            if (OutputPath == "")
            {
                return;
            }
            if (MainFolder == null)
            {
                return;
            }
            if (Version == null)
            {
                return;
            }
            string ModFolder = System.IO.Path.Combine(OutputPath, MainFolder);
            ModFolder = System.IO.Path.Combine(ModFolder, Version);
            ModFolder = System.IO.Path.Combine(ModFolder, Xml);
            ModFolder = System.IO.Path.Combine(ModFolder, GenericAutoCrafterFolder);
            System.IO.Directory.CreateDirectory(ModFolder);
        }
    }
}

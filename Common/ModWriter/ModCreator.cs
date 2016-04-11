using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ModWriter
{
    public static class ModCreator
    {
        public static string AuthorID { get; set; }
        public static string ModName { get; set; }
        public static string Version { get; set; }
        public static string MainFolder;
        public static string Xml = "Xml";
        private static string GenericAutoCrafterFolder = "GenericAutoCrafter";
        static private void SetMainFolderName()
        {
            MainFolder = AuthorID + "." + ModName;
        }
        static public void GenerateDirectory(string OutputPath)
        {
            SetMainFolderName();
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

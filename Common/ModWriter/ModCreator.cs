using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ModWriter
{
    public static class ModCreator
    {
        public static string Xml = "Xml";
        private static string GenericAutoCrafterFolder = "GenericAutoCrafter";
        static public void GenerateDirectory(string OutputPath, ModConfiguration Config)
        {
            if (OutputPath == "")
            {
                return;
            }
            if (Config.Id == null)
            {
                return;
            }
            if (Config.Version == null)
            {
                return;
            }
            string ModFolder = System.IO.Path.Combine(OutputPath, Config.Id);
            ModFolder = System.IO.Path.Combine(ModFolder, Config.Version);
            ModFolder = System.IO.Path.Combine(ModFolder, Xml);
            ModFolder = System.IO.Path.Combine(ModFolder, GenericAutoCrafterFolder);
            System.IO.Directory.CreateDirectory(ModFolder);
        }
    }
}

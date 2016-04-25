using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.XmlLogic;
using Common.ModWriter;

namespace Common.ModWriter
{
    public static class ModCreator
    {
        public static string Xml = "Xml";
        private static string GenericAutoCrafterFolder = "GenericAutoCrafter";
        static public void GenerateDirectory(string OutputPath, ModConfiguration Config)
        {
            if (string.IsNullOrEmpty(Config.Id) || string.IsNullOrEmpty(Config.Version))
            {
                return;
            }

            // Only one Path.Combine is required, since we are not on .Net 2.0
            string ModFolder = Path.Combine(OutputPath, Config.Id, Config.Version, Xml);
            string XmlFolder = ModFolder;
            ModFolder = Path.Combine(ModFolder, GenericAutoCrafterFolder);
            Directory.CreateDirectory(ModFolder);
            GenerateXmlFiles(XmlFolder);
        }
        private static void GenerateXmlFiles(string XmlFilePath)
        {
            string CraftData = XMLSerializer.Serialize(ModWriterDataHolder.ManufacturerEntries, false);
            string ItemData = XMLSerializer.Serialize(ModWriterDataHolder.Items, false);
            string ResearchData = XMLSerializer.Serialize(ModWriterDataHolder.ResearchEntires, false);
            string TerrainData = XMLSerializer.Serialize(ModWriterDataHolder.TerrainDataEntries, false);

            string basePath = Path.Combine(XmlFilePath, "{0}.xml");

            //File.WriteAllText(string.Format(basePath, "ManufacturerRecipes"), CraftData);
            //File.WriteAllText(string.Format(basePath, "Items"), ItemData);
            //File.WriteAllText(string.Format(basePath, "Research"), ResearchData);
            //File.WriteAllText(string.Format(basePath, "TerrainData"), TerrainData);
        }
    }
}

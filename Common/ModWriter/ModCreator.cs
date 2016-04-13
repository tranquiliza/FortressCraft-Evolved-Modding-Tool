using System;
using System.Collections.Generic;
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
            if (Config.Id == null)
            {
                return;
            }
            if (Config.Version == null)
            {
                return;
            }
            string XmlFolder = null;
            string ModFolder = System.IO.Path.Combine(OutputPath, Config.Id);
            ModFolder = System.IO.Path.Combine(ModFolder, Config.Version);
            ModFolder = System.IO.Path.Combine(ModFolder, Xml);
            XmlFolder = ModFolder + "\\";
            ModFolder = System.IO.Path.Combine(ModFolder, GenericAutoCrafterFolder);
            System.IO.Directory.CreateDirectory(ModFolder);
            GenerateXmlFiles(XmlFolder);
        }
        private static void GenerateXmlFiles(string XmlFilePath)
        {
            string CraftData = XMLSerializer.Serialize(ModWriterDataHolder.ManufacturerEntries, false);
            string ItemData = XMLSerializer.Serialize(ModWriterDataHolder.Items, false);
            string ResearchData = XMLSerializer.Serialize(ModWriterDataHolder.ResearchEntires, false);
            string TerrainData = XMLSerializer.Serialize(ModWriterDataHolder.TerrainDataEntries, false);

            System.IO.File.WriteAllText(XmlFilePath + "ManufacturerRecipes.xml", CraftData);
            System.IO.File.WriteAllText(XmlFilePath + "Items.xml", ItemData);
            System.IO.File.WriteAllText(XmlFilePath + "Research.xml", ResearchData);
            System.IO.File.WriteAllText(XmlFilePath + "TerrainData.xml", TerrainData);
        }
    }
}

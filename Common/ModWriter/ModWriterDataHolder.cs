using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ModLogics;

namespace Common.ModWriter
{
    public static class ModWriterDataHolder
    {
        public static ModConfiguration Config { get; set; }
        //Mod Data
        public static List<CraftData> ManufacturerEntries = new List<CraftData>();
        public static List<ItemEntry> Items = new List<ItemEntry>();
        public static List<ResearchDataEntry> ResearchEntires = new List<ResearchDataEntry>();
        public static List<TerrainDataEntry> TerrainDataEntries = new List<TerrainDataEntry>();
        //Not sure how to manage this?  Maybe load a single at a time?
        public static List<GenericAutoCrafterDataEntry> GACMachines = new List<GenericAutoCrafterDataEntry>();

        public static string[] Sprites()
        {
            string[] IconNames = null;
            try
            {
                IconNames = System.IO.File.ReadAllLines("IconNames.txt");
            }
            catch (Exception x)
            {
                System.IO.File.WriteAllText("SpriteReaderError.txt", x.ToString());
            }
            return IconNames;
        }
    }
}

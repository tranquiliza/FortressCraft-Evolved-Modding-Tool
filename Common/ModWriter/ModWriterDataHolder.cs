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
        //Not sure how to manage this? 
        public static List<GenericAutoCrafterDataEntry> GACMachines = new List<GenericAutoCrafterDataEntry>();
    }
}

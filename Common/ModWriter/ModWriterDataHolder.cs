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
        public static List<CraftData> ManufacturerEntries { get; set; }
        public static List<ItemEntry> Items { get; set; }
        public static List<ResearchDataEntry> ResearchEntires { get; set; }
        public static List<TerrainDataEntry> TerrainDataEntries { get; set; }

        //Not sure how to manage this? 
        public static List<GenericAutoCrafterDataEntry> GACMachines { get; set; }
    }
}

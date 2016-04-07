using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.GameLogics;

namespace Common.Data
{
    public static class DataHolder
    {
        //These hold all the data, and only needs to be assigned values once per run!
        public static List<ItemEntry> ItemEntries = new List<ItemEntry>();
        public static List<CraftData> ManufacturerEntries = new List<CraftData>();
        public static List<ResearchDataEntry> ResearchEntries = new List<ResearchDataEntry>();
        public static List<TerrainDataEntry> TerrainDataEntries = new List<TerrainDataEntry>();
    }
}

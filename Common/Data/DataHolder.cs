using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.GameLogics;

namespace Common.Data
{
    public static class Version
    {
        //0.0.0
        //Release.Beta.Revision
        public static string Value = " V0.1.5";
    }
    public static class DataHolder
    {
        //These hold all the data, and only needs to be assigned values once per run!
        public static List<ItemEntry> ItemEntries = new List<ItemEntry>();
        public static List<CraftData> ManufacturerEntries = new List<CraftData>();
        public static List<ResearchDataEntry> ResearchEntries = new List<ResearchDataEntry>();
        public static List<TerrainDataEntry> TerrainDataEntries = new List<TerrainDataEntry>();
    }
    public static class ModDataHolder
    {
        public static List<CraftData> RefineryRecipes = new List<CraftData>();
    }
    public static class GACDataHolder
    {
        public static GenericAutoCrafterDataEntry GAC = new GenericAutoCrafterDataEntry();
        public static List<GenericAutoCrafterDataEntry> GACList = new List<GenericAutoCrafterDataEntry>();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FortressCraftEvolved_Modding_Tool.GameLogics;

namespace FortressCraftEvolved_Modding_Tool.Data
{
    public static class DataHolder
    {
        //These hold all the data, and only needs to be assigned values once per run!
        public static List<ItemEntry> ItemEntries = new List<ItemEntry>();
        public static List<ManufacturerEntry> ManufacturerEntries = new List<ManufacturerEntry>();
        public static List<ResearchEntry> ResearchEntries = new List<ResearchEntry>();
    }
}

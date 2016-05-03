using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ModLogics;
using Common.Data;

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
        //public static List<GenericAutoCrafterDataEntry> GACMachines = new List<GenericAutoCrafterDataEntry>();

        public static GenericAutoCrafterDataEntry GACMachine = new GenericAutoCrafterDataEntry();
        public static string[] Sprites()
        {
            string[] IconNames = null;
            if (System.IO.File.Exists("IconList.txt")) //If the file exists already, no reason to read everything again. Faster load
            {
                try
                {
                    IconNames = System.IO.File.ReadAllLines("IconList.txt");
                }
                catch (Exception x)
                {
                    Error.Log("Error: IconList.txt Sprites wasn't loaded: " + x);
                }
            }
            else
            {
                ReadIcons();
            }
            return IconNames;
        }
        private static void ReadIcons()
        {
            List<string> lcIcons = new List<string>();
            for (int i = 0; i < DataHolder.ItemEntries.Count; i++)
            {
                lcIcons.Add(DataHolder.ItemEntries[i].Sprite);
            }
            for (int i = 0; i < DataHolder.ResearchEntries.Count; i++)
            {
                lcIcons.Add(DataHolder.ResearchEntries[i].IconName);
            }
            for (int i = 0; i < DataHolder.TerrainDataEntries.Count; i++)
            {
                if (DataHolder.TerrainDataEntries[i].Values != null)
                {
                    lcIcons.Add(DataHolder.TerrainDataEntries[i].IconName);
                    for (int j = 0; j < DataHolder.TerrainDataEntries[i].Values.Count; j++)
                    {
                        lcIcons.Add(DataHolder.TerrainDataEntries[i].Values[j].IconName);
                    }
                }
                else
                {
                    lcIcons.Add(DataHolder.TerrainDataEntries[i].IconName);
                }
            }
            string[] ExtraIcons = null;
            try
            {
                ExtraIcons = System.IO.File.ReadAllLines("IconNames.txt");
            }
            catch (Exception x)
            {
                Error.Log("Error: IconNames.txt Sprites wasn't loaded: " + x);
            }
            for (int i = 0; i < ExtraIcons.Length; i++)
            {
                lcIcons.Add(ExtraIcons[i]);
            }
            string[] IconNames = lcIcons.Distinct().ToArray();
            Array.Sort(IconNames);
            System.IO.File.WriteAllLines("IconList.txt", IconNames);
        }
    }
}

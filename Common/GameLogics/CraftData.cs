using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Common.GameLogics
{
    public class CraftData
    {
        //This is ManufacturerEntries Class (For Some reason it has a different name!)
        public string Key { get; set; }
        public string Category { get; set; }
        public ushort Tier { get; set; }
        //public string CraftedName { get; set; }
        public string CraftedKey { get; set; }
        public int CraftedAmount { get; set; }
        public float CraftTime { get; set; }
        [XmlArray, XmlArrayItem("CraftCost")]
        public List<CraftCost> Costs { get; set; }
        [XmlArray, XmlArrayItem("Research")]
        public List<string> ResearchRequirements { get; set; }
        [XmlArray, XmlArrayItem("Scan")]
        public List<string> ScanRequirements { get; set; }
        [DefaultValue(0)]
        public int ResearchCost { get; set; }
        public eManufacturingPlantModule RequiredModule;
        public string Description { get; set; }
        public string Hint { get; set; }
        [DefaultValue(false)]
        public bool CanCraftAnywhere { get; set; }
        [DefaultValue(true)]
        public bool CanBeAutomated { get; set; }
        public string MasterRecipe { get; set; }
    }

    public class CraftCost
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public uint Amount { get; set; }
        public string Text()
        {
            return Amount + " X " + Key;
        }
    }
    public enum eManufacturingPlantModule
    {
        None,
        Printer = 10,
        Compressor = 20,
        Extruder = 30,
        ChipEtcher = 40,
        HydrojetCutter = 50,
        RoboticWelder = 60,
        Incubator = 70,
        AssemblyStation = 80
    }
}

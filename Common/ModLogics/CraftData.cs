using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common.ModLogics
{
    [Serializable]
    public class CraftData
    {
        //Commented things are not needed for Mod Manufacturer Recipes!
        //OR are illegal for us to set!

        // Modded Variables:
        public string IsOverride { get; set; }
        [DefaultValue(false)]
        public bool Delete { get; set; }
        //Lists to remove Research Req and Scan Req!

        [XmlArray, XmlArrayItem("Research")]
        public List<string> RemoveResearchRequirements { get; set; }

        [XmlArray, XmlArrayItem("Scan")]
        public List<string> RemoveScanRequirements { get; set; }
        //Modded Variables End:

        public string Key { get; set; }
        public string Category { get; set; }
        public ushort Tier { get; set; }
        public string CraftedName { get; set; }
        public string CraftedKey { get; set; }
        public int CraftedAmount { get; set; }
        public float CraftTime { get; set; }
        [XmlArray, XmlArrayItem("CraftCost")]
        public List<CraftCost> Costs { get; set; }
        [XmlArray, XmlArrayItem("Research")]
        public List<string> ResearchRequirements { get; set; }
        [XmlArray, XmlArrayItem("Scan")]
        public List<string> ScanRequirements { get; set; }
        //[DefaultValue(0)]
        public int ResearchCost { get; set; }
        //public eManufacturingPlantModule RequiredModule;
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
        public string Delete { get; set; }
        public string Key { get; set; }
        public uint Amount { get; set; }
        public string Readable()
        {
            return Amount + " x " + Key;
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
    public enum Category
    {
        mining,
        power,
        logistics,
        upgrades,
        defences,
        consumables,
        CraftingIngredient,
        minecarts,
        manufacturing,
        decoration
    }
}

using System.Collections.Generic;
using System.Xml.Serialization;

namespace Common.GameLogics
{
    public class ItemEntry
    {
        public string IsOverride { get; set; }
        [XmlArray, XmlArrayItem("Research")]
        public List<string> RemoveResearchRequirements { get; set; }

        [XmlArray, XmlArrayItem("Scan")]
        public List<string> RemoveScanRequirements { get; set; }
        public int ItemID { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Plural { get; set; }
        public ItemType Type { get; set; }
        public bool Hidden { get; set; }
        public SpawnableObjectEnum Object { get; set; }
        public string Sprite { get; set; }
        public string Atlas { get; set; }
        public MaterialCategories Category { get; set; }
        public string Description { get; set; }
        [XmlArray, XmlArrayItem("Research")]
        public List<string> ResearchRequirements { get; set; }
        [XmlArray, XmlArrayItem("Scan")]
        public List<string> ScanRequirements { get; set; }
        public string UnknownHint { get; set; }
        public int MaxDurability { get; set; }
        public int MaxStack { get; set; }
        public SuitUpgradeEnum SuitUpgrade { get; set; }
        public ItemActions ItemAction { get; set; }
        public string ActionParameter { get; set; }
        public int DecomposeValue { get; set; }
    }
    public enum ItemType
    {
        ItemSingle,
        ItemStack,
        ItemCubeStack,
        ItemDurability,
        ItemCharge,
        ItemLocation
    }
    public enum SuitUpgradeEnum
    {
        None,
        SolarCell,
        Cooler,
        Heater,
        HeadLight,
        PowerPack,
        BuildModule,
        CollectionModule,
        Insulation,
        BuildGunV2,
        JetPack,
        Num
    }
    public enum ItemActions
    {
        None,
        BuildBlock,
        OrePing,
        ThrowGlowStick,
        GainPower,
        MarkLocation,
        Consume,
        Custom
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FortressCraftEvolved_Modding_Tool.GameLogics
{
	[Serializable()]
	[System.Xml.Serialization.XmlRoot("ArrayOfItemEntry")]
	public class ArrayOfItemEntry
	{
		[XmlArray("ArrayOfItemEntry")]
		[XmlArrayItem("ItemEntry", typeof(ItemEntry))]
		public ItemEntry[] ItemEntry { get; set; }
	}

	[Serializable()]
	public class ItemEntry
    {
        public int ItemID { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Plural { get; set; }
        public string Type { get; set; }
        public bool Hidden { get; set; }
        public string Object { get; set; }
        public string Sprite { get; set; }
        public string Category { get; set; }
		[XmlArray("ResearchRequirements")]
		[XmlArrayItem("Research", typeof(string))]
        public string[] ResearchRequirements { get; set; }
		[XmlArray("ScanRequirements")]
		[XmlArrayItem("Scan", typeof(string))]
		public string[] ScanRequirements { get; set; }
    }
}

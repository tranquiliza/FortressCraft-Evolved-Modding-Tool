using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressCraftEvolved_Modding_Tool.GameLogics
{
    [Serializable()]
    public class ItemEntry
    {
        [System.Xml.Serialization.XmlElement("ItemID")]
        public int ItemID { get; set; }

        [System.Xml.Serialization.XmlElement("Key")]
        public string Key { get; set; }

        [System.Xml.Serialization.XmlElement("Name")]
        public string Name { get; set; }

        [System.Xml.Serialization.XmlElement("Plural")]
        public string Plural { get; set; }

        [System.Xml.Serialization.XmlElement("Type")]
        public string Type { get; set; }

        [System.Xml.Serialization.XmlElement("Hidden")]
        public bool Hidden { get; set; }

        [System.Xml.Serialization.XmlElement("Object")]
        public string Object { get; set; }

        [System.Xml.Serialization.XmlElement("Sprite")]
        public string Sprite { get; set; }

        [System.Xml.Serialization.XmlElement("Category")]
        public string Category { get; set; }

        [System.Xml.Serialization.XmlElement("ResearchRequirements")]
        public List<Research> ResearchRequirements { get; set; }

        [System.Xml.Serialization.XmlElement("ScanRequirements")]
        public List<Scan> ScanRequirements { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common.ModLogics
{
    [Serializable()]
    public class ItemEntry
    {
        public string IsOverride { get; set; }
        public int ItemID { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Plural { get; set; }
        public string Type { get; set; }
        public bool Hidden { get; set; }
        public string Object { get; set; }
        public string Sprite { get; set; }
        public string Category { get; set; }
        [XmlArray, XmlArrayItem("Research")]
        public List<string> ResearchRequirements { get; set; }
        [XmlArray, XmlArrayItem("Research")]
        public List<string> RemoveResearchRequirements { get; set; }
        [XmlArray, XmlArrayItem("Scan")]
        public List<string> ScanRequirements { get; set; }
        [XmlArray, XmlArrayItem("Scan")]
        public List<string> RemoveScanRequirements { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common.GameLogics
{
    public class ResearchDataEntry
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string IconName { get; set; }
        public int ResearchCost { get; set; }
        public string PreDescription { get; set; }
        public string PostDescription { get; set; }
        [XmlArray, XmlArrayItem("Scan")]
        public List<string> ScanRequirements { get; set; }
        [XmlArray, XmlArrayItem("Research")]
        public List<string> ResearchRequirements { get; set; }
        [XmlArray, XmlArrayItem("Requirement")]
        public List<ProjectItemRequirement> ProjectItemRequirements { get; set; }
    }
    public class ProjectItemRequirement
    {
        public string Key { get; set; }
        public int Amount { get; set; }

        public string Readable()
        {
            return Amount + " x " + Key;
        }
    }
}

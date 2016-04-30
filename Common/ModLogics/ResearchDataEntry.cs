using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common.ModLogics
{
    [Serializable]
    public class ResearchDataEntry
    {
        public bool IsOverride { get; set; }
        public string Delete { get; set; } //This is a string, otherwise if true, the Serialization of the instance doesn't happen! 
        public string Key { get; set; }
        public string Name { get; set; }
        public string IconName { get; set; }
        public int ResearchCost { get; set; }
        public string PreDescription { get; set; }
        public string PostDescription { get; set; }
        [XmlArray, XmlArrayItem("Scan")]
        public List<string> ScanRequirements { get; set; }
        [XmlArray, XmlArrayItem("Scan")]
        public List<string> RemoveScanRequirements { get; set; }
        [XmlArray, XmlArrayItem("Research")]
        public List<string> ResearchRequirements { get; set; }
        [XmlArray, XmlArrayItem("Research")]
        public List<string> RemoveResearchRequirements { get; set; }
        [XmlArray, XmlArrayItem("Requirement")]
        public List<ProjectItemRequirement> ProjectItemRequirements { get; set; }
    }
    public class ProjectItemRequirement
    {
        public string Delete { get; set; } //This is a string, otherwise if true, the Serialization of the instance doesn't happen! 
        public string Key { get; set; }
        public int Amount { get; set; }
        public override string ToString()
        {
            string lIsDelete;
            if (Delete == "true")
            {
                lIsDelete = "-";
            }
            else
            {
                lIsDelete = "+";
            }
            return Amount + " x " + Key + " : " + lIsDelete;
        }
    }
}

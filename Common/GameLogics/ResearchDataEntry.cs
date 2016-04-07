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


        /* OLD CODE
        public String Key { get; set; }
        public String Name { get; set; }
        public String IconName { get; set; }
        public uint ResearchCost { get; set; }
        public String PreDescription { get; set; }
        public String PostDescription { get; set; }
        public List<String> ResearchRequirements { get; set; }
        public List<String> ScanRequirements { get; set; }
        public List<ProjectItemRequirements> LabResearchItems { get; set; }

        public bool dirty = false;
        */
    }
    public class ProjectItemRequirement
    {
        public string Key { get; set; }
        public int Amount { get; set; }

        public string Readable()
        {
            return Amount + " x " + Key;
        }

        /* OLD CODE
        public ProjectItemRequirement(String ItemKey, int Amount)
        {
            this.ItemKey = ItemKey;
            this.Amount = Amount;
        }
        public String Text()
        {
            return ItemKey + " x " + Amount;
        }
        */
    }
}

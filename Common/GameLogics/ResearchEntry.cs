using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.GameLogics
{
    public class ResearchEntry
    {
        public String Key;
        public String Name;
        public String IconName;
        public uint ResearchCost;
        public String PreDescription;
        public String PostDescription;
        public List<String> ResearchRequirements = new List<string>();
        public List<String> ScanRequirements = new List<string>();
        public List<ProjectItemRequirements> LabResearchItems = new List<ProjectItemRequirements>();
        public bool dirty = false;

        public ResearchEntry()
        {
        }
    }
    public class ProjectItemRequirements
    {
        public String ItemKey;
        public int Amount;
        public ProjectItemRequirements(String ItemKey, int Amount)
        {
            this.ItemKey = ItemKey;
            this.Amount = Amount;
        }
        public String Text()
        {
            return ItemKey + " x " + Amount;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressCraftEvolved_Modding_Tool.GameLogics
{
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
        public List<string> ResearchRequirements { get; set; }
        public List<string> ScanRequirements { get; set; }
    }
}

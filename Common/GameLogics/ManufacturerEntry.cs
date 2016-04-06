using System;
using System.Collections.Generic;

namespace FortressCraftEvolved_Modding_Tool.GameLogics
{
    public class ManufacturerEntry
    {
        public String Key;
        public String Category;
        public int Tier;
        public String CraftedName;
        public int CraftedAmount;
        public String Hint;
        public String Description;
        public int ResearchCost;
        public List<CraftCost> CraftingCosts; // = new List<CraftCost>();
        public List<String> ResearchRequirement = new List<string>();
        public List<String> ScanRequirement = new List<string>();
        public Boolean CanCraftAnywhere = false;
        public Boolean IsDirty = false;
        public ManufacturerEntry()
        {
        }
    }
}

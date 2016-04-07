using System;
using System.Collections.Generic;

namespace Common.GameLogics
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

    public class CraftCost
    {
        public String Name;
        public uint Amount;

        public CraftCost(String Name, uint Amount)
        {
            this.Name = Name;
            this.Amount = Amount;
        }

        public string Text()
        {
            return Name + " x " + Amount;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;

namespace Common.ModLogics
{
    public static class CreativeSurvivalMod
    {
        public static List<CraftData> ModdedRecipes = new List<CraftData>();
        public static List<ItemEntry> ModdedItems = new List<ItemEntry>();
        public static void ConvertRecipes()
        {
            for (int i = 0; i < DataHolder.ManufacturerEntries.Count; i++)
            {
                CraftData Holder = new CraftData();
                if (DataHolder.ManufacturerEntries[i].Key == "arther core")
                {
                    Holder.Key = "tranq.arther core";
                    Holder.IsOverride = "false";
                }
                else
                {
                    Holder.IsOverride = "true";
                    Holder.Key = DataHolder.ManufacturerEntries[i].Key;
                }
                Holder.Category = DataHolder.ManufacturerEntries[i].Category;
                Holder.Tier = DataHolder.ManufacturerEntries[i].Tier;
                Holder.CraftedKey = DataHolder.ManufacturerEntries[i].CraftedKey;
                //Holder.CraftedName = DataHolder.ManufacturerEntries[i].CraftedName;
                Holder.CraftedAmount = DataHolder.ManufacturerEntries[i].CraftedAmount;
                Holder.CraftTime = DataHolder.ManufacturerEntries[i].CraftTime;

                Holder.Costs = new List<CraftCost>();
                for (int j = 0; j < DataHolder.ManufacturerEntries[i].Costs.Count; j++)
                {
                    CraftCost CostHolder = new CraftCost();
                    CostHolder.Delete = "true";
                    CostHolder.Key = DataHolder.ManufacturerEntries[i].Costs[j].Key;
                    CostHolder.Amount = DataHolder.ManufacturerEntries[i].Costs[j].Amount;
                    Holder.Costs.Add(CostHolder);
                }
                Holder.RemoveResearchRequirements = new List<string>();
                for (int j = 0; j < DataHolder.ManufacturerEntries[i].ResearchRequirements.Count; j++)
                {
                    Holder.RemoveResearchRequirements.Add(DataHolder.ManufacturerEntries[i].ResearchRequirements[j]);
                }
                Holder.RemoveScanRequirements = new List<string>();
                for (int j = 0; j < DataHolder.ManufacturerEntries[i].ScanRequirements.Count; j++)
                {
                    Holder.RemoveScanRequirements.Add(DataHolder.ManufacturerEntries[i].ScanRequirements[j]);
                }
                Holder.ResearchCost = 0;    //DataHolder.ManufacturerEntries[i].ResearchCost;
                Holder.Description = DataHolder.ManufacturerEntries[i].Description;
                Holder.Hint = DataHolder.ManufacturerEntries[i].Hint;
                Holder.CanCraftAnywhere = DataHolder.ManufacturerEntries[i].CanCraftAnywhere;
                Holder.CanBeAutomated = true;
                Holder.MasterRecipe = DataHolder.ManufacturerEntries[i].MasterRecipe;
                ModdedRecipes.Add(Holder);
            }
            for (int i = 0; i < ModDataHolder.RefineryRecipes.Count; i++)
            {
                CraftData Holder = new CraftData();
                Holder.IsOverride = "false";
                Holder.Key = "tranq." + ModDataHolder.RefineryRecipes[i].Key;
                Holder.Category = "CraftingIngredient";
                Holder.CraftedKey = ModDataHolder.RefineryRecipes[i].CraftedKey;
                Holder.CraftedAmount = ModDataHolder.RefineryRecipes[i].CraftedAmount;
                Holder.Description = ModDataHolder.RefineryRecipes[i].Description;
                ModdedRecipes.Add(Holder);
            }
        }
        public static void ConvertItems()
        {
            for (int i = 0; i < DataHolder.ItemEntries.Count; i++)
            {
                ItemEntry Holder = new ItemEntry();
                Holder.IsOverride = "true";
                Holder.ItemID = DataHolder.ItemEntries[i].ItemID;
                Holder.Key = DataHolder.ItemEntries[i].Key;
                Holder.Name = DataHolder.ItemEntries[i].Name;
                Holder.Plural = DataHolder.ItemEntries[i].Plural;
                Holder.Type = DataHolder.ItemEntries[i].Type;
                Holder.Hidden = DataHolder.ItemEntries[i].Hidden;
                Holder.Object = DataHolder.ItemEntries[i].Object;
                Holder.Sprite = DataHolder.ItemEntries[i].Sprite;
                Holder.Category = DataHolder.ItemEntries[i].Category;

                Holder.RemoveResearchRequirements = new List<string>();
                for (int j = 0; j < DataHolder.ItemEntries[i].ResearchRequirements.Count; j++)
                {
                    Holder.RemoveResearchRequirements.Add(DataHolder.ItemEntries[i].ResearchRequirements[j]);
                }
                Holder.RemoveScanRequirements = new List<string>();
                for (int j = 0; j < DataHolder.ItemEntries[i].ScanRequirements.Count; j++)
                {
                    Holder.RemoveScanRequirements.Add(DataHolder.ItemEntries[i].ScanRequirements[j]);
                }
                ModdedItems.Add(Holder);
            }
        }
    }
}

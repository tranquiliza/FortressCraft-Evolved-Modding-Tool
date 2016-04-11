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
        public static string AuthorID { get; set; }
        public static void ConvertRecipes()
        {
            if (AuthorID == null)
            {
                return;
            }
            for (int i = 0; i < DataHolder.ManufacturerEntries.Count; i++)
            {
                CraftData Holder = new CraftData();
                if (DataHolder.ManufacturerEntries[i].Key == "arther core")
                {
                    Holder.Key = AuthorID + ".arther core";
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
        public static void AddExtraRecipes()
        {

            CraftData CopperOre = new CraftData();
            CopperOre.Key = AuthorID + "CopperOre";
            CopperOre.CraftedName = "Copper Ore";
            CopperOre.CraftedAmount = 1;
            CopperOre.CraftTime = 0;
            CopperOre.Category = "decoration";
            CopperOre.Tier = 5;
            CopperOre.Hint = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData TinOre = new CraftData();
            TinOre.Key = AuthorID + "TinOre";
            TinOre.CraftedName = "Tin Ore";
            TinOre.CraftedAmount = 1;
            TinOre.CraftTime = 0;
            TinOre.Category = "decoration";
            TinOre.Tier = 5;
            TinOre.Hint = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData IronOre = new CraftData();
            IronOre.Key = AuthorID + "IronOre";
            IronOre.CraftedName = "Iron Ore";
            IronOre.CraftedAmount = 1;
            IronOre.CraftTime = 0;
            IronOre.Category = "decoration";
            IronOre.Tier = 5;
            IronOre.Hint = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData LithiumOre = new CraftData();
            LithiumOre.Key = AuthorID + "LithiumOre";
            LithiumOre.CraftedName = "Lithium Ore";
            LithiumOre.CraftedAmount = 1;
            LithiumOre.CraftTime = 0;
            LithiumOre.Category = "decoration";
            LithiumOre.Tier = 5;
            LithiumOre.Hint = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData GoldOre = new CraftData();
            GoldOre.Key = AuthorID + "GoldOre";
            GoldOre.CraftedName = "Gold Ore";
            GoldOre.CraftedAmount = 1;
            GoldOre.CraftTime = 0;
            GoldOre.Category = "decoration";
            GoldOre.Tier = 5;
            GoldOre.Hint = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData NickelOre = new CraftData();
            NickelOre.Key = AuthorID + "NickelOre";
            NickelOre.CraftedName = "Nickel Ore";
            NickelOre.CraftedAmount = 1;
            NickelOre.CraftTime = 0;
            NickelOre.Category = "decoration";
            NickelOre.Tier = 5;
            NickelOre.Hint = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData TitaniumOre = new CraftData();
            TitaniumOre.Key = AuthorID + "TitaniumOre";
            TitaniumOre.CraftedName = "Titanium Ore";
            TitaniumOre.CraftedAmount = 1;
            TitaniumOre.CraftTime = 0;
            TitaniumOre.Category = "decoration";
            TitaniumOre.Tier = 5;
            TitaniumOre.Hint = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData CrystalDeposit = new CraftData();
            CrystalDeposit.Key = AuthorID + "CrystalDeposit";
            CrystalDeposit.CraftedName = "Crystal Deposit";
            CrystalDeposit.CraftedAmount = 1;
            CrystalDeposit.CraftTime = 0;
            CrystalDeposit.Category = "decoration";
            CrystalDeposit.Tier = 5;
            CrystalDeposit.Hint = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData BiomassGrowth = new CraftData();
            BiomassGrowth.Key = AuthorID + "BiomassGrowth";
            BiomassGrowth.CraftedName = "Biomass Growth";
            BiomassGrowth.CraftedAmount = 1;
            BiomassGrowth.CraftTime = 0;
            BiomassGrowth.Category = "decoration";
            BiomassGrowth.Tier = 5;
            BiomassGrowth.Hint = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            ModdedRecipes.Add(CopperOre);
            ModdedRecipes.Add(TinOre);
            ModdedRecipes.Add(IronOre);
            ModdedRecipes.Add(LithiumOre);
            ModdedRecipes.Add(GoldOre);
            ModdedRecipes.Add(NickelOre);
            ModdedRecipes.Add(TitaniumOre);
            ModdedRecipes.Add(CrystalDeposit);
            ModdedRecipes.Add(BiomassGrowth);
        }
    }
}

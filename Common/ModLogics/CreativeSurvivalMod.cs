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
                Holder.IsOverride = true;
                Holder.ItemID = DataHolder.ItemEntries[i].ItemID;
                Holder.Key = DataHolder.ItemEntries[i].Key;
                Holder.Name = DataHolder.ItemEntries[i].Name;
                Holder.Plural = DataHolder.ItemEntries[i].Plural;
                Holder.Type = (ItemType)DataHolder.ItemEntries[i].Type;
                Holder.Hidden = DataHolder.ItemEntries[i].Hidden;
                Holder.Object = (SpawnableObjectEnum)DataHolder.ItemEntries[i].Object;
                Holder.Sprite = DataHolder.ItemEntries[i].Sprite;
                Holder.Atlas = DataHolder.ItemEntries[i].Atlas;
                Holder.Category = (MaterialCategories)DataHolder.ItemEntries[i].Category;
                Holder.Description = DataHolder.ItemEntries[i].Description;
                Holder.UnknownHint = DataHolder.ItemEntries[i].UnknownHint;
                Holder.MaxDurability = DataHolder.ItemEntries[i].MaxDurability;
                Holder.MaxStack = DataHolder.ItemEntries[i].MaxStack;
                Holder.SuitUpgrade = (SuitUpgradeEnum)DataHolder.ItemEntries[i].SuitUpgrade;
                Holder.ItemAction = (ItemActions)DataHolder.ItemEntries[i].ItemAction;
                Holder.ActionParameter = DataHolder.ItemEntries[i].ActionParameter;
                Holder.DecomposeValue = DataHolder.ItemEntries[i].DecomposeValue;
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

            //Missiles

            CraftData BasicMissile = new CraftData();
            BasicMissile.Key = AuthorID + ".BasicMissile";
            BasicMissile.CraftedKey = "BasicMissile";
            BasicMissile.CraftedAmount = 1;
            BasicMissile.CraftTime = 0;
            BasicMissile.Category = "defences";
            BasicMissile.Tier = 5;
            BasicMissile.Description = "Because Making a factory takes a while...";

            CraftData ArmourPiercingMissile = new CraftData();
            ArmourPiercingMissile.Key = AuthorID + ".ArmourPiercingMissile";
            ArmourPiercingMissile.CraftedKey = "ArmourPiercingMissile";
            ArmourPiercingMissile.CraftedAmount = 1;
            ArmourPiercingMissile.CraftTime = 0;
            ArmourPiercingMissile.Category = "defences";
            ArmourPiercingMissile.Tier = 5;
            ArmourPiercingMissile.Description = "Wub Wub Wub!";

            CraftData PlasmaImbuedMissile = new CraftData();
            PlasmaImbuedMissile.Key = AuthorID + ".PlasmaImbuedMissile";
            PlasmaImbuedMissile.CraftedKey = "PlasmaImbuedMissile";
            PlasmaImbuedMissile.CraftedAmount = 1;
            PlasmaImbuedMissile.CraftTime = 0;
            PlasmaImbuedMissile.Category = "defences";
            PlasmaImbuedMissile.Tier = 5;
            PlasmaImbuedMissile.Description = "Make it sting!";


            ModdedRecipes.Add(BasicMissile);
            ModdedRecipes.Add(PlasmaImbuedMissile);
            ModdedRecipes.Add(ArmourPiercingMissile);

            //Ores:

            CraftData CoalOre = new CraftData();
            CoalOre.Key = AuthorID + ".CoalOre";
            CoalOre.CraftedKey = "CoalOre";
            CoalOre.CraftedAmount = 1;
            CoalOre.CraftTime = 0;
            CoalOre.Category = "decoration";
            CoalOre.Tier = 5;
            CoalOre.Description = "Just Coal";

            CraftData CopperOre = new CraftData();
            CopperOre.Key = AuthorID + ".CopperOre";
            CopperOre.CraftedKey = "CopperOre";
            CopperOre.CraftedAmount = 1;
            CopperOre.CraftTime = 0;
            CopperOre.Category = "decoration";
            CopperOre.Tier = 5;
            CopperOre.Description = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData TinOre = new CraftData();
            TinOre.Key = AuthorID + ".TinOre";
            TinOre.CraftedKey = "TinOre";
            TinOre.CraftedAmount = 1;
            TinOre.CraftTime = 0;
            TinOre.Category = "decoration";
            TinOre.Tier = 5;
            TinOre.Description = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData IronOre = new CraftData();
            IronOre.Key = AuthorID + ".IronOre";
            IronOre.CraftedKey = "IronOre";
            IronOre.CraftedAmount = 1;
            IronOre.CraftTime = 0;
            IronOre.Category = "decoration";
            IronOre.Tier = 5;
            IronOre.Description = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData LithiumOre = new CraftData();
            LithiumOre.Key = AuthorID + ".LithiumOre";
            LithiumOre.CraftedKey = "LithiumOre";
            LithiumOre.CraftedAmount = 1;
            LithiumOre.CraftTime = 0;
            LithiumOre.Category = "decoration";
            LithiumOre.Tier = 5;
            LithiumOre.Description = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData GoldOre = new CraftData();
            GoldOre.Key = AuthorID + ".GoldOre";
            GoldOre.CraftedKey = "GoldOre";
            GoldOre.CraftedAmount = 1;
            GoldOre.CraftTime = 0;
            GoldOre.Category = "decoration";
            GoldOre.Tier = 5;
            GoldOre.Description = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData NickelOre = new CraftData();
            NickelOre.Key = AuthorID + ".NickelOre";
            NickelOre.CraftedKey = "NickelOre";
            NickelOre.CraftedAmount = 1;
            NickelOre.CraftTime = 0;
            NickelOre.Category = "decoration";
            NickelOre.Tier = 5;
            NickelOre.Description = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData TitaniumOre = new CraftData();
            TitaniumOre.Key = AuthorID + ".TitaniumOre";
            TitaniumOre.CraftedKey = "TitaniumOre";
            TitaniumOre.CraftedAmount = 1;
            TitaniumOre.CraftTime = 0;
            TitaniumOre.Category = "decoration";
            TitaniumOre.Tier = 5;
            TitaniumOre.Description = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData CrystalDeposit = new CraftData();
            CrystalDeposit.Key = AuthorID + ".CrystalDeposit";
            CrystalDeposit.CraftedKey = "CrystalDeposit";
            CrystalDeposit.CraftedAmount = 1;
            CrystalDeposit.CraftTime = 0;
            CrystalDeposit.Category = "decoration";
            CrystalDeposit.Tier = 5;
            CrystalDeposit.Description = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData BiomassGrowth = new CraftData();
            BiomassGrowth.Key = AuthorID + ".BiomassGrowth";
            BiomassGrowth.CraftedKey = "BiomassGrowth";
            BiomassGrowth.CraftedAmount = 1;
            BiomassGrowth.CraftTime = 0;
            BiomassGrowth.Category = "decoration";
            BiomassGrowth.Tier = 5;
            BiomassGrowth.Description = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            ModdedRecipes.Add(CoalOre);
            ModdedRecipes.Add(CopperOre);
            ModdedRecipes.Add(TinOre);
            ModdedRecipes.Add(IronOre);
            ModdedRecipes.Add(LithiumOre);
            ModdedRecipes.Add(GoldOre);
            ModdedRecipes.Add(NickelOre);
            ModdedRecipes.Add(TitaniumOre);
            ModdedRecipes.Add(CrystalDeposit);
            ModdedRecipes.Add(BiomassGrowth);

            //Organics:consumables

            CraftData PristineHeavyChitin = new CraftData();
            PristineHeavyChitin.Key = AuthorID + ".PristineHeavyChitin";
            PristineHeavyChitin.CraftedKey = "PristineHeavyChitin";
            PristineHeavyChitin.CraftedAmount = 1;
            PristineHeavyChitin.CraftTime = 0;
            PristineHeavyChitin.Category = "consumables";
            PristineHeavyChitin.Tier = 5;
            PristineHeavyChitin.Description = "Bug parts for everyone!";

            ModdedRecipes.Add(PristineHeavyChitin);

            CraftData RuinedHeavyChitin = new CraftData();
            RuinedHeavyChitin.Key = AuthorID + ".RuinedHeavyChitin";
            RuinedHeavyChitin.CraftedKey = "RuinedHeavyChitin";
            RuinedHeavyChitin.CraftedAmount = 1;
            RuinedHeavyChitin.CraftTime = 0;
            RuinedHeavyChitin.Category = "consumables";
            RuinedHeavyChitin.Tier = 5;
            RuinedHeavyChitin.Description = "Bug parts for everyone!";

            ModdedRecipes.Add(RuinedHeavyChitin);

            CraftData PristineLightChitin = new CraftData();
            PristineLightChitin.Key = AuthorID + ".PristineLightChitin";
            PristineLightChitin.CraftedKey = "PristineLightChitin";
            PristineLightChitin.CraftedAmount = 1;
            PristineLightChitin.CraftTime = 0;
            PristineLightChitin.Category = "consumables";
            PristineLightChitin.Tier = 5;
            PristineLightChitin.Description = "Bug parts for everyone!";

            ModdedRecipes.Add(PristineLightChitin);

            CraftData RuinedLightChitin = new CraftData();
            RuinedLightChitin.Key = AuthorID + ".RuinedLightChitin";
            RuinedLightChitin.CraftedKey = "RuinedLightChitin";
            RuinedLightChitin.CraftedAmount = 1;
            RuinedLightChitin.CraftTime = 0;
            RuinedLightChitin.Category = "consumables";
            RuinedLightChitin.Tier = 5;
            RuinedLightChitin.Description = "Bug parts for everyone!";

            ModdedRecipes.Add(RuinedLightChitin);

            CraftData PristineStinger = new CraftData();
            PristineStinger.Key = AuthorID + ".PristineStinger";
            PristineStinger.CraftedKey = "PristineStinger";
            PristineStinger.CraftedAmount = 1;
            PristineStinger.CraftTime = 0;
            PristineStinger.Category = "consumables";
            PristineStinger.Tier = 5;
            PristineStinger.Description = "Bug parts for everyone!";

            ModdedRecipes.Add(PristineStinger);

            CraftData RuinedStinger = new CraftData();
            RuinedStinger.Key = AuthorID + ".RuinedStinger";
            RuinedStinger.CraftedKey = "RuinedStinger";
            RuinedStinger.CraftedAmount = 1;
            RuinedStinger.CraftTime = 0;
            RuinedStinger.Category = "consumables";
            RuinedStinger.Tier = 5;
            RuinedStinger.Description = "Bug parts for everyone!";

            ModdedRecipes.Add(RuinedStinger);

            CraftData PristineFacetedEye = new CraftData();
            PristineFacetedEye.Key = AuthorID + ".PristineFacetedEye";
            PristineFacetedEye.CraftedKey = "PristineFacetedEye";
            PristineFacetedEye.CraftedAmount = 1;
            PristineFacetedEye.CraftTime = 0;
            PristineFacetedEye.Category = "consumables";
            PristineFacetedEye.Tier = 5;
            PristineFacetedEye.Description = "Bug parts for everyone!";

            ModdedRecipes.Add(PristineFacetedEye);

            CraftData RuinedFacetedEye = new CraftData();
            RuinedFacetedEye.Key = AuthorID + ".RuinedFacetedEye";
            RuinedFacetedEye.CraftedKey = "RuinedFacetedEye";
            RuinedFacetedEye.CraftedAmount = 1;
            RuinedFacetedEye.CraftTime = 0;
            RuinedFacetedEye.Category = "consumables";
            RuinedFacetedEye.Tier = 5;
            RuinedFacetedEye.Description = "Bug parts for everyone!";

            ModdedRecipes.Add(RuinedFacetedEye);

            CraftData PristinePhosphorescentGland = new CraftData();
            PristinePhosphorescentGland.Key = AuthorID + ".PristinePhosphorescentGland";
            PristinePhosphorescentGland.CraftedKey = "PristinePhosphorescentGland";
            PristinePhosphorescentGland.CraftedAmount = 1;
            PristinePhosphorescentGland.CraftTime = 0;
            PristinePhosphorescentGland.Category = "consumables";
            PristinePhosphorescentGland.Tier = 5;
            PristinePhosphorescentGland.Description = "Bug parts for everyone!";

            ModdedRecipes.Add(PristinePhosphorescentGland);

            CraftData RuinedPhosphorescentGland = new CraftData();
            RuinedPhosphorescentGland.Key = AuthorID + ".RuinedPhosphorescentGland";
            RuinedPhosphorescentGland.CraftedKey = "RuinedPhosphorescentGland";
            RuinedPhosphorescentGland.CraftedAmount = 1;
            RuinedPhosphorescentGland.CraftTime = 0;
            RuinedPhosphorescentGland.Category = "consumables";
            RuinedPhosphorescentGland.Tier = 5;
            RuinedPhosphorescentGland.Description = "Bug parts for everyone!";

            ModdedRecipes.Add(RuinedPhosphorescentGland);

            CraftData PerfectFacetedEye = new CraftData();
            PerfectFacetedEye.Key = AuthorID + ".PerfectFacetedEye";
            PerfectFacetedEye.CraftedKey = "PerfectFacetedEye";
            PerfectFacetedEye.CraftedAmount = 1;
            PerfectFacetedEye.CraftTime = 0;
            PerfectFacetedEye.Category = "consumables";
            PerfectFacetedEye.Tier = 5;
            PerfectFacetedEye.Description = "Bug parts for everyone!";

            ModdedRecipes.Add(PerfectFacetedEye);

            CraftData MassiveFacetedEye = new CraftData();
            MassiveFacetedEye.Key = AuthorID + ".MassiveFacetedEye";
            MassiveFacetedEye.CraftedKey = "MassiveFacetedEye";
            MassiveFacetedEye.CraftedAmount = 1;
            MassiveFacetedEye.CraftTime = 0;
            MassiveFacetedEye.Category = "consumables";
            MassiveFacetedEye.Tier = 5;
            MassiveFacetedEye.Description = "Bug parts for everyone!";

            ModdedRecipes.Add(MassiveFacetedEye);

            CraftData HiveBrainMatter = new CraftData();
            HiveBrainMatter.Key = AuthorID + ".HiveBrainMatter";
            HiveBrainMatter.CraftedKey = "HiveBrainMatter";
            HiveBrainMatter.CraftedAmount = 1;
            HiveBrainMatter.CraftTime = 0;
            HiveBrainMatter.Category = "consumables";
            HiveBrainMatter.Tier = 5;
            HiveBrainMatter.Description = "Bug parts for everyone!";

            ModdedRecipes.Add(HiveBrainMatter);

            CraftData RefinedLiquidResin = new CraftData();
            RefinedLiquidResin.Key = AuthorID + ".RefinedLiquidResin";
            RefinedLiquidResin.CraftedKey = "RefinedLiquidResin";
            RefinedLiquidResin.CraftedAmount = 1;
            RefinedLiquidResin.CraftTime = 0;
            RefinedLiquidResin.Category = "consumables";
            RefinedLiquidResin.Tier = 5;
            RefinedLiquidResin.Description = "Bug parts for everyone!";

            ModdedRecipes.Add(RefinedLiquidResin);

            CraftData SpoiledOrganicRemains = new CraftData();
            SpoiledOrganicRemains.Key = AuthorID + ".SpoiledOrganicRemains";
            SpoiledOrganicRemains.CraftedKey = "SpoiledOrganicRemains";
            SpoiledOrganicRemains.CraftedAmount = 1;
            SpoiledOrganicRemains.CraftTime = 0;
            SpoiledOrganicRemains.Category = "consumables";
            SpoiledOrganicRemains.Tier = 5;
            SpoiledOrganicRemains.Description = "Bug parts for everyone!";

            ModdedRecipes.Add(SpoiledOrganicRemains);

            CraftData RecombinedOrganicMatter = new CraftData();
            RecombinedOrganicMatter.Key = AuthorID + ".RecombinedOrganicMatter";
            RecombinedOrganicMatter.CraftedKey = "RecombinedOrganicMatter";
            RecombinedOrganicMatter.CraftedAmount = 1;
            RecombinedOrganicMatter.CraftTime = 0;
            RecombinedOrganicMatter.Category = "consumables";
            RecombinedOrganicMatter.Tier = 5;
            RecombinedOrganicMatter.Description = "Bug parts for everyone!";

            ModdedRecipes.Add(RecombinedOrganicMatter);

            CraftData CopperBar = new CraftData();
            CopperBar.Key = AuthorID + ".CopperBar";
            CopperBar.CraftedKey = "CopperBar";
            CopperBar.CraftedAmount = 1;
            CopperBar.CraftTime = 0;
            CopperBar.Category = "decoration";
            CopperBar.Tier = 5;
            CopperBar.Description = "Woo!";
            ModdedRecipes.Add(CopperBar);

            CraftData TinBar = new CraftData();
            TinBar.Key = AuthorID + ".TinBar";
            TinBar.CraftedKey = "TinBar";
            TinBar.CraftedAmount = 1;
            TinBar.CraftTime = 0;
            TinBar.Category = "decoration";
            TinBar.Tier = 5;
            TinBar.Description = "Woo!";
            ModdedRecipes.Add(TinBar);

            CraftData IronBar = new CraftData();
            IronBar.Key = AuthorID + ".IronBar";
            IronBar.CraftedKey = "IronBar";
            IronBar.CraftedAmount = 1;
            IronBar.CraftTime = 0;
            IronBar.Category = "decoration";
            IronBar.Tier = 5;
            IronBar.Description = "Woo!";
            ModdedRecipes.Add(IronBar);

            CraftData LithiumBar = new CraftData();
            LithiumBar.Key = AuthorID + ".LithiumBar";
            LithiumBar.CraftedKey = "LithiumBar";
            LithiumBar.CraftedAmount = 1;
            LithiumBar.CraftTime = 0;
            LithiumBar.Category = "decoration";
            LithiumBar.Tier = 5;
            LithiumBar.Description = "Woo!";
            ModdedRecipes.Add(LithiumBar);

            CraftData GoldBar = new CraftData();
            GoldBar.Key = AuthorID + ".GoldBar";
            GoldBar.CraftedKey = "GoldBar";
            GoldBar.CraftedAmount = 1;
            GoldBar.CraftTime = 0;
            GoldBar.Category = "decoration";
            GoldBar.Tier = 5;
            GoldBar.Description = "Woo!";
            ModdedRecipes.Add(GoldBar);

            CraftData NickelBar = new CraftData();
            NickelBar.Key = AuthorID + ".NickelBar";
            NickelBar.CraftedKey = "NickelBar";
            NickelBar.CraftedAmount = 1;
            NickelBar.CraftTime = 0;
            NickelBar.Category = "decoration";
            NickelBar.Tier = 5;
            NickelBar.Description = "Woo!";
            ModdedRecipes.Add(NickelBar);

            CraftData TitaniumBar = new CraftData();
            TitaniumBar.Key = AuthorID + ".TitaniumBar";
            TitaniumBar.CraftedKey = "TitaniumBar";
            TitaniumBar.CraftedAmount = 1;
            TitaniumBar.CraftTime = 0;
            TitaniumBar.Category = "decoration";
            TitaniumBar.Tier = 5;
            TitaniumBar.Description = "Woo!";
            ModdedRecipes.Add(TitaniumBar);

            CraftData RackRail = new CraftData();
            RackRail.Key = AuthorID + ".RackRail";
            RackRail.CraftedKey = "RackRail";
            RackRail.CraftedAmount = 1;
            RackRail.CraftTime = 0;
            RackRail.Category = "decoration";
            RackRail.Tier = 5;
            RackRail.Description = "Woo!";
            ModdedRecipes.Add(RackRail);

        }
    }
}

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
                Holder.Key = AuthorID + "." + ModDataHolder.RefineryRecipes[i].Key;
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
                //Holder.ItemID = DataHolder.ItemEntries[i].ItemID;
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
            //Special
            CraftData RackRail = new CraftData();
            RackRail.Key = AuthorID + ".RackRail";
            RackRail.CraftedKey = "RackRail";
            RackRail.CraftedAmount = 1;
            RackRail.CraftTime = 0;
            RackRail.Category = "decoration";
            RackRail.Tier = 5;
            RackRail.Description = "Woo!";
            ModdedRecipes.Add(RackRail);

            //This Recipe requires a TerrainData Override, to make sure that HiveCore hardness is below 500. Otherwise players cannot place.
            CraftData HiveCore = new CraftData();
            HiveCore.Key = AuthorID + ".HiveCore";
            HiveCore.CraftedKey = "HiveCore";
            HiveCore.CraftedAmount = 1;
            HiveCore.CraftTime = 0;
            HiveCore.Category = "decoration";
            HiveCore.Tier = 5;
            HiveCore.Description = "Be careful where you place this, will probably encase itself in resin.";
            ModdedRecipes.Add(HiveCore);

            //Missiles
            #region Missiles
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
            #endregion
            //Ores:
            #region Ores
            CraftData CoalOre = new CraftData();
            CoalOre.Key = AuthorID + ".CoalOre";
            CoalOre.CraftedKey = "CoalOre";
            CoalOre.CraftedAmount = 1;
            CoalOre.CraftTime = 0;
            CoalOre.Category = "CraftingIngredient";
            CoalOre.Tier = 5;
            CoalOre.Description = "Just Coal";

            CraftData CopperOre = new CraftData();
            CopperOre.Key = AuthorID + ".CopperOre";
            CopperOre.CraftedKey = "CopperOre";
            CopperOre.CraftedAmount = 1;
            CopperOre.CraftTime = 0;
            CopperOre.Category = "CraftingIngredient";
            CopperOre.Tier = 5;
            CopperOre.Description = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData TinOre = new CraftData();
            TinOre.Key = AuthorID + ".TinOre";
            TinOre.CraftedKey = "TinOre";
            TinOre.CraftedAmount = 1;
            TinOre.CraftTime = 0;
            TinOre.Category = "CraftingIngredient";
            TinOre.Tier = 5;
            TinOre.Description = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData IronOre = new CraftData();
            IronOre.Key = AuthorID + ".IronOre";
            IronOre.CraftedKey = "IronOre";
            IronOre.CraftedAmount = 1;
            IronOre.CraftTime = 0;
            IronOre.Category = "CraftingIngredient";
            IronOre.Tier = 5;
            IronOre.Description = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData LithiumOre = new CraftData();
            LithiumOre.Key = AuthorID + ".LithiumOre";
            LithiumOre.CraftedKey = "LithiumOre";
            LithiumOre.CraftedAmount = 1;
            LithiumOre.CraftTime = 0;
            LithiumOre.Category = "CraftingIngredient";
            LithiumOre.Tier = 5;
            LithiumOre.Description = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData GoldOre = new CraftData();
            GoldOre.Key = AuthorID + ".GoldOre";
            GoldOre.CraftedKey = "GoldOre";
            GoldOre.CraftedAmount = 1;
            GoldOre.CraftTime = 0;
            GoldOre.Category = "CraftingIngredient";
            GoldOre.Tier = 5;
            GoldOre.Description = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData NickelOre = new CraftData();
            NickelOre.Key = AuthorID + ".NickelOre";
            NickelOre.CraftedKey = "NickelOre";
            NickelOre.CraftedAmount = 1;
            NickelOre.CraftTime = 0;
            NickelOre.Category = "CraftingIngredient";
            NickelOre.Tier = 5;
            NickelOre.Description = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData TitaniumOre = new CraftData();
            TitaniumOre.Key = AuthorID + ".TitaniumOre";
            TitaniumOre.CraftedKey = "TitaniumOre";
            TitaniumOre.CraftedAmount = 1;
            TitaniumOre.CraftTime = 0;
            TitaniumOre.Category = "CraftingIngredient";
            TitaniumOre.Tier = 5;
            TitaniumOre.Description = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData CrystalDeposit = new CraftData();
            CrystalDeposit.Key = AuthorID + ".CrystalDeposit";
            CrystalDeposit.CraftedKey = "CrystalDeposit";
            CrystalDeposit.CraftedAmount = 1;
            CrystalDeposit.CraftTime = 0;
            CrystalDeposit.Category = "CraftingIngredient";
            CrystalDeposit.Tier = 5;
            CrystalDeposit.Description = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";

            CraftData BiomassGrowth = new CraftData();
            BiomassGrowth.Key = AuthorID + ".BiomassGrowth";
            BiomassGrowth.CraftedKey = "BiomassGrowth";
            BiomassGrowth.CraftedAmount = 1;
            BiomassGrowth.CraftTime = 0;
            BiomassGrowth.Category = "CraftingIngredient";
            BiomassGrowth.Tier = 5;
            BiomassGrowth.Description = "Use the Manufacturer Plants Auto-Crafting to simulate an Ore Extractor!";


            //This might cause issues later? (If DJ changes the name of the fucking thing and the key? Probably not)
            CraftData HardRock = new CraftData();
            HardRock.Key = AuthorID + ".HardRock";
            HardRock.CraftedKey = "HardRock";
            HardRock.CraftedAmount = 1;
            HardRock.CraftTime = 0;
            HardRock.Category = "CraftingIngredient";
            HardRock.Tier = 5;
            HardRock.Description = "Woo!";
            ModdedRecipes.Add(HardRock);

            CraftData VeryHardRock = new CraftData();
            VeryHardRock.Key = AuthorID + ".VeryHardRock";
            VeryHardRock.CraftedKey = "VeryHardRock";
            VeryHardRock.CraftedAmount = 1;
            VeryHardRock.CraftTime = 0;
            VeryHardRock.Category = "CraftingIngredient";
            VeryHardRock.Tier = 5;
            VeryHardRock.Description = "Woo!";
            ModdedRecipes.Add(VeryHardRock);
            //End of problem area?

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
            #endregion
            //Organics:consumables
            #region Organics
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
            #endregion
            //Bars
            #region Bars
            CraftData CopperBar = new CraftData();
            CopperBar.Key = AuthorID + ".CopperBar";
            CopperBar.CraftedKey = "CopperBar";
            CopperBar.CraftedAmount = 1;
            CopperBar.CraftTime = 0;
            CopperBar.Category = "CraftingIngredient";
            CopperBar.Tier = 5;
            CopperBar.Description = "Woo!";
            ModdedRecipes.Add(CopperBar);

            CraftData TinBar = new CraftData();
            TinBar.Key = AuthorID + ".TinBar";
            TinBar.CraftedKey = "TinBar";
            TinBar.CraftedAmount = 1;
            TinBar.CraftTime = 0;
            TinBar.Category = "CraftingIngredient";
            TinBar.Tier = 5;
            TinBar.Description = "Woo!";
            ModdedRecipes.Add(TinBar);

            CraftData IronBar = new CraftData();
            IronBar.Key = AuthorID + ".IronBar";
            IronBar.CraftedKey = "IronBar";
            IronBar.CraftedAmount = 1;
            IronBar.CraftTime = 0;
            IronBar.Category = "CraftingIngredient";
            IronBar.Tier = 5;
            IronBar.Description = "Woo!";
            ModdedRecipes.Add(IronBar);

            CraftData LithiumBar = new CraftData();
            LithiumBar.Key = AuthorID + ".LithiumBar";
            LithiumBar.CraftedKey = "LithiumBar";
            LithiumBar.CraftedAmount = 1;
            LithiumBar.CraftTime = 0;
            LithiumBar.Category = "CraftingIngredient";
            LithiumBar.Tier = 5;
            LithiumBar.Description = "Woo!";
            ModdedRecipes.Add(LithiumBar);

            CraftData GoldBar = new CraftData();
            GoldBar.Key = AuthorID + ".GoldBar";
            GoldBar.CraftedKey = "GoldBar";
            GoldBar.CraftedAmount = 1;
            GoldBar.CraftTime = 0;
            GoldBar.Category = "CraftingIngredient";
            GoldBar.Tier = 5;
            GoldBar.Description = "Woo!";
            ModdedRecipes.Add(GoldBar);

            CraftData NickelBar = new CraftData();
            NickelBar.Key = AuthorID + ".NickelBar";
            NickelBar.CraftedKey = "NickelBar";
            NickelBar.CraftedAmount = 1;
            NickelBar.CraftTime = 0;
            NickelBar.Category = "CraftingIngredient";
            NickelBar.Tier = 5;
            NickelBar.Description = "Woo!";
            ModdedRecipes.Add(NickelBar);

            CraftData TitaniumBar = new CraftData();
            TitaniumBar.Key = AuthorID + ".TitaniumBar";
            TitaniumBar.CraftedKey = "TitaniumBar";
            TitaniumBar.CraftedAmount = 1;
            TitaniumBar.CraftTime = 0;
            TitaniumBar.Category = "CraftingIngredient";
            TitaniumBar.Tier = 5;
            TitaniumBar.Description = "Woo!";
            ModdedRecipes.Add(TitaniumBar);

            CraftData ChromiumBar = new CraftData();
            ChromiumBar.Key = AuthorID + ".ChromiumBar";
            ChromiumBar.CraftedKey = "ChromiumBar";
            ChromiumBar.CraftedAmount = 1;
            ChromiumBar.CraftTime = 0;
            ChromiumBar.Category = "CraftingIngredient";
            ChromiumBar.Tier = 5;
            ChromiumBar.Description = "Woo!";
            ModdedRecipes.Add(ChromiumBar);

            CraftData MolybdenumBar = new CraftData();
            MolybdenumBar.Key = AuthorID + ".MolybdenumBar";
            MolybdenumBar.CraftedKey = "MolybdenumBar";
            MolybdenumBar.CraftedAmount = 1;
            MolybdenumBar.CraftTime = 0;
            MolybdenumBar.Category = "CraftingIngredient";
            MolybdenumBar.Tier = 5;
            MolybdenumBar.Description = "Woo!";
            ModdedRecipes.Add(MolybdenumBar);

            #endregion
            //Wires
            #region Wires
            // Wires
            CraftData CopperWire = new CraftData();
            CopperWire.Key = AuthorID + ".CopperWire";
            CopperWire.CraftedKey = "CopperWire";
            CopperWire.CraftedAmount = 1;
            CopperWire.CraftTime = 0;
            CopperWire.Category = "CraftingIngredient";
            CopperWire.Tier = 5;
            CopperWire.Description = "Make sure to make neat bundles!";
            ModdedRecipes.Add(CopperWire);

            CraftData TinWire = new CraftData();
            TinWire.Key = AuthorID + ".TinWire";
            TinWire.CraftedKey = "TinWire";
            TinWire.CraftedAmount = 1;
            TinWire.CraftTime = 0;
            TinWire.Category = "CraftingIngredient";
            TinWire.Tier = 5;
            TinWire.Description = "Make sure to make neat bundles!";
            ModdedRecipes.Add(TinWire);

            CraftData IronWire = new CraftData();
            IronWire.Key = AuthorID + ".IronWire";
            IronWire.CraftedKey = "IronWire";
            IronWire.CraftedAmount = 1;
            IronWire.CraftTime = 0;
            IronWire.Category = "CraftingIngredient";
            IronWire.Tier = 5;
            IronWire.Description = "Make sure to make neat bundles!";
            ModdedRecipes.Add(IronWire);

            CraftData LithiumWire = new CraftData();
            LithiumWire.Key = AuthorID + ".LithiumWire";
            LithiumWire.CraftedKey = "LithiumWire";
            LithiumWire.CraftedAmount = 1;
            LithiumWire.CraftTime = 0;
            LithiumWire.Category = "CraftingIngredient";
            LithiumWire.Tier = 5;
            LithiumWire.Description = "Make sure to make neat bundles!";
            ModdedRecipes.Add(LithiumWire);

            CraftData GoldWire = new CraftData();
            GoldWire.Key = AuthorID + ".GoldWire";
            GoldWire.CraftedKey = "GoldWire";
            GoldWire.CraftedAmount = 1;
            GoldWire.CraftTime = 0;
            GoldWire.Category = "CraftingIngredient";
            GoldWire.Tier = 5;
            GoldWire.Description = "Make sure to make neat bundles!";
            ModdedRecipes.Add(GoldWire);

            CraftData NickelWire = new CraftData();
            NickelWire.Key = AuthorID + ".NickelWire";
            NickelWire.CraftedKey = "NickelWire";
            NickelWire.CraftedAmount = 1;
            NickelWire.CraftTime = 0;
            NickelWire.Category = "CraftingIngredient";
            NickelWire.Tier = 5;
            NickelWire.Description = "Make sure to make neat bundles!";
            ModdedRecipes.Add(NickelWire);

            CraftData TitaniumWire = new CraftData();
            TitaniumWire.Key = AuthorID + ".TitaniumWire";
            TitaniumWire.CraftedKey = "TitaniumWire";
            TitaniumWire.CraftedAmount = 1;
            TitaniumWire.CraftTime = 0;
            TitaniumWire.Category = "CraftingIngredient";
            TitaniumWire.Tier = 5;
            TitaniumWire.Description = "Make sure to make neat bundles!";
            ModdedRecipes.Add(TitaniumWire);
            #endregion
            //Coils
            #region Coils
            CraftData CopperCoil = new CraftData();
            CopperCoil.Key = AuthorID + ".CopperCoil";
            CopperCoil.CraftedKey = "CopperCoil";
            CopperCoil.CraftedAmount = 1;
            CopperCoil.CraftTime = 0;
            CopperCoil.Category = "CraftingIngredient";
            CopperCoil.Tier = 5;
            CopperCoil.Description = "Cause sometimes, wires are better in a coil!";
            ModdedRecipes.Add(CopperCoil);

            CraftData TinCoil = new CraftData();
            TinCoil.Key = AuthorID + ".TinCoil";
            TinCoil.CraftedKey = "TinCoil";
            TinCoil.CraftedAmount = 1;
            TinCoil.CraftTime = 0;
            TinCoil.Category = "CraftingIngredient";
            TinCoil.Tier = 5;
            TinCoil.Description = "Make sure to make neat bundles!";
            ModdedRecipes.Add(TinCoil);

            CraftData IronCoil = new CraftData();
            IronCoil.Key = AuthorID + ".IronCoil";
            IronCoil.CraftedKey = "IronCoil";
            IronCoil.CraftedAmount = 1;
            IronCoil.CraftTime = 0;
            IronCoil.Category = "CraftingIngredient";
            IronCoil.Tier = 5;
            IronCoil.Description = "Cause sometimes, wires are better in a coil!";
            ModdedRecipes.Add(IronCoil);

            CraftData LithiumCoil = new CraftData();
            LithiumCoil.Key = AuthorID + ".LithiumCoil";
            LithiumCoil.CraftedKey = "LithiumCoil";
            LithiumCoil.CraftedAmount = 1;
            LithiumCoil.CraftTime = 0;
            LithiumCoil.Category = "CraftingIngredient";
            LithiumCoil.Tier = 5;
            LithiumCoil.Description = "Cause sometimes, wires are better in a coil!";
            ModdedRecipes.Add(LithiumCoil);

            CraftData GoldCoil = new CraftData();
            GoldCoil.Key = AuthorID + ".GoldCoil";
            GoldCoil.CraftedKey = "GoldCoil";
            GoldCoil.CraftedAmount = 1;
            GoldCoil.CraftTime = 0;
            GoldCoil.Category = "CraftingIngredient";
            GoldCoil.Tier = 5;
            GoldCoil.Description = "Cause sometimes, wires are better in a coil!";
            ModdedRecipes.Add(GoldCoil);

            CraftData NickelCoil = new CraftData();
            NickelCoil.Key = AuthorID + ".NickelCoil";
            NickelCoil.CraftedKey = "NickelCoil";
            NickelCoil.CraftedAmount = 1;
            NickelCoil.CraftTime = 0;
            NickelCoil.Category = "CraftingIngredient";
            NickelCoil.Tier = 5;
            NickelCoil.Description = "Cause sometimes, wires are better in a coil!";
            ModdedRecipes.Add(NickelCoil);

            CraftData TitaniumCoil = new CraftData();
            TitaniumCoil.Key = AuthorID + ".TitaniumCoil";
            TitaniumCoil.CraftedKey = "TitaniumCoil";
            TitaniumCoil.CraftedAmount = 1;
            TitaniumCoil.CraftTime = 0;
            TitaniumCoil.Category = "CraftingIngredient";
            TitaniumCoil.Tier = 5;
            TitaniumCoil.Description = "Cause sometimes, wires are better in a coil!";
            ModdedRecipes.Add(TitaniumCoil);
            #endregion
            //PBCs
            #region PCBs
            CraftData BasicPCB = new CraftData();
            BasicPCB.Key = AuthorID + ".BasicPCB";
            BasicPCB.CraftedKey = "BasicPCB";
            BasicPCB.CraftedAmount = 1;
            BasicPCB.CraftTime = 0;
            BasicPCB.Category = "CraftingIngredient";
            BasicPCB.Tier = 5;
            BasicPCB.Description = "Who doesnt love Printed Circuits?";
            ModdedRecipes.Add(BasicPCB);

            CraftData PrimaryPCB = new CraftData();
            PrimaryPCB.Key = AuthorID + ".PrimaryPCB";
            PrimaryPCB.CraftedKey = "PrimaryPCB";
            PrimaryPCB.CraftedAmount = 1;
            PrimaryPCB.CraftTime = 0;
            PrimaryPCB.Category = "CraftingIngredient";
            PrimaryPCB.Tier = 5;
            PrimaryPCB.Description = "Who doesnt love Printed Circuits?";
            ModdedRecipes.Add(PrimaryPCB);

            CraftData HardenedPCB = new CraftData();
            HardenedPCB.Key = AuthorID + ".HardenedPCB";
            HardenedPCB.CraftedKey = "HardenedPCB";
            HardenedPCB.CraftedAmount = 1;
            HardenedPCB.CraftTime = 0;
            HardenedPCB.Category = "CraftingIngredient";
            HardenedPCB.Tier = 5;
            HardenedPCB.Description = "Who doesnt love Printed Circuits?";
            ModdedRecipes.Add(HardenedPCB);

            CraftData ConductivePCB = new CraftData();
            ConductivePCB.Key = AuthorID + ".ConductivePCB";
            ConductivePCB.CraftedKey = "ConductivePCB";
            ConductivePCB.CraftedAmount = 1;
            ConductivePCB.CraftTime = 0;
            ConductivePCB.Category = "CraftingIngredient";
            ConductivePCB.Tier = 5;
            ConductivePCB.Description = "Who doesnt love Printed Circuits?";
            ModdedRecipes.Add(ConductivePCB);

            CraftData ChargedPCB = new CraftData();
            ChargedPCB.Key = AuthorID + ".ChargedPCB";
            ChargedPCB.CraftedKey = "ChargedPCB";
            ChargedPCB.CraftedAmount = 1;
            ChargedPCB.CraftTime = 0;
            ChargedPCB.Category = "CraftingIngredient";
            ChargedPCB.Tier = 5;
            ChargedPCB.Description = "Who doesnt love Printed Circuits?";
            ModdedRecipes.Add(ChargedPCB);

            CraftData FortifiedPCB = new CraftData();
            FortifiedPCB.Key = AuthorID + ".FortifiedPCB";
            FortifiedPCB.CraftedKey = "FortifiedPCB";
            FortifiedPCB.CraftedAmount = 1;
            FortifiedPCB.CraftTime = 0;
            FortifiedPCB.Category = "CraftingIngredient";
            FortifiedPCB.Tier = 5;
            FortifiedPCB.Description = "Who doesnt love Printed Circuits?";
            ModdedRecipes.Add(FortifiedPCB);

            CraftData LightweightPCB = new CraftData();
            LightweightPCB.Key = AuthorID + ".LightweightPCB";
            LightweightPCB.CraftedKey = "LightweightPCB";
            LightweightPCB.CraftedAmount = 1;
            LightweightPCB.CraftTime = 0;
            LightweightPCB.Category = "CraftingIngredient";
            LightweightPCB.Tier = 5;
            LightweightPCB.Description = "Who doesnt love Printed Circuits?";
            ModdedRecipes.Add(LightweightPCB);
            #endregion
            //Plates
            #region Plates
            CraftData CopperPlate = new CraftData();
            CopperPlate.Key = AuthorID + ".CopperPlate";
            CopperPlate.CraftedKey = "CopperPlate";
            CopperPlate.CraftedAmount = 1;
            CopperPlate.CraftTime = 0;
            CopperPlate.Category = "CraftingIngredient";
            CopperPlate.Tier = 5;
            CopperPlate.Description = "Cause flat bars are cool too!";
            ModdedRecipes.Add(CopperPlate);

            CraftData TinPlate = new CraftData();
            TinPlate.Key = AuthorID + ".TinPlate";
            TinPlate.CraftedKey = "TinPlate";
            TinPlate.CraftedAmount = 1;
            TinPlate.CraftTime = 0;
            TinPlate.Category = "CraftingIngredient";
            TinPlate.Tier = 5;
            TinPlate.Description = "Cause flat bars are cool too!";
            ModdedRecipes.Add(TinPlate);

            CraftData IronPlate = new CraftData();
            IronPlate.Key = AuthorID + ".IronPlate";
            IronPlate.CraftedKey = "IronPlate";
            IronPlate.CraftedAmount = 1;
            IronPlate.CraftTime = 0;
            IronPlate.Category = "CraftingIngredient";
            IronPlate.Tier = 5;
            IronPlate.Description = "Cause flat bars are cool too!";
            ModdedRecipes.Add(IronPlate);

            CraftData LithiumPlate = new CraftData();
            LithiumPlate.Key = AuthorID + ".LithiumPlate";
            LithiumPlate.CraftedKey = "LithiumPlate";
            LithiumPlate.CraftedAmount = 1;
            LithiumPlate.CraftTime = 0;
            LithiumPlate.Category = "CraftingIngredient";
            LithiumPlate.Tier = 5;
            LithiumPlate.Description = "Cause flat bars are cool too!";
            ModdedRecipes.Add(LithiumPlate);

            CraftData GoldPlate = new CraftData();
            GoldPlate.Key = AuthorID + ".GoldPlate";
            GoldPlate.CraftedKey = "GoldPlate";
            GoldPlate.CraftedAmount = 1;
            GoldPlate.CraftTime = 0;
            GoldPlate.Category = "CraftingIngredient";
            GoldPlate.Tier = 5;
            GoldPlate.Description = "Cause flat bars are cool too!";
            ModdedRecipes.Add(GoldPlate);

            CraftData NickelPlate = new CraftData();
            NickelPlate.Key = AuthorID + ".NickelPlate";
            NickelPlate.CraftedKey = "NickelPlate";
            NickelPlate.CraftedAmount = 1;
            NickelPlate.CraftTime = 0;
            NickelPlate.Category = "CraftingIngredient";
            NickelPlate.Tier = 5;
            NickelPlate.Description = "Cause flat bars are cool too!";
            ModdedRecipes.Add(NickelPlate);

            CraftData TitaniumPlate = new CraftData();
            TitaniumPlate.Key = AuthorID + ".TitaniumPlate";
            TitaniumPlate.CraftedKey = "TitaniumPlate";
            TitaniumPlate.CraftedAmount = 1;
            TitaniumPlate.CraftTime = 0;
            TitaniumPlate.Category = "CraftingIngredient";
            TitaniumPlate.Tier = 5;
            TitaniumPlate.Description = "Cause flat bars are cool too!";
            ModdedRecipes.Add(TitaniumPlate);
            #endregion
            //Research Pods
            #region ResearchPods

            CraftData BasicExperimentalPod = new CraftData();
            BasicExperimentalPod.Key = AuthorID + ".BasicExperimentalPod";
            BasicExperimentalPod.CraftedKey = "BasicExperimentalPod";
            BasicExperimentalPod.CraftedAmount = 1;
            BasicExperimentalPod.CraftTime = 0;
            BasicExperimentalPod.Category = "CraftingIngredient";
            BasicExperimentalPod.Tier = 5;
            BasicExperimentalPod.Description = "Pods Fo days!";
            ModdedRecipes.Add(BasicExperimentalPod);

            CraftData SimplifiedExperimentalPod = new CraftData();
            SimplifiedExperimentalPod.Key = AuthorID + ".SimplifiedExperimentalPod";
            SimplifiedExperimentalPod.CraftedKey = "SimplifiedExperimentalPod";
            SimplifiedExperimentalPod.CraftedAmount = 1;
            SimplifiedExperimentalPod.CraftTime = 0;
            SimplifiedExperimentalPod.Category = "CraftingIngredient";
            SimplifiedExperimentalPod.Tier = 5;
            SimplifiedExperimentalPod.Description = "Pods Fo days!";
            ModdedRecipes.Add(SimplifiedExperimentalPod);

            CraftData IntermediateExperimentalPod = new CraftData();
            IntermediateExperimentalPod.Key = AuthorID + ".IntermediateExperimentalPod";
            IntermediateExperimentalPod.CraftedKey = "IntermediateExperimentalPod";
            IntermediateExperimentalPod.CraftedAmount = 1;
            IntermediateExperimentalPod.CraftTime = 0;
            IntermediateExperimentalPod.Category = "CraftingIngredient";
            IntermediateExperimentalPod.Tier = 5;
            IntermediateExperimentalPod.Description = "Pods Fo days!";
            ModdedRecipes.Add(IntermediateExperimentalPod);

            CraftData ComplexExperimentalPod = new CraftData();
            ComplexExperimentalPod.Key = AuthorID + ".ComplexExperimentalPod";
            ComplexExperimentalPod.CraftedKey = "ComplexExperimentalPod";
            ComplexExperimentalPod.CraftedAmount = 1;
            ComplexExperimentalPod.CraftTime = 0;
            ComplexExperimentalPod.Category = "CraftingIngredient";
            ComplexExperimentalPod.Tier = 5;
            ComplexExperimentalPod.Description = "Pods Fo days!";
            ModdedRecipes.Add(ComplexExperimentalPod);

            CraftData AdvancedExperimentalPod = new CraftData();
            AdvancedExperimentalPod.Key = AuthorID + ".AdvancedExperimentalPod";
            AdvancedExperimentalPod.CraftedKey = "AdvancedExperimentalPod";
            AdvancedExperimentalPod.CraftedAmount = 1;
            AdvancedExperimentalPod.CraftTime = 0;
            AdvancedExperimentalPod.Category = "CraftingIngredient";
            AdvancedExperimentalPod.Tier = 5;
            AdvancedExperimentalPod.Description = "Pods Fo days!";
            ModdedRecipes.Add(AdvancedExperimentalPod);

            CraftData XLExperimentalPod = new CraftData();
            XLExperimentalPod.Key = AuthorID + ".XLExperimentalPod";
            XLExperimentalPod.CraftedKey = "XLExperimentalPod";
            XLExperimentalPod.CraftedAmount = 1;
            XLExperimentalPod.CraftTime = 0;
            XLExperimentalPod.Category = "CraftingIngredient";
            XLExperimentalPod.Tier = 5;
            XLExperimentalPod.Description = "Pods Fo days!";
            ModdedRecipes.Add(XLExperimentalPod);

            CraftData UltimateExperimentalPod = new CraftData();
            UltimateExperimentalPod.Key = AuthorID + ".UltimateExperimentalPod";
            UltimateExperimentalPod.CraftedKey = "UltimateExperimentalPod";
            UltimateExperimentalPod.CraftedAmount = 1;
            UltimateExperimentalPod.CraftTime = 0;
            UltimateExperimentalPod.Category = "CraftingIngredient";
            UltimateExperimentalPod.Tier = 5;
            UltimateExperimentalPod.Description = "Pods Fo days!";
            ModdedRecipes.Add(UltimateExperimentalPod);
            #endregion

        }
    }

    public static class FreeDecorMod
    {
        public static List<CraftData> ModdedRecipes = new List<CraftData>();
        public static string AuthorID { get; set; }
        public static void CreateRecipes()
        {
            for (int i = 0; i < DataHolder.ManufacturerEntries.Count; i++)
            {
                if (DataHolder.ManufacturerEntries[i].Category == "decoration")
                {
                    CraftData Holder = new CraftData();
                    Holder.Key = DataHolder.ManufacturerEntries[i].Key;
                    Holder.IsOverride = "true";
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
                    Holder.ResearchCost = 0;
                    Holder.Description = DataHolder.ManufacturerEntries[i].Description;
                    Holder.Hint = DataHolder.ManufacturerEntries[i].Hint;
                    Holder.CanCraftAnywhere = DataHolder.ManufacturerEntries[i].CanCraftAnywhere;
                    Holder.CanBeAutomated = true;
                    Holder.MasterRecipe = DataHolder.ManufacturerEntries[i].MasterRecipe;
                    ModdedRecipes.Add(Holder);
                }
                if (DataHolder.ManufacturerEntries[i].CanCraftAnywhere == true && DataHolder.ManufacturerEntries[i].Category == "defences")
                {
                    CraftData Holder = new CraftData();
                    Holder.Key = DataHolder.ManufacturerEntries[i].Key;
                    Holder.IsOverride = "true";
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
                    Holder.ResearchCost = 0;
                    Holder.Description = DataHolder.ManufacturerEntries[i].Description;
                    Holder.Hint = DataHolder.ManufacturerEntries[i].Hint;
                    Holder.CanCraftAnywhere = DataHolder.ManufacturerEntries[i].CanCraftAnywhere;
                    Holder.CanBeAutomated = true;
                    Holder.MasterRecipe = DataHolder.ManufacturerEntries[i].MasterRecipe;
                    ModdedRecipes.Add(Holder);
                }
            }
        }
    }
}

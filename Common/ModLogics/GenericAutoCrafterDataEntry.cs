﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ModLogics
{
    [Serializable]
    public class GenericAutoCrafterDataEntry
    {
        public string FriendlyName { get; set; }
        public SpawnableObjectEnum SpawnObject { get; set; }
        public string LoadingAnimation { get; set; }
        public string WorkingAnimation { get; set; }
        public string UnloadingAnimation { get; set; }
        public string CraftingString { get; set; }
        public string Value { get; set; }
        public float PowerUsePerSecond { get; set; }
        public float PowerTransferPerSecond { get; set; }
        public float MaxPowerStorage { get; set; }
        public float CraftTime { get; set; }
        public bool OptionalIngredients { get; set; }
        public CraftData Recipe { get; set; }
    }
    public enum SpawnableObjectEnum
    {
        Sign,
        LaserEmitter,
        LaserAndGate,
        LaserOrGate,
        LaserNotGate,
        LaserPressurePad,
        LaserSplitter,
        LaserMirror,
        LaserReciever,
        TorchFloor,
        TorchWall,
        TorchRoof,
        CentralPowerHub,
        HeatSource,
        ThreatScanner,
        StorageHopper,
        OreExtractor,
        OreSmelter,
        LaserPowerTransmitter,
        BFL9000,
        PyrothermicGenerator,
        ManufacturingPlantCore,
        LaserPowerTransmitter_T2,
        LaserPowerTransmitter_T3,
        LaserPowerTransmitter_T4,
        LaserPowerTransmitter_T0,
        SolarPanel_T1,
        AIMover,
        MatterMover,
        UnknownItem,
        ItemCube,
        PowerStorageBlock,
        PowerStorageBlock_T2,
        PowerStorageBlock_T3,
        PowerStorageBlock_MK4_Organic,
        PowerStorageBlock_MK4,
        Conveyor,
        Conveyor_Filter_Single,
        Conveyor_Filter_Turntable,
        TransportPipe,
        TransportPipe_Filter_Single,
        TransportPipe_Filter_Turntable,
        DrillMotor,
        DrillHead,
        ResearchStation,
        AirInductor,
        PopUpTurret,
        AutoExcavator,
        RefineryController,
        RefineryReactorVat,
        WaspGlandPristine,
        WaspGlandRuined,
        WaspEyePristine,
        WaspEyeRuined,
        WaspStingerPristine,
        WaspStingerRuined,
        WaspHeavyChitinPristine,
        WaspHeavyChitinRuined,
        WaspLightChitinPristine,
        WaspLightChitinRuined,
        Lift_T1,
        Lift_T2,
        Lift_T3,
        Lift_Compressor,
        Lift_ManualControl,
        Printer,
        Compressor,
        Extruder,
        ChipEtcher,
        HydrojetCutter,
        RoboticWelder,
        Incubator,
        AssemblyStation,
        JetTurbine,
        MK1RobotArm,
        MassStorageCrate,
        MassStorageInputPort,
        MassStorageOutputPort,
        BarCopper,
        BarIron,
        BarTin,
        BarLithium,
        BarTitanium,
        BarNickel,
        BarGold,
        BarChromium,
        BarMolybdenum,
        Bar_Spare_3,
        Bar_Spare_4,
        Bar_Spare_5,
        Bar_Spare_6,
        Bar_Spare_7,
        Bar_Spare_8,
        Bar_Spare_9,
        Bar_Spare_10,
        IronGear,
        CopperWire,
        MachinePlate,
        ServoMotor,
        MachineFrame,
        TinPlate,
        JetFuelCanister,
        EmptyCanister,
        EmptyMetalTube,
        Macerator,
        Turntable_T1,
        Turntable_T2,
        ARTHERRechargeStation,
        Crystal_Diamond,
        Crystal_Emerald,
        Crystal_Ruby,
        Crystal_Sapphire,
        Crystal_Topaz,
        Crystal_Sugilite,
        Lens_Diamond,
        Lens_Emerald,
        Lens_Ruby,
        Lens_Sapphire,
        Lens_Topaz,
        Lens_Sugilite,
        WaypointObject,
        AutoBuilder,
        ChargeExplosive,
        Teleporter,
        LogisticsHopper,
        MicroHopper,
        CryoHopper,
        AirInductor_T2,
        AirInductor_T3,
        Laboratory,
        CreeperSpawn,
        MobWasp,
        Stamper_T1,
        PipeExtruder_T1,
        Coiler_T1,
        Extruder_T1,
        WireCopper,
        WireIron,
        WireTin,
        WireLithium,
        WireTitanium,
        WireNickel,
        WireGold,
        CoilCopper,
        CoilIron,
        CoilTin,
        CoilLithium,
        CoilTitanium,
        CoilNickel,
        CoilGold,
        PlateCopper,
        PlateIron,
        PlateTin,
        PlateLithium,
        PlateTitanium,
        PlateNickel,
        PlateGold,
        HousingCopper,
        HousingIron,
        HousingTin,
        HousingLithium,
        HousingTitanium,
        HousingNickel,
        HousingGold,
        PipeCopper,
        PipeIron,
        PipeTin,
        PipeLithium,
        PipeTitanium,
        PipeNickel,
        PipeGold,
        BentPipeCopper,
        BentPipeIron,
        BentPipeTin,
        BentPipeLithium,
        BentPipeTitanium,
        BentPipeNickel,
        BentPipeGold,
        PCBAssembler_T1,
        BasicPCB,
        PrimaryPCB,
        HardenedPCB,
        ChargedPCB,
        LightweightPCB,
        FortifiedPCB,
        ConductivePCB,
        TestMob,
        ExperimentalAssembler,
        ResearchPod_Basic,
        ResearchPod_Simplified,
        ResearchPod_Intermediate,
        ResearchPod_Complex,
        ResearchPod_Advanced,
        ResearchPod_XL,
        ResearchPod_Ultimate,
        GeologicalSurveyor,
        Quarry,
        BasicRocketFramework,
        BasicRocketEngine,
        BasicRocketFuelTank,
        BasicRocketStabiliser,
        BasicRocketCargoHold,
        BasicRocketSRB,
        BasicRocketNoseCone,
        BasicRocketFin,
        CreativeWaterSmall,
        CreativeWaterMed,
        CreativeWaterLarge,
        Alien_Plant1_GlowpodOrange,
        Alien_Plant2_GlowpodBlue,
        Alien_Plant3_GlowpodGreen,
        Alien_Plant4_GlowpodPurple,
        Alien_Plant5_GlowShroom,
        Alien_Plant6_BaubleBerry,
        Alien_Plant7,
        Alien_Plant8,
        Alien_Plant9,
        Alien_Plant10,
        Alien_Plant_CC_1,
        Alien_Plant_CC_2,
        Alien_Plant_CC_3,
        Alien_Plant_CC_4,
        Alien_Plant_CC_5,
        Alien_Plant_CC_6,
        Alien_Plant_CC_7,
        Alien_Plant18,
        Alien_Plant19,
        Alien_Plant20,
        Alien_Plant_Toxic_1,
        Alien_Plant_Toxic_2,
        Alien_Plant_Toxic_3,
        Alien_Plant_Toxic_4,
        Alien_Plant_Toxic_5,
        Alien_Plant_Toxic_6,
        Alien_Plant_Toxic_7,
        Alien_Plant28,
        Alien_Plant29,
        Alien_Plant30,
        Arachnid_Rock,
        Arachnid_Mob,
        GrapplingPoint,
        ElectricLight,
        ElectricSpotlight,
        Ooze_Mob,
        CamoBot_Mob,
        Mob_Mole_T1,
        Mob_Placeholder3,
        Mob_Placeholder4,
        Mob_Placeholder5,
        Mob_TunnelNuker,
        Mob_RobotWasp,
        CreativeGreenScreen,
        HiveMob,
        HiveEntity,
        Minecart_T1,
        Minecart_T2,
        Minecart_T3,
        Minecart_T4,
        Minecart_T5,
        Minecart_T6,
        Minecart_T7,
        Minecart_T8,
        Minecart_T9,
        Minecart_T10,
        Minecart_Track_Straight,
        Minecart_Track_Corner,
        Minecart_Track_Slope,
        Minecart_Track_Buffer,
        Minecart_Track_Brake,
        Minecart_Track_Boost,
        Minecart_Track_Factory,
        Minecart_Track_Despawn,
        Minecart_Track_LoadStation,
        Minecart_Track_UnloadStation,
        Minecart_Track_Recharger,
        Minecart_Track_BufferEmpty,
        Minecart_Track_BufferFull,
        Minecart_Track_Expansion6,
        Minecart_Track_Expansion7,
        Minecart_Track_Expansion8,
        Minecart_Track_Expansion9,
        Minecart_Track_Expansion10,
        Minecart_Track_Expansion11,
        Minecart_Track_Expansion12,
        ARTHER_Turret,
        ARTHER_Base1,
        ARTHER_Base2,
        ARTHER_Base3,
        ARTHER_Base4,
        MobWasp_Fast,
        MobWasp_Hard,
        MobWasp_Boss,
        PopUpTurret_T2,
        PopUpTurret_T3,
        Turret_T4,
        PopUpTurret_T5,
        MissileTurret_T1,
        MissileTurret_T2,
        MissileTurret_T3,
        MissileTurret_T4,
        MissileTurret_T5,
        MissileAssembler,
        Missile_T1,
        Missile_T2,
        Missile_T3,
        Missile_T4,
        Missile_T5,
        Missile_T6,
        Missile_T7,
        Missile_T8,
        BasicConveyor,
        OrbitalEnergyTransmitter,
        AutoUpgrader,
        PowerStorageBlock_MK5,
        SolarPanel_T2,
        TrackGate,
        TrackTerminus,
        TrackPoint,
        TrackStation,
        TrackExpansion1,
        TrackExpansion2,
        TrackExpansion3,
        TrackExpansion4,
        TrackExpansion5,
        TrackExpansion6,
        OreSmelterBasic,
        CentralPowerHub_MB,
        Pumpkin_Torch,
        Conveyor_Filter_Advanced,
        SolarPanel_Organic,
        Conveyor_SlopeUp,
        Conveyor_SlopeDown,
        Wasp_Agitator,
        Wasp_Calmer,
        SpoiledOrganicRemains,
        OrganicReassembler,
        RecombinedOrganicMatter,
        Snowman_Torch,
        DrillHead_Default,
        DrillHead_Steel,
        DrillHead_Crystal,
        DrillHead_Organic,
        Zipper_Merge,
        FusionPowerPylon,
        FusionPowerCore,
        FusionPowerMonitor,
        ServerMonitor,
        CoalEnricher,
        EmergencySiren,
        EmergencyOrange,
        EmergencyRedLight,
        EmergencyStrobe,
        PlasmaHeadCharger,
        DrillHead_Plasma,
        MotorisedLogisticsHopper,
        ChargeableExplosiveDrop,
        Conveyor_Motorised,
        HeatConductantPipe,
        GeothermalGenerator,
        EmptyMissileCrafter,
        MissileFueller,
        WarheadFitter,
        PlasmaWarheadImbuer,
        EmptyMissile,
        ArmourPiercingMissile,
        ImbuedMissile,
        AirInductor_T4,
        AirInductor_T5,
        MB_Ore_Extractor_Drill,
        MB_Ore_Extractor_Motor,
        Minecart_T1_Object,
        Minecart_T2_Object,
        Minecart_T3_Object,
        Minecart_T4_Object,
        Minecart_T5_Object,
        Minecart_T6_Object,
        Minecart_T7_Object,
        Minecart_T8_Object,
        Minecart_T9_Object,
        Minecart_T10_Object,
        CastingPipe,
        CastingPipeBend,
        BlastFurnace,
        ContinuousCastingBasin,
        T4_Conduit,
        AnimatedHiveBrain,
        WormBoss,
        WormBossLava,
        RackRail,
        CargoLiftController,
        CargoLift,
        OrbitalStrikeController,
        SurfaceHive,
        FALCOR_MK1,
        FALCOR_MK2,
        FALCOR_MK3,
        FALCOR_Beacon,
        SpiderBotBase,
        SpiderBotPowerCoreCharger,
        SpiderBotPowerCore,
        BrainIncubator,
        MatterMover_T2,
        MatterMover_T3,
        MatterMover_T4,
        LaserAblator,
        LaserLiquifier,
        SolarPanel_T2_Organic,
        RefinedLiquidResin,
        LiquidResinRefiner,
        CCCCC,
        T4_Grinder,
        Creep_Lancer,
        Creep_Mortar,
        T4_CreepBurner,
        HardResinDetector,
        WorkFloorExcavator,
        WorkFloorExcavator_MK2,
        AirInductor_ARC,
        MK1PowerBooster,
        MK2PowerBooster,
        MK3PowerBooster,
        MK4PowerBooster,
        MK5PowerBooster,
        MK1PowerBoosterCharger,
        MK2PowerBoosterCharger,
        MK3PowerBoosterCharger,
        MK4PowerBoosterCharger,
        MK5PowerBoosterCharger,
        MagmaBore,
        ClockCrystal,
        ClockMaker,
        DeOrbitContainerEntity,
        DeOrbitContainerMob,
        DeOrbitController,
        ResearchParts,
        OverclockedClockCrystal,
        CoalInfuser,
        SlimeAttractor,
        T4_ParticleFilter,
        Canister_Freezon,
        Canister_Chlorine,
        Canister_Sulphur,
        FluidPipe_Straight,
        FluidPipe_Bend,
        T4_ParticleCompressor,
        T4_GasStorage,
        T4_GasBottler,
        FreezeMissileCrafterObj,
        PoisonMissileCrafterObj,
        FreezeMissileObj,
        PoisonMissileObj,
        MiniSmelterObj,
        InductionCharger,
        Num,
        XXX,
        XXXX,
        Error
    }
}

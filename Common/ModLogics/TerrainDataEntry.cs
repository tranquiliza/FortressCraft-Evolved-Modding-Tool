using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common.ModLogics
{
    public class TerrainDataEntry
    {
        public ushort CubeType { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Plural { get; set; }
        public string IconName { get; set; }
        public string ObjectName { get; set; }
        public string Description { get; set; }
        public int ResearchValue { get; set; }
        //Hardness is standard value of 400?
        public float Hardness { get; set; }
        public int MaxStack { get; set; }
        public int DecomposeValue { get; set; }
        [XmlArray, XmlArrayItem("ValueEntry")]
        public List<TerrainDataValueEntry> Values { get; set; }
        public ushort DefaultValue { get; set; }
        public SegmentRendererLayerType LayerType { get; set; }
        public int TopTexture { get; set; }
        public int SideTexture { get; set; }
        public int BottomTexture { get; set; }
        public int GUITexture { get; set; }
        [XmlArray, XmlArrayItem("Stage")]
        public List<TerrainDataStageEntry> Stages { get; set; }
        public bool Hidden { get; set; }
        public bool isSolid { get; set; }
        public bool isTransparent { get; set; }
        public bool isHollow { get; set; }
        public bool isGlass { get; set; }
        public bool isPassable { get; set; }
        public bool isColorised { get; set; }
        public bool isPaintable { get; set; }
        public bool hasObject { get; set; }
        public bool hasEntity { get; set; }
        public MaterialCategories Category { get; set; }
        public eBlockWalkAudioType AudioWalkType { get; set; }
        public eBlockBuildAudioType AudioBuildType { get; set; }
        public eBlockDestroyAudioType AudioDestroyType { get; set; }
        [XmlArray, XmlArrayItem("tag")]
        public List<TerrainData.eTags> tags { get; set; }
    }
    public class TerrainDataValueEntry
    {
        public ushort Value { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Plural { get; set; }
        public string IconName { get; set; }
        public string ObjectName { get; set; }
        public string Description { get; set; }
        public int ResearchValue { get; set; }
        public float Hardness { get; set; }

        public string Text()
        {
            return Name + " (" + Key + ")";
        }
    }

    public enum SegmentRendererLayerType
    {
        PrimaryTerrain,
        SecondaryTerrain,
        Transparent,
        Water,
        Lava
    }
    public class TerrainDataStageEntry
    {
        public ushort Id { get; set; }
        public ushort RangeMinimum { get; set; }
        public ushort RangeMaximum { get; set; }
        public int TopTexture { get; set; }
        public int SideTexture { get; set; }
        public int BottomTexture { get; set; }
        public int GUITexture { get; set; }
    }
    public enum MaterialCategories
    {
        Material,
        Ore,
        CraftingIngredient,
        SuitUpgrade,
        ArtherUpgrade,
        Machine,
        MachineUpgrade,
        Consumable,
        Minecarts,
        Manufacturing,
        Rockets,
        Num
    }
    public enum eBlockWalkAudioType
    {
        Dirt,
        Concrete,
        Sand,
        Gravel,
        Wood,
        Metal,
        Num
    }
    public enum eBlockBuildAudioType
    {
        Canvas,
        Metal,
        Stone,
        Wood,
        Dirt,
        NumTypes
    }
    public enum eBlockDestroyAudioType
    {
        None,
        Dirt,
        Stone,
        Metal,
        Wood,
        Canvas,
        Num
    }
    public class TerrainData
    {
        public enum eTags
        {
            eNone,
            DetailBlock,
            Metal,
            Ore,
            Wood,
            SciFi,
            Cute,
            Fantasy,
            Decoration,
            Machinery,
            Lights,
            Effect,
            Urban,
            Outdoor,
            Indoor,
            Medieval,
            Art,
            Retro,
            Character,
            BodyPart,
            Voxel,
            Paintable,
            Animated,
            Transparent,
            Fluid,
            Untagged,
            eNumTags
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FortressCraftEvolved_Modding_Tool.GameLogics
{
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("ArrayOfItemEntry")]
    public class ArrayOfItemEntry
    {
        [XmlArray("ItemEntry")]
        [XmlArrayItem("ItemID", typeof(ItemEntry))]
        public ItemEntry[] ItemEntry { get; set; }
    }
}

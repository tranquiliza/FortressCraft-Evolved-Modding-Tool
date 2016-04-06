using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using FortressCraftEvolved_Modding_Tool.GameLogics;
using FortressCraftEvolved_Modding_Tool.Data;
using System.IO;

namespace FortressCraftEvolved_Modding_Tool.XmlLogic
{
    public class ItemsReader
    {
		public static void ReadItems(string path)
        {
            DataHolder.ItemEntries = XMLSerializer.Deserialize<List<ItemEntry>>(File.ReadAllText(path));
        }
    }
}

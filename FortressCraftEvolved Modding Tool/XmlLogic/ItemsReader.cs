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
    class ItemsReader
    {
        public static void ReadItems()
        {
            ArrayOfItemEntry ItemEntries = null;
            string path = User.Default.ItemsXmlPath;
            XmlSerializer Serializer = new XmlSerializer(typeof(ArrayOfItemEntry));

            StreamReader reader = new StreamReader(path);
            ItemEntries = (ArrayOfItemEntry)Serializer.Deserialize(reader);
            reader.Close();
            //DataHolder.ItemEntries = XMLSerializer.Deserialize<List<ItemEntry>>(File.ReadAllText(User.Default.ItemsXmlPath));
        }
    }
}

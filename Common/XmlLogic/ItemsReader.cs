﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Common.GameLogics;
using Common.Data;
using System.IO;

namespace Common.XmlLogic
{
    public class ItemsReader
    {
        public static void ReadItems(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return;
            }
            if (path.Contains("Items.xml"))
            {
                try
                {
                    DataHolder.ItemEntries = XMLSerializer.Deserialize<List<ItemEntry>>(File.ReadAllText(path));
                }
                catch (Exception x)
                {
                    Error.Log("Error: failed to deserialize Items.xml: " + x);
                }
            }
            else
            {
                return;
            }
        }
    }
}

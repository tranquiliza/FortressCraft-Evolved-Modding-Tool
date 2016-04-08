using System;
using System.Collections.Generic;
using System.Xml;
using Common.Data;
using Common.GameLogics;
using System.IO;

namespace Common.XmlLogic
{
    public class ManufacturerRecipesReader
    {
		public static void ReadManufactoringXML(string ManufactorerXmlPath)
        {
            if (ManufactorerXmlPath == "")
            {
                return;
            }
            DataHolder.ManufacturerEntries = XMLSerializer.Deserialize<List<CraftData>>(File.ReadAllText(ManufactorerXmlPath));
        }
        public static void ReadRefineryRecipes(string RefineryPath)
        {
            if (RefineryPath == "")
            {
                return;
            }
            ModDataHolder.RefineryRecipes = XMLSerializer.Deserialize<List<CraftData>>(File.ReadAllText(RefineryPath));
        }
    }
}

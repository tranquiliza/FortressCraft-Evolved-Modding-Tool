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
            if (string.IsNullOrEmpty(ManufactorerXmlPath) || !File.Exists(ManufactorerXmlPath))
            {
                return;
            }
            if (ManufactorerXmlPath.Contains("ManufacturerRecipes.xml"))
            {
                DataHolder.ManufacturerEntries = XMLSerializer.Deserialize<List<CraftData>>(File.ReadAllText(ManufactorerXmlPath));
            }
            else
            {
                return;
            }
        }
        public static void ReadRefineryRecipes(string RefineryPath)
        {
            if (string.IsNullOrEmpty(RefineryPath) || !File.Exists(RefineryPath))
            {
                return;
            }
            if (RefineryPath.Contains("RefineryRecipes.xml"))
            {
                ModDataHolder.RefineryRecipes = XMLSerializer.Deserialize<List<CraftData>>(File.ReadAllText(RefineryPath));
            }
            else
            {
                return;
            }
        }
    }
}

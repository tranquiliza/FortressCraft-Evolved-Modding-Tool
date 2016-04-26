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
                try
                {
                    DataHolder.ManufacturerEntries = XMLSerializer.Deserialize<List<CraftData>>(File.ReadAllText(ManufactorerXmlPath));
                }
                catch (Exception x)
                {
                    Error.Log("Error: failed to deserialize ManufacturerRecipes.xml : " + x);
                }
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
                try
                {
                    ModDataHolder.RefineryRecipes = XMLSerializer.Deserialize<List<CraftData>>(File.ReadAllText(RefineryPath));
                }
                catch (Exception x)
                {
                    Error.Log("Error: Failed to Deserialize RefineryRecipes.xml " + x);
                }
            }
        }
    }
}

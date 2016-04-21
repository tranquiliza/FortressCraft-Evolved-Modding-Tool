using System;
using System.Collections.Generic;
using System.Xml;
using Common.GameLogics;
using Common.Data;
using System.IO;

namespace Common.XmlLogic
{
    public static class ResearchReader
    {
        public static void ReadResearchXML(string ResearchXmlPath)
        {
            if (string.IsNullOrEmpty(ResearchXmlPath) || !File.Exists(ResearchXmlPath))
            {
                return;
            }
            if (ResearchXmlPath.Contains("Research.xml"))
            {
                DataHolder.ResearchEntries = XMLSerializer.Deserialize<List<ResearchDataEntry>>(File.ReadAllText(ResearchXmlPath));
            }
            else
            {
                return;
            }
        }

    }
}

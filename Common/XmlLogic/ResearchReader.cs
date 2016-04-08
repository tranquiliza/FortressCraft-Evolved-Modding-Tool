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
            if (ResearchXmlPath == "")
            {
                return;
            }
            DataHolder.ResearchEntries = XMLSerializer.Deserialize<List<ResearchDataEntry>>(File.ReadAllText(ResearchXmlPath));
        }

    }
}

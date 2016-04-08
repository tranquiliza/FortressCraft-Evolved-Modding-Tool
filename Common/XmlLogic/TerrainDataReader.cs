using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.GameLogics;
using Common.Data;
using System.IO;

namespace Common.XmlLogic
{
    public class TerrainDataReader
    {
        public static void ReadTerrainDataEntry(string TerrainDataPath)
        {
            if (TerrainDataPath == "")
            {
                return;
            }
            DataHolder.TerrainDataEntries = XMLSerializer.Deserialize<List<TerrainDataEntry>>(File.ReadAllText(TerrainDataPath));
        }
    }
}

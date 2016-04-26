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
            if (string.IsNullOrEmpty(TerrainDataPath) || !File.Exists(TerrainDataPath))
            {
                return;
            }
            if (TerrainDataPath.Contains("TerrainData.xml"))
            {
                try
                {
                    DataHolder.TerrainDataEntries = XMLSerializer.Deserialize<List<TerrainDataEntry>>(File.ReadAllText(TerrainDataPath));
                }
                catch (Exception x)
                {
                    Error.Log("Error: Failed to Deserialze TerrainData.xml" + x);
                }
            }
        }
    }
}

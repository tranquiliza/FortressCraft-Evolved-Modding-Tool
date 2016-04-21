using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using Common.GameLogics;
using System.IO;

namespace Common.XmlLogic
{
    public class GACReader
    {
        public static void ReadGAC(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return;
            }
            try
            {
                GACDataHolder.GAC = XMLSerializer.Deserialize<GenericAutoCrafterDataEntry>(File.ReadAllText(path));
            }
            catch (Exception x)
            {
                File.WriteAllText("Error.txt", "User failed to select a GAC Path" + x);
            }
        }
    }
}

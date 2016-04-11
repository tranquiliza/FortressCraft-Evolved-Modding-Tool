using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ModWriter
{
    [Serializable]
    public class ModConfiguration
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public bool IsLocalMod { get; set; }
        public bool IsServerOnlyMod { get; set; }
    }
}

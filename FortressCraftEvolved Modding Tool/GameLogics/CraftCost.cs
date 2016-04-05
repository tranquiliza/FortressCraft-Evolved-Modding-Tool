using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressCraftEvolved_Modding_Tool.GameLogics
{
    class CraftCost
    {
        public String Name;
        public uint Amount;

        public CraftCost(String Name, uint Amount)
        {
            this.Name = Name;
            this.Amount = Amount;
        }

        public string Text()
        {
            return Name + " x " + Amount;
        }
    }
}

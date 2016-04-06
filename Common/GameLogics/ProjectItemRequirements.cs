using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressCraftEvolved_Modding_Tool.GameLogics
{
    class ProjectItemRequirements
    {
        public String ItemKey;
        public int Amount;
        public ProjectItemRequirements(String ItemKey, int Amount)
        {
            this.ItemKey = ItemKey;
            this.Amount = Amount;
        }
        public String Text()
        {
            return ItemKey + " x " + Amount;
        }
    }
}

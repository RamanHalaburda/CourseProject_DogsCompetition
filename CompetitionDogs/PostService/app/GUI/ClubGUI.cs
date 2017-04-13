using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractDB;

namespace CompetitionDog
{
    class ClubGUI:AbstractEntityGUI
    {
        public ClubGUI() : base() { }

        public override void setHeaderName()
        {
            headName.Add("NAME","Наименование клуба");
        }
    }
}

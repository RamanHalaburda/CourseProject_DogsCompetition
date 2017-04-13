using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractDB;

namespace CompetitionDog
{
    class ExpertGUI:AbstractEntityGUI
    {
        public ExpertGUI() : base() { }

        public override void setHeaderName()
        {
            headName.Add("FIO","Эксперт");
            headName.Add("RING", "Ринг");
            headName.Add("CLUBNAME", "Наименование клуба");
        }
    }
}

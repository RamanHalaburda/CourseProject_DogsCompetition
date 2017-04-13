using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractDB;

namespace CompetitionDog
{
    class HostGUI:AbstractEntityGUI
    {
        public HostGUI() : base() { }

        public override void setHeaderName()
        {
            headName.Add("FIO","Владелец");
            headName.Add("ADDRESS", "Адрес");
            headName.Add("PASSPORT", "Паспорт");
        }
    }
}

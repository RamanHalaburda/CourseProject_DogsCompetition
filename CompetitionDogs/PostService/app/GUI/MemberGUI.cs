using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractDB;

namespace CompetitionDog
{
    class MemberGUI:AbstractEntityGUI
    {
        public MemberGUI() : base() { }

        public override void setHeaderName()
        {
            headName.Add("NAME","Кличка");
            headName.Add("BREED", "Порода");
            headName.Add("AGE", "Возраст");
            headName.Add("RING", "Ринг");
        }
    }
}

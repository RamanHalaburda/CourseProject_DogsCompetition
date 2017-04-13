using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractDB;

namespace CompetitionDog
{
    class DogGUI:AbstractEntityGUI
    {
        public DogGUI() : base() { }

        public override void setHeaderName()
        {
            headName.Add("NAME","Кличка");
            headName.Add("BREED", "Порода");
            headName.Add("AGE", "Возраст");
            headName.Add("CLUBNAME", "Наименование клуба");
            headName.Add("DOCUMENT", "Документ");
            headName.Add("PARENT", "Клички родителей");
            headName.Add("GRAFTING", "Дата прививки");
            headName.Add("HFIO", "Владелец");
        }
    }
}

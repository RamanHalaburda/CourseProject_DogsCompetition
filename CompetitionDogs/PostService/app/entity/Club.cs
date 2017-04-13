using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractDB;

namespace CompetitionDog
{
    class Club:AbstractEntity
    {
        private int club_id;
        private string name;

        public override string queryInsert()
        {
            return string.Format("INSERT INTO Club (name) Values('{0}')", name);
        }

        public override string queryUpdate()
        {
            return string.Format("UPDATE Club SET name='{1}' WHERE club_id = {0}", club_id, name);
        }

        public override string queryDelete()
        {
            return string.Format("DELETE FROM Club WHERE Club_id={0}", club_id);
        }

        public override string querySearch()
        {
            return string.Format("SELECT * FROM viewClub WHERE {0} LIKE '%{1}%'", getFieldName(), getText());
        }

        public override string querySelect()
        {
            return "SELECT * FROM Club";
        }

        public override string queryView()
        {
            return "SELECT * FROM viewClub";
        }

        public string queryViewName()
        {
            return "SELECT club_id,name FROM Club";
        }

        public Club() { }

        public Club(int club_id)
        {
            this.club_id = club_id;
        }

        public Club(int club_id, string name)
        {
            this.name = name;
            this.club_id = club_id;
        }

        public Club(string name)
        {
            this.name = name;
        }

        public Club(string fieldName, string text) : base(fieldName, text) { }
    }
}

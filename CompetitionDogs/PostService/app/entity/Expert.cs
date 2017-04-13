using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractDB;

namespace CompetitionDog
{
    class Expert:AbstractEntity
    {
        private int expert_id;
        private string firstname;
        private string lastname;
        private string middlename; 
        private int ring; 
        private int club_id;

        public override string queryInsert()
        {
            return string.Format("INSERT INTO Expert" +
                   "(firstname,lastname,middlename,ring,club_id)" +
                   "Values('{0}', '{1}', '{2}', {3}, {4})", firstname, lastname, middlename, ring, club_id);
        }

        public override string queryUpdate()
        {
            return string.Format("UPDATE Expert" +
                    " SET firstname='{1}',lastname='{2}',middlename='{3}',ring={4},club_id={5}" +
                    " WHERE expert_id = {0}", expert_id, firstname, lastname, middlename, ring, club_id);
        }

        public override string queryDelete()
        {
            return string.Format("DELETE FROM Expert WHERE expert_id={0}", expert_id);
        }

        public override string querySearch()
        {
            return string.Format("SELECT * FROM viewExpert WHERE {0} LIKE '%{1}%'", getFieldName(), getText());
        }

        public override string querySelect()
        {
            return "SELECT * FROM Expert";
        }

        public override string queryView()
        {
            return "SELECT * FROM viewExpert";
        }

        public Expert() { }

        public Expert(int expert_id)
        {
            this.expert_id = expert_id;
        }

        public Expert(int expert_id, string firstname, string lastname, string middlename, int ring, int club_id)
        {
            this.expert_id = expert_id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.middlename = middlename;
            this.ring = ring;
            this.club_id = club_id;
        }

        public Expert(string firstname, string lastname, string middlename, int ring, int club_id)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.middlename = middlename;
            this.ring = ring;
            this.club_id = club_id;
        }

        public Expert(string fieldName, string text) : base(fieldName, text) { }
    }
}

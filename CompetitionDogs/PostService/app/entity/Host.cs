using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractDB;

namespace CompetitionDog
{
    class Host:AbstractEntity
    {
        private int host_id;
        private string firstname;
        private string lastname;
        private string middlename;
        private string address;
        private string passport;

        public override string queryInsert()
        {
            return string.Format("INSERT INTO Host" +
                   "(firstname,lastname,middlename,address,passport)" +
                   "Values('{0}', '{1}', '{2}', '{3}', '{4}')", firstname, lastname, middlename,address,passport);
        }

        public override string queryUpdate()
        {
            return string.Format("UPDATE Host" +
                    " SET firstname='{1}',lastname='{2}',middlename='{3}',address='{4}',passport='{5}'" +
                    " WHERE host_id = {0}", host_id, firstname, lastname, middlename,address, passport);
        }

        public override string queryDelete()
        {
            return string.Format("DELETE FROM Host WHERE host_id={0}", host_id);
        }

        public override string querySearch()
        {
            return string.Format("SELECT * FROM viewHost WHERE {0} LIKE '%{1}%'", getFieldName(), getText());
        }

        public override string querySelect()
        {
            return "SELECT * FROM Host";
        }

        public override string queryView()
        {
            return "SELECT * FROM viewHost";
        }

        public string queryViewFIO()
        {
            return "Select (firstname||' '||lastname||' '||middlename)as fio,host_id FROM Host";
        }

        public Host() { }

        public Host(int host_id)
        {
            this.host_id = host_id;
        }

        public Host(int host_id, string firstname, string lastname, string middlename, string address, string passport)
        {
            this.host_id = host_id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.middlename = middlename;
            this.address = address;
            this.passport = passport;
        }

        public Host(string firstname, string lastname, string middlename, string address, string passport)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.middlename = middlename;
            this.address = address;
            this.passport = passport;
        }

        public Host(string fieldName, string text) : base(fieldName, text) { }
    }
}

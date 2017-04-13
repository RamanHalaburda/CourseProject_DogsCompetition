using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionDog
{
    class Stats
    {
        private int club_id;
        private string lastname;
        private string breed;

        public string queryOne()
        {
            return string.Format("Select (h.firstname||' '||h.lastname||' '||h.middlename) as hfio,d.name,m.ring FROM Member m,Dog d,Host h" +
                " WHERE m.dog_id = d.dog_id AND d.host_id = h.host_id AND h.lastname = '{0}'", lastname);
        }

        public string queryTwo()
        {
            return string.Format("SELECT d.breed,count(*) as countdog FROM Club c,Dog d" +
                " WHERE c.club_id = d.club_id AND c.club_id ={0} GROUP BY d.breed", club_id);
        }

        public string queryThree()
        {            
            return string.Format("SELECT d.breed,(e.firstname||' '||e.lastname||' '||middlename) as hfio" +
                " FROM Member m, Expert e,Dog d WHERE m.ring=e.ring AND d.dog_id= m.dog_id AND d.breed='{0}'", breed);
        }

        public string queryFour()
        {
            return string.Format("SELECT d.breed,count(*) as countdog " +
                "FROM Member m, Dog d WHERE m.dog_id = d.dog_id GROUP BY d.breed");
        }     

        public void setClub(int club_id)
        {
            this.club_id = club_id;
        }

        public void setLastname(string lastname)
        {
            this.lastname= lastname;
        }

        public void setBreed(string breed)
        {
            this.breed = breed;
        }

        public Stats() { }
    }
}

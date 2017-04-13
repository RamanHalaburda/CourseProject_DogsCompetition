using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractDB;

namespace CompetitionDog
{
    class Member:AbstractEntity
    {
        private int member_id;
        private int dog_id;
        private int ring;

        public override string queryInsert()
        {
            return string.Format("INSERT INTO Member (dog_id,ring) Values({0}, {1})",dog_id,ring);
        }

        public override string queryUpdate()
        {
            return string.Format("UPDATE Member SET dog_id={1},ring={2} WHERE member_id = {0}", member_id, dog_id, ring);
        }

        public override string queryDelete()
        {
            return string.Format("DELETE FROM Member WHERE member_id={0}", member_id);
        }

        public override string querySearch()
        {
            return string.Format("SELECT * FROM viewMember WHERE {0} LIKE '%{1}%'", getFieldName(), getText());
        }

        public override string querySelect()
        {
            return "SELECT * FROM Member";
        }

        public override string queryView()
        {
            return "SELECT * FROM viewMember";
        }

        public Member() { }

        public Member(int member_id)
        {
            this.member_id = member_id;
        }

        public Member(int dog_id, int ring)
        {
            this.dog_id = dog_id;
            this.ring = ring;         
        }

        public Member(int member_id,int dog_id, int ring)
        {
            this.member_id = member_id;
            this.dog_id = dog_id;
            this.ring = ring;
        }

        public Member(string fieldName, string text) : base(fieldName, text) { }
    }
}

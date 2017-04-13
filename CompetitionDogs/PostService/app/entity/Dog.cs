using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractDB;

namespace CompetitionDog
{
    class Dog:AbstractEntity
    {
        private int dog_id;
        private string name;
        private string breed;
        private int age;
        private string document;
        private string parent;
        private string grafting;
        private int club_id;
        private int host_id;

        public override string queryInsert()
        {
            return string.Format("INSERT INTO Dog" +
                   "(name,breed,age,document,parent,grafting,club_id,host_id)" +
                   "Values('{0}', '{1}', {2}, '{3}','{4}',TO_DATE('{5}','MM/DD/YYYY'),{6},{7})", name, breed, age, document, parent, grafting,club_id,host_id); 
        }

        public override string queryUpdate()
        {
            return string.Format("UPDATE Dog" +
                    " SET name='{1}',breed='{2}',age={3},document='{4}',parent='{5}',grafting=TO_DATE('{6}','MM/DD/YYYY'),club_id={7},host_id={8}"+
                    " WHERE dog_id = {0}", dog_id, name, breed, age, document, parent,grafting,club_id,host_id);
        }

        public override string queryDelete()
        {
            return string.Format("DELETE FROM Dog WHERE dog_id={0}", dog_id);
        }

        public override string querySearch()
        {
            return string.Format("SELECT * FROM viewDog WHERE {0} LIKE '%{1}%'", getFieldName(), getText());
        }

        public override string querySelect()
        {
            return "SELECT * FROM Dog"; 
        }

        public override string queryView()
        {
            return "SELECT * FROM viewDog";
        }

        public string queryViewName()
        {
            return "SELECT name,dog_id FROM Dog";
        }

        public Dog() { }

        public Dog(int dog_id)
        {
            this.dog_id = dog_id;
        }

        public Dog(int dog_id, string name, string breed, int age, string document,string parent,string grafting,int club_id,int host_id)
        {
            this.dog_id = dog_id;
            this.name= name;
            this.breed = breed;
            this.age = age;
            this.parent = parent;
            this.document = document;
            this.grafting = grafting;
            this.club_id = club_id;
            this.host_id = host_id;
        }

        public Dog(string name, string breed, int age, string document, string parent, string grafting, int club_id, int host_id)
        {
            this.name = name;
            this.breed = breed;
            this.age = age;
            this.parent = parent;
            this.document = document;
            this.grafting = grafting;
            this.club_id = club_id;
            this.host_id = host_id;
        }

        public Dog(string fieldName, string text) : base(fieldName, text) { }
    }
}

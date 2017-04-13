using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AbstractDB;


namespace CompetitionDog
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        //Обьекты логики
        private AbstractDBConnection connection = null;
        private AbstractDBOperation operation = null;

        //Обьекты данных сущностей и представлений сущностей
        private DataTable clubDt = null;
        private DataTable clubRefDt = null;

        private DataTable dogDt = null;
        private DataTable dogRefDt = null;

        private DataTable expertDt = null;
        private DataTable expertRefDt = null;

        private DataTable hostDt = null;
        private DataTable hostRefDt = null;

        private DataTable memberDt = null;
        private DataTable memberRefDt = null;

        //Обьекты граф. оболочки
        ClubGUI clubGUI = null;
        DogGUI dogGUI = null;
        ExpertGUI expertGUI = null;
        HostGUI hostGUI = null;
        MemberGUI memberGUI = null;

        //Обьекты сущностей и их поля
        private Club club = null;
        private int club_id;
        private string name;

        private Dog dog = null;
        private int dog_id;
        private string breed;
        private int age;
        private string document;
        private string parent;
        private string grafting;

        private Expert expert = null;
        private int expert_id;
        private string firstname;
        private string lastname;
        private string middlename;

        private Host host = null;
        private int host_id;
        private string address;
        private string passport;

        private Member member= null;
        private int member_id;
        private int ring;

        private int pressTable = 0;
        private int selectRow = 0;
        private string text, fieldName;
        private Dictionary<string, string> headName = null;

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                Application.ExitThread();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                connection = new OracleDBConnection("SYSTEM","root","competition","localhost");
                operation = new OracleDBOperation(connection);
                club = new Club();
                dog = new Dog();
                expert= new Expert();
                host = new Host();
                member= new Member();

                clubGUI = new ClubGUI();
                memberGUI = new MemberGUI();
                dogGUI = new DogGUI();
                expertGUI = new ExpertGUI();
                hostGUI = new HostGUI();

                viewClub();
                fieldClubList.DataSource = clubGUI.getHeaderNameList();

                clubTables.ReadOnly = true;
                expertTables.ReadOnly = true;
                dogTables.ReadOnly = true;
                hostTables.ReadOnly = true;
                memberTables.ReadOnly = true;
            }
            catch (CompetitionDogException ex)
            {
                MessageBox.Show(ex.getMessage(), "Ошибка");
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                pressTable = tabPage6.SelectedIndex;
                switch (pressTable)
                {
                    case 0:
                        {
                            setFieldClub();
                            operation.insert(new Club(name));
                            viewClub();
                            break;
                        }
                    case 1:
                        {
                            setFieldExpert();
                            operation.insert(new Expert(firstname, lastname, middlename, ring, club_id));
                            viewExpert();
                            break;
                        }
                    case 2:
                        {
                            setFieldDog();
                            operation.insert(new Dog(name, breed, age, document, parent, grafting, club_id, host_id));
                            viewDog();
                            break;
                        }
                    case 3:
                        {
                            setFieldHost();
                            operation.insert(new Host(firstname, lastname, middlename, address, passport));
                            viewHost();
                            break;
                        }
                    case 4:
                        {
                            setFieldMember();
                            operation.insert(new Member(dog_id, ring));
                            viewMember();
                            break;
                        }
                }
                MessageBox.Show("Запись была успешно давлена!");
            }
            catch (CompetitionDogException ex)
            {
                MessageBox.Show(ex.getMessage());
            }
            catch (FormatException)
            {
                MessageBox.Show("Передаваемые данные не соответсвуют типу!", "Ошибка");
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                pressTable = tabPage6.SelectedIndex;
                switch (pressTable)
                {
                    case 0:
                        {
                            setFieldClub();
                            club_id = Convert.ToInt32(clubRefDt.Rows[selectRow]["CLUB_ID"].ToString());
                            operation.update(new Club(club_id, name));
                            viewClub();
                            break;
                        }
                    case 1:
                        {
                            setFieldExpert();
                            expert_id = Convert.ToInt32(expertRefDt.Rows[selectRow]["EXPERT_ID"].ToString());
                            operation.update(new Expert(expert_id, firstname, lastname, middlename, ring, club_id));
                            viewExpert();
                            break;
                        }
                    case 2:
                        {
                            setFieldDog();
                            dog_id = Convert.ToInt32(dogRefDt.Rows[selectRow]["DOG_ID"].ToString());
                            operation.update(new Dog(dog_id, name, breed, age, document, parent, grafting, club_id, host_id));
                            viewDog();
                            break;
                        }
                    case 3:
                        {
                            setFieldHost();
                            host_id = Convert.ToInt32(hostRefDt.Rows[selectRow]["HOST_ID"].ToString());
                            operation.update(new Host(host_id, firstname, lastname, middlename, address, passport));
                            viewHost();
                            break;
                        }
                    case 4:
                        {
                            setFieldMember();
                            member_id = Convert.ToInt32(memberRefDt.Rows[selectRow]["MEMBER_ID"].ToString());
                            operation.update(new Member(member_id, dog_id, ring));
                            viewMember();
                            break;
                        }
                }
                MessageBox.Show("Запись была успешно изменена!");
            }
            catch (CompetitionDogException ex)
            {
                MessageBox.Show(ex.getMessage(), "Ошибка");
            }
            catch (FormatException)
            {
                MessageBox.Show("Передаваемые данные не соответсвуют типу!", "Ошибка");
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                pressTable = tabPage6.SelectedIndex;
                switch (pressTable)
                {
                    case 0:
                        {
                            club_id = Convert.ToInt32(clubRefDt.Rows[selectRow]["club_id"].ToString());
                            operation.delete(new Club(club_id));
                            viewClub();
                            break;
                        }
                    case 1:
                        {
                            expert_id = Convert.ToInt32(expertRefDt.Rows[selectRow]["expert_id"].ToString());
                            operation.delete(new Expert(expert_id));
                            viewExpert();
                            break;
                        }
                    case 2:
                        {
                            dog_id = Convert.ToInt32(dogRefDt.Rows[selectRow]["dog_id"].ToString());
                            operation.delete(new Dog(dog_id));
                            viewDog();
                            break;
                        }
                    case 3:
                        {
                            host_id = Convert.ToInt32(hostRefDt.Rows[selectRow]["host_id"].ToString());
                            operation.delete(new Host(host_id));
                            viewHost();
                            break;
                        }
                    case 4:
                        {
                            member_id = Convert.ToInt32(memberRefDt.Rows[selectRow]["member_id"].ToString());
                            operation.delete(new Member(member_id));
                            viewMember();
                            break;
                        }
                }
                MessageBox.Show("Запись была успешно удалена!");
            }
            catch (CompetitionDogException ex)
            {
                MessageBox.Show(ex.getMessage(), "Ошибка");
            }
            catch (FormatException)
            {
                MessageBox.Show("Передаваемые данные не соответсвуют типу!", "Ошибка");
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                pressTable = tabPage6.SelectedIndex;
                switch (pressTable)
                {
                    case 0:
                        {
                            text = textClubTxt.Text;
                            fieldName = getSelectedField(fieldClubList.SelectedValue.ToString());
                            clubTables.DataSource = operation.search(new Club(fieldName, text));
                            break;
                        }
                    case 1:
                        {
                            text = textExpertTxt.Text;
                            fieldName = getSelectedField(fieldExpertList.SelectedValue.ToString());
                            expertTables.DataSource = operation.search(new Expert(fieldName, text));
                            break;
                        }
                    case 2:
                        {
                            text = textDogTxt.Text;
                            fieldName = getSelectedField(fieldDogList.SelectedValue.ToString());
                            dogTables.DataSource = operation.search(new Dog(fieldName, text));
                            break;
                        }
                    case 3:
                        {
                            text = textHostTxt.Text;
                            fieldName = getSelectedField(fieldHostList.SelectedValue.ToString());
                            hostTables.DataSource = operation.search(new Host(fieldName, text));
                            break;
                        }
                    case 4:
                        {
                            text = textMemberTxt.Text;
                            fieldName = getSelectedField(fieldMemberList.SelectedValue.ToString());
                            memberTables.DataSource = operation.search(new Member(fieldName, text));
                            break;
                        }
                }
            }
            catch (CompetitionDogException ex)
            {
                MessageBox.Show(ex.getMessage());
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pressTable = tabPage6.SelectedIndex;
                addButton.Enabled = true;
                reportButton.Enabled = true;
                updateButton.Enabled = true;
                deleteButton.Enabled = true;
                searchButton.Enabled = true;

                switch (pressTable)
                {
                    case 0:
                        {
                            viewClub();
                            fieldClubList.DataSource = clubGUI.getHeaderNameList();
                            break;
                        }
                    case 1:
                        {
                            viewExpert();
                            fieldExpertList.DataSource = expertGUI.getHeaderNameList();
                            expertClubList.DataSource = operation.select(club.queryViewName());
                            expertClubList.DisplayMember = "NAME";
                            expertClubList.ValueMember = "CLUB_ID";
                            break;
                        }
                    case 2:
                        {
                            viewDog();
                            fieldDogList.DataSource = dogGUI.getHeaderNameList();
                            
                            dogHostList.DataSource = operation.select(host.queryViewFIO());
                            dogHostList.DisplayMember = "FIO";
                            dogHostList.ValueMember = "HOST_ID";

                            dogClubList.DataSource = operation.select(club.queryViewName());
                            dogClubList.DisplayMember = "NAME";
                            dogClubList.ValueMember = "CLUB_ID";

                            break;
                        }
                    case 3:
                        {
                            viewHost();
                            fieldHostList.DataSource = hostGUI.getHeaderNameList();
                            break;
                        }
                    case 4:
                        {
                            viewMember();
                            fieldMemberList.DataSource = memberGUI.getHeaderNameList();
                            dogList.DataSource = operation.select(dog.queryViewName());
                            dogList.DisplayMember = "NAME";
                            dogList.ValueMember = "DOG_ID";
                            break;
                        }
                    case 5:
                        {
                            statsClubList.DataSource = operation.select(club.queryViewName());
                            statsClubList.DisplayMember = "NAME";
                            statsClubList.ValueMember = "CLUB_ID";
                            addButton.Enabled = false;
                            reportButton.Enabled = false;
                            updateButton.Enabled = false;
                            deleteButton.Enabled = false;
                            searchButton.Enabled = false;
                            break;
                        }
                }
            }
            catch (CompetitionDogException ex)
            {
                MessageBox.Show(ex.getMessage(), "Ошибка");
            }
        }

        private void clubTables_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                selectRow = clubTables.CurrentRow.Index;
                clubNameTxt.Text = clubRefDt.Rows[selectRow]["NAME"].ToString();
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Передаваемые данные отсутствуют!", "Ошибка");
            }
        }

        private void expertTables_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                selectRow = expertTables.CurrentRow.Index;
                expertFirstnameTxt.Text = expertRefDt.Rows[selectRow]["FIRSTNAME"].ToString();
                expertLastnameTxt.Text= expertRefDt.Rows[selectRow]["LASTNAME"].ToString();
                expertMiddlenameTxt.Text= expertRefDt.Rows[selectRow]["MIDDLENAME"].ToString();
                expertRingTxt.Text= expertRefDt.Rows[selectRow]["RING"].ToString();
                expertClubList.SelectedValue = Convert.ToInt32(expertRefDt.Rows[selectRow]["CLUB_ID"].ToString());
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Передаваемые данные отсутствуют!", "Ошибка");
            }
        }

        private void dogTables_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                selectRow = dogTables.CurrentRow.Index;
                dogNameTxt.Text= dogRefDt.Rows[selectRow]["NAME"].ToString();
                dogBreedTxt.Text= dogRefDt.Rows[selectRow]["BREED"].ToString(); 
                dogAgeTxt.Text= dogRefDt.Rows[selectRow]["AGE"].ToString(); 
                dogDocumentTxt.Text= dogRefDt.Rows[selectRow]["DOCUMENT"].ToString(); 
                dogParentTxt.Text= dogRefDt.Rows[selectRow]["PARENT"].ToString(); ;
                dogGraftingDate.Value = Convert.ToDateTime(dogRefDt.Rows[selectRow]["GRAFTING"].ToString());
                dogClubList.SelectedValue = Convert.ToInt32(dogRefDt.Rows[selectRow]["CLUB_ID"].ToString());
                dogHostList.SelectedValue = Convert.ToInt32(dogRefDt.Rows[selectRow]["HOST_ID"].ToString());
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Передаваемые данные отсутствуют!", "Ошибка");
            }
        }

        private void hostTables_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                selectRow = hostTables.CurrentRow.Index;
                hostFirstnameTxt.Text = hostRefDt.Rows[selectRow]["FIRSTNAME"].ToString();
                hostLastnameTxt.Text = hostRefDt.Rows[selectRow]["LASTNAME"].ToString();
                hostMiddlenameTxt.Text = hostRefDt.Rows[selectRow]["MIDDLENAME"].ToString();
                hostAddressTxt.Text = hostRefDt.Rows[selectRow]["ADDRESS"].ToString();
                hostPassportTxt.Text = hostRefDt.Rows[selectRow]["PASSPORT"].ToString();
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Передаваемые данные отсутствуют!", "Ошибка");
            }
        }

        private void memberTables_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                selectRow = memberTables.CurrentRow.Index;
                memberRingTxt.Text= memberRefDt.Rows[selectRow]["RING"].ToString();
                dogList.SelectedValue = Convert.ToInt32(memberRefDt.Rows[selectRow]["DOG_ID"].ToString());
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Передаваемые данные отсутствуют!", "Ошибка");
            }
        }

        void viewDog()
        {
            dogDt = operation.select(dog.queryView());
            dogRefDt = operation.select(dog);
            dogTables.DataSource = dogDt;
            dogTables.AutoResizeColumns();
            setHeaderNameDog();
        }

        void viewExpert()
        {
            expertDt = operation.select(expert.queryView());
            expertRefDt = operation.select(expert);
            expertTables.DataSource = expertDt;
            expertTables.AutoResizeColumns();
            setHeaderNameExpert();
        }

        void viewHost()
        {
            hostRefDt = operation.select(host.querySelect());
            hostDt = operation.select(host.queryView());
            hostTables.DataSource = hostDt;
            hostTables.AutoResizeColumns();
            setHeaderNameHost();
        }

        void viewMember()
        {
            memberDt = operation.select(member.queryView());
            memberRefDt = operation.select(member);
            memberTables.DataSource = memberDt;
            memberTables.AutoResizeColumns();
            setHeaderNameMember();
        }

        void viewClub()
        {
            clubDt = operation.select(club.queryView());
            clubRefDt = operation.select(club);
            clubTables.DataSource = clubDt;
            clubTables.AutoResizeColumns();
            setHeaderNameClub();
        }

        void setFieldDog()
        {
            name = dogNameTxt.Text;
            breed = dogBreedTxt.Text;
            age = Convert.ToInt32(dogAgeTxt.Text);
            document = dogDocumentTxt.Text;
            parent = dogParentTxt.Text;
            DateTime graftingDate = dogGraftingDate.Value;
            grafting = graftingDate.ToString("MM/dd/yyyy");
            club_id= Convert.ToInt32(dogClubList.SelectedValue);
            host_id= Convert.ToInt32(dogHostList.SelectedValue);
        }

        void setFieldExpert()
        {
            firstname = expertFirstnameTxt.Text;
            lastname = expertLastnameTxt.Text;
            middlename = expertMiddlenameTxt.Text;
            ring = Convert.ToInt32(expertRingTxt.Text);
            club_id = Convert.ToInt32(expertClubList.SelectedValue);
        }

        void setFieldHost()
        {
            firstname = hostFirstnameTxt.Text;
            lastname = hostLastnameTxt.Text;
            middlename = hostMiddlenameTxt.Text;
            address = hostAddressTxt.Text;
            passport = hostPassportTxt.Text;
        }

        void setFieldClub()
        {
            name = clubNameTxt.Text;
        }

        void setFieldMember()
        {
            dog_id = Convert.ToInt32(dogList.SelectedValue);
            ring = Convert.ToInt32(memberRingTxt.Text);
        }

        void setHeaderNameDog()
        {
            headName = dogGUI.getHeaderName();
            foreach (KeyValuePair<string, string> pair in headName)
            {
                for (int i = 0; i < dogTables.ColumnCount; i++)
                {
                    if (pair.Key == dogTables.Columns[i].HeaderText)
                    {
                        dogTables.Columns[i].HeaderText = pair.Value;
                    }
                }
            }

        }

        void setHeaderNameExpert()
        {
            headName = expertGUI.getHeaderName();
            foreach (KeyValuePair<string, string> pair in headName)
            {
                for (int i = 0; i < expertTables.ColumnCount; i++)
                {
                    if (pair.Key == expertTables.Columns[i].HeaderText)
                    {
                        expertTables.Columns[i].HeaderText = pair.Value;
                    }
                }
            }
        }

        void setHeaderNameHost()
        {
            headName = hostGUI.getHeaderName();
            foreach (KeyValuePair<string, string> pair in headName)
            {
                for (int i = 0; i < hostTables.ColumnCount; i++)
                {
                    if (pair.Key == hostTables.Columns[i].HeaderText)
                    {
                        hostTables.Columns[i].HeaderText = pair.Value;
                    }
                }
            }
        }

        void setHeaderNameMember()
        {
            headName = memberGUI.getHeaderName();
            foreach (KeyValuePair<string, string> pair in headName)
            {
                for (int i = 0; i < memberTables.ColumnCount; i++)
                {
                    if (pair.Key == memberTables.Columns[i].HeaderText)
                    {
                        memberTables.Columns[i].HeaderText = pair.Value;
                    }
                }
            }
        }

        void setHeaderNameClub()
        {
            headName = clubGUI.getHeaderName();
            foreach (KeyValuePair<string, string> pair in headName)
            {
                for (int i = 0; i < clubTables.ColumnCount; i++)
                {
                    if (pair.Key == clubTables.Columns[i].HeaderText)
                    {
                        clubTables.Columns[i].HeaderText = pair.Value;
                    }
                }
            }
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            string fileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog1.FileName;
                DataTable dt = null;
                switch (pressTable)
                {
                    case 0:
                        {
                            dt = clubDt;
                            break;
                        }
                    case 1:
                        {
                            dt = expertDt;
                            break;
                        }
                    case 2:
                        {
                            dt = dogDt;
                            break;
                        }
                    case 3:
                        {
                            dt = hostDt;
                            break;
                        }
                    case 4:
                        {
                            dt = memberDt;
                            break;
                        }
                }
                new ReportExcel(dt, fileName, headName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                Stats stats = new Stats();
                if (checkBox1.Checked == true)
                {
                    stats.setLastname(statsLastnameTxt.Text);
                    statsTables.DataSource = operation.select(stats.queryOne());
                }
                if (checkBox2.Checked == true)
                {
                    stats.setClub(Convert.ToInt32(statsClubList.SelectedValue));
                    statsTables.DataSource = operation.select(stats.queryTwo());                    

                }
                if (checkBox3.Checked == true)
                {
                    stats.setBreed(statsBreedTxt.Text);
                    statsTables.DataSource = operation.select(stats.queryThree());
                }
                if (checkBox4.Checked == true)
                {
                    statsTables.DataSource = operation.select(stats.queryFour());
                }
                statsTables.RowHeadersVisible = false;
                statsTables.ColumnHeadersVisible = false;
            }
            catch (CompetitionDogException ex)
            {
                MessageBox.Show(ex.getMessage(), "Ошибка");
            }
        }

        public string getSelectedField(string fieldValue)
        {
            foreach (KeyValuePair<string, string> p in headName)
            {
                if (p.Value == fieldValue)
                {
                    return p.Key;
                }
            }
            return "NULL";
        }
    }
}


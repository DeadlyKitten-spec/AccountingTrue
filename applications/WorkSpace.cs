using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace applications
{



    public partial class WorkSpace : Form
    {
        Color colorButton;
        Color FlatColor;
        public static bool f = false;
        public static int idxx = -1;
        public static Color BackColor;
        public static bool click = false;

        public bool g = false;
        public static bool ooo = true;
        public static string[] nameCargo = new string[100];
        public static string[] cargoCount = new string[100];
        public string backLine = "пусто";
        public string idCupter = "";
        public bool idCupterBool = false;

        public WorkSpace()
        {
            //this.Width = 1940;
            //this.Height = 1040;
            this.WindowState = FormWindowState.Maximized;

            InitializeComponent();

            LoadDB();
            FillCombo2();
            FillCombo4();
            FlatColor = button1.FlatAppearance.BorderColor;

            button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = FlatColor;

            button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button6.FlatAppearance.BorderColor = FlatColor;

            button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button7.FlatAppearance.BorderColor = FlatColor;

            button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button8.FlatAppearance.BorderColor = FlatColor;

            button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button9.FlatAppearance.BorderColor = FlatColor;

            button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button10.FlatAppearance.BorderColor = FlatColor;

            button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button3.FlatAppearance.BorderColor = FlatColor;

            button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button4.FlatAppearance.BorderColor = FlatColor;

            button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button2.FlatAppearance.BorderColor = FlatColor;
            button2.BringToFront();

            button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button11.FlatAppearance.BorderColor = FlatColor;

            button15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button15.FlatAppearance.BorderColor = FlatColor;

            button16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button16.FlatAppearance.BorderColor = FlatColor;
            button16.Hide();

            button17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button17.FlatAppearance.BorderColor = FlatColor;

            button18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button18.FlatAppearance.BorderColor = FlatColor;

            button20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button20.FlatAppearance.BorderColor = FlatColor;

            button21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button21.FlatAppearance.BorderColor = FlatColor;
            button21.BringToFront();

            button22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button22.FlatAppearance.BorderColor = FlatColor;

            button23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button23.FlatAppearance.BorderColor = FlatColor;
            button23.BringToFront();

            button24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button24.FlatAppearance.BorderColor = FlatColor;

            button25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button25.FlatAppearance.BorderColor = FlatColor;

            button26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button26.FlatAppearance.BorderColor = FlatColor;

            button27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button27.FlatAppearance.BorderColor = FlatColor;

            button28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button28.FlatAppearance.BorderColor = FlatColor;

            button29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button29.FlatAppearance.BorderColor = FlatColor;

            //dgv.AutoGenerateColumns = false;

            if (f == true)
            {
                this.Hide();
            }
            textBox1.Hide();
            label7.Hide();
        }

        Point lastPoint;

        private void closeButton_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            //g = false;
            WriteRequest writeRequest = new WriteRequest();
            string Query = "SELECT * FROM `request` ORDER BY `id` ASC";
            DB db = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            string objId = "";
            try
            {
                db.openConnection();
                object obj = cmdDataBase.ExecuteScalar();
                if (obj == null)
                {
                    objId = "0";
                }
                else
                {
                    myReader = cmdDataBase.ExecuteReader();

                    while (myReader.Read())
                    {
                        objId = myReader.GetString("id");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            int a = int.Parse(objId);
            a++;
            objId = Convert.ToString(a);
            writeRequest.Show();
            writeRequest.label1.Text = "Регистрация заявки № " + objId;
   //         writeRequest.textBox5.Text = "Б/Н";
            writeRequest.idRequest = objId;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            colorButton = button1.BackColor;
            button1.BackColor = Color.Khaki;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = colorButton;
        }


        void LoadDB()
        {
            /*string Line = "SELECT * FROM `request`";
            DB db = new DB();
            MySqlCommand command = new MySqlCommand(Line, db.getConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable tbl = new DataTable();
            adapter.Fill(tbl);
            dgv.DataSource = tbl;*/


        }

        void UpdateDB()
        {
            DB db = new DB();
            MySqlCommand com = new MySqlCommand("UPDATE request SET id=@id, docDate=@docDate, docDateWrite=@docDateWrite, deal=@deal, ourFirms=@ourFirms, buyer=@buyer, sender=@sender, recipient=@recipient, object=@object", db.getConnection());
            int i = 0;
            for (; i < dgv.ColumnCount; i++)
            {

                com.Parameters.AddWithValue("@id", dgv.Rows[i].Cells[0].Value);
                com.Parameters.AddWithValue("@docDate", dgv.Rows[i].Cells[1].Value);
                com.Parameters.AddWithValue("@docDateWrite", dgv.Rows[i].Cells[2].Value);
                com.Parameters.AddWithValue("@deal", dgv.Rows[i].Cells[3].Value);
                com.Parameters.AddWithValue("@ourFirms", dgv.Rows[i].Cells[4].Value);
                com.Parameters.AddWithValue("@buyer", dgv.Rows[i].Cells[5].Value);
                com.Parameters.AddWithValue("@sender", dgv.Rows[i].Cells[6].Value);
                com.Parameters.AddWithValue("@recipient", dgv.Rows[i].Cells[7].Value);
                com.Parameters.AddWithValue("@object", dgv.Rows[i].Cells[8].Value);
                db.openConnection();
                com.ExecuteNonQuery();
                db.closeConnection();
                com.Parameters.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateDB();
            LoadDB();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string time = dateTimePicker1.Value.ToString();
            string[] asd = time.Split(' ');
            string[] zxc = asd[0].Split('.');
            string answer = zxc[2] + '-' + zxc[1] + '-' + zxc[0];
            string Line = "SELECT * FROM `request` WHERE `docDate` = '" + answer + "';";
            DB db = new DB();
            MySqlCommand command = new MySqlCommand(Line, db.getConnection());
            DataTable tbl = new DataTable();
            dgv.DataSource = tbl;
            MySqlDataReader myReader;
            try
            {
                db.openConnection();
                myReader = command.ExecuteReader();
                int i = 0;
                while (myReader.Read())
                {
                    string id = myReader.GetString("id");
                    string docDate = myReader.GetString("docDate");
                    string docDateWrite = myReader.GetString("docDateWrite");
                    string deal = myReader.GetString("deal");
                    string ourFirms = myReader.GetString("ourFirms");
                    string buyer = myReader.GetString("buyer");
                    string sendeer = myReader.GetString("sender");
                    string recipient = myReader.GetString("recipient");
                    string objectt = myReader.GetString("object");
                    dgv.Rows[i].Cells[0].Value = id;
                    dgv.Rows[i].Cells[1].Value = docDate;
                    dgv.Rows[i].Cells[2].Value = docDateWrite;
                    dgv.Rows[i].Cells[3].Value = deal;
                    dgv.Rows[i].Cells[4].Value = ourFirms;
                    dgv.Rows[i].Cells[5].Value = buyer;
                    dgv.Rows[i].Cells[6].Value = sender;
                    dgv.Rows[i].Cells[7].Value = recipient;
                    dgv.Rows[i].Cells[8].Value = objectt;
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }

        void FillCombo2()
        {
            string Query = "SELECT * FROM `counterparty` WHERE `status` != 'Грузоотправитель' ORDER BY `name` ASC";
            DB db = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            string[] names = new string[1000];
            try
            {
                db.openConnection();
                myReader = cmdDataBase.ExecuteReader();

                int j = 0;
                while (myReader.Read())
                {
                    string objName = myReader.GetString("name");
                    bool f = true;
                    for (int i = 0; i < names.Length; i++)
                    {
                        if (names[i] != null)
                        {
                            if (names[i].Equals(objName))
                            {
                                f = false;
                                break;
                            }
                        }
                    }
                    if (f == true)
                    {
                        names[j] = objName;
                        j++;
                        comboBox2.Items.Add(objName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }

        void FillCombo4()
        {
            string Query = "SELECT * FROM `drivers` ORDER BY `contractor` ASC";
            //string Query = "(SELECT * FROM `drivers` ORDER BY `name` ASC) ORDER BY `count` desc;";
            DB db = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            string[] names = new string[100];
            try
            {
                db.openConnection();
                myReader = cmdDataBase.ExecuteReader();

                int j = 0;
                while (myReader.Read())
                {
                    string objName = myReader.GetString("contractor");
                    bool f = true;
                    for (int i = 0; i < names.Length; i++)
                    {
                        if (names[i] != null)
                        {
                            if (names[i].Equals(objName))
                            {
                                f = false;
                                break;
                            }
                        }
                    }
                    if (f == true)
                    {
                        names[j] = objName;
                        j++;
                        comboBox4.Items.Add(objName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string Line = "SELECT * FROM `request` WHERE `docDate` = '" + comboBox1.Text.ToString() + "';";
            DB db = new DB();
            MySqlCommand command = new MySqlCommand(Line, db.getConnection());
            DataTable tbl = new DataTable();

            dgv.DataSource = tbl;
            MySqlDataReader myReader;
            try
            {
                db.openConnection();
                myReader = command.ExecuteReader();
                int i = 0;
                while (myReader.Read())
                {
                    string id = myReader.GetString("id");
                    string docDate = myReader.GetString("docDate");
                    string docDateWrite = myReader.GetString("docDateWrite");
                    string deal = myReader.GetString("deal");
                    string ourFirms = myReader.GetString("ourFirms");
                    string buyer = myReader.GetString("buyer");
                    string sendeer = myReader.GetString("sender");
                    string recipient = myReader.GetString("recipient");
                    string objectt = myReader.GetString("object");
                    dgv.Rows[i].Cells[0].Value = id;
                    dgv.Rows[i].Cells[1].Value = docDate;
                    dgv.Rows[i].Cells[2].Value = docDateWrite;
                    dgv.Rows[i].Cells[3].Value = deal;
                    dgv.Rows[i].Cells[4].Value = ourFirms;
                    dgv.Rows[i].Cells[5].Value = buyer;
                    dgv.Rows[i].Cells[6].Value = sender;
                    dgv.Rows[i].Cells[7].Value = recipient;
                    dgv.Rows[i].Cells[8].Value = objectt;
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }

        private void button6_MouseEnter(object sender, EventArgs e)
        {
            colorButton = button1.BackColor;
            button6.BackColor = Color.Khaki;
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.BackColor = colorButton;
        }

        private void button7_MouseEnter(object sender, EventArgs e)
        {
            colorButton = button1.BackColor;
            button7.BackColor = Color.Khaki;
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            button7.BackColor = colorButton;
        }

        private void button8_MouseEnter(object sender, EventArgs e)
        {
            colorButton = button1.BackColor;
            button8.BackColor = Color.Khaki;
        }

        private void button8_MouseLeave(object sender, EventArgs e)
        {
            button8.BackColor = colorButton;
        }

        private void button9_MouseEnter(object sender, EventArgs e)
        {
            colorButton = button1.BackColor;
            button9.BackColor = Color.Khaki;
        }

        private void button9_MouseLeave(object sender, EventArgs e)
        {
            button9.BackColor = colorButton;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Cars cars = new Cars();
            cars.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Drivers drivers = new Drivers();
            drivers.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            CounterParty party = new CounterParty();
            party.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Nomenclature nom = new Nomenclature();
            nom.Show();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*string idStiring = dgv.CurrentCell.Value.ToString();
            int id = Int32.Parse(idStiring);
            MessageBox.Show(idStiring);*/
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ObjectCounterparty objectt = new ObjectCounterparty();
            objectt.Show();
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            idCupterBool = false;
            button16.Hide();
            textBox1.Hide();
            label7.Hide();
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            string time = dateTimePicker1.Value.ToString();
            string[] asd = time.Split(' ');
            string[] zxc = asd[0].Split('.');
            string answer1 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];

            time = dateTimePicker2.Value.ToString();
            asd = null;
            asd = time.Split(' ');
            zxc = null;
            zxc = asd[0].Split('.');
            string answer2 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];

            //string Line = "SELECT * FROM `request` WHERE `docDate` = '" + answer + "';";
            bool fstatus = false;
            bool fbuyer = false;
            bool fobject = false;

            string Line = "";// = "SELECT * FROM `request` WHERE `status` = '" + comboBox1.Text + "';";
            if (!comboBox1.Text.Equals(""))
            {
                fstatus = true;
            }
            if (!comboBox2.Text.Equals(""))
            {
                fbuyer = true;
            }
            if (!comboBox3.Text.Equals(""))
            {
                fobject = true;
            }

            if (fstatus && fobject)
            {
                Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `status` = '" + comboBox1.Text + "' AND `object` = '" + comboBox3.Text + "' ORDER BY `id` ASC;";
            }
            else
            {
                if (fstatus && fbuyer)
                {
                    Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `status` = '" + comboBox1.Text + "' AND `buyer` = '" + comboBox2.Text + "' ORDER BY `id` ASC;";
                }
                else
                {
                    if (fstatus)
                    {
                        Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `status` = '" + comboBox1.Text + "' ORDER BY `id` ASC;";
                    }
                    else
                    {
                        if (fbuyer)
                        {
                            Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `buyer` = '" + comboBox2.Text + "' ORDER BY `id` ASC;";
                        }
                        else
                        {
                            if (fobject)
                            {
                                Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `object` = '" + comboBox3.Text + "' ORDER BY `id` ASC;";
                            }
                        }
                    }
                }
            }
            if (!fstatus && !fbuyer && !fobject)
            {
                Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' ORDER BY `id` ASC;";
            }
            //Line = "SELECT * FROM `request` WHERE `docDate` = '" + answer1 + "';";

            backLine = Line;



            DB db = new DB();
            MySqlCommand command = new MySqlCommand(Line, db.getConnection());
            db.openConnection();
            object obj = command.ExecuteScalar();
            /*DataTable tbl = new DataTable();
            DataSet ds = new DataSet();
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(ds);
            dgv.DataSource = ds.Tables[0];*/
            int idlast = 0;
            int idmorelast = 0;

            if (obj != null)
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlDataReader myReader;
                Work[] works = new Work[100000];
                for (int i = 0; i < works.Length; i++)
                {
                    works[i] = new Work();
                    works[i].hour = 23;
                    works[i].minute = 59;
                    works[i].traffic = "пусто";
                }
                int itter = 0;
                bool f = false;
                //string deal = "";
                try
                {
                    //db.openConnection();
                    myReader = command.ExecuteReader();

                    while (myReader.Read())
                    {
                        //deal = myReader.GetString("deal");
                        idlast = int.Parse(myReader.GetString("id"));
                        if (idlast == idmorelast)
                        {
                            idmorelast = idlast;
                            continue;
                        }
                        for (int i = 0; i < itter; i++)
                        {
                            f = false;
                            if (myReader.GetString("buyer").Equals(works[i].buyer) && myReader.GetString("sender").Equals(works[i].sender) && myReader.GetString("object").Equals(works[i].objectBuyer))
                            {
                                if (myReader.GetString("status").Equals("В работе"))
                                {
                                    works[i].inWork++;
                                }
                                if (myReader.GetString("status").Equals("Назначена"))
                                {
                                    works[i].point++;
                                }
                                if (myReader.GetString("status").Equals("Исполнена") || myReader.GetString("status").Equals("Оплачена"))
                                {
                                    works[i].done++;
                                }
                                f = true;
                                idmorelast = idlast;
                                works[i].deal = myReader.GetString("deal");
                                string timeAccept = myReader.GetString("timeAccept");
                                string[] timeAcceptt = timeAccept.Split(':');
                                if (myReader.GetString("status").Equals("В работе"))
                                {
                                    //MessageBox.Show(timeAccept);
                                    if (works[i].hour > int.Parse(timeAcceptt[0]))
                                    {
                                        //MessageBox.Show(works[itter].hour.ToString());
                                        //MessageBox.Show(timeAcceptt[0]);
                                        //MessageBox.Show(timeAccept);
                                        works[i].hour = int.Parse(timeAcceptt[0]);
                                        works[i].minute = int.Parse(timeAcceptt[1]);
                                    }
                                    else
                                    {
                                        if (works[i].minute > int.Parse(timeAcceptt[1]))
                                        {
                                            works[i].hour = int.Parse(timeAcceptt[0]);
                                            works[i].minute = int.Parse(timeAcceptt[1]);
                                        }
                                    }
                                }
                                if (works[i].traffic.Equals("пусто"))
                                {
                                    if (!myReader.GetString("traffic").Equals("пусто"))
                                    {
                                        works[i].traffic = myReader.GetString("traffic");
                                    }
                                }
                                break;
                            }
                            idmorelast = idlast;
                        }
                        if (!f)
                        {
                            works[itter].deal = myReader.GetString("deal");
                            works[itter].buyer = myReader.GetString("buyer");
                            works[itter].sender = myReader.GetString("sender");
                            works[itter].objectBuyer = myReader.GetString("object");
                            works[itter].traffic = myReader.GetString("traffic");
                            if (myReader.GetString("status").Equals("В работе"))
                            {
                                works[itter].inWork++;
                            }
                            if (myReader.GetString("status").Equals("Назначена"))
                            {
                                works[itter].point++;
                            }
                            if (myReader.GetString("status").Equals("Исполнена") || myReader.GetString("status").Equals("Оплачена"))
                            {
                                works[itter].done++;
                            }
                            idmorelast = idlast;
                            string timeAccept = myReader.GetString("timeAccept");
                            string[] timeAcceptt = timeAccept.Split(':');
                            //MessageBox.Show(timeAcceptt[0]);
                            if (myReader.GetString("status").Equals("В работе"))
                            {
                                if (works[itter].hour > int.Parse(timeAcceptt[0]))
                                {
                                    works[itter].hour = int.Parse(timeAcceptt[0]);
                                    works[itter].minute = int.Parse(timeAcceptt[1]);
                                }
                                else
                                {
                                    if (works[itter].minute > int.Parse(timeAcceptt[1]))
                                    {
                                        works[itter].hour = int.Parse(timeAcceptt[0]);
                                        works[itter].minute = int.Parse(timeAcceptt[1]);
                                    }
                                }
                            }
                            itter++;
                        }
                        //MessageBox.Show(works[itter].hour.ToString() + ":" + works[itter].minute.ToString());
                    }
                    for (int i = 0; i < itter; i++)
                    {
                        works[i].accept = works[i].inWork + works[i].point + works[i].done;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                db.closeConnection();



                dgv.Columns.AddRange(
                new DataGridViewTextBoxColumn() { Name = "deal", HeaderText = "Деятельность"},
                new DataGridViewTextBoxColumn() { Name = "timeAccept", HeaderText = "Время поставки на объект"},
                new DataGridViewTextBoxColumn() { Name = "traffic", HeaderText = "Вид транспорта" },
                new DataGridViewTextBoxColumn() { Name = "buyeer", HeaderText = "Покупатель/заказчик" },
                new DataGridViewTextBoxColumn() { Name = "sendeerr", HeaderText = "Грузоотправитель" },
                new DataGridViewTextBoxColumn() { Name = "objectBuyeer", HeaderText = "Объект поставки" },
                new DataGridViewTextBoxColumn() { Name = "accept", HeaderText = "Принятые заявки" },
                new DataGridViewTextBoxColumn() { Name = "inWork", HeaderText = "Неназначенные заявки" },
                new DataGridViewTextBoxColumn() { Name = "point", HeaderText = "Назначенные заявки" },
                new DataGridViewTextBoxColumn() { Name = "done", HeaderText = "Выпоненные" });
                dgv.Rows.Clear();
                int sumAccept = 0;
                int sumInWork = 0;
                int sumPoint = 0;
                int sumDone = 0;
                int it = 0;
                for (int i = 0; i < itter; i++)
                {
                    //MessageBox.Show(works[i].hour.ToString() + ":" + works[i].minute.ToString());
                    dgv.Rows.Add();
                    dgv["deal", i].Value = works[i].deal;
                    if (works[i].hour == 23 && works[i].minute == 59)
                    {
                        if (works[i].accept == works[i].done)
                        {
                            dgv["timeAccept", i].Value = "Все исполнены";
                        }
                        else
                        {
                            dgv["timeAccept", i].Value = "Все назначены";
                        }
                    }
                    else
                    {
                        if (works[i].minute >= 0 && works[i].minute < 10)
                        {
                            dgv["timeAccept", i].Value = works[i].hour.ToString() + ":0" + works[i].minute.ToString();
                        }
                        else
                        {
                            dgv["timeAccept", i].Value = works[i].hour.ToString() + ":" + works[i].minute.ToString();
                        }
                    }
                    dgv["traffic", i].Value = works[i].traffic.ToString();
                    dgv["buyeer", i].Value = works[i].buyer.ToString();
                    dgv["sendeerr", i].Value = works[i].sender.ToString();
                    dgv["objectBuyeer", i].Value = works[i].objectBuyer.ToString();
                    dgv["accept", i].Value = works[i].accept.ToString();
                    dgv["inWork", i].Value = works[i].inWork.ToString();
                    dgv["point", i].Value = works[i].point.ToString();
                    dgv["done", i].Value = works[i].done.ToString();
                    if (works[i].point != 0)
                    {
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            dgv[j, i].Style.BackColor = Color.Khaki;
                        }
                        //dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;
                    }
                    if (works[i].inWork != 0)
                    {
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            dgv[j, i].Style.BackColor = Color.LightPink;
                        }
                        //dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;
                    }
                    if (works[i].accept == works[i].point)
                    {
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            dgv[j, i].Style.BackColor = Color.Khaki;
                        }
                        //dgv.Rows[i].DefaultCellStyle.BackColor = Color.Khaki;
                    }
                    if (works[i].point != 0)
                    {
                        dgv["point", i].Style.BackColor = Color.Khaki;
                    }
                    if (works[i].accept == works[i].done)
                    {
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            dgv[j, i].Style.BackColor = Color.PaleGreen;
                        }
                        //dgv.Rows[i].DefaultCellStyle.BackColor = Color.PaleGreen;
                    }
                    if (works[i].done != 0)
                    {
                        dgv["done", i].Style.BackColor = Color.PaleGreen;
                    }
                    sumAccept = sumAccept + works[i].accept;
                    sumInWork = sumInWork + works[i].inWork;
                    sumPoint = sumPoint + works[i].point;
                    sumDone = sumDone + works[i].done;
                    it = i;
                }
                dgv.Sort(dgv.Columns["timeAccept"], ListSortDirection.Ascending);
                dgv[0, it + 1].Value = "Всего";
                dgv["accept", it + 1].Value = sumAccept;
                dgv["inWork", it + 1].Value = sumInWork;
                dgv["point", it + 1].Value = sumPoint;
                dgv["done", it + 1].Value = sumDone;
                dgv.Rows[it + 1].DefaultCellStyle.BackColor = Color.LightGray;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                /*for(int i = 0; i < itter; i++)
                {
                    MessageBox.Show(works[i].buyer + works[i].sender + works[i].objectBuyer + works[i].accept + works[i].point + works[i].done);
                }*/
                g = false;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            string Query = "SELECT * FROM `counterparty` WHERE `name` = '" + comboBox2.Text.ToString() + "' ORDER BY `objectName` ASC;";
            DB db = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;

            try
            {
                db.openConnection();
                myReader = cmdDataBase.ExecuteReader();

                while (myReader.Read())
                {
                    string objName = myReader.GetString("objectName");
                    if (objName.Equals("пусто"))
                    {
                        continue;
                    }
                    comboBox3.Items.Add(objName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            db.closeConnection();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            idCupterBool = false;
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            dateTimePicker1.ResetText();
            dateTimePicker2.ResetText();
        }


        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (idCupterBool)
            {
                for (int i = 0; i < dgv.ColumnCount; i++)
                {
                    dgv.Columns.RemoveAt(i);
                }
                dgv.Columns.Clear();
                dgv.Rows.Clear();

                DB db = new DB();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `request` WHERE `id` = '" + idCupter + "';", db.getConnection());
                MySqlDataReader myReader;

                dgv.Columns.AddRange(
                new DataGridViewTextBoxColumn() { Name = "status", HeaderText = "Статус" },
                new DataGridViewTextBoxColumn() { Name = "deal", HeaderText = "Деятельность"},
                new DataGridViewTextBoxColumn() { Name = "id", HeaderText = "№" },
                new DataGridViewTextBoxColumn() { Name = "docDate", HeaderText = "Дата" },
                new DataGridViewTextBoxColumn() { Name = "timeAccept", HeaderText = "Время поставки" },
                //new DataGridViewTextBoxColumn() { Name = "buyer", HeaderText = "Покупатель/заказчик" },
                //new DataGridViewTextBoxColumn() { Name = "object", HeaderText = "Объект" },
                new DataGridViewTextBoxColumn() { Name = "recipient", HeaderText = "Грузополучатель" },
                new DataGridViewTextBoxColumn() { Name = "numberDocDriver", HeaderText = "Номер доверенности" },
                new DataGridViewTextBoxColumn() { Name = "dateDocDriver", HeaderText = "Дата доверенности" },
                new DataGridViewTextBoxColumn() { Name = "fromDounterparty", HeaderText = "От кого доверенность" },
                new DataGridViewTextBoxColumn() { Name = "nameCargo", HeaderText = "Наименование" },
                new DataGridViewTextBoxColumn() { Name = "numberNom", HeaderText = "Кол-во номенклатуры" },
                new DataGridViewTextBoxColumn() { Name = "traffic", HeaderText = "Вид транспорта" },
                new DataGridViewTextBoxColumn() { Name = "сars", HeaderText = "Автомобиль" },
                new DataGridViewTextBoxColumn() { Name = "driver", HeaderText = "Водитель" },
                new DataGridViewTextBoxColumn() { Name = "cash", HeaderText = "Расчет наличными" },
                new DataGridViewTextBoxColumn() { Name = "numberDocTrip", HeaderText = "Номер ТТН" },
                new DataGridViewTextBoxColumn() { Name = "paid", HeaderText = "Оплачено" });
                dgv.Rows.Clear();
                try
                {
                    db.openConnection();
                    myReader = command.ExecuteReader();
                    int i = 0;
                    while (myReader.Read())
                    {
                        dgv.Rows.Add();
                        if (!myReader.GetString("status").Equals("пусто"))
                        {
                            dgv[0, i].Value = myReader.GetString("status");
                            if (dgv[0, i].Value.Equals("В работе"))
                            {
                                dgv[0, i].Style.BackColor = Color.LightPink;
                            }
                            if (dgv[0, i].Value.Equals("Назначена") || dgv[0, i].Value.Equals("Оплачена"))
                            {
                                dgv[0, i].Style.BackColor = Color.Khaki;
                            }
                            if (dgv[0, i].Value.Equals("Исполнена"))
                            {
                                dgv[0, i].Style.BackColor = Color.PaleGreen;
                            }
                        }
                        else
                            dgv[0, i].Value = " ";
                        dgv[1, i].Value = myReader.GetString("deal");
                        if (i != 0)
                        {
                            if (myReader.GetString("id").Equals(dgv[2, i - 1].Value))
                            {
                                dgv[2, i].Value = myReader.GetString("id");
                                /*dgv.Rows[i].DefaultCellStyle.BackColor = Color.MistyRose;
                                dgv.Rows[i - 1].DefaultCellStyle.BackColor = Color.MistyRose;*/
                            }
                            else
                            {
                                dgv[2, i].Value = myReader.GetString("id");
                            }
                        }
                        else
                        {
                            dgv[2, i].Value = myReader.GetString("id");
                        }
                        string datefirst = myReader.GetString("docDate");
                        string[] datesplit = datefirst.Split(' ');
                        dgv[3, i].Value = datesplit[0];
                        dgv[4, i].Value = myReader.GetString("timeAccept");
                        if (!myReader.GetString("buyer").Equals("пусто"))
                        {
                            //dgv[3, i].Value = myReader.GetString("buyer");
                            comboBox2.Text = myReader.GetString("buyer");
                        }
                        else
                        {
                            //dgv[3, i].Value = " ";
                            comboBox2.Text = " ";
                        }
                        if (!myReader.GetString("object").Equals("пусто"))
                        {
                            //dgv[4, i].Value = myReader.GetString("object");
                            comboBox3.Text = myReader.GetString("object");
                        }
                        else
                        {
                            //dgv[4, i].Value = " ";
                            comboBox3.Text = " ";
                        }
                        if (!myReader.GetString("recipient").Equals("пусто"))
                            dgv[5, i].Value = myReader.GetString("recipient");
                        else
                            dgv[5, i].Value = " ";
                        if (!myReader.GetString("sender").Equals("пусто"))
                        {
                            //dgv[4, i].Value = myReader.GetString("sender");
                            textBox1.Show();
                            label7.Show();
                            textBox1.Text = myReader.GetString("sender");
                        }
                        //dgv[4, i].Value = " ";
                        if (!myReader.GetString("numberDocDriver").Equals("пусто"))
                            dgv[6, i].Value = myReader.GetString("numberDocDriver");
                        else
                            dgv[6, i].Value = " ";
                        string dateDocDriver = myReader.GetString("dateDocDriver");
                        string[] datesplitt = dateDocDriver.Split(' ');
                        if (!myReader.GetString("dateDocDriver").Equals("пусто"))
                            dgv[7, i].Value = datesplitt[0];
                        else
                            dgv[7, i].Value = " ";
                        if (!myReader.GetString("fromCounterparty").Equals("пусто"))
                            dgv[8, i].Value = myReader.GetString("fromCounterparty");
                        else
                            dgv[8, i].Value = " ";
                        if (!myReader.GetString("nameCargo").Equals("пусто"))
                            dgv[9, i].Value = myReader.GetString("nameCargo");
                        else
                            dgv[9, i].Value = " ";
                        if (!myReader.GetString("numberNomenclature").Equals("0"))
                            dgv[10, i].Value = myReader.GetString("numberNomenclature");
                        else
                            dgv[10, i].Value = " ";
                        if (!myReader.GetString("traffic").Equals("пусто"))
                            dgv[11, i].Value = myReader.GetString("traffic");
                        else
                            dgv[11, i].Value = " ";
                        if (!myReader.GetString("cars").Equals("пусто"))
                            dgv[12, i].Value = myReader.GetString("cars");
                        else
                            dgv[12, i].Value = " ";
                        if (!myReader.GetString("drivers").Equals("пусто"))
                            dgv[13, i].Value = myReader.GetString("drivers");
                        else
                            dgv[13, i].Value = " ";
                        if (!myReader.GetString("cash").Equals("пусто"))
                            dgv[14, i].Value = myReader.GetString("cash");
                        else
                            dgv[14, i].Value = " ";
                        if (!myReader.GetString("numberDocTrip").Equals("-1"))
                            dgv[15, i].Value = myReader.GetString("numberDocTrip");
                        else
                            dgv[15, i].Value = " ";
                        if (!myReader.GetString("paid").Equals("пусто"))
                            dgv[16, i].Value = myReader.GetString("paid");
                        else
                            dgv[16, i].Value = " ";
                        i++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                g = true;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                db.closeConnection();
                idCupterBool = false;
            }
            else
            {
                if (!g)
                {
                    button16.Show();
                    click = false;
                    int idx = dgv.CurrentRow.Index;
                    string buyer = dgv["buyeer", idx].Value.ToString();
                    string senderr = dgv["sendeerr", idx].Value.ToString();
                    string objectBuyer = dgv["objectBuyeer", idx].Value.ToString();
                    string time = dateTimePicker1.Value.ToString();
                    string[] asd = time.Split(' ');
                    string[] zxc = asd[0].Split('.');
                    string answer1 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];

                    time = dateTimePicker2.Value.ToString();
                    asd = null;
                    asd = time.Split(' ');
                    zxc = null;
                    zxc = asd[0].Split('.');
                    string answer2 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];

                    /*dgv.Columns.Remove(buyeerr);
                    dgv.Columns.Remove(sendeerr);
                    dgv.Columns.Remove(objectBuyeer);
                    dgv.Columns.Remove(accept);
                    dgv.Columns.Remove(inWork);
                    dgv.Columns.Remove(point); 
                    dgv.Columns.Remove(done);*/
                    for (int i = 0; i < dgv.ColumnCount; i++)
                    {
                        dgv.Columns.RemoveAt(i);
                    }
                    dgv.Columns.Clear();
                    dgv.Rows.Clear();

                    DB db = new DB();
                    MySqlCommand command = new MySqlCommand();
                    if (comboBox5.Text.Equals(""))
                    {
                        command = new MySqlCommand("SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `buyer` = '" + buyer + "'AND `sender` = '" + senderr + "' AND `object` = '" + objectBuyer + "';", db.getConnection());
                    }
                    else
                    {
                        command = new MySqlCommand("SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `buyer` = '" + buyer + "'AND `sender` = '" + senderr + "' AND `object` = '" + objectBuyer + "' AND `cars` = '" + comboBox5.Text + "';", db.getConnection());
                    }
                        MySqlDataReader myReader;

                    dgv.Columns.AddRange(
                    new DataGridViewTextBoxColumn() { Name = "status", HeaderText = "Статус" },
                    new DataGridViewTextBoxColumn() { Name = "deal", HeaderText = "Деятельность" },
                    new DataGridViewTextBoxColumn() { Name = "id", HeaderText = "№" },
                    new DataGridViewTextBoxColumn() { Name = "docDate", HeaderText = "Дата" },
                    new DataGridViewTextBoxColumn() { Name = "timeAccept", HeaderText = "Время поставки" },
                    //new DataGridViewTextBoxColumn() { Name = "buyer", HeaderText = "Покупатель/заказчик" },
                    //new DataGridViewTextBoxColumn() { Name = "object", HeaderText = "Объект" },
                    new DataGridViewTextBoxColumn() { Name = "recipient", HeaderText = "Грузополучатель" },
                    new DataGridViewTextBoxColumn() { Name = "numberDocDriver", HeaderText = "Номер доверенности" },
                    new DataGridViewTextBoxColumn() { Name = "dateDocDriver", HeaderText = "Дата доверенности" },
                    new DataGridViewTextBoxColumn() { Name = "fromDounterparty", HeaderText = "От кого доверенность" },
                    new DataGridViewTextBoxColumn() { Name = "nameCargo", HeaderText = "Наименование" },
                    new DataGridViewTextBoxColumn() { Name = "numberNom", HeaderText = "Кол-во номенклатуры" },
                    new DataGridViewTextBoxColumn() { Name = "traffic", HeaderText = "Вид транспорта" },
                    new DataGridViewTextBoxColumn() { Name = "сars", HeaderText = "Автомобиль" },
                    new DataGridViewTextBoxColumn() { Name = "driver", HeaderText = "Водитель" },
                    new DataGridViewTextBoxColumn() { Name = "cash", HeaderText = "Расчет наличными" },
                    new DataGridViewTextBoxColumn() { Name = "numberDocTrip", HeaderText = "Номер ТТН" },
                    new DataGridViewTextBoxColumn() { Name = "paid", HeaderText = "Оплачено" });
                    dgv.Rows.Clear();
                    try
                    {
                        db.openConnection();
                        myReader = command.ExecuteReader();
                        int i = 0;
                        while (myReader.Read())
                        {
                            dgv.Rows.Add();
                            if (!myReader.GetString("status").Equals("пусто"))
                            {
                                dgv[0, i].Value = myReader.GetString("status");
                                if (dgv[0, i].Value.Equals("В работе"))
                                {
                                    dgv[0, i].Style.BackColor = Color.LightPink;
                                }
                                if (dgv[0, i].Value.Equals("Назначена") || dgv[0, i].Value.Equals("Оплачена"))
                                {
                                    dgv[0, i].Style.BackColor = Color.Khaki;
                                }
                                if (dgv[0, i].Value.Equals("Исполнена"))
                                {
                                    dgv[0, i].Style.BackColor = Color.PaleGreen;
                                }
                            }
                            else
                                dgv[0, i].Value = " ";
                            dgv[1, i].Value = myReader.GetString("deal");
                            if (i != 0)
                            {
                                if (myReader.GetString("id").Equals(dgv[2, i - 1].Value))
                                {
                                    dgv[2, i].Value = myReader.GetString("id");
                                    /*dgv.Rows[i].DefaultCellStyle.BackColor = Color.MistyRose;
                                    dgv.Rows[i - 1].DefaultCellStyle.BackColor = Color.MistyRose;*/
                                }
                                else
                                {
                                    dgv[2, i].Value = myReader.GetString("id");
                                }
                            }
                            else
                            {
                                dgv[2, i].Value = myReader.GetString("id");
                            }
                            string datefirst = myReader.GetString("docDate");
                            string[] datesplit = datefirst.Split(' ');
                            dgv[3, i].Value = datesplit[0];
                            dgv[4, i].Value = myReader.GetString("timeAccept");
                            if (!myReader.GetString("buyer").Equals("пусто"))
                            {
                                //dgv[3, i].Value = myReader.GetString("buyer");
                                comboBox2.Text = myReader.GetString("buyer");
                            }
                            else
                            {
                                //dgv[3, i].Value = " ";
                                comboBox2.Text = " ";
                            }
                            if (!myReader.GetString("object").Equals("пусто"))
                            {
                                //dgv[4, i].Value = myReader.GetString("object");
                                comboBox3.Text = myReader.GetString("object");
                            }
                            else
                            {
                                //dgv[4, i].Value = " ";
                                comboBox3.Text = " ";
                            }
                            if (!myReader.GetString("recipient").Equals("пусто"))
                                dgv[5, i].Value = myReader.GetString("recipient");
                            else
                                dgv[5, i].Value = " ";
                            if (!myReader.GetString("sender").Equals("пусто"))
                            {
                                //dgv[4, i].Value = myReader.GetString("sender");
                                textBox1.Show();
                                label7.Show();
                                textBox1.Text = myReader.GetString("sender");
                            }
                            //dgv[4, i].Value = " ";
                            if (!myReader.GetString("numberDocDriver").Equals("пусто"))
                                dgv[6, i].Value = myReader.GetString("numberDocDriver");
                            else
                                dgv[6, i].Value = " ";
                            string dateDocDriver = myReader.GetString("dateDocDriver");
                            string[] datesplitt = dateDocDriver.Split(' ');
                            if (!myReader.GetString("dateDocDriver").Equals("пусто"))
                                dgv[7, i].Value = datesplitt[0];
                            else
                                dgv[7, i].Value = " ";
                            if (!myReader.GetString("fromCounterparty").Equals("пусто"))
                                dgv[8, i].Value = myReader.GetString("fromCounterparty");
                            else
                                dgv[8, i].Value = " ";
                            if (!myReader.GetString("nameCargo").Equals("пусто"))
                                dgv[9, i].Value = myReader.GetString("nameCargo");
                            else
                                dgv[9, i].Value = " ";
                            if (!myReader.GetString("numberNomenclature").Equals("0"))
                                dgv[10, i].Value = myReader.GetString("numberNomenclature");
                            else
                                dgv[10, i].Value = " ";
                            if (!myReader.GetString("traffic").Equals("пусто"))
                                dgv[11, i].Value = myReader.GetString("traffic");
                            else
                                dgv[11, i].Value = " ";
                            if (!myReader.GetString("cars").Equals("пусто"))
                                dgv[12, i].Value = myReader.GetString("cars");
                            else
                                dgv[12, i].Value = " ";
                            if (!myReader.GetString("drivers").Equals("пусто"))
                                dgv[13, i].Value = myReader.GetString("drivers");
                            else
                                dgv[13, i].Value = " ";
                            if (!myReader.GetString("cash").Equals("пусто"))
                                dgv[14, i].Value = myReader.GetString("cash");
                            else
                                dgv[14, i].Value = " ";
                            if (!myReader.GetString("numberDocTrip").Equals("-1"))
                                dgv[15, i].Value = myReader.GetString("numberDocTrip");
                            else
                                dgv[15, i].Value = " ";
                            if (!myReader.GetString("paid").Equals("пусто"))
                                dgv[16, i].Value = myReader.GetString("paid");
                            else
                                dgv[16, i].Value = " ";
                            i++;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    g = true;
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                    db.closeConnection();
                }
                else
                {
                    /*try
                    {*/
                    comboBox1.Text = "";
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                    int idx = dgv.CurrentRow.Index;
                    string idTar = dgv[2, idx].Value.ToString();

                    WriteRequest write = new WriteRequest();
                    DB db = new DB();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM `request` WHERE `id` = '" + idTar + "';", db.getConnection());
                    MySqlDataReader myReader;
                    write.Show();
                    write.label12.Hide();
                    write.button14.Hide();
                    write.textBox3.Hide();
                    write.button5.Hide();
                    int i = 0;
                    int ittercargo = 0;
                    int tripCount = 0;
                    bool size = false;
                    write.dataGridView1.Rows.Clear();
                    for (int kkk = 0; kkk < nameCargo.Length; kkk++)
                    {
                        nameCargo[kkk] = " ";
                        cargoCount[kkk] = " ";
                    }
                    /*}
                    catch
                    {
                        MessageBox.Show("asd");
                    }*/
                    try
                    {
                        db.openConnection();
                        myReader = command.ExecuteReader();

                        while (myReader.Read())
                        {
                            write.status = myReader.GetString("status");
                            if (myReader.GetString("status").Equals("В работе"))
                            {
                                tripCount++;
                                write.idRequest = myReader.GetString("id");
                                write.label14.Text = "Назначение заявки № " + myReader.GetString("id");
                                write.dateTimePicker1.Value = myReader.GetDateTime("docDate");
                                write.dateTimePicker3.Value = myReader.GetDateTime("dateAccept");
                                string[] time = myReader.GetString("timeAccept").Split(':');
                                int hour = int.Parse(time[0]);
                                int minute = int.Parse(time[1]);
                                write.dateTimePicker4.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, 0);
                                write.comboBox1.Text = myReader.GetString("deal");
                                write.comboBox2.Text = myReader.GetString("ourFirms");
                                write.comboBox3.Text = myReader.GetString("buyer");
                                write.comboBox4.Text = myReader.GetString("sender");
                                write.comboBox5.Text = myReader.GetString("recipient");
                                write.comboBox6.Text = myReader.GetString("object");
                                write.comboBox13.Text = myReader.GetString("objectArrive");
                                write.comboBox12.Text = myReader.GetString("objectSend");
                                if (!myReader.GetString("nameCargo").Equals("пусто"))
                                {
                                    size = false;
                                    for (int j = 0; j < ittercargo; j++)
                                    {
                                        if (nameCargo[j].Equals(myReader.GetString("nameCargo")) && cargoCount[j].Equals(myReader.GetString("numberNomenclature")))
                                        {
                                            size = true;
                                        }
                                    }
                                    if (size == false)
                                    {   
                                        //MessageBox.Show("asd");
                                        write.dataGridView1.Rows.Add();
                                        nameCargo[i] = myReader.GetString("nameCargo");
                                        cargoCount[i] = myReader.GetString("numberNomenclature");
                                        write.dataGridView1[0, i].Value = myReader.GetString("nameCargo");
                                        write.dataGridView1[1, i].Value = myReader.GetString("fromCounterparty");
                                        write.dataGridView1[2, i].Value = myReader.GetString("numberDocDriver");
                                        string[] asddd = myReader.GetString("dateDocDriver").Split(' ');
                                        write.dataGridView1[3, i].Value = asddd[0];
                                        write.dataGridView1[4, i].Value = myReader.GetString("numberNomenclature");

                                        write.nom[i] = myReader.GetString("nameCargo");
                                        write.from[i] = myReader.GetString("fromCounterparty");
                                        write.numm[i] = myReader.GetString("numberDocDriver");
                                        write.date[i] = asddd[0];
                                        write.numOfNom[i] = myReader.GetString("numberNomenclature");
                                        write.size = i;

                                        ittercargo++;
                                        //i++;
                                    }
                                    if(ittercargo > 1)
                                    {
                                        write.severalCargo = true;
                                    }
                                }
                                write.comboBox14.Text = myReader.GetString("traffic");
                                write.dateTimePicker5.Text = myReader.GetString("dateTTN");
                                write.textBox3.Text = myReader.GetString("numberTrip");
                                write.textBox1.Text = myReader.GetString("priceSalary");
                                write.comboBox7.Text = myReader.GetString("contractor");
                                write.comboBox8.Text = myReader.GetString("cars");
                                write.comboBox11.Text = myReader.GetString("driveCont");
                                write.comboBox9.Text = myReader.GetString("drivers");
                                //write.dataGridView1[2, i].Value = myReader.GetString("numberDocDriver"); 
                                //string[] asddd = myReader.GetString("dateDocDriver").Split(' ');
                                //write.dataGridView1[3, i].Value = asddd[0];
                                //write.dataGridView1[1, i].Value = myReader.GetString("fromCounterparty");
                                //write.textBox3.Text = tripCount.ToString();
                                if (myReader.GetString("coment").Equals("пусто"))
                                {
                                    write.textBox2.Text = "Добавьте комментарий";
                                    write.textBox2.ForeColor = Color.Gray;
                                }
                                else
                                {
                                    write.textBox2.Text = myReader.GetString("coment");
                                }
                                if (myReader.GetString("cash").Equals("Да"))
                                {
                                    write.checkBox1.CheckState = CheckState.Checked;
                                }
                                else
                                {
                                    write.checkBox1.CheckState = CheckState.Unchecked;
                                }
                                if (myReader.GetString("paid").Equals("Да"))
                                {
                                    write.checkBox2.CheckState = CheckState.Checked;
                                }
                                else
                                {
                                    write.checkBox2.CheckState = CheckState.Unchecked;
                                }
                                i++;
                                write.buttonAdd.Hide();
                                write.button2.Show();
                                write.button3.Hide();
                            }
                            if (myReader.GetString("status").Equals("Назначена") || myReader.GetString("status").Equals("Исполнена") || myReader.GetString("status").Equals("Оплачена"))
                            {
                                //MessageBox.Show("ads");
                                tripCount++;
                                write.idRequest = myReader.GetString("id");
                                write.label21.Text = "Исполнение заявки № " + myReader.GetString("id");
                                write.dateTimePicker1.Value = myReader.GetDateTime("docDate");
                                write.dateTimePicker3.Value = myReader.GetDateTime("dateAccept");
                                string[] time = myReader.GetString("timeAccept").Split(':');
                                int hour = int.Parse(time[0]);
                                int minute = int.Parse(time[1]);
                                write.dateTimePicker4.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, 0);
                                write.comboBox1.Text = myReader.GetString("deal");
                                write.comboBox2.Text = myReader.GetString("ourFirms");
                                write.comboBox3.Text = myReader.GetString("buyer");
                                write.comboBox4.Text = myReader.GetString("sender");
                                write.comboBox5.Text = myReader.GetString("recipient");
                                write.comboBox6.Text = myReader.GetString("object");
                                write.comboBox13.Text = myReader.GetString("objectArrive");
                                write.comboBox12.Text = myReader.GetString("objectSend");
                                if (!myReader.GetString("nameCargo").Equals("пусто"))
                                {
                                    size = false;
                                    for (int j = 0; j < ittercargo; j++)
                                    {
                                        if (nameCargo[j].Equals(myReader.GetString("nameCargo")) && cargoCount[j].Equals(myReader.GetString("numberNomenclature")))
                                        {
                                            size = true;
                                        }
                                    }
                                    if (size == false)
                                    {
                                        write.dataGridView1.Rows.Add();
                                        nameCargo[i] = myReader.GetString("nameCargo");
                                        cargoCount[i] = myReader.GetString("numberNomenclature");
                                        write.dataGridView1[0, i].Value = myReader.GetString("nameCargo");
                                        write.dataGridView1[1, i].Value = myReader.GetString("fromCounterparty");
                                        write.dataGridView1[2, i].Value = myReader.GetString("numberDocDriver");
                                        string[] asddd = myReader.GetString("dateDocDriver").Split(' ');
                                        write.dataGridView1[3, i].Value = asddd[0];
                                        write.dataGridView1[4, i].Value = myReader.GetString("numberNomenclature");

                                        write.nom[i] = myReader.GetString("nameCargo");
                                        write.from[i] = myReader.GetString("fromCounterparty");
                                        write.numm[i] = myReader.GetString("numberDocDriver");
                                        write.date[i] = asddd[0];
                                        write.numOfNom[i] = myReader.GetString("numberNomenclature");
                                        write.size = i;

                                        ittercargo++;
                                        //i++;
                                    }
                                    if (ittercargo > 1)
                                    {
                                        write.severalCargo = true;
                                    }
                                }
                                //write.textBox3.Text = myReader.GetString("numberTrip");
                                write.comboBox14.Text = myReader.GetString("traffic");
                                write.dateTimePicker5.Text = myReader.GetString("dateTTN");
                                write.textBox1.Text = myReader.GetString("priceSalary");
                                write.comboBox7.Text = myReader.GetString("contractor");
                                write.comboBox8.Text = myReader.GetString("cars");
                                write.comboBox11.Text = myReader.GetString("driveCont");
                                write.comboBox9.Text = myReader.GetString("drivers");
                                //write.dataGridView1[2, i].Value = myReader.GetString("numberDocDriver");
                                //string[] asddd = myReader.GetString("dateDocDriver").Split(' ');
                                //write.dataGridView1[3, i].Value = asddd[0];
                                //write.dataGridView1[1, i].Value = myReader.GetString("fromCounterparty");
                                //write.textBox3.Text = tripCount.ToString();
                                if (myReader.GetString("coment").Equals("пусто"))
                                {
                                    write.textBox2.Text = "Добавьте комментарий";
                                    write.textBox2.ForeColor = Color.Gray;
                                }
                                else
                                {
                                    write.textBox2.Text = myReader.GetString("coment");
                                }
                                if (myReader.GetString("cash").Equals("Да"))
                                {
                                    write.checkBox1.CheckState = CheckState.Checked;
                                    write.checkBox2.Show();
                                    write.label11.Show();
                                }
                                else
                                {
                                    write.checkBox2.CheckState = CheckState.Unchecked;
                                }
                                if (!myReader.GetString("numberDocTrip").Equals("-1"))
                                {
                                    write.textBox6.Text = myReader.GetString("numberDocTrip");
                                    write.textBox6.ForeColor = Color.Black;
                                }
                                //write.textBox6.Text = myReader.GetString("numberDocTrip");
                                if (myReader.GetString("cash").Equals("Да"))
                                {
                                    write.checkBox1.CheckState = CheckState.Checked;
                                }
                                else
                                {
                                    write.checkBox1.CheckState = CheckState.Unchecked;
                                }
                                if (myReader.GetString("paid").Equals("Да"))
                                {
                                    write.checkBox2.CheckState = CheckState.Checked;
                                }
                                else
                                {
                                    write.checkBox2.CheckState = CheckState.Unchecked;
                                }
                                i++;
                                write.buttonAdd.Hide();
                                write.button2.Hide();
                                write.button3.Show();
                            }
                            /*WriteRequest.Cargo[WriteRequest.CargoCount] = myReader.GetString("nameCargo");
                            WriteRequest.CargoCount++;*/
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    //dgv.Rows.Clear();

                    db.closeConnection();
                    write.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            click = true;
            /*if (!g)
            {
                if (ooo)
                {
                    idxx = -1;
                    ooo = false;
                }
                if (idxx == -1)
                {
                    idxx = dgv.CurrentRow.Index;
                    BackColor = dgv.Rows[idxx].DefaultCellStyle.BackColor;
                    dgv.Rows[idxx].DefaultCellStyle.BackColor = Color.FromArgb(195, 242, 250);
                }
                else
                {
                    dgv.Rows[idxx].DefaultCellStyle.BackColor = BackColor;
                    idxx = dgv.CurrentRow.Index;
                    BackColor = dgv.Rows[idxx].DefaultCellStyle.BackColor;
                    dgv.Rows[idxx].DefaultCellStyle.BackColor = Color.FromArgb(195, 242, 250);
                }
            }
            else
            {
                if (ooo)
                {
                    idxx = -1;
                    ooo = false;
                }
                if (idxx == -1)
                {
                    idxx = dgv.CurrentRow.Index;
                    BackColor = dgv.Rows[idxx].DefaultCellStyle.BackColor;
                    dgv.Rows[idxx].DefaultCellStyle.BackColor = Color.FromArgb(195, 242, 250);
                }
                else
                {
                    dgv.Rows[idxx].DefaultCellStyle.BackColor = BackColor;
                    idxx = dgv.CurrentRow.Index;
                    BackColor = dgv.Rows[idxx].DefaultCellStyle.BackColor;
                    dgv.Rows[idxx].DefaultCellStyle.BackColor = Color.FromArgb(195, 242, 250);
                }
            }*/
            if (!g)
            {

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (!dgv["point", i].Value.Equals("0"))
                    {
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            dgv[j, i].Style.BackColor = Color.Khaki;
                        }
                    }
                    if (!dgv["inwork", i].Value.Equals("0"))
                    {
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            dgv[j, i].Style.BackColor = Color.LightPink;
                        }
                    }
                    if (dgv["accept", i].Value.Equals(dgv["point", i].Value))
                    {
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            dgv[j, i].Style.BackColor = Color.Khaki;
                        }
                    }
                    if (!dgv["done", i].Value.Equals("0"))
                    {
                        dgv["done", i].Style.BackColor = Color.PaleGreen;
                    }
                    if (!dgv["point", i].Value.Equals("0"))
                    {
                        dgv["point", i].Style.BackColor = Color.Khaki;
                    }
                    if (dgv["accept", i].Value.Equals(dgv["done", i].Value))
                    {
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            dgv[j, i].Style.BackColor = Color.PaleGreen;
                        }
                    }
                }
                Color bright = dgv.Rows[dgv.CurrentRow.Index].DefaultCellStyle.BackColor;
                if (bright == Color.LightPink)
                {
                    for (int i = 0; i < dgv.ColumnCount; i++)
                    {
                        dgv[i, dgv.CurrentRow.Index].Style.BackColor = Color.Salmon;
                    }
                    //dgv.Rows[dgv.CurrentRow.Index].DefaultCellStyle.BackColor = Color.Red;
                }
                if (bright == Color.Khaki)
                {
                    for (int i = 0; i < dgv.ColumnCount; i++)
                    {
                        dgv[i, dgv.CurrentRow.Index].Style.BackColor = Color.Yellow;
                    }
                    //dgv.Rows[dgv.CurrentRow.Index].DefaultCellStyle.BackColor = Color.Red;
                }
                if (bright == Color.PaleGreen)
                {
                    for (int i = 0; i < dgv.ColumnCount; i++)
                    {
                        dgv[i, dgv.CurrentRow.Index].Style.BackColor = Color.GreenYellow;
                    }
                    //dgv.Rows[dgv.CurrentRow.Index].DefaultCellStyle.BackColor = Color.Red;
                }
                //dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;
                for (int i = 0; i < dgv.ColumnCount; i++)
                {
                    dgv[i, dgv.Rows.Count - 1].Style.BackColor = Color.LightGray;
                }
            }
            else
            {
                int chet = 0;
                for (int i = 0; i < dgv.Rows.Count - 1; i++)
                {
                    if (chet != 0)
                    {
                        /*if (dgv[1, i].Value.Equals(dgv[1, i - 1].Value))
                        {
                            for (int h = 0; h < dgv.ColumnCount; h++)
                            {
                                dgv[h, i - 1].Style.BackColor = Color.MistyRose;
                            }
                            for (int h = 0; h < dgv.ColumnCount; h++)
                            {
                                dgv[h, i].Style.BackColor = Color.MistyRose;
                            }
                        }
                        else
                        {*/
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            dgv[j, i].Style.BackColor = Color.White;
                            if (dgv[0, i].Value.Equals("В работе"))
                            {
                                dgv[0, i].Style.BackColor = Color.LightPink;
                            }
                            if (dgv[0, i].Value.Equals("Назначена") || dgv[0, i].Value.Equals("Оплачена"))
                            {
                                dgv[0, i].Style.BackColor = Color.Khaki;
                            }
                            if (dgv[0, i].Value.Equals("Исполнена"))
                            {
                                dgv[0, i].Style.BackColor = Color.PaleGreen;
                            }
                        }
                        //}
                    }
                    else
                    {
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            dgv[j, i].Style.BackColor = Color.White;
                            if (dgv[0, i].Value.Equals("В работе"))
                            {
                                dgv[0, i].Style.BackColor = Color.LightPink;
                            }
                            if (dgv[0, i].Value.Equals("Назначена") || dgv[0, i].Value.Equals("Оплачена"))
                            {
                                dgv[0, i].Style.BackColor = Color.Khaki;
                            }
                            if (dgv[0, i].Value.Equals("Исполнена"))
                            {
                                dgv[0, i].Style.BackColor = Color.PaleGreen;
                            }
                        }
                    }
                    chet++;
                }
                Color bright = dgv.Rows[dgv.CurrentRow.Index].DefaultCellStyle.BackColor;
                if (bright == Color.White || bright == Color.Empty)
                {
                    for (int i = 1; i < dgv.ColumnCount; i++)
                    {
                        dgv[i, dgv.CurrentRow.Index].Style.BackColor = Color.LightCyan;
                    }
                    /*if(dgv[0, dgv.CurrentRow.Index].Value.Equals("В работе"))
                    {

                    }
                    dgv[0, dgv.CurrentRow.Index].Style.BackColor */
                }
                if (bright == Color.MistyRose)
                {
                    for (int i = 0; i < dgv.ColumnCount; i++)
                    {
                        dgv[i, dgv.CurrentRow.Index].Style.BackColor = Color.LightPink;
                    }
                    //dgv.Rows[dgv.CurrentRow.Index].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //g = false;
            StatementSalary salary = new StatementSalary();
            salary.Show();
        }


        int[] visok(int year)
        {
            int[] dateArr = new int[12];
            if (((year % 100) % 4) != 0)
            {
                dateArr = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            }
            else
            {
                dateArr = new int[] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            }
            return dateArr;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int year = dateTimePicker1.Value.Date.Year;
            int month = dateTimePicker1.Value.Date.Month;
            int day = dateTimePicker1.Value.Date.Day;
            int[] dateArr = visok(dateTimePicker1.Value.Date.Year);
            if (day == 1)
            {
                month--;
                if (month == 0)
                {
                    month = 12;
                    year--;
                }
                day = dateArr[month - 1];
                dateTimePicker1.Value = new DateTime(year, month, day);
            }
            else
            {
                dateTimePicker1.Value = new DateTime(year, month, day - 1);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int year = dateTimePicker1.Value.Date.Year;
            int month = dateTimePicker1.Value.Date.Month;
            int day = dateTimePicker1.Value.Date.Day;
            int[] dateArr = visok(dateTimePicker1.Value.Date.Year);
            if (day == dateArr[month - 1])
            {
                if (month == 12)
                {
                    month = 0;
                    year++;
                }
                day = 1;
                dateTimePicker1.Value = new DateTime(year, month + 1, day);
            }
            else
            {
                dateTimePicker1.Value = new DateTime(year, month, day + 1);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int year = dateTimePicker2.Value.Date.Year;
            int month = dateTimePicker2.Value.Date.Month;
            int day = dateTimePicker2.Value.Date.Day;
            int[] dateArr = visok(dateTimePicker2.Value.Date.Year);
            if (day == 1)
            {
                month--;
                if (month == 0)
                {
                    month = 12;
                    year--;
                }
                day = dateArr[month - 1];
                dateTimePicker2.Value = new DateTime(year, month, day);
            }
            else
            {
                dateTimePicker2.Value = new DateTime(year, month, day - 1);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int year = dateTimePicker2.Value.Date.Year;
            int month = dateTimePicker2.Value.Date.Month;
            int day = dateTimePicker2.Value.Date.Day;
            int[] dateArr = visok(dateTimePicker2.Value.Date.Year);
            if (day == dateArr[month - 1])
            {
                if (month == 12)
                {
                    month = 0;
                    year++;
                }
                day = 1;
                dateTimePicker2.Value = new DateTime(year, month + 1, day);
            }
            else
            {
                dateTimePicker2.Value = new DateTime(year, month, day + 1);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Contractor cont = new Contractor();
            cont.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Price price = new Price();
            price.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            StatementBuyer buy = new StatementBuyer();
            buy.Show();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count != 0)
            {
                click = false;
                dgv.ClearSelection();
                if (!g)
                {

                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        if (!dgv["point", i].Value.Equals("0"))
                        {
                            for (int j = 0; j < dgv.ColumnCount; j++)
                            {
                                dgv[j, i].Style.BackColor = Color.Khaki;
                            }
                        }
                        if (!dgv["inwork", i].Value.Equals("0"))
                        {
                            for (int j = 0; j < dgv.ColumnCount; j++)
                            {
                                dgv[j, i].Style.BackColor = Color.LightPink;
                            }
                        }
                        if (dgv["accept", i].Value.Equals(dgv["point", i].Value))
                        {
                            for (int j = 0; j < dgv.ColumnCount; j++)
                            {
                                dgv[j, i].Style.BackColor = Color.Khaki;
                            }
                        }
                        if (!dgv["done", i].Value.Equals("0"))
                        {
                            dgv["done", i].Style.BackColor = Color.PaleGreen;
                        }
                        if (!dgv["point", i].Value.Equals("0"))
                        {
                            dgv["point", i].Style.BackColor = Color.Khaki;
                        }
                        if (dgv["accept", i].Value.Equals(dgv["done", i].Value))
                        {
                            for (int j = 0; j < dgv.ColumnCount; j++)
                            {
                                dgv[j, i].Style.BackColor = Color.PaleGreen;
                            }
                        }
                    }
                    Color bright = dgv.Rows[dgv.CurrentRow.Index].DefaultCellStyle.BackColor;
                    if (bright == Color.LightPink)
                    {
                        for (int i = 0; i < dgv.ColumnCount; i++)
                        {
                            dgv[i, dgv.CurrentRow.Index].Style.BackColor = Color.Salmon;
                        }
                        //dgv.Rows[dgv.CurrentRow.Index].DefaultCellStyle.BackColor = Color.Red;
                    }
                    if (bright == Color.Khaki)
                    {
                        for (int i = 0; i < dgv.ColumnCount; i++)
                        {
                            dgv[i, dgv.CurrentRow.Index].Style.BackColor = Color.Yellow;
                        }
                        //dgv.Rows[dgv.CurrentRow.Index].DefaultCellStyle.BackColor = Color.Red;
                    }
                    if (bright == Color.PaleGreen)
                    {
                        for (int i = 0; i < dgv.ColumnCount; i++)
                        {
                            dgv[i, dgv.CurrentRow.Index].Style.BackColor = Color.GreenYellow;
                        }
                        //dgv.Rows[dgv.CurrentRow.Index].DefaultCellStyle.BackColor = Color.Red;
                    }
                    //dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;
                    for (int i = 0; i < dgv.ColumnCount; i++)
                    {
                        dgv[i, dgv.Rows.Count - 1].Style.BackColor = Color.LightGray;
                    }
                }
                else
                {
                    int chet = 0;
                    for (int i = 0; i < dgv.Rows.Count - 1; i++)
                    {
                        if (chet != 0)
                        {
                            /*if (dgv[1, i].Value.Equals(dgv[1, i - 1].Value))
                            {
                                for (int h = 0; h < dgv.ColumnCount; h++)
                                {
                                    dgv[h, i - 1].Style.BackColor = Color.MistyRose;
                                }
                                for (int h = 0; h < dgv.ColumnCount; h++)
                                {
                                    dgv[h, i].Style.BackColor = Color.MistyRose;
                                }
                            }
                            else
                            {*/
                            for (int j = 0; j < dgv.ColumnCount; j++)
                            {
                                dgv[j, i].Style.BackColor = Color.White;
                                if (dgv[0, i].Value.Equals("В работе"))
                                {
                                    dgv[0, i].Style.BackColor = Color.LightPink;
                                }
                                if (dgv[0, i].Value.Equals("Назначена") || dgv[0, i].Value.Equals("Оплачена"))
                                {
                                    dgv[0, i].Style.BackColor = Color.Khaki;
                                }
                                if (dgv[0, i].Value.Equals("Исполнена"))
                                {
                                    dgv[0, i].Style.BackColor = Color.PaleGreen;
                                }
                            }
                            //}
                        }
                        else
                        {
                            for (int j = 0; j < dgv.ColumnCount; j++)
                            {
                                dgv[j, i].Style.BackColor = Color.White;
                                if (dgv[0, i].Value.Equals("В работе"))
                                {
                                    dgv[0, i].Style.BackColor = Color.LightPink;
                                }
                                if (dgv[0, i].Value.Equals("Назначена") || dgv[0, i].Value.Equals("Оплачена"))
                                {
                                    dgv[0, i].Style.BackColor = Color.Khaki;
                                }
                                if (dgv[0, i].Value.Equals("Исполнена"))
                                {
                                    dgv[0, i].Style.BackColor = Color.PaleGreen;
                                }
                            }
                        }
                        chet++;
                    }
                    Color bright = dgv.Rows[dgv.CurrentRow.Index].DefaultCellStyle.BackColor;
                    if (bright == Color.White || bright == Color.Empty)
                    {
                        for (int i = 0; i < dgv.ColumnCount; i++)
                        {
                            dgv[i, dgv.CurrentRow.Index].Style.BackColor = Color.LightCyan;
                        }
                    }
                    if (bright == Color.MistyRose)
                    {
                        for (int i = 0; i < dgv.ColumnCount; i++)
                        {
                            dgv[i, dgv.CurrentRow.Index].Style.BackColor = Color.LightPink;
                        }
                        //dgv.Rows[dgv.CurrentRow.Index].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            DB db = new DB();
            string Line = "UPDATE `counterparty` SET `status` = 'пусто' WHERE `status` = '1';";
            // "UPDATE `counterparty` SET `status` = 'Грузоотправитель' WHERE `name` = '" + val + "'"
            MySqlCommand command = new MySqlCommand(Line, db.getConnection());

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("asd");
            }
            else
            {
                MessageBox.Show("cv");
            }

            db.closeConnection();
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            DB db = new DB();
            string Line = "UPDATE `counterparty` SET `objectName` = 'пусто' WHERE `objectName` = '1';";
            // "UPDATE `counterparty` SET `status` = 'Грузоотправитель' WHERE `name` = '" + val + "'"
            MySqlCommand command = new MySqlCommand(Line, db.getConnection());

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("asd");
            }
            else
            {
                MessageBox.Show("cv");
            }

            db.closeConnection();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            comboBox3.Text = "";
        }

        private void button11_Click_2(object sender, EventArgs e)
        {
            if (g)
            {
                int id = int.Parse(dgv["id", dgv.CurrentRow.Index].Value.ToString());
                string nom = dgv["nameCargo", dgv.CurrentRow.Index].Value.ToString();
                string cargoCount = dgv["numberNom", dgv.CurrentRow.Index].Value.ToString();
                string date = dgv["dateDocDriver", dgv.CurrentRow.Index].Value.ToString();
                string from = dgv["fromDounterparty", dgv.CurrentRow.Index].Value.ToString();
                string number = dgv["numberDocDriver", dgv.CurrentRow.Index].Value.ToString();
                string status = dgv["status", dgv.CurrentRow.Index].Value.ToString();
                //MessageBox.Show(nom + cargoCount + date + from + number);
                DB db = new DB();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `request` WHERE `id` = '" + id + "';", db.getConnection());
                MySqlDataReader myReader;
                int count = 0;
                ProxyForDel[] prx = new ProxyForDel[10];
                for(int i = 0; i < 10; i++)
                {
                    prx[i] = new ProxyForDel();
                }
                try
                {
                    db.openConnection();
                    myReader = command.ExecuteReader();
                    while (myReader.Read())
                    {
                        prx[count].nameCargo = myReader.GetString("nameCargo");
                        prx[count].cargoCount = myReader.GetString("numberNomenclature");
                        prx[count].date = myReader.GetString("dateDocDriver");
                        prx[count].fromCounter = myReader.GetString("fromCounterparty");
                        prx[count].numberDoc = myReader.GetString("numberDocDriver");
                        count++;
                    }
                    myReader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                db.closeConnection();
                bool del = false;
                if (count > 1)
                {
                    DialogResult result = MessageBox.Show(
                        "Удалить все заявки с таким номером?",
                        "Сообщение",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1
                        //MessageBoxOptions.DefaultDesktopOnly
                        );

                    if (result == DialogResult.Yes)
                    {
                        if (status.Equals("Назначена") || status.Equals("Исполнена"))
                        {
                            for (int p = 0; p < count; p++)
                            {
                                string[] a = prx[p].date.Split(' ');
                                string[] b = a[0].Split('.');
                                prx[p].date = b[2] + "-" + b[1] + "-" + b[0];
                                MySqlCommand cmdd = new MySqlCommand("SELECT * FROM `proxy` WHERE `date` = '" + prx[p].date + "' AND `counterparty` = '" + prx[p].fromCounter + "' AND `numberDoc` = '" + prx[p].numberDoc + "' AND `nameCargo` = '" + prx[p].nameCargo + "';", db.getConnection());
                                string forChange = "";
                                try
                                {
                                    db.openConnection();
                                    MySqlDataReader read = cmdd.ExecuteReader();
                                    while (read.Read())
                                    {
                                        forChange = read.GetString("change");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                db.closeConnection();
                                string tarChange = (double.Parse(forChange) + double.Parse(prx[p].cargoCount)).ToString();
                                cmdd = new MySqlCommand("UPDATE `proxy` SET `change` = '" + tarChange + "' WHERE `date` = '" + prx[p].date + "' AND `counterparty` = '" + prx[p].fromCounter + "' AND `numberDoc` = '" + prx[p].numberDoc + "' AND `nameCargo` = '" + prx[p].nameCargo + "';", db.getConnection());
                                db.openConnection();
                                cmdd.ExecuteNonQuery();
                                db.closeConnection();

                                if (status.Equals("Исполнена"))
                                {
                                    if (dgv[1, p].Value.Equals("Поставка"))
                                    {
                                        MySqlCommand comand = new MySqlCommand("SELECT * FROM `product` WHERE `name` = '" + dgv[9, p].Value + "';", db.getConnection());
                                        //MessageBox.Show("SELECT * FROM `product` WHERE `name` = '" + dataGridView1[0, p].Value + "';");
                                        MySqlDataReader myReader2;
                                        double change = 0;
                                        try
                                        {
                                            db.openConnection();
                                            myReader2 = comand.ExecuteReader();

                                            while (myReader2.Read())
                                            {
                                                change = double.Parse(myReader2.GetString("count"));
                                                change += double.Parse(dgv[10, p].Value.ToString());
                                            }
                                            myReader2.Close();
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                        db.closeConnection();

                                        comand = new MySqlCommand("UPDATE `product` SET `count` = '" + change.ToString() + "' WHERE `name` = '" + dgv[9, p].Value + "';", db.getConnection());
                                        //MessageBox.Show("UPDATE `product` SET `count` = '" + change.ToString() + "' WHERE `name` = '" + dataGridView1[0, p].Value + "';");
                                        db.openConnection();

                                        comand.ExecuteNonQuery();

                                        db.closeConnection();
                                    }
                                }
                            }
                        }
                        MySqlCommand cmd = new MySqlCommand("DELETE FROM `request` WHERE `id` = '" + id + "';", db.getConnection());
                        db.openConnection();
                        cmd.ExecuteNonQuery();
                        db.closeConnection();
                        del = true;
                        MySqlCommand com = new MySqlCommand("DELETE FROM `operations` WHERE `operation` = 'Заявка №" + id + "';", db.getConnection());

                        //string[] fordate = dateTimePicker1.Value.ToString().Split(' ');
                        db.openConnection();

                        com.ExecuteNonQuery();

                        db.closeConnection();
                    }
                    else
                    {
                        if (result == DialogResult.No)
                        {
                            MySqlCommand cmd = new MySqlCommand("DELETE FROM `request` WHERE `id` = '" + id + "' AND `nameCargo` = '" + nom + "';", db.getConnection());
                            db.openConnection();
                            cmd.ExecuteNonQuery();
                            db.closeConnection();

                            MySqlCommand com = new MySqlCommand("DELETE FROM `operations` WHERE `operation` = 'Заявка №" + id + "' AND `product` = '" + nom + "';", db.getConnection());

                            //string[] fordate = dateTimePicker1.Value.ToString().Split(' ');
                            db.openConnection();

                            com.ExecuteNonQuery();

                            db.closeConnection();

                            del = true;
                            //&& (status.Equals("Назначена") || status.Equals("Исполнена"))
                            string[] frd = date.Split('.');
                            date = frd[2] + "-" + frd[1] + "-" + frd[0];
                            if (!nom.Equals("пусто") && (status.Equals("Назначена") || status.Equals("Исполнена")))
                            {
                                string nomen = "";
                                MySqlCommand cmdd = new MySqlCommand("SELECT * FROM `proxy` WHERE `date` = '" + date + "' AND `counterparty` = '" + from + "' AND `numberDoc` = '" + number + "' AND `nameCargo` = '" + nom + "';", db.getConnection());
                                try
                                {
                                    db.openConnection();
                                    MySqlDataReader read = cmdd.ExecuteReader();
                                    while (read.Read())
                                    {
                                        nomen = read.GetString("change");
                                    }
                                    read.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                db.closeConnection();
                                string changeNew = (double.Parse(nomen) + double.Parse(cargoCount)).ToString();
                                string query = "UPDATE `proxy` SET `change` = '" + changeNew + "' WHERE `date` = '" + date + "' AND `counterparty` = '" + from + "' AND `numberDoc` = '" + number + "' AND `nameCargo` = '" + nom + "';";
                                MySqlCommand commandd = new MySqlCommand(query, db.getConnection());
                                db.openConnection();
                                commandd.ExecuteNonQuery();
                                db.closeConnection();
                            }
                            if (status.Equals("Исполнена"))
                            {
                                if (dgv[1, dgv.CurrentRow.Index].Value.Equals("Поставка"))
                                {
                                    MySqlCommand comand = new MySqlCommand("SELECT * FROM `product` WHERE `name` = '" + dgv[9, dgv.CurrentRow.Index].Value + "';", db.getConnection());
                                    //MessageBox.Show("SELECT * FROM `product` WHERE `name` = '" + dataGridView1[0, p].Value + "';");
                                    MySqlDataReader myReader2;
                                    double change = 0;
                                    try
                                    {
                                        db.openConnection();
                                        myReader2 = comand.ExecuteReader();

                                        while (myReader2.Read())
                                        {
                                            change = double.Parse(myReader2.GetString("count"));
                                            change += double.Parse(dgv[10, dgv.CurrentRow.Index].Value.ToString());
                                        }
                                        myReader2.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    db.closeConnection();

                                    comand = new MySqlCommand("UPDATE `product` SET `count` = '" + change.ToString() + "' WHERE `name` = '" + dgv[9, dgv.CurrentRow.Index].Value + "';", db.getConnection());
                                    //MessageBox.Show("UPDATE `product` SET `count` = '" + change.ToString() + "' WHERE `name` = '" + dataGridView1[0, p].Value + "';");
                                    db.openConnection();

                                    comand.ExecuteNonQuery();

                                    db.closeConnection();
                                }
                            }
                        }
                    }

                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM `request` WHERE `id` = '" + id + "';", db.getConnection());
                    db.openConnection();
                    cmd.ExecuteNonQuery();
                    db.closeConnection();
                    MySqlCommand com = new MySqlCommand("DELETE FROM `operations` WHERE `operation` = 'Заявка №" + id + "';", db.getConnection());

                    //string[] fordate = dateTimePicker1.Value.ToString().Split(' ');
                    db.openConnection();

                    com.ExecuteNonQuery();

                    db.closeConnection();
                    del = true;
                    string nomen = "";
                    string[] frd = date.Split('.');
                    date = frd[2] + "-" + frd[1] + "-" + frd[0];
                    //MessageBox.Show(nom + cargoCount + date + from + number);
                    if (!nom.Equals(" ") && !cargoCount.Equals(" ") && !date.Equals(" ") && !from.Equals(" ") && !number.Equals(" ") && (status.Equals("Назначена") || status.Equals("Исполнена")))
                    {
                        MySqlCommand cmdd = new MySqlCommand("SELECT * FROM `proxy` WHERE `date` = '" + date + "' AND `counterparty` = '" + from + "' AND `numberDoc` = '" + number + "' AND `nameCargo` = '" + nom + "';", db.getConnection());
                        //MessageBox.Show("SELECT * FROM `proxy` WHERE `date` = '" + date + "' AND `counterparty` = '" + from + "' AND `numberDoc` = '" + number + "' AND `nameCargo` = '" + nameCargo + "';");
                        try
                        {

                            db.openConnection();
                            MySqlDataReader read = cmdd.ExecuteReader();
                            while (read.Read())
                            {
                                nomen = read.GetString("change");
                                //MessageBox.Show(nomen);
                            }
                            read.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        db.closeConnection();
                        string changeNew = (double.Parse(nomen) + double.Parse(cargoCount)).ToString();
                        string query = "UPDATE `proxy` SET `change` = '" + changeNew + "' WHERE `date` = '" + date + "' AND `counterparty` = '" + from + "' AND `numberDoc` = '" + number + "' AND `nameCargo` = '" + nom + "';";
                        MySqlCommand commandd = new MySqlCommand(query, db.getConnection());
                        db.openConnection();
                        commandd.ExecuteNonQuery();
                        db.closeConnection();
                    }
                    if (status.Equals("Исполнена"))
                    {
                        if (dgv[1, dgv.CurrentRow.Index].Value.Equals("Поставка"))
                        {
                            MySqlCommand comand = new MySqlCommand("SELECT * FROM `product` WHERE `name` = '" + dgv[9, dgv.CurrentRow.Index].Value + "';", db.getConnection());
                            //MessageBox.Show("SELECT * FROM `product` WHERE `name` = '" + dataGridView1[0, p].Value + "';");
                            MySqlDataReader myReader2;
                            //MessageBox.Show("adasd");
                            double change = 0;
                            try
                            {
                                db.openConnection();
                                myReader2 = comand.ExecuteReader();

                                while (myReader2.Read())
                                {
                                    change = double.Parse(myReader2.GetString("count"));
                                    change += double.Parse(dgv[10, dgv.CurrentRow.Index].Value.ToString());
                                }
                                myReader2.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            db.closeConnection();

                            comand = new MySqlCommand("UPDATE `product` SET `count` = '" + change.ToString() + "' WHERE `name` = '" + dgv[9, dgv.CurrentRow.Index].Value + "';", db.getConnection());
                            //MessageBox.Show("UPDATE `product` SET `count` = '" + change.ToString() + "' WHERE `name` = '" + dataGridView1[0, p].Value + "';");
                            db.openConnection();

                            comand.ExecuteNonQuery();

                            db.closeConnection();
                        }
                    }
                }
                if (del)
                {
                    this.Show();
                    comboBox1.Text = "";
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                    textBox1.Text = "";
                    label7.Hide();
                    textBox1.Hide();/*
                dateTimePicker1.ResetText();
                dateTimePicker2.ResetText();*/

                    dgv.Rows.Clear();
                    dgv.Columns.Clear();
                    string time = dateTimePicker1.Value.ToString();
                    string[] asd = time.Split(' ');
                    string[] zxc = asd[0].Split('.');
                    string answer1 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];

                    time = dateTimePicker2.Value.ToString();
                    asd = null;
                    asd = time.Split(' ');
                    zxc = null;
                    zxc = asd[0].Split('.');
                    string answer2 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];

                    //string Line = "SELECT * FROM `request` WHERE `docDate` = '" + answer + "';";
                    bool fstatus = false;
                    bool fbuyer = false;
                    bool fobject = false;

                    string Line = "";// = "SELECT * FROM `request` WHERE `status` = '" + comboBox1.Text + "';";
                    if (!comboBox1.Text.Equals(""))
                    {
                        fstatus = true;
                    }
                    if (!comboBox2.Text.Equals(""))
                    {
                        fbuyer = true;
                    }
                    if (!comboBox3.Text.Equals(""))
                    {
                        fobject = true;
                    }

                    if (fstatus && fobject)
                    {
                        Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `status` = '" + comboBox1.Text + "' AND `object` = '" + comboBox3.Text + "' ORDER BY `id` ASC;";
                    }
                    else
                    {
                        if (fstatus && fbuyer)
                        {
                            Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `status` = '" + comboBox1.Text + "' AND `buyer` = '" + comboBox2.Text + "' ORDER BY `id` ASC;";
                        }
                        else
                        {
                            if (fstatus)
                            {
                                Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `status` = '" + comboBox1.Text + "' ORDER BY `id` ASC;";
                            }
                            else
                            {
                                if (fbuyer)
                                {
                                    Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `buyer` = '" + comboBox2.Text + "' ORDER BY `id` ASC;";
                                }
                                else
                                {
                                    if (fobject)
                                    {
                                        Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `object` = '" + comboBox3.Text + "' ORDER BY `id` ASC;";
                                    }
                                }
                            }
                        }
                    }
                    if (!fstatus && !fbuyer && !fobject)
                    {
                        Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' ORDER BY `id` ASC;";
                    }
                    //Line = "SELECT * FROM `request` WHERE `docDate` = '" + answer1 + "';";

                    backLine = Line;

                    command = new MySqlCommand(Line, db.getConnection());
                    db.openConnection();
                    object obj = command.ExecuteScalar();
                    /*DataTable tbl = new DataTable();
                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(ds);
                    dgv.DataSource = ds.Tables[0];*/
                    int idlast = 0;
                    int idmorelast = 0;

                    if (obj != null)
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter();
                        Work[] works = new Work[100000];
                        for (int i = 0; i < works.Length; i++)
                        {
                            works[i] = new Work();
                            works[i].hour = 23;
                            works[i].minute = 59;
                            works[i].traffic = "пусто";
                        }
                        int itter = 0;
                        bool f = false;
                        try
                        {
                            //db.openConnection();
                            myReader = command.ExecuteReader();

                            while (myReader.Read())
                            {
                                idlast = int.Parse(myReader.GetString("id"));
                                if (idlast == idmorelast)
                                {
                                    idmorelast = idlast;
                                    continue;
                                }
                                for (int i = 0; i < itter; i++)
                                {
                                    f = false;
                                    if (myReader.GetString("buyer").Equals(works[i].buyer) && myReader.GetString("sender").Equals(works[i].sender) && myReader.GetString("object").Equals(works[i].objectBuyer))
                                    {
                                        if (myReader.GetString("status").Equals("В работе"))
                                        {
                                            works[i].inWork++;
                                        }
                                        if (myReader.GetString("status").Equals("Назначена"))
                                        {
                                            works[i].point++;
                                        }
                                        if (myReader.GetString("status").Equals("Исполнена") || myReader.GetString("status").Equals("Оплачена"))
                                        {
                                            works[i].done++;
                                        }
                                        f = true;
                                        idmorelast = idlast;
                                        string timeAccept = myReader.GetString("timeAccept");
                                        string[] timeAcceptt = timeAccept.Split(':');
                                        if (myReader.GetString("status").Equals("В работе"))
                                        {
                                            if (works[i].hour > int.Parse(timeAcceptt[0]))
                                            {
                                                works[i].hour = int.Parse(timeAcceptt[0]);
                                                works[i].minute = int.Parse(timeAcceptt[1]);
                                            }
                                            else
                                            {
                                                if (works[i].minute > int.Parse(timeAcceptt[1]))
                                                {
                                                    works[i].hour = int.Parse(timeAcceptt[0]);
                                                    works[i].minute = int.Parse(timeAcceptt[1]);
                                                }
                                            }
                                        }
                                        if (works[i].traffic.Equals("пусто"))
                                        {
                                            if (!myReader.GetString("traffic").Equals("пусто"))
                                            {
                                                works[i].traffic = myReader.GetString("traffic");
                                            }
                                        }
                                        break;
                                    }
                                    idmorelast = idlast;
                                }
                                if (!f)
                                {
                                    works[itter].traffic = myReader.GetString("traffic");
                                    works[itter].buyer = myReader.GetString("buyer");
                                    works[itter].sender = myReader.GetString("sender");
                                    works[itter].objectBuyer = myReader.GetString("object");
                                    if (myReader.GetString("status").Equals("В работе"))
                                    {
                                        works[itter].inWork++;
                                    }
                                    if (myReader.GetString("status").Equals("Назначена"))
                                    {
                                        works[itter].point++;
                                    }
                                    if (myReader.GetString("status").Equals("Исполнена") || myReader.GetString("status").Equals("Оплачена"))
                                    {
                                        works[itter].done++;
                                    }
                                    idmorelast = idlast;
                                    string timeAccept = myReader.GetString("timeAccept");
                                    string[] timeAcceptt = timeAccept.Split(':');
                                    //MessageBox.Show(timeAcceptt[0]);
                                    if (myReader.GetString("status").Equals("В работе"))
                                    {
                                        if (works[itter].hour > int.Parse(timeAcceptt[0]))
                                        {
                                            works[itter].hour = int.Parse(timeAcceptt[0]);
                                            works[itter].minute = int.Parse(timeAcceptt[1]);
                                        }
                                        else
                                        {
                                            if (works[itter].minute > int.Parse(timeAcceptt[1]))
                                            {
                                                works[itter].hour = int.Parse(timeAcceptt[0]);
                                                works[itter].minute = int.Parse(timeAcceptt[1]);
                                            }
                                        }
                                    }
                                    itter++;
                                }
                            }
                            for (int i = 0; i < itter; i++)
                            {
                                works[i].accept = works[i].inWork + works[i].point + works[i].done;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        db.closeConnection();


                        dgv.Columns.AddRange(
                        new DataGridViewTextBoxColumn() { Name = "timeAccept", HeaderText = "Время поставки на объект" },
                        new DataGridViewTextBoxColumn() { Name = "traffic", HeaderText = "Вид транспорта" },
                        new DataGridViewTextBoxColumn() { Name = "buyeer", HeaderText = "Покупатель/заказчик" },
                        new DataGridViewTextBoxColumn() { Name = "sendeerr", HeaderText = "Грузоотправитель" },
                        new DataGridViewTextBoxColumn() { Name = "objectBuyeer", HeaderText = "Объект поставки" },
                        new DataGridViewTextBoxColumn() { Name = "accept", HeaderText = "Принятые заявки" },
                        new DataGridViewTextBoxColumn() { Name = "inWork", HeaderText = "Неназначенные заявки" },
                        new DataGridViewTextBoxColumn() { Name = "point", HeaderText = "Назначенные заявки" },
                        new DataGridViewTextBoxColumn() { Name = "done", HeaderText = "Выпоненные" });
                        dgv.Rows.Clear();
                        int sumAccept = 0;
                        int sumInWork = 0;
                        int sumPoint = 0;
                        int sumDone = 0;
                        int it = 0;
                        for (int i = 0; i < itter; i++)
                        {
                            dgv.Rows.Add();
                            if (works[i].hour == 23 && works[i].minute == 59)
                            {
                                if (works[i].accept == works[i].done)
                                {
                                    dgv["timeAccept", i].Value = "Все исполнены";
                                }
                                else
                                {
                                    dgv["timeAccept", i].Value = "Все назначены";
                                }
                            }
                            else
                            {
                                if (works[i].minute >= 0 && works[i].minute < 10)
                                {
                                    dgv["timeAccept", i].Value = works[i].hour.ToString() + ":0" + works[i].minute.ToString();
                                }
                                else
                                {
                                    dgv["timeAccept", i].Value = works[i].hour.ToString() + ":" + works[i].minute.ToString();
                                }
                            }
                            dgv["traffic", i].Value = works[i].traffic.ToString();
                            dgv["buyeer", i].Value = works[i].buyer.ToString();
                            dgv["sendeerr", i].Value = works[i].sender.ToString();
                            dgv["objectBuyeer", i].Value = works[i].objectBuyer.ToString();
                            dgv["accept", i].Value = works[i].accept.ToString();
                            dgv["inWork", i].Value = works[i].inWork.ToString();
                            dgv["point", i].Value = works[i].point.ToString();
                            dgv["done", i].Value = works[i].done.ToString();
                            if (works[i].point != 0)
                            {
                                for (int j = 0; j < dgv.ColumnCount; j++)
                                {
                                    dgv[j, i].Style.BackColor = Color.Khaki;
                                }
                                //dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;
                            }
                            if (works[i].inWork != 0)
                            {
                                for (int j = 0; j < dgv.ColumnCount; j++)
                                {
                                    dgv[j, i].Style.BackColor = Color.LightPink;
                                }
                                //dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;
                            }
                            if (works[i].accept == works[i].point)
                            {
                                for (int j = 0; j < dgv.ColumnCount; j++)
                                {
                                    dgv[j, i].Style.BackColor = Color.Khaki;
                                }
                                //dgv.Rows[i].DefaultCellStyle.BackColor = Color.Khaki;
                            }
                            if (works[i].point != 0)
                            {
                                dgv["point", i].Style.BackColor = Color.Khaki;
                            }
                            if (works[i].accept == works[i].done)
                            {
                                for (int j = 0; j < dgv.ColumnCount; j++)
                                {
                                    dgv[j, i].Style.BackColor = Color.PaleGreen;
                                }
                                //dgv.Rows[i].DefaultCellStyle.BackColor = Color.PaleGreen;
                            }
                            if (works[i].done != 0)
                            {
                                dgv["done", i].Style.BackColor = Color.PaleGreen;
                            }
                            sumAccept = sumAccept + works[i].accept;
                            sumInWork = sumInWork + works[i].inWork;
                            sumPoint = sumPoint + works[i].point;
                            sumDone = sumDone + works[i].done;
                            it = i;
                        }
                        dgv[0, it + 1].Value = "Всего";
                        dgv["accept", it + 1].Value = sumAccept;
                        dgv["inWork", it + 1].Value = sumInWork;
                        dgv["point", it + 1].Value = sumPoint;
                        dgv["done", it + 1].Value = sumDone;
                        dgv.Rows[it + 1].DefaultCellStyle.BackColor = Color.LightGray;
                        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                        /*for(int i = 0; i < itter; i++)
                        {
                            MessageBox.Show(works[i].buyer + works[i].sender + works[i].objectBuyer + works[i].accept + works[i].point + works[i].done);
                        }*/
                        g = false;

                        db.closeConnection();
                    }
                    g = false;
                }
            }
            else
            {
                MessageBox.Show("Ячейка не выбрана");
            }





            /*bool IsTheSameCellValue(int column, int row)
            {
                DataGridViewCell cell1 = dgv[column, row];
                DataGridViewCell cell2 = dgv[column, row - 1];
                if (cell1.Value == null || cell2.Value == null)
                {
                    return false;
                }
                return cell1.Value.ToString() == cell2.Value.ToString();
            }

            private void dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
            {
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
                if (e.RowIndex < 1 || e.ColumnIndex < 0)
                    return;
                if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
                {
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                }
                else
                {
                    e.AdvancedBorderStyle.Top = dgv.AdvancedCellBorderStyle.Top;
                }
            }*/
        }

        private void button16_Click_2(object sender, EventArgs e)
        {
            if (!backLine.Equals("пусто"))
            {
                dgv.Rows.Clear();
                dgv.Columns.Clear();
                DB db = new DB();
                MySqlCommand command = new MySqlCommand(backLine, db.getConnection());
                db.openConnection();
                object obj = command.ExecuteScalar();
                /*DataTable tbl = new DataTable();
                DataSet ds = new DataSet();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(ds);
                dgv.DataSource = ds.Tables[0];*/
                int idlast = 0;
                int idmorelast = 0;

                if (obj != null)
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlDataReader myReader;
                    Work[] works = new Work[100000];
                    for (int i = 0; i < works.Length; i++)
                    {
                        works[i] = new Work();
                        works[i].hour = 23;
                        works[i].minute = 59;
                        works[i].traffic = "пусто";
                    }
                    int itter = 0;
                    bool f = false;
                    try
                    {
                        //db.openConnection();
                        myReader = command.ExecuteReader();

                        while (myReader.Read())
                        {
                            idlast = int.Parse(myReader.GetString("id"));
                            if (idlast == idmorelast)
                            {
                                idmorelast = idlast;
                                continue;
                            }
                            for (int i = 0; i < itter; i++)
                            {
                                f = false;
                                if (myReader.GetString("buyer").Equals(works[i].buyer) && myReader.GetString("sender").Equals(works[i].sender) && myReader.GetString("object").Equals(works[i].objectBuyer))
                                {
                                    if (myReader.GetString("status").Equals("В работе"))
                                    {
                                        works[i].inWork++;
                                    }
                                    if (myReader.GetString("status").Equals("Назначена"))
                                    {
                                        works[i].point++;
                                    }
                                    if (myReader.GetString("status").Equals("Исполнена") || myReader.GetString("status").Equals("Оплачена"))
                                    {
                                        works[i].done++;
                                    }
                                    f = true;
                                    idmorelast = idlast;
                                    works[i].deal = myReader.GetString("deal");
                                    string timeAccept = myReader.GetString("timeAccept");
                                    string[] timeAcceptt = timeAccept.Split(':');
                                    if (myReader.GetString("status").Equals("В работе"))
                                    {
                                        if (works[i].hour > int.Parse(timeAcceptt[0]))
                                        {
                                            works[i].hour = int.Parse(timeAcceptt[0]);
                                            works[i].minute = int.Parse(timeAcceptt[1]);
                                        }
                                        else
                                        {
                                            if (works[i].minute > int.Parse(timeAcceptt[1]))
                                            {
                                                works[i].hour = int.Parse(timeAcceptt[0]);
                                                works[i].minute = int.Parse(timeAcceptt[1]);
                                            }
                                        }
                                    }
                                    if (works[i].traffic.Equals("пусто"))
                                    {
                                        if (!myReader.GetString("traffic").Equals("пусто"))
                                        {
                                            works[i].traffic = myReader.GetString("traffic");
                                        }
                                    }
                                    break;
                                }
                                idmorelast = idlast;
                            }
                            if (!f)
                            {
                                works[itter].deal = myReader.GetString("deal");
                                works[itter].traffic = myReader.GetString("traffic");
                                works[itter].buyer = myReader.GetString("buyer");
                                works[itter].sender = myReader.GetString("sender");
                                works[itter].objectBuyer = myReader.GetString("object");
                                if (myReader.GetString("status").Equals("В работе"))
                                {
                                    works[itter].inWork++;
                                }
                                if (myReader.GetString("status").Equals("Назначена"))
                                {
                                    works[itter].point++;
                                }
                                if (myReader.GetString("status").Equals("Исполнена") || myReader.GetString("status").Equals("Оплачена"))
                                {
                                    works[itter].done++;
                                }
                                idmorelast = idlast;
                                string timeAccept = myReader.GetString("timeAccept");
                                string[] timeAcceptt = timeAccept.Split(':');
                                //MessageBox.Show(timeAcceptt[0]);
                                if (myReader.GetString("status").Equals("В работе"))
                                {
                                    if (works[itter].hour > int.Parse(timeAcceptt[0]))
                                    {
                                        works[itter].hour = int.Parse(timeAcceptt[0]);
                                        works[itter].minute = int.Parse(timeAcceptt[1]);
                                    }
                                    else
                                    {
                                        if (works[itter].minute > int.Parse(timeAcceptt[1]))
                                        {
                                            works[itter].hour = int.Parse(timeAcceptt[0]);
                                            works[itter].minute = int.Parse(timeAcceptt[1]);
                                        }
                                    }
                                }
                                itter++;
                            }
                        }
                        for (int i = 0; i < itter; i++)
                        {
                            works[i].accept = works[i].inWork + works[i].point + works[i].done;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                    db.closeConnection();
                    dgv.Columns.AddRange(
                    new DataGridViewTextBoxColumn() { Name = "deal", HeaderText = "Деятельность"},
                    new DataGridViewTextBoxColumn() { Name = "timeAccept", HeaderText = "Время поставки на объект" },
                    new DataGridViewTextBoxColumn() { Name = "traffic", HeaderText = "Вид транспорта"},
                    new DataGridViewTextBoxColumn() { Name = "buyeer", HeaderText = "Покупатель/заказчик" },
                    new DataGridViewTextBoxColumn() { Name = "sendeerr", HeaderText = "Грузоотправитель" },
                    new DataGridViewTextBoxColumn() { Name = "objectBuyeer", HeaderText = "Объект поставки" },
                    new DataGridViewTextBoxColumn() { Name = "accept", HeaderText = "Принятые заявки" },
                    new DataGridViewTextBoxColumn() { Name = "inWork", HeaderText = "Неназначенные заявки" },
                    new DataGridViewTextBoxColumn() { Name = "point", HeaderText = "Назначенные заявки" },
                    new DataGridViewTextBoxColumn() { Name = "done", HeaderText = "Выпоненные" });
                    dgv.Rows.Clear();
                    int sumAccept = 0;
                    int sumInWork = 0;
                    int sumPoint = 0;
                    int sumDone = 0;
                    int it = 0;
                    for (int i = 0; i < itter; i++)
                    {
                        dgv.Rows.Add();
                        dgv["deal", i].Value = works[i].deal;
                        if (works[i].hour == 23 && works[i].minute == 59)
                        {
                            if (works[i].accept == works[i].done)
                            {
                                dgv["timeAccept", i].Value = "Все исполнены";
                            }
                            else
                            {
                                dgv["timeAccept", i].Value = "Все назначены";
                            }
                        }
                        else
                        {
                            if (works[i].minute >= 0 && works[i].minute < 10)
                            {
                                dgv["timeAccept", i].Value = works[i].hour.ToString() + ":0" + works[i].minute.ToString();
                            }
                            else
                            {
                                dgv["timeAccept", i].Value = works[i].hour.ToString() + ":" + works[i].minute.ToString();
                            }
                        }
                        dgv["traffic", i].Value = works[i].traffic.ToString();
                        dgv["buyeer", i].Value = works[i].buyer.ToString();
                        dgv["sendeerr", i].Value = works[i].sender.ToString();
                        dgv["objectBuyeer", i].Value = works[i].objectBuyer.ToString();
                        dgv["accept", i].Value = works[i].accept.ToString();
                        dgv["inWork", i].Value = works[i].inWork.ToString();
                        dgv["point", i].Value = works[i].point.ToString();
                        dgv["done", i].Value = works[i].done.ToString();
                        if (works[i].point != 0)
                        {
                            for (int j = 0; j < dgv.ColumnCount; j++)
                            {
                                dgv[j, i].Style.BackColor = Color.Khaki;
                            }
                            //dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;
                        }
                        if (works[i].inWork != 0)
                        {
                            for (int j = 0; j < dgv.ColumnCount; j++)
                            {
                                dgv[j, i].Style.BackColor = Color.LightPink;
                            }
                            //dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;
                        }
                        if (works[i].accept == works[i].point)
                        {
                            for (int j = 0; j < dgv.ColumnCount; j++)
                            {
                                dgv[j, i].Style.BackColor = Color.Khaki;
                            }
                            //dgv.Rows[i].DefaultCellStyle.BackColor = Color.Khaki;
                        }
                        if (works[i].point != 0)
                        {
                            dgv["point", i].Style.BackColor = Color.Khaki;
                        }
                        if (works[i].accept == works[i].done)
                        {
                            for (int j = 0; j < dgv.ColumnCount; j++)
                            {
                                dgv[j, i].Style.BackColor = Color.PaleGreen;
                            }
                            //dgv.Rows[i].DefaultCellStyle.BackColor = Color.PaleGreen;
                        }
                        if (works[i].done != 0)
                        {
                            dgv["done", i].Style.BackColor = Color.PaleGreen;
                        }
                        sumAccept = sumAccept + works[i].accept;
                        sumInWork = sumInWork + works[i].inWork;
                        sumPoint = sumPoint + works[i].point;
                        sumDone = sumDone + works[i].done;
                        it = i;
                    }
                    dgv[0, it + 1].Value = "Всего";
                    dgv["accept", it + 1].Value = sumAccept;
                    dgv["inWork", it + 1].Value = sumInWork;
                    dgv["point", it + 1].Value = sumPoint;
                    dgv["done", it + 1].Value = sumDone;
                    dgv.Rows[it + 1].DefaultCellStyle.BackColor = Color.LightGray;
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                    /*for(int i = 0; i < itter; i++)
                    {
                        MessageBox.Show(works[i].buyer + works[i].sender + works[i].objectBuyer + works[i].accept + works[i].point + works[i].done);
                    }*/
                    g = false;
                }
            }
            //backLine = "пусто";
            textBox1.Hide();
            label7.Hide();
            button16.Hide();
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox1.Text = "";
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            DB db = new DB();
            comboBox5.Items.Clear();
            string Query = "SELECT * FROM `cars` WHERE `contractor` = '" + comboBox4.Text.ToString() + "' ORDER BY `name` ASC;";
            //string Query = "(SELECT * FROM `cars` WHERE `contractor` = '" + comboBox7.Text.ToString() + "' ORDER BY `name` ASC) ORDER BY `count` desc;";
            MySqlCommand cmdDataBase2 = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader2;

            try
            {
                db.openConnection();
                myReader2 = cmdDataBase2.ExecuteReader();

                while (myReader2.Read())
                {
                    string objName = myReader2.GetString("name");
                    if (objName.Equals(""))
                    {
                        continue;
                    }
                    comboBox5.Items.Add(objName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            button16.Hide();
            textBox1.Hide();
            label7.Hide();
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            string time = dateTimePicker1.Value.ToString();
            string[] asd = time.Split(' ');
            string[] zxc = asd[0].Split('.');
            string answer1 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];

            time = dateTimePicker2.Value.ToString();
            asd = null;
            asd = time.Split(' ');
            zxc = null;
            zxc = asd[0].Split('.');
            string answer2 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];
            string Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `cars` = '" + comboBox5.Text + "' ORDER BY `id` ASC;";

            backLine = Line;

            DB db = new DB();
            MySqlCommand command = new MySqlCommand(Line, db.getConnection());
            db.openConnection();
            object obj = command.ExecuteScalar();
            /*DataTable tbl = new DataTable();
            DataSet ds = new DataSet();
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(ds);
            dgv.DataSource = ds.Tables[0];*/
            int idlast = 0;
            int idmorelast = 0;

            if (obj != null)
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlDataReader myReader;
                Work[] works = new Work[100000];
                for (int i = 0; i < works.Length; i++)
                {
                    works[i] = new Work();
                    works[i].hour = 23;
                    works[i].minute = 59;
                    works[i].traffic = "пусто";
                }
                int itter = 0;
                bool f = false;
                try
                {
                    //db.openConnection();
                    myReader = command.ExecuteReader();

                    while (myReader.Read())
                    {
                        idlast = int.Parse(myReader.GetString("id"));
                        if (idlast == idmorelast)
                        {
                            idmorelast = idlast;
                            continue;
                        }
                        for (int i = 0; i < itter; i++)
                        {
                            f = false;
                            if (myReader.GetString("buyer").Equals(works[i].buyer) && myReader.GetString("sender").Equals(works[i].sender) && myReader.GetString("object").Equals(works[i].objectBuyer))
                            {
                                if (myReader.GetString("status").Equals("В работе"))
                                {
                                    works[i].inWork++;
                                }
                                if (myReader.GetString("status").Equals("Назначена"))
                                {
                                    works[i].point++;
                                }
                                if (myReader.GetString("status").Equals("Исполнена") || myReader.GetString("status").Equals("Оплачена"))
                                {
                                    works[i].done++;
                                }
                                f = true;
                                idmorelast = idlast;
                                string timeAccept = myReader.GetString("timeAccept");
                                string[] timeAcceptt = timeAccept.Split(':');
                                if (myReader.GetString("status").Equals("В работе"))
                                {
                                    if (works[i].hour > int.Parse(timeAcceptt[0]))
                                    {
                                        works[i].hour = int.Parse(timeAcceptt[0]);
                                        works[i].minute = int.Parse(timeAcceptt[1]);
                                    }
                                    else
                                    {
                                        if (works[i].minute > int.Parse(timeAcceptt[1]))
                                        {
                                            works[i].hour = int.Parse(timeAcceptt[0]);
                                            works[i].minute = int.Parse(timeAcceptt[1]);
                                        }
                                    }
                                }
                                if (works[i].traffic.Equals("пусто"))
                                {
                                    if (!myReader.GetString("traffic").Equals("пусто"))
                                    {
                                        works[i].traffic = myReader.GetString("traffic");
                                    }
                                }
                                break;
                            }
                            idmorelast = idlast;
                        }
                        if (!f)
                        {
                            works[itter].traffic = myReader.GetString("traffic");
                            works[itter].buyer = myReader.GetString("buyer");
                            works[itter].sender = myReader.GetString("sender");
                            works[itter].objectBuyer = myReader.GetString("object");
                            if (myReader.GetString("status").Equals("В работе"))
                            {
                                works[itter].inWork++;
                            }
                            if (myReader.GetString("status").Equals("Назначена"))
                            {
                                works[itter].point++;
                            }
                            if (myReader.GetString("status").Equals("Исполнена") || myReader.GetString("status").Equals("Оплачена"))
                            {
                                works[itter].done++;
                            }
                            idmorelast = idlast;
                            string timeAccept = myReader.GetString("timeAccept");
                            string[] timeAcceptt = timeAccept.Split(':');
                            //MessageBox.Show(timeAcceptt[0]);
                            if (myReader.GetString("status").Equals("В работе"))
                            {
                                if (works[itter].hour > int.Parse(timeAcceptt[0]))
                                {
                                    works[itter].hour = int.Parse(timeAcceptt[0]);
                                    works[itter].minute = int.Parse(timeAcceptt[1]);
                                }
                                else
                                {
                                    if (works[itter].minute > int.Parse(timeAcceptt[1]))
                                    {
                                        works[itter].hour = int.Parse(timeAcceptt[0]);
                                        works[itter].minute = int.Parse(timeAcceptt[1]);
                                    }
                                }
                            }
                            itter++;
                        }
                    }
                    for (int i = 0; i < itter; i++)
                    {
                        works[i].accept = works[i].inWork + works[i].point + works[i].done;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                db.closeConnection();



                dgv.Columns.AddRange(
                new DataGridViewTextBoxColumn() { Name = "timeAccept", HeaderText = "Время поставки на объект" },
                new DataGridViewTextBoxColumn() { Name = "traffic", HeaderText = "Вид транспорта" },
                new DataGridViewTextBoxColumn() { Name = "buyeer", HeaderText = "Покупатель/заказчик" },
                new DataGridViewTextBoxColumn() { Name = "sendeerr", HeaderText = "Грузоотправитель" },
                new DataGridViewTextBoxColumn() { Name = "objectBuyeer", HeaderText = "Объект поставки" },
                new DataGridViewTextBoxColumn() { Name = "accept", HeaderText = "Принятые заявки" },
                new DataGridViewTextBoxColumn() { Name = "inWork", HeaderText = "Неназначенные заявки" },
                new DataGridViewTextBoxColumn() { Name = "point", HeaderText = "Назначенные заявки" },
                new DataGridViewTextBoxColumn() { Name = "done", HeaderText = "Выпоненные" });
                dgv.Rows.Clear();
                int sumAccept = 0;
                int sumInWork = 0;
                int sumPoint = 0;
                int sumDone = 0;
                int it = 0;
                for (int i = 0; i < itter; i++)
                {
                    dgv.Rows.Add();
                    if (works[i].hour == 23 && works[i].minute == 59)
                    {
                        if (works[i].accept == works[i].done)
                        {
                            dgv["timeAccept", i].Value = "Все исполнены";
                        }
                        else
                        {
                            dgv["timeAccept", i].Value = "Все назначены";
                        }
                    }
                    else
                    {
                        if (works[i].minute >= 0 && works[i].minute < 10)
                        {
                            dgv["timeAccept", i].Value = works[i].hour.ToString() + ":0" + works[i].minute.ToString();
                        }
                        else
                        {
                            dgv["timeAccept", i].Value = works[i].hour.ToString() + ":" + works[i].minute.ToString();
                        }
                    }
                    dgv["traffic", i].Value = works[i].traffic.ToString();
                    dgv["buyeer", i].Value = works[i].buyer.ToString();
                    dgv["sendeerr", i].Value = works[i].sender.ToString();
                    dgv["objectBuyeer", i].Value = works[i].objectBuyer.ToString();
                    dgv["accept", i].Value = works[i].accept.ToString();
                    dgv["inWork", i].Value = works[i].inWork.ToString();
                    dgv["point", i].Value = works[i].point.ToString();
                    dgv["done", i].Value = works[i].done.ToString();
                    if (works[i].point != 0)
                    {
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            dgv[j, i].Style.BackColor = Color.Khaki;
                        }
                        //dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;
                    }
                    if (works[i].inWork != 0)
                    {
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            dgv[j, i].Style.BackColor = Color.LightPink;
                        }
                        //dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;
                    }
                    if (works[i].accept == works[i].point)
                    {
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            dgv[j, i].Style.BackColor = Color.Khaki;
                        }
                        //dgv.Rows[i].DefaultCellStyle.BackColor = Color.Khaki;
                    }
                    if (works[i].point != 0)
                    {
                        dgv["point", i].Style.BackColor = Color.Khaki;
                    }
                    if (works[i].accept == works[i].done)
                    {
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            dgv[j, i].Style.BackColor = Color.PaleGreen;
                        }
                        //dgv.Rows[i].DefaultCellStyle.BackColor = Color.PaleGreen;
                    }
                    if (works[i].done != 0)
                    {
                        dgv["done", i].Style.BackColor = Color.PaleGreen;
                    }
                    sumAccept = sumAccept + works[i].accept;
                    sumInWork = sumInWork + works[i].inWork;
                    sumPoint = sumPoint + works[i].point;
                    sumDone = sumDone + works[i].done;
                    it = i;
                }
                dgv[0, it + 1].Value = "Всего";
                dgv["accept", it + 1].Value = sumAccept;
                dgv["inWork", it + 1].Value = sumInWork;
                dgv["point", it + 1].Value = sumPoint;
                dgv["done", it + 1].Value = sumDone;
                dgv.Rows[it + 1].DefaultCellStyle.BackColor = Color.LightGray;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                /*for(int i = 0; i < itter; i++)
                {
                    MessageBox.Show(works[i].buyer + works[i].sender + works[i].objectBuyer + works[i].accept + works[i].point + works[i].done);
                }*/
                g = false;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            button16.Hide();
            textBox1.Hide();
            label7.Hide();
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            string time = dateTimePicker1.Value.ToString();
            string[] asd = time.Split(' ');
            string[] zxc = asd[0].Split('.');
            string answer1 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];

            time = dateTimePicker2.Value.ToString();
            asd = null;
            asd = time.Split(' ');
            zxc = null;
            zxc = asd[0].Split('.');
            string answer2 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];
            string Line = "SELECT * FROM `request` WHERE `numberDocTrip` = '" + textBox2.Text + "';";

            backLine = Line;

            DB db = new DB();
            MySqlCommand command = new MySqlCommand(Line, db.getConnection());
            db.openConnection();
            object obj = command.ExecuteScalar();
            /*DataTable tbl = new DataTable();
            DataSet ds = new DataSet();
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(ds);
            dgv.DataSource = ds.Tables[0];*/
            int idlast = 0;
            int idmorelast = 0;

            if (obj != null)
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlDataReader myReader;
                Work[] works = new Work[100000];
                for (int i = 0; i < works.Length; i++)
                {
                    works[i] = new Work();
                    works[i].hour = 23;
                    works[i].minute = 59;
                    works[i].traffic = "пусто";
                }
                int itter = 0;
                bool f = false;
                try
                {
                    //db.openConnection();
                    myReader = command.ExecuteReader();

                    while (myReader.Read())
                    {   
                        idlast = int.Parse(myReader.GetString("id"));
                        idCupter = myReader.GetString("id");
                        idCupterBool = true;
                        if (idlast == idmorelast)
                        {
                            idmorelast = idlast;
                            continue;
                        }
                        for (int i = 0; i < itter; i++)
                        {
                            f = false;
                            if (myReader.GetString("buyer").Equals(works[i].buyer) && myReader.GetString("sender").Equals(works[i].sender) && myReader.GetString("object").Equals(works[i].objectBuyer))
                            {
                                if (myReader.GetString("status").Equals("В работе"))
                                {
                                    works[i].inWork++;
                                }
                                if (myReader.GetString("status").Equals("Назначена"))
                                {
                                    works[i].point++;
                                }
                                if (myReader.GetString("status").Equals("Исполнена") || myReader.GetString("status").Equals("Оплачена"))
                                {
                                    works[i].done++;
                                }
                                f = true;
                                idmorelast = idlast;
                                string timeAccept = myReader.GetString("timeAccept");
                                string[] timeAcceptt = timeAccept.Split(':');
                                if (myReader.GetString("status").Equals("В работе"))
                                {
                                    if (works[i].hour > int.Parse(timeAcceptt[0]))
                                    {
                                        works[i].hour = int.Parse(timeAcceptt[0]);
                                        works[i].minute = int.Parse(timeAcceptt[1]);
                                    }
                                    else
                                    {
                                        if (works[i].minute > int.Parse(timeAcceptt[1]))
                                        {
                                            works[i].hour = int.Parse(timeAcceptt[0]);
                                            works[i].minute = int.Parse(timeAcceptt[1]);
                                        }
                                    }
                                }
                                if (works[i].traffic.Equals("пусто"))
                                {
                                    if (!myReader.GetString("traffic").Equals("пусто"))
                                    {
                                        works[i].traffic = myReader.GetString("traffic");
                                    }
                                }
                                break;
                            }
                            idmorelast = idlast;
                        }
                        if (!f)
                        {
                            works[itter].traffic = myReader.GetString("traffic");
                            works[itter].buyer = myReader.GetString("buyer");
                            works[itter].sender = myReader.GetString("sender");
                            works[itter].objectBuyer = myReader.GetString("object");
                            if (myReader.GetString("status").Equals("В работе"))
                            {
                                works[itter].inWork++;
                            }
                            if (myReader.GetString("status").Equals("Назначена"))
                            {
                                works[itter].point++;
                            }
                            if (myReader.GetString("status").Equals("Исполнена") || myReader.GetString("status").Equals("Оплачена"))
                            {
                                works[itter].done++;
                            }
                            idmorelast = idlast;
                            string timeAccept = myReader.GetString("timeAccept");
                            string[] timeAcceptt = timeAccept.Split(':');
                            //MessageBox.Show(timeAcceptt[0]);
                            if (myReader.GetString("status").Equals("В работе"))
                            {
                                if (works[itter].hour > int.Parse(timeAcceptt[0]))
                                {
                                    works[itter].hour = int.Parse(timeAcceptt[0]);
                                    works[itter].minute = int.Parse(timeAcceptt[1]);
                                }
                                else
                                {
                                    if (works[itter].minute > int.Parse(timeAcceptt[1]))
                                    {
                                        works[itter].hour = int.Parse(timeAcceptt[0]);
                                        works[itter].minute = int.Parse(timeAcceptt[1]);
                                    }
                                }
                            }
                            itter++;
                        }
                    }
                    for (int i = 0; i < itter; i++)
                    {
                        works[i].accept = works[i].inWork + works[i].point + works[i].done;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                db.closeConnection();



                dgv.Columns.AddRange(
                new DataGridViewTextBoxColumn() { Name = "timeAccept", HeaderText = "Время поставки на объект" },
                new DataGridViewTextBoxColumn() { Name = "traffic", HeaderText = "Вид транспорта" },
                new DataGridViewTextBoxColumn() { Name = "buyeer", HeaderText = "Покупатель/заказчик" },
                new DataGridViewTextBoxColumn() { Name = "sendeerr", HeaderText = "Грузоотправитель" },
                new DataGridViewTextBoxColumn() { Name = "objectBuyeer", HeaderText = "Объект поставки" },
                new DataGridViewTextBoxColumn() { Name = "accept", HeaderText = "Принятые заявки" },
                new DataGridViewTextBoxColumn() { Name = "inWork", HeaderText = "Неназначенные заявки" },
                new DataGridViewTextBoxColumn() { Name = "point", HeaderText = "Назначенные заявки" },
                new DataGridViewTextBoxColumn() { Name = "done", HeaderText = "Выпоненные" });
                dgv.Rows.Clear();
                int sumAccept = 0;
                int sumInWork = 0;
                int sumPoint = 0;
                int sumDone = 0;
                int it = 0;
                for (int i = 0; i < itter; i++)
                {
                    dgv.Rows.Add();
                    if (works[i].hour == 23 && works[i].minute == 59)
                    {
                        if (works[i].accept == works[i].done)
                        {
                            dgv["timeAccept", i].Value = "Все исполнены";
                        }
                        else
                        {
                            dgv["timeAccept", i].Value = "Все назначены";
                        }
                    }
                    else
                    {
                        if (works[i].minute >= 0 && works[i].minute < 10)
                        {
                            dgv["timeAccept", i].Value = works[i].hour.ToString() + ":0" + works[i].minute.ToString();
                        }
                        else
                        {
                            dgv["timeAccept", i].Value = works[i].hour.ToString() + ":" + works[i].minute.ToString();
                        }
                    }
                    dgv["traffic", i].Value = works[i].traffic.ToString();
                    dgv["buyeer", i].Value = works[i].buyer.ToString();
                    dgv["sendeerr", i].Value = works[i].sender.ToString();
                    dgv["objectBuyeer", i].Value = works[i].objectBuyer.ToString();
                    dgv["accept", i].Value = works[i].accept.ToString();
                    dgv["inWork", i].Value = works[i].inWork.ToString();
                    dgv["point", i].Value = works[i].point.ToString();
                    dgv["done", i].Value = works[i].done.ToString();
                    if (works[i].point != 0)
                    {
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            dgv[j, i].Style.BackColor = Color.Khaki;
                        }
                        //dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;
                    }
                    if (works[i].inWork != 0)
                    {
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            dgv[j, i].Style.BackColor = Color.LightPink;
                        }
                        //dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;
                    }
                    if (works[i].accept == works[i].point)
                    {
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            dgv[j, i].Style.BackColor = Color.Khaki;
                        }
                        //dgv.Rows[i].DefaultCellStyle.BackColor = Color.Khaki;
                    }
                    if (works[i].point != 0)
                    {
                        dgv["point", i].Style.BackColor = Color.Khaki;
                    }
                    if (works[i].accept == works[i].done)
                    {
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            dgv[j, i].Style.BackColor = Color.PaleGreen;
                        }
                        //dgv.Rows[i].DefaultCellStyle.BackColor = Color.PaleGreen;
                    }
                    if (works[i].done != 0)
                    {
                        dgv["done", i].Style.BackColor = Color.PaleGreen;
                    }
                    sumAccept = sumAccept + works[i].accept;
                    sumInWork = sumInWork + works[i].inWork;
                    sumPoint = sumPoint + works[i].point;
                    sumDone = sumDone + works[i].done;
                    it = i;
                }
                dgv[0, it + 1].Value = "Всего";
                dgv["accept", it + 1].Value = sumAccept;
                dgv["inWork", it + 1].Value = sumInWork;
                dgv["point", it + 1].Value = sumPoint;
                dgv["done", it + 1].Value = sumDone;
                dgv.Rows[it + 1].DefaultCellStyle.BackColor = Color.LightGray;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                /*for(int i = 0; i < itter; i++)
                {
                    MessageBox.Show(works[i].buyer + works[i].sender + works[i].objectBuyer + works[i].accept + works[i].point + works[i].done);
                }*/
                g = false;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            idCupterBool = false;
            button16.Hide();
            textBox1.Hide();
            label7.Hide();
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            string time = dateTimePicker1.Value.ToString();
            string[] asd = time.Split(' ');
            string[] zxc = asd[0].Split('.');
            string answer1 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];

            time = dateTimePicker2.Value.ToString();
            asd = null;
            asd = time.Split(' ');
            zxc = null;
            zxc = asd[0].Split('.');
            string answer2 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];

            //string Line = "SELECT * FROM `request` WHERE `docDate` = '" + answer + "';";
            bool fstatus = false;
            bool fbuyer = false;
            bool fobject = false;

            string Line = "";// = "SELECT * FROM `request` WHERE `status` = '" + comboBox1.Text + "';";
            if (!comboBox1.Text.Equals(""))
            {
                fstatus = true;
            }
            if (!comboBox2.Text.Equals(""))
            {
                fbuyer = true;
            }
            if (!comboBox3.Text.Equals(""))
            {
                fobject = true;
            }

            if (fstatus && fobject)
            {
                Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `status` = '" + comboBox1.Text + "' AND `object` = '" + comboBox3.Text + "' ORDER BY `id` ASC;";
            }
            else
            {
                if (fstatus && fbuyer)
                {
                    Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `status` = '" + comboBox1.Text + "' AND `buyer` = '" + comboBox2.Text + "' ORDER BY `id` ASC;";
                }
                else
                {
                    if (fstatus)
                    {
                        Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `status` = '" + comboBox1.Text + "' ORDER BY `id` ASC;";
                    }
                    else
                    {
                        if (fbuyer)
                        {
                            Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `buyer` = '" + comboBox2.Text + "' ORDER BY `id` ASC;";
                        }
                        else
                        {
                            if (fobject)
                            {
                                Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `object` = '" + comboBox3.Text + "' ORDER BY `id` ASC;";
                            }
                        }
                    }
                }
            }
            if (!fstatus && !fbuyer && !fobject)
            {
                Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' ORDER BY `id` ASC;";
            }
            //Line = "SELECT * FROM `request` WHERE `docDate` = '" + answer1 + "';";

            backLine = Line;

            DB db = new DB();
            MySqlCommand command = new MySqlCommand(Line, db.getConnection());
            db.openConnection();
            object obj = command.ExecuteScalar();
            if (obj != null)
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlDataReader myReader;

                dgv.Columns.AddRange(
                new DataGridViewTextBoxColumn() { Name = "status", HeaderText = "Статус" },
                new DataGridViewTextBoxColumn() { Name = "id", HeaderText = "№" },
                new DataGridViewTextBoxColumn() { Name = "docDate", HeaderText = "Дата" },
                new DataGridViewTextBoxColumn() { Name = "timeAccept", HeaderText = "Время поставки" },
                new DataGridViewTextBoxColumn() { Name = "buyer", HeaderText = "Покупатель/заказчик" },
                new DataGridViewTextBoxColumn() { Name = "object", HeaderText = "Объект" },
                new DataGridViewTextBoxColumn() { Name = "recipient", HeaderText = "Грузополучатель" },
                new DataGridViewTextBoxColumn() { Name = "sender", HeaderText = "Грузоотправитель" },
                new DataGridViewTextBoxColumn() { Name = "numberDocDriver", HeaderText = "Номер доверенности" },
                new DataGridViewTextBoxColumn() { Name = "dateDocDriver", HeaderText = "Дата доверенности" },
                new DataGridViewTextBoxColumn() { Name = "fromDounterparty", HeaderText = "От кого доверенность" },
                new DataGridViewTextBoxColumn() { Name = "nameCargo", HeaderText = "Наименование" },
                new DataGridViewTextBoxColumn() { Name = "numberNom", HeaderText = "Кол-во номенклатуры" },
                new DataGridViewTextBoxColumn() { Name = "traffic", HeaderText = "Вид транспорта" },
                new DataGridViewTextBoxColumn() { Name = "сars", HeaderText = "Автомобиль" },
                new DataGridViewTextBoxColumn() { Name = "driver", HeaderText = "Водитель" },
                new DataGridViewTextBoxColumn() { Name = "cash", HeaderText = "Расчет наличными" },
                new DataGridViewTextBoxColumn() { Name = "numberDocTrip", HeaderText = "Номер ТТН" },
                new DataGridViewTextBoxColumn() { Name = "paid", HeaderText = "Оплачено" });
                dgv.Rows.Clear();
                try
                {
                    //db.openConnection();
                    myReader = command.ExecuteReader();
                    int i = 0;
                    while (myReader.Read())
                    {
                        dgv.Rows.Add();
                        if (!myReader.GetString("status").Equals("пусто"))
                        {
                            dgv[0, i].Value = myReader.GetString("status");
                            if (dgv[0, i].Value.Equals("В работе"))
                            {
                                dgv[0, i].Style.BackColor = Color.LightPink;
                            }
                            if (dgv[0, i].Value.Equals("Назначена") || dgv[0, i].Value.Equals("Оплачена"))
                            {
                                dgv[0, i].Style.BackColor = Color.Khaki;
                            }
                            if (dgv[0, i].Value.Equals("Исполнена"))
                            {
                                dgv[0, i].Style.BackColor = Color.PaleGreen;
                            }
                        }
                        else
                            dgv[0, i].Value = " ";
                        if (i != 0)
                        {
                            if (myReader.GetString("id").Equals(dgv[1, i - 1].Value))
                            {
                                dgv[1, i].Value = myReader.GetString("id");
                                /*dgv.Rows[i].DefaultCellStyle.BackColor = Color.MistyRose;
                                dgv.Rows[i - 1].DefaultCellStyle.BackColor = Color.MistyRose;*/
                            }
                            else
                            {
                                dgv[1, i].Value = myReader.GetString("id");
                            }
                        }
                        else
                        {
                            dgv[1, i].Value = myReader.GetString("id");
                        }
                        string datefirst = myReader.GetString("docDate");
                        string[] datesplit = datefirst.Split(' ');
                        dgv[2, i].Value = datesplit[0];
                        dgv[3, i].Value = myReader.GetString("timeAccept");
                        if (!myReader.GetString("buyer").Equals("пусто"))
                        {
                            dgv[4, i].Value = myReader.GetString("buyer");
                            //comboBox2.Text = myReader.GetString("buyer");
                        }
                        else
                        {
                            dgv[4, i].Value = " ";
                            //comboBox2.Text = " ";
                        }
                        if (!myReader.GetString("object").Equals("пусто"))
                        {
                            dgv[5, i].Value = myReader.GetString("object");
                            //comboBox3.Text = myReader.GetString("object");
                        }
                        else
                        {
                            dgv[5, i].Value = " ";
                            //comboBox3.Text = " ";
                        }
                        if (!myReader.GetString("recipient").Equals("пусто"))
                            dgv[6, i].Value = myReader.GetString("recipient");
                        else
                            dgv[6, i].Value = " ";
                        if (!myReader.GetString("sender").Equals("пусто"))
                        {
                            dgv[7, i].Value = myReader.GetString("sender");
                            //textBox1.Show();
                            //label7.Show();
                            //textBox1.Text = myReader.GetString("sender");
                        }
                        else
                        {
                            dgv[7, i].Value = " ";
                        }
                        if (!myReader.GetString("numberDocDriver").Equals("пусто"))
                            dgv[8, i].Value = myReader.GetString("numberDocDriver");
                        else
                            dgv[8, i].Value = " ";
                        string dateDocDriver = myReader.GetString("dateDocDriver");
                        string[] datesplitt = dateDocDriver.Split(' ');
                        if (!myReader.GetString("dateDocDriver").Equals("пусто"))
                            dgv[9, i].Value = datesplitt[0];
                        else
                            dgv[9, i].Value = " ";
                        if (!myReader.GetString("fromCounterparty").Equals("пусто"))
                            dgv[10, i].Value = myReader.GetString("fromCounterparty");
                        else
                            dgv[10, i].Value = " ";
                        if (!myReader.GetString("nameCargo").Equals("пусто"))
                            dgv[11, i].Value = myReader.GetString("nameCargo");
                        else
                            dgv[11, i].Value = " ";
                        if (!myReader.GetString("numberNomenclature").Equals("0"))
                            dgv[12, i].Value = myReader.GetString("numberNomenclature");
                        else
                            dgv[12, i].Value = " ";
                        if (!myReader.GetString("traffic").Equals("пусто"))
                            dgv[13, i].Value = myReader.GetString("traffic");
                        else
                            dgv[13, i].Value = " ";
                        if (!myReader.GetString("cars").Equals("пусто"))
                            dgv[14, i].Value = myReader.GetString("cars");
                        else
                            dgv[14, i].Value = " ";
                        if (!myReader.GetString("drivers").Equals("пусто"))
                            dgv[15, i].Value = myReader.GetString("drivers");
                        else
                            dgv[15, i].Value = " ";
                        if (!myReader.GetString("cash").Equals("пусто"))
                            dgv[16, i].Value = myReader.GetString("cash");
                        else
                            dgv[16, i].Value = " ";
                        if (!myReader.GetString("numberDocTrip").Equals("-1"))
                            dgv[17, i].Value = myReader.GetString("numberDocTrip");
                        else
                            dgv[17, i].Value = " ";
                        if (!myReader.GetString("paid").Equals("пусто"))
                            dgv[18, i].Value = myReader.GetString("paid");
                        else
                            dgv[18, i].Value = " ";
                        i++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                g = true;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                db.closeConnection();
                idCupterBool = false;
            }            
        }

        private void button19_Click(object sender, EventArgs e)
        {
            /*string[] cargo = new string[46];
            DB db = new DB();
            //MySqlCommand command = new MySqlCommand("UPDATE request SET status = 'В работе' WHERE cars = 'пусто'", db.getConnection());
            MySqlCommand command = new MySqlCommand("SELECT * FROM `namecargo`", db.getConnection());
            MySqlDataReader myReader;
            int i = 0;
            try
            {
                db.openConnection();
                myReader  = command.ExecuteReader();
                //command.ExecuteNonQuery();
                while (myReader.Read())
                {
                    cargo[i] = myReader.GetString("name");
                    i++;
                    MessageBox.Show(cargo[i]);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            for(int j = 0; j < 46; j++)
            {
                command = new MySqlCommand("INSERT INTO `product` (`name`) VALUES (@cargo)", db.getConnection());
                command.Parameters.Add("@cargo", MySqlDbType.VarChar).Value = cargo[j];
                db.openConnection();
                command.ExecuteNonQuery();
                db.closeConnection();
            }*/

            DB db = new DB();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `request` WHERE `deal` = 'Поставка' AND `docDate` > '2020-12-01' AND `status` = 'Исполнена';", db.getConnection());
            MySqlDataReader myReader;
            string[] t = new string[48];
            string[] date = new string[48];
            string[] id = new string[48];
            string[] product = new string[48];
            string[] count = new string[48];
            int i = 0;
            try
            {
                db.openConnection();
                myReader = command.ExecuteReader();
                //command.ExecuteNonQuery();
                while (myReader.Read())
                {
                    t[i] = myReader.GetString("docDate");
                    string[] fs = t[i].Split(' ');
                    date[i] = fs[0];
                    id[i] = myReader.GetString("id");
                    product[i] = myReader.GetString("nameCargo");
                    count[i] = myReader.GetString("numberNomenclature");
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            for(int j = 0; j < i; j++)
            {
                command = new MySqlCommand("INSERT INTO `operations` (`date`, `operation`, `product`, `count`) VALUES (@date, @ope, @product, @count)", db.getConnection());
                command.Parameters.Add("@date", MySqlDbType.Date).Value = "2020-12-31";
                command.Parameters.Add("@ope", MySqlDbType.VarChar).Value = "Заявка №" + id[j];
                command.Parameters.Add("@product", MySqlDbType.VarChar).Value = product[j];
                command.Parameters.Add("@count", MySqlDbType.VarChar).Value = count[j];
                db.openConnection();

                command.ExecuteNonQuery();

                db.closeConnection();
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            comboBox4.Text = "";
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            comboBox5.Text = "";
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            NomGroup nomGroup = new NomGroup();
            nomGroup.Show();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            StatementCounterparty stc = new StatementCounterparty();
            stc.Show();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Proxy proxy = new Proxy();
            proxy.Show();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Trailer trailer = new Trailer();
            trailer.Show();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            CarsWithTrailer cwt = new CarsWithTrailer();
            cwt.Show();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Worker worker = new Worker();
            worker.Show();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            Vault vault = new Vault();
            vault.Show();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            PersonalStatBuy psb = new PersonalStatBuy();
            psb.Show();
        }
    }
}

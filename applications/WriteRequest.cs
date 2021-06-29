using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace applications
{
    public partial class WriteRequest : Form
    {   

        public static string[] Cargo = new string[100];
        public static double[] numCargo = new double[100];
        public static int CargoCount = 0;
        public static string Deal = "";
        public static string objId = "";
        public static bool f = false;
        public string idRequest = "";
        public bool comboName = false;
        public string priceNow = "";
        public int priceNowCount = 0;
        public int priceNowCountBuy = 0;
        public string priceNowBuy = "";


        Color FlatColor;
        public WriteRequest()
        {
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            /*
            FlatColor = button4.FlatAppearance.BorderColor;
            button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button4.FlatAppearance.BorderColor = FlatColor;
            */
            
            InitializeComponent();
            FillCombo2();
            FillCombo3();
            FillCombo4();
            FillCombo5();
            FillCombo7();
            //FillCombo8();
            //FillCombo9();
            FillCombo71();
            FillCombo10();
            FillText1();
            FlatColor = button1.FlatAppearance.BorderColor;
            button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = FlatColor;

            label11.Hide();
            checkBox2.Hide();
            label13.Hide();
            textBox1.Hide();
            label24.Hide();
            textBox4.Hide();
            button2.Hide();
            button3.Hide();

            if (WriteRequest.f)
            {
                MessageBox.Show(WriteRequest.Cargo[0]);
                for (int i = 0; i < WriteRequest.CargoCount; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1["cargoName", i].Value = WriteRequest.Cargo[i];
                    dataGridView1["cargoNum", i].Value = WriteRequest.numCargo[i];
                }
            }

        }

        private void WriteRequest_Load(object sender, EventArgs e)
        {
            string[] combobox1items = new string[] { "Поставка", "Перевозка" };
            comboBox1.Items.AddRange(combobox1items);
            comboBox1.SelectedIndex = 0;
            textBox3.Text = "1";
            textBox2.Text = "Добавьте комментарий";
            textBox2.ForeColor = Color.Gray;
            textBox1.Text = "";
        }

        private void closeButton_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            WorkSpace workSpace = new WorkSpace();
            workSpace.Show();
        }


        Point lastPoint;

        private void mainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void mainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }


        private void buttonLogin_Click(object sender, EventArgs e)
        {
            bool pointCheck = false;
            bool doneCheck = false;
            bool paid = false;

            if(!comboBox8.Text.Equals("") || !comboBox9.Text.Equals("") || checkBox1.CheckState == CheckState.Checked)
            {
                pointCheck = true;
            }
            if (!textBox6.Text.Equals(""))
            {
                doneCheck = true;
            }
            if(checkBox2.CheckState == CheckState.Checked)
            {
                paid = true;
            }

            DB db = new DB();
            /*if (!priceNow.Equals(textBox1.Text))
            {
                priceNowCount++;
                string priceNew = "price" + priceNowCount;
                string Query2 = "UPDATE `counterparty` SET `" + priceNew + "` = '" + textBox1.Text + "' WHERE `objectName` = '" + comboBox6.Text + "';";
                MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                MySqlCommand cmdDataBase2 = new MySqlCommand(Query2, db.getConnection());
                db.openConnection();
                cmdDataBase2.ExecuteNonQuery();
                db.closeConnection();
            }*/

            bool f = true;
            bool g = false;
            bool cargoEmpty = true;
            string num = "proba";


            for (int p = 0; p < dataGridView1.Rows.Count - 1; p++)
            {                 
                WriteRequest.Cargo[WriteRequest.CargoCount] = dataGridView1["cargo", p].Value.ToString();
                num = dataGridView1["cargoNum", p].Value.ToString();
                for (int j = 0; j < num.Length; j++)
                {
                    if (num[j] == '.')
                    {
                        string[] ans = num.Split('.');
                        num = ans[0] + "," + ans[1];
                        break;
                    }
                }
                WriteRequest.numCargo[WriteRequest.CargoCount] = double.Parse(num);
                WriteRequest.CargoCount++;
                cargoEmpty = false;
            }
            if(WriteRequest.CargoCount == 0)
            {
                WriteRequest.CargoCount++;
            }
            int b = int.Parse(idRequest);
            int a;
            if (b != 1)
            {
                string Query = "SELECT * FROM `request` ORDER BY `id` ASC";
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
                MySqlDataReader myReader;
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
                        myReader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                a = int.Parse(objId);
                a++;
                idRequest = Convert.ToString(a);
            }

            db.closeConnection();
            int countTrip = int.Parse(textBox3.Text);
            string statusans = "";
            for (int k = 0; k < countTrip; k++)
            {
                for (int i = 0; i < WriteRequest.CargoCount; i++)
                {
                    MySqlCommand command = new MySqlCommand("INSERT INTO `request` (`id`, `docDate`, `deal`, `ourFirms`, `buyer`, `sender`, `recipient`, `object`, `objectSend`, `status`, `nameCargo`, `numberNomenclature`, `numberDocDriver`, `dateDocDriver`, `fromCounterparty`, `coment`, `cars`, `drivers`, `cash`, `numberDocTrip`, `paid`, `priceSalary`) VALUES (@id, @data, @deal, @ourFirms, @buyer, @sender, @recipient, @object, @objectSend, @status, @cargo, @nomenclature, @numberDocDriver, @dateDocDriver, @from, @coment, @cars, @drivers, @cash, @trip, @paid, @salary)", db.getConnection());

                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = idRequest;
                    command.Parameters.Add("@data", MySqlDbType.Date).Value = dateTimePicker1.Value;
                    command.Parameters.Add("@deal", MySqlDbType.VarChar).Value = comboBox1.Text;
                    if (!comboBox2.Text.Equals(""))
                        command.Parameters.Add("@ourFirms", MySqlDbType.VarChar).Value = comboBox2.Text;
                    else
                        command.Parameters.Add("@ourFirms", MySqlDbType.VarChar).Value = "пусто";
                    if (!comboBox3.Text.Equals(""))
                        command.Parameters.Add("@buyer", MySqlDbType.VarChar).Value = comboBox3.Text;
                    else
                        command.Parameters.Add("@buyer", MySqlDbType.VarChar).Value = "пусто";
                    if (!comboBox4.Text.Equals(""))
                        command.Parameters.Add("@sender", MySqlDbType.VarChar).Value = comboBox4.Text;
                    else
                        command.Parameters.Add("@sender", MySqlDbType.VarChar).Value = "пусто";
                    if (!comboBox5.Text.Equals(""))
                        command.Parameters.Add("@recipient", MySqlDbType.VarChar).Value = comboBox5.Text;
                    else
                        command.Parameters.Add("@recipient", MySqlDbType.VarChar).Value = "пусто";
                    if (!comboBox6.Text.Equals(""))
                        command.Parameters.Add("@object", MySqlDbType.VarChar).Value = comboBox6.Text;
                    else
                        command.Parameters.Add("@object", MySqlDbType.VarChar).Value = "пусто";
                    if (!comboBox12.Text.Equals(""))
                        command.Parameters.Add("@objectSend", MySqlDbType.VarChar).Value = comboBox12.Text;
                    else
                        command.Parameters.Add("@objectSend", MySqlDbType.VarChar).Value = "пусто";

                    if (pointCheck)
                    {
                        if (paid)
                        {
                            if (doneCheck)
                            {
                                command.Parameters.Add("@status", MySqlDbType.VarChar).Value = "Исполнена";
                                statusans = "исполнена";
                            }
                            else
                            {
                                command.Parameters.Add("@status", MySqlDbType.VarChar).Value = "Оплачена";
                                statusans = "назначена";
                            }
                        }
                        else
                        {
                            if (doneCheck)
                            {
                                command.Parameters.Add("@status", MySqlDbType.VarChar).Value = "Исполнена";
                                statusans = "исполнена";
                            }
                            else
                            {
                                command.Parameters.Add("@status", MySqlDbType.VarChar).Value = "Назначена";
                                statusans = "назначена";
                            }
                        }
                    }
                    else
                    {
                        command.Parameters.Add("@status", MySqlDbType.VarChar).Value = "В работе";
                        statusans = "в работе";
                    }
                    //command.Parameters.Add("@status", MySqlDbType.VarChar).Value = "В работе";
                    if (cargoEmpty == false)
                    {
                        command.Parameters.Add("@cargo", MySqlDbType.VarChar).Value = WriteRequest.Cargo[i];
                        command.Parameters.Add("@nomenclature", MySqlDbType.Double).Value = WriteRequest.numCargo[i];
                    }
                    else
                    {
                        command.Parameters.Add("@cargo", MySqlDbType.VarChar).Value = "пусто";
                        command.Parameters.Add("@nomenclature", MySqlDbType.Double).Value = "0";
                    }
                    if (!textBox5.Text.Equals("Б/Н"))
                        command.Parameters.Add("@numberDocDriver", MySqlDbType.VarChar).Value = textBox5.Text;
                    else
                        command.Parameters.Add("@numberDocDriver", MySqlDbType.VarChar).Value = "Б/Н";

                    command.Parameters.Add("@dateDocDriver", MySqlDbType.Date).Value = dateTimePicker2.Value;

                    if (!comboBox10.Text.Equals(""))
                        command.Parameters.Add("@from", MySqlDbType.VarChar).Value = comboBox10.Text;
                    else
                        command.Parameters.Add("@from", MySqlDbType.VarChar).Value = "пусто";
                    if(textBox2.Text.Equals("Добавьте комментарий"))
                    {
                        command.Parameters.Add("@coment", MySqlDbType.VarChar).Value = "пусто";
                    }
                    else
                    {
                        command.Parameters.Add("@coment", MySqlDbType.VarChar).Value = textBox2.Text;
                    }
                    if (!comboBox8.Text.Equals(""))
                    {
                        command.Parameters.Add("@cars", MySqlDbType.VarChar).Value = comboBox8.Text;
                    }
                    else
                    {
                        command.Parameters.Add("@cars", MySqlDbType.VarChar).Value = "пусто";
                    }
                    if (!comboBox9.Text.Equals(""))
                    {
                        command.Parameters.Add("@drivers", MySqlDbType.VarChar).Value = comboBox9.Text;
                    }
                    else
                    {
                        command.Parameters.Add("@drivers", MySqlDbType.VarChar).Value = "пусто";
                    }
                    if (checkBox1.CheckState == CheckState.Checked)
                    {
                        command.Parameters.Add("@cash", MySqlDbType.VarChar).Value = "Да";
                    }
                    else
                    {
                        command.Parameters.Add("@cash", MySqlDbType.VarChar).Value = "Нет";
                    }
                    if (!textBox6.Text.Equals(""))
                    {
                        command.Parameters.Add("@trip", MySqlDbType.VarChar).Value = textBox6.Text;
                    }
                    else
                    {
                        command.Parameters.Add("@trip", MySqlDbType.Int32).Value = 101;
                    }
                    if (checkBox2.CheckState == CheckState.Checked)
                    {
                        command.Parameters.Add("@paid", MySqlDbType.VarChar).Value = "Да";
                    }
                    else
                    {
                        command.Parameters.Add("@paid", MySqlDbType.VarChar).Value = "Нет";
                    }
                    if (!textBox1.Text.Equals(""))
                    {
                        command.Parameters.Add("@salary", MySqlDbType.VarChar).Value = textBox1.Text;
                    }
                    else
                    {
                        command.Parameters.Add("@salary", MySqlDbType.VarChar).Value = "пусто";
                    }

                    db.openConnection();
                    if (command.ExecuteNonQuery() != 1)
                    {
                        f = false;
                    }


                    db.closeConnection();

                }
                int iddd = int.Parse(idRequest);
                iddd++;
                idRequest = Convert.ToString(iddd);
            }
            if (!comboBox6.Text.Equals(""))
            {
                CheckPrices();
            }
            if (f)
            {
                MessageBox.Show("Заявка " + statusans);
            }
            else
            {
                MessageBox.Show("Заявка не принята");
            }
            dateTimePicker1.Text = "";
            comboBox2.Text = "АвтоСтройКом";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            comboBox6.Text = "";
            comboBox7.Text = "";
            comboBox8.Text = "";
            comboBox9.Text = "";
            comboBox10.Text = "";
            comboBox11.Text = "";
            textBox5.Text = "Б/Н";
            textBox2.Text = "Добавьте комментарий";
            textBox2.ForeColor = Color.Gray;
            textBox6.Text = "";
            if (cargoEmpty == true)
            {
                for (int p = 0; p < WriteRequest.CargoCount; p++)
                {
                    WriteRequest.Cargo[p] = " ";
                    WriteRequest.numCargo[p] = 0.0;
                }
            }
            WriteRequest.CargoCount = 0;
            dataGridView1.Rows.Clear();
            /*a = int.Parse(idRequest);
            a++;
            idRequest = Convert.ToString(a);*/
            label1.Text = "Регистрация заявки №" + idRequest;
            comboBox1.Text = "Поставка";
            textBox3.Text = "1";
            checkBox1.CheckState = CheckState.Unchecked;
            checkBox2.CheckState = CheckState.Unchecked;
            textBox1.Text = "";
            textBox4.Text = "";
            label13.Hide();
            textBox1.Hide();
            label24.Hide();
            textBox4.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*DB db = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT counterparty.name FROM `counterparty`", db.getConnection());
            comboBox1.Items.Add(command);*/
            //0 - поставка
            //1 - перевозка
            if (comboBox1.SelectedIndex == 0)
            {
                label8.Text = "Поставщик \n(наши организации)";
                label7.Text = "Покупатель";
                label5.Text = "Объект разгрузки";
                WriteRequest.Deal = "товар";
                comboName = false;
                comboBox2.Text = "АвтоСтройКом";

            }
            else
            {
                label8.Text = "Исполнитель \n(наши организации)";
                label7.Text = "Заказчик";
                label5.Text = "Объект поставки"; 
                WriteRequest.Deal = "груз";
                comboName = true;
                comboBox2.Text = "АвтоСтройХолдинг";
            }
        }

        void FillText1()
        {
            string Query = "SELECT * FROM `request` ORDER BY `id` ASC";
            DB db = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
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
                    myReader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            Query = "SELECT * FROM `request` WHERE `id` = '" + objId + "';";
            cmdDataBase = new MySqlCommand(Query, db.getConnection());
            string status = "";
            MySqlDataReader myReader2;
            try
            {
                db.openConnection();
                myReader2 = cmdDataBase.ExecuteReader();

                while (myReader2.Read())
                {
                    status = myReader2.GetString("status");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (status.Equals("В работе") || status.Equals("Назначена") || status.Equals("Исполнена"))
            {
                int a = int.Parse(objId);
                a++;
                idRequest = Convert.ToString(a);
            }
            db.closeConnection();
        }


        void FillCombo2()
        {
            string Query = "SELECT * FROM `ourFirms` ORDER BY `name` ASC";
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
                    string objName = myReader.GetString("name");
                    comboBox2.Items.Add(objName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }

        void FillCombo3()
        {
            string Query = "(SELECT * FROM `counterparty` " + /*WHERE status = 'Грузополучатель/грузоотправитель' OR status = 'Диспетчер'*/  "ORDER BY `name` ASC) ORDER BY `count` desc;";
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
                        comboBox3.Items.Add(objName);
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
            string Query = "(SELECT * FROM `counterparty` WHERE status = 'Грузополучатель/грузоотправитель' OR status = 'Грузоотправитель' ORDER BY `name` ASC) ORDER BY `count` DESC";
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
                    if(f == true)
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

        void FillCombo5()
        {
            
            string Query = "(SELECT * FROM `counterparty` WHERE status = 'Грузополучатель/грузоотправитель' OR status = 'Грузополучатель' ORDER BY `name` ASC) ORDER BY `count` DESC";
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
                        comboBox5.Items.Add(objName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }



        void FillCombo71()
        {
            dataGridView1.Rows.Clear();
            string Query = "SELECT * FROM `nameCargo` ORDER BY `name` ASC";
            //string Query = "(SELECT * FROM `nameCargo` ORDER BY `name` ASC) ORDER BY `count` desc;";
            DB db = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            ArrayList row = new ArrayList();
            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
            if (!comboName)
            {
                combo.HeaderText = "Наименование товара";
            }
            else
            {
                combo.HeaderText = "Наименование груза";
            }
            combo.Name = "cargo";
            try
            {
                db.openConnection();
                myReader = cmdDataBase.ExecuteReader();

                while (myReader.Read())
                {
                    string objName = myReader.GetString("name");
                    row.Add(objName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            combo.Items.AddRange(row.ToArray());
            dataGridView1.Columns.Add(combo);
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "cargoNum", HeaderText = "Кол-во номенклатуры" });
            //dataGridView1.Columns[0].Width = 250;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        void FillCombo7()
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
                        comboBox7.Items.Add(objName);
                        comboBox11.Items.Add(objName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }

        void FillCombo8()
        {
            string Query = "SELECT * FROM `cars` ORDER BY `contractor` ASC";
            //string Query = "(SELECT * FROM `counterparty` ORDER BY `name` ASC) ORDER BY `count` desc;";
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
                    string objName = myReader.GetString("name");
                    comboBox8.Items.Add(objName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }


        void FillCombo9()
        {
            string Query = "SELECT * FROM `drivers`";
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
                    string objName = myReader.GetString("name");
                    comboBox9.Items.Add(objName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }


        void FillCombo10()
        {
            //string Query = "SELECT * FROM `counterparty` ORDER BY `name` ASC";
            if (comboBox1.Text.Equals("Перевозка"))
            {
                comboBox10.Text = comboBox3.Text;
            }
            string Query = "(SELECT * FROM `counterparty` " + /*WHERE status = 'Грузополучатель/грузоотправитель' OR status = 'Диспетчер'*/  "ORDER BY `name` ASC) ORDER BY `count` desc;";
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
                        comboBox10.Items.Add(objName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox5.Text = comboBox3.Text;
            comboBox6.Items.Clear();
            //string Query = "SELECT * FROM `counterparty` WHERE `name` = '" + comboBox3.Text.ToString() + "' ORDER BY `objectName` ASC;";
            string Query = "(SELECT * FROM `counterparty` WHERE `name` = '" + comboBox3.Text.ToString() + "' ORDER BY `objectName` ASC) ORDER BY `count` desc;";
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
                    comboBox6.Items.Add(objName);
                    if (myReader.GetString("status").Equals("Б/С"))
                    {
                        comboBox4.Text = comboBox3.Text;
                        comboBox5.Text = comboBox3.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            bool pointCheck = false;
            bool doneCheck = false;
            bool paid = false;

            if (!comboBox8.Text.Equals("") || !comboBox9.Text.Equals("") || checkBox1.CheckState == CheckState.Checked)
            {
                pointCheck = true;
            }
            if (!textBox6.Text.Equals(""))
            {
                doneCheck = true;
            }
            if (checkBox2.CheckState == CheckState.Checked)
            {
                paid = true;
            }

            DB db = new DB();
            MySqlCommand com = new MySqlCommand("UPDATE request SET id=@id, docDate=@docDate, deal=@deal, ourFirms=@ourFirms, buyer=@buyer, sender=@sender, recipient=@recipient, object=@object, objectSend=@objectSend, status=@status, coment=@coment, contractor=@contractor, cars=@cars, drivers=@drivers, numberDocDriver=@numberDocDriver, dateDocDriver=@dateDocDriver, fromCounterparty=@fromCounterparty, cash=@cash, paid=@paid, numberDocTrip=@trip, priceSalary=@salary WHERE id = '" + idRequest + "';", db.getConnection());


            com.Parameters.AddWithValue("@id", idRequest);
            com.Parameters.AddWithValue("@docDate", dateTimePicker1.Value);
            com.Parameters.AddWithValue("@deal", comboBox1.Text);
            //if(!comboBox2.Text.Equals(""))
                com.Parameters.AddWithValue("@ourFirms", comboBox2.Text);
            //else
             //   com.Parameters.AddWithValue("@ourFirms", " ");
            //if (!comboBox3.Text.Equals(""))
                com.Parameters.AddWithValue("@buyer", comboBox3.Text);
            //else
              //  com.Parameters.AddWithValue("@buyer", " ");
            //if (!comboBox4.Text.Equals(""))
                com.Parameters.AddWithValue("@sender", comboBox4.Text);
            //else
              //  com.Parameters.AddWithValue("@sender", " ");
            //if (!comboBox5.Text.Equals(""))
                com.Parameters.AddWithValue("@recipient", comboBox5.Text);
            //else
               // com.Parameters.AddWithValue("@recipient", " ");
            //if (!comboBox6.Text.Equals(""))
                com.Parameters.AddWithValue("@object", comboBox6.Text);
            //else
            //  com.Parameters.AddWithValue("@object", " ");
            com.Parameters.AddWithValue("@objectSend", comboBox12.Text);
            //com.Parameters.AddWithValue("@status", "Назначена");
            //com.Parameters.AddWithValue("@nameCargo", Cargo[i]);
            //com.Parameters.AddWithValue("@numberNomenclature", textBox2.Text);
            //if (!textBox3.Text.Equals(""))
            //else
            //  com.Parameters.AddWithValue("@numberTrip", " ");
            //if (!comboBox8.Text.Equals(""))
            if (!textBox2.Text.Equals("Добавьте комментарий"))
            {
                com.Parameters.AddWithValue("@coment", textBox2.Text);
            }
            else
            {
                com.Parameters.AddWithValue("@coment", "пусто");
            }
            if (!textBox1.Text.Equals(""))
            {
                com.Parameters.AddWithValue("@salary", textBox1.Text);
            }
            else
            {
                com.Parameters.AddWithValue("@salary", "пусто");
            }
            com.Parameters.AddWithValue("@contractor", comboBox7.Text);
            com.Parameters.AddWithValue("@cars", comboBox8.Text);
            //else
              //  com.Parameters.AddWithValue("@cars", " ");
            //if (!comboBox9.Text.Equals(""))
                com.Parameters.AddWithValue("@drivers", comboBox9.Text);
            //else
              //  com.Parameters.AddWithValue("@drivers", " ");
            //if (!textBox5.Text.Equals(""))
                com.Parameters.AddWithValue("@numberDocDriver", textBox5.Text);
            //else
              //  com.Parameters.AddWithValue("@numberDocDriver", " ");

            com.Parameters.AddWithValue("@dateDocDriver", dateTimePicker2.Value);
            
            //if (!comboBox10.Text.Equals(""))
                com.Parameters.AddWithValue("@fromCounterparty", comboBox10.Text);
            //else
            //  com.Parameters.AddWithValue("@fromCounterparty", " ");
            if (checkBox1.CheckState == CheckState.Checked)
            {
                com.Parameters.AddWithValue("@cash", "Да");
            }
            else
            {
                com.Parameters.AddWithValue("@cash", "Нет");
            }
            if (pointCheck)
            {
                if (paid)
                {
                    if (doneCheck)
                    {
                        com.Parameters.AddWithValue("@status", "Исполнена");
                    }
                    else
                    {
                        com.Parameters.AddWithValue("@status", "Оплачена");
                    }
                }
                else
                {
                    if (doneCheck)
                    {
                        com.Parameters.AddWithValue("@status", "Исполнена");
                    }
                    else
                    {
                        com.Parameters.AddWithValue("@status", "Назначена");
                    }
                }
            }
            else
            {
                com.Parameters.AddWithValue("@status", "В работе");
            }
            if (!textBox6.Text.Equals(""))
                com.Parameters.AddWithValue("@trip", textBox6.Text);
            else
                com.Parameters.AddWithValue("@trip", 101);
            if (checkBox2.CheckState == CheckState.Checked)
            {
                com.Parameters.AddWithValue("@paid", "Да");
                //com.Parameters.AddWithValue("@status", "Оплачена");
            }
            else
            {
                com.Parameters.AddWithValue("@paid", "Нет");
                //com.Parameters.AddWithValue("@status", "Исполнена");
            }
            db.openConnection();

            if (com.ExecuteNonQuery() >= 1)
            {
                //MessageBox.Show(idRequest);
                MessageBox.Show("Заявка обновлена и назначена");
            }
            else
            {
                //MessageBox.Show(idRequest);
                MessageBox.Show("Заявка не обновлена");
            }

            db.closeConnection();

            Cargo = null;
            CargoCount = 0;
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand com = new MySqlCommand("UPDATE request SET id=@id, docDate=@docDate, deal=@deal, ourFirms=@ourFirms, buyer=@buyer, sender=@sender, recipient=@recipient, object=@object, objectSend=@objectSend, status=@status, coment=@coment, contractor=@contractor, cars=@cars, drivers=@drivers, numberDocDriver=@numberDocDriver, dateDocDriver=@dateDocDriver, fromCounterparty=@fromCounterparty, cash=@cash, paid=@paid, numberDocTrip=@trip, priceSalary=@salary WHERE id = '" + idRequest + "';", db.getConnection());

            com.Parameters.AddWithValue("@id", idRequest);
            com.Parameters.AddWithValue("@docDate", dateTimePicker1.Value);
            com.Parameters.AddWithValue("@deal", comboBox1.Text);
            //if (!comboBox2.Text.Equals(""))
                com.Parameters.AddWithValue("@ourFirms", comboBox2.Text);
            //else
             //   com.Parameters.AddWithValue("@ourFirms", " ");
            //if (!comboBox3.Text.Equals(""))
                com.Parameters.AddWithValue("@buyer", comboBox3.Text);
            //else
              //  com.Parameters.AddWithValue("@buyer", " ");
            //if (!comboBox4.Text.Equals(""))
                com.Parameters.AddWithValue("@sender", comboBox4.Text);
            //else
              //  com.Parameters.AddWithValue("@sender", " ");
            //if (!comboBox5.Text.Equals(""))
                com.Parameters.AddWithValue("@recipient", comboBox5.Text);
            //else
              //  com.Parameters.AddWithValue("@recipient", " ");
            //if (!comboBox6.Text.Equals(""))
                com.Parameters.AddWithValue("@object", comboBox6.Text);
            //else
            //  com.Parameters.AddWithValue("@object", " ");

            com.Parameters.AddWithValue("@objectSend", comboBox12.Text);
            //com.Parameters.AddWithValue("@nameCargo", Cargo[i]);
            //com.Parameters.AddWithValue("@numberNomenclature", textBox2.Text);
            //if (!textBox3.Text.Equals(""))
            //else
            //  com.Parameters.AddWithValue("@numberTrip", " ");
            //if (!comboBox8.Text.Equals(""))
            if (!textBox2.Text.Equals("Добавьте комментарий"))
            {
                com.Parameters.AddWithValue("@coment", textBox2.Text);
            }
            else
            {
                com.Parameters.AddWithValue("@coment", "пусто");
            }
            if (!textBox1.Text.Equals(""))
            {
                com.Parameters.AddWithValue("@salary", textBox1.Text);
            }
            else
            {
                com.Parameters.AddWithValue("@salary", "пусто");
            }
            com.Parameters.AddWithValue("@contractor", comboBox7.Text);
            com.Parameters.AddWithValue("@cars", comboBox8.Text);
            //else
              //  com.Parameters.AddWithValue("@cars", " ");
            //if (!comboBox9.Text.Equals(""))
                com.Parameters.AddWithValue("@drivers", comboBox9.Text);
            //else
              //  com.Parameters.AddWithValue("@drivers", " ");
            //if (!textBox5.Text.Equals(""))
                com.Parameters.AddWithValue("@numberDocDriver", textBox5.Text);
            //else
              //  com.Parameters.AddWithValue("@numberDocDriver", " ");

            com.Parameters.AddWithValue("@dateDocDriver", dateTimePicker2.Value);

            //if (!comboBox10.Text.Equals(""))
                com.Parameters.AddWithValue("@fromCounterparty", comboBox10.Text);

            //else
            //  com.Parameters.AddWithValue("@fromCounterparty", " ");
            /*if (!textBox1.Text.Equals(""))
            {
                com.Parameters.AddWithValue("@salary", textBox1.Text);
            }
            else
            {
                com.Parameters.AddWithValue("@salary", "пусто");
            }*/
            if (checkBox1.CheckState == CheckState.Checked)
            {
                com.Parameters.AddWithValue("@cash", "Да");
            }
            else
            {
                com.Parameters.AddWithValue("@cash", "Нет");
            }
            if (!textBox6.Text.Equals(""))
                com.Parameters.AddWithValue("@trip", textBox6.Text);
            else
              com.Parameters.AddWithValue("@trip", 101);
            if (checkBox2.CheckState == CheckState.Checked)
            {
                com.Parameters.AddWithValue("@paid", "Да");
                //com.Parameters.AddWithValue("@status", "Оплачена");
            }
            else
            {
                com.Parameters.AddWithValue("@paid", "Нет");
            }
            if (!textBox6.Equals(""))
            {
                com.Parameters.AddWithValue("@status", "Исполнена");
            }
            else
            {
                com.Parameters.AddWithValue("@status", "Назначена");
            }
            db.openConnection();
            //com.ExecuteNonQuery();

            if (com.ExecuteNonQuery() >= 1)
            {
                MessageBox.Show("Заявка обновлена и исполнена");
            }
            else
            {
                MessageBox.Show("Заявка не обновлена");
            }

            db.closeConnection();

            this.Hide();
        }

        Color colorButton;


        void cupter(int id)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `request` WHERE `id` = '" + id.ToString() + "';", db.getConnection());
            MySqlDataReader myReader;

            try
            {
                db.openConnection();
                myReader = command.ExecuteReader();

                while (myReader.Read())
                {
                    dateTimePicker1.Value = myReader.GetDateTime("docDate");
                    comboBox1.Text = myReader.GetString("deal");
                    comboBox2.Text = myReader.GetString("ourFirms");
                    comboBox3.Text = myReader.GetString("buyer");
                    comboBox4.Text = myReader.GetString("sender");
                    comboBox5.Text = myReader.GetString("recipient");
                    comboBox6.Text = myReader.GetString("object");
                    textBox3.Text = myReader.GetString("numberTrip");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            this.Hide();
        }




        /*private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            bool g = false;
            WriteRequest.Cargo[WriteRequest.CargoCount] = comboBox7.Text;
            string num = textBox2.Text;
            for (int j = 0; j < num.Length; j++) {
                if (num[j] == '.')
                {
                    string[] ans = num.Split('.');
                    num = ans[0] + "," + ans[1];
                    break;
                }
            }
            WriteRequest.numCargo[WriteRequest.CargoCount] = double.Parse(num);
            WriteRequest.CargoCount++;
            comboBox7.Text = "";
            textBox2.Text = "";
            MessageBox.Show(WriteRequest.Deal + " добавлен");
            for (int j = 0; j < WriteRequest.CargoCount; j++)
            {
                dataGridView1.Rows.Add();
                dataGridView1["cargoName", j].Value = WriteRequest.Cargo[j];
                dataGridView1["cargoNum", j].Value = WriteRequest.numCargo[j];
            }

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }*/

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "Б/Н")
            {
                textBox5.Text = "";
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "Б/Н";
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox11.Text = comboBox7.Text;
            /*comboBox9.Items.Clear();
            string Query = "SELECT * FROM `drivers` WHERE `contractor` = '" + comboBox7.Text.ToString() + "' ORDER BY `name` ASC;";
            //string Query = "(SELECT * FROM `drivers` WHERE `contractor` = '" + comboBox7.Text.ToString() + "' ORDER BY `name` ASC) ORDER BY `count` desc;";
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
                    string objName = myReader.GetString("name");
                    if (objName.Equals(""))
                    {
                        continue;
                    }
                    comboBox9.Items.Add(objName);
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/

            DB db = new DB();
            comboBox8.Items.Clear();
            string Query = "SELECT * FROM `cars` WHERE `contractor` = '" + comboBox7.Text.ToString() + "' ORDER BY `name` ASC;";
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
                    comboBox8.Items.Add(objName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i = int.Parse(textBox3.Text);
            if(i == 1)
            {
                MessageBox.Show("Невозможно сделать меньше");
            }
            else
            {
                i--;
                textBox3.Text = i.ToString();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int i = int.Parse(textBox3.Text);
            i++;
            textBox3.Text = i.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                label11.Show();
                checkBox2.Show();
            }
            else
            {
                label11.Hide();
                checkBox2.Hide();
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            label13.Show();
            label13.Text = "Расценок \nдля ЗП";
            textBox1.Show();
            label24.Show();
            label24.Text = "Расценок \nпокупателю";
            textBox4.Show();

            //string Query = "SELECT * FROM `counterparty` WHERE `objectName` = '" + comboBox6.Text.ToString() + "';";
            string Query = "(SELECT * FROM `counterparty` WHERE `objectName` = '" + comboBox6.Text.ToString() + "' ORDER BY `name` ASC) ORDER BY `count` desc;";
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
                    int priceCount = myReader.GetInt32("priceCount");
                    string objName = "price" + priceCount;
                    string pricec = myReader.GetString(objName);
                    priceNow = pricec;
                    priceNowCount = priceCount;
                    textBox1.Text = pricec;
                    int priceCountBuy = myReader.GetInt32("priceBuyerCount");
                    string objNameBuy = "priceBuyer" + priceCountBuy;
                    string pricecBuy = myReader.GetString(objNameBuy);
                    priceNowBuy = pricecBuy;
                    priceNowCountBuy = priceCountBuy;
                    textBox4.Text = pricecBuy;
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }
        
        public void CheckPrices()
        {
            DB db = new DB();
            string placePrice = "";
            string placePriceBuy = "";
            if (!priceNow.Equals(textBox1.Text)) {
                priceNow = textBox1.Text;
                priceNowCount++;
            }
            if (!priceNowBuy.Equals(textBox4.Text))
            {
                priceNowBuy = textBox4.Text;
                priceNowCountBuy++;
            }
            placePrice = "price" + priceNowCount;
            placePriceBuy = "priceBuyer" + priceNowCountBuy;
            string Query = "UPDATE `counterparty` SET " + placePrice + " = " + priceNow + ", priceCount = " + priceNowCount + ", " + placePriceBuy + " = " + priceNowBuy + ", priceBuyerCount = " + priceNowCountBuy + " WHERE `objectName` = '" + comboBox6.Text + "';";
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            db.openConnection();
            cmdDataBase.ExecuteNonQuery();
            db.closeConnection();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {   
            if(textBox2.Text.Equals("Добавьте комментарий"))
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text.Equals(""))
            {
                textBox2.Text = "Добавьте комментарий";
                textBox2.ForeColor = Color.Gray;
            }
        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox9.Items.Clear();
            string Query = "SELECT * FROM `drivers` WHERE `contractor` = '" + comboBox11.Text.ToString() + "' ORDER BY `name` ASC;";
            //string Query = "(SELECT * FROM `drivers` WHERE `contractor` = '" + comboBox7.Text.ToString() + "' ORDER BY `name` ASC) ORDER BY `count` desc;";
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
                    string objName = myReader.GetString("name");
                    if (objName.Equals(""))
                    {
                        continue;
                    }
                    comboBox9.Items.Add(objName);
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox12.Items.Clear();
            //string Query = "SELECT * FROM `counterparty` WHERE `name` = '" + comboBox3.Text.ToString() + "' ORDER BY `objectName` ASC;";
            string Query = "(SELECT * FROM `counterparty` WHERE `name` = '" + comboBox4.Text.ToString() + "' ORDER BY `objectName` ASC) ORDER BY `count` desc;";
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
                    comboBox12.Items.Add(objName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }

    }
}

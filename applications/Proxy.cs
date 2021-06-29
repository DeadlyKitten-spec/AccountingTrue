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

namespace applications
{
    public partial class Proxy : Form
    {
        Color FlatColor;
        public Proxy()
        {
            this.WindowState = FormWindowState.Maximized;
            this.ControlBox = false;
            InitializeComponent();
            FillCombo2();
            FillCombo4();
            FillDGV();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

            button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button3.FlatAppearance.BorderColor = FlatColor;

            button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button4.FlatAppearance.BorderColor = FlatColor;

            button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            comboBox5.Text = "По всем";
            comboBox1.Text = "Товар";
        }

        void FillDGV()
        {
            dataGridView1.Rows.Clear();
            DateTime dt = DateTime.Today.Date;
            string dtt = dt.ToString();
            string[] fs = dtt.Split(' ');
            //MessageBox.Show(fs[0]);
            string[] fss = fs[0].Split('.');
            dtt = fss[2] + "-" + fss[1] + "-" + "1";
            //WHERE `date` BETWEEN '" + answer1 + "' AND '" + answer2 + "'
            string dttt = fss[2] + "-" + fss[1] + "-" + fss[0];
            string Query = "SELECT * FROM `proxy` WHERE `date` BETWEEN '"+ dtt +"' AND '" + dttt + "'ORDER BY `date` ASC;";
            DB db = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            //string[] names = new string[100];
            try
            {
                db.openConnection();
                myReader = cmdDataBase.ExecuteReader();

                int j = 0;
                while (myReader.Read())
                {
                    dataGridView1.Rows.Add();
                    string td = myReader.GetString("date");
                    string[] tdd = td.Split(' ');
                    dataGridView1[0, j].Value = tdd[0];
                    dataGridView1[1, j].Value = myReader.GetString("type");
                    dataGridView1[2, j].Value = myReader.GetString("numberDoc");
                    dataGridView1[3, j].Value = myReader.GetString("counterparty");
                    dataGridView1[4, j].Value = myReader.GetString("nameCargo");
                    dataGridView1[5, j].Value = myReader.GetString("cargoCount");
                    dataGridView1[6, j].Value = myReader.GetString("price");
                    dataGridView1[7, j].Value = myReader.GetString("sum");
                    double change = double.Parse(myReader.GetString("change"));
                    if (change < 0)
                    {
                        dataGridView1[8, j].Value = 0;
                    }
                    else
                    {
                        dataGridView1[8, j].Value = myReader.GetString("change");
                    }
                    j++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        void FillCombo2()
        {
            string Query = "SELECT * FROM `counterparty` " + /*WHERE status = 'Грузополучатель/грузоотправитель' OR status = 'Диспетчер'*/  "ORDER BY `name` ASC;";
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
            string Query = "SELECT * FROM `counterparty` " + /*WHERE status = 'Грузополучатель/грузоотправитель' OR status = 'Диспетчер'*/  "ORDER BY `name` ASC;";
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `proxy` (`date`, `type`, `counterparty`, `numberDoc`, `nameCargo`, `cargoCount`, `price`, `sum`, `change`) VALUES (@date, @type, @counter, @number, @cargo, @count, @price, @sum, @change)", db.getConnection());

            command.Parameters.Add("@date", MySqlDbType.Date).Value = dateTimePicker1.Value;
            command.Parameters.Add("@type", MySqlDbType.VarChar).Value = comboBox1.Text;
            command.Parameters.Add("@counter", MySqlDbType.VarChar).Value = comboBox2.Text;
            command.Parameters.Add("@number", MySqlDbType.VarChar).Value = textBox1.Text;
            command.Parameters.Add("@cargo", MySqlDbType.VarChar).Value = comboBox3.Text;
            for(int i = 0; i < textBox2.Text.Length; i++)
            {
                if(textBox2.Text[i] == '.')
                {
                    string tar = "";
                    string[] asdf = textBox2.Text.Split('.');
                    tar = asdf[0] + "," + asdf[1];
                    textBox2.Text = tar;
                    break;
                }
            }
            command.Parameters.Add("@count", MySqlDbType.VarChar).Value = textBox2.Text;
            for (int i = 0; i < textBox3.Text.Length; i++)
            {
                if (textBox3.Text[i] == '.')
                {
                    string tar = "";
                    string[] asdf = textBox3.Text.Split('.');
                    tar = asdf[0] + "," + asdf[1];
                    textBox3.Text = tar;
                    break;
                }
            }
            if (textBox3.Text.Equals(""))
            {
                command.Parameters.Add("@price", MySqlDbType.VarChar).Value = "пусто";
            }
            else
            {
                command.Parameters.Add("@price", MySqlDbType.VarChar).Value = textBox3.Text;
            }
            if (textBox4.Text.Equals(""))
            {
                command.Parameters.Add("@sum", MySqlDbType.VarChar).Value = "пусто";
            }
            else
            {
                command.Parameters.Add("@sum", MySqlDbType.VarChar).Value = textBox4.Text;
            }
            command.Parameters.Add("@change", MySqlDbType.VarChar).Value = textBox2.Text;
            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            string counterpp = "";
            if (!comboBox4.Text.Equals(""))
            {
                counterpp = "AND `counterparty` = '" + comboBox4.Text + "' ";
            }
            dataGridView1.Rows.Clear();
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
            string Query = "";
            if (comboBox5.Text.Equals("По всем"))
            {
                Query = "SELECT * FROM `proxy` WHERE `date` BETWEEN '" + answer1 + "' AND '" + answer2 + "' " + counterpp + " ORDER BY `date` ASC;";
            }
            else
            {
                Query = "SELECT * FROM `proxy` WHERE `date` BETWEEN '" + answer1 + "' AND '" + answer2 + "' " + counterpp + " AND `type` = '" + comboBox5.Text + "'ORDER BY `date` ASC;";
            }
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            //string[] names = new string[100];
            try
            {
                db.openConnection();
                myReader = cmdDataBase.ExecuteReader();

                int j = 0;
                while (myReader.Read())
                {
                    dataGridView1.Rows.Add();
                    string td = myReader.GetString("date");
                    string[] tdd = td.Split(' ');
                    dataGridView1[0, j].Value = tdd[0];
                    dataGridView1[1, j].Value = myReader.GetString("type");
                    dataGridView1[2, j].Value = myReader.GetString("numberDoc");
                    dataGridView1[3, j].Value = myReader.GetString("counterparty");
                    dataGridView1[4, j].Value = myReader.GetString("nameCargo");
                    dataGridView1[5, j].Value = myReader.GetString("cargoCount");
                    dataGridView1[6, j].Value = myReader.GetString("price");
                    dataGridView1[7, j].Value = myReader.GetString("sum");
                    double change = double.Parse(myReader.GetString("change"));
                    if (change < 0)
                    {
                        dataGridView1[8, j].Value = 0;
                    }
                    else
                    {
                        dataGridView1[8, j].Value = myReader.GetString("change");
                    }
                    j++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dateTimePicker1.ResetText();
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals("Товар"))
            {
                label9.Show();
                button1.Show();
                textBox4.Show();
                label6.Text = "Наименование\nтовара";
                comboBox3.Items.Clear();
                DB db = new DB();
                string Query = "SELECT * FROM `product` ORDER BY `name` ASC";
                MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
                MySqlDataReader myReader;
                try
                {
                    db.openConnection();
                    myReader = cmdDataBase.ExecuteReader();

                    while (myReader.Read())
                    {
                        string objName = myReader.GetString("name");
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
                label8.Show();
                textBox3.Show();
                button1.Show();
            }
            else
            {
                label9.Hide();
                button1.Hide();
                textBox4.Hide();
                label6.Text = "Наименование\nгруза";
                comboBox3.Items.Clear();
                DB db = new DB();
                string Query = "SELECT * FROM `namecargo` ORDER BY `name` ASC";
                MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
                MySqlDataReader myReader;
                try
                {
                    db.openConnection();
                    myReader = cmdDataBase.ExecuteReader();

                    while (myReader.Read())
                    {
                        string objName = myReader.GetString("name");
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
                label8.Hide();
                textBox3.Hide();
                button1.Hide();
            }
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
            int year = dateTimePicker3.Value.Date.Year;
            int month = dateTimePicker3.Value.Date.Month;
            int day = dateTimePicker3.Value.Date.Day;
            int[] dateArr = visok(dateTimePicker3.Value.Date.Year);
            if (day == 1)
            {
                month--;
                if (month == 0)
                {
                    month = 12;
                    year--;
                }
                day = dateArr[month - 1];
                dateTimePicker3.Value = new DateTime(year, month, day);
            }
            else
            {
                dateTimePicker3.Value = new DateTime(year, month, day - 1);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int year = dateTimePicker3.Value.Date.Year;
            int month = dateTimePicker3.Value.Date.Month;
            int day = dateTimePicker3.Value.Date.Day;
            int[] dateArr = visok(dateTimePicker3.Value.Date.Year);
            if (day == dateArr[month - 1])
            {
                if (month == 12)
                {
                    month = 0;
                    year++;
                }
                day = 1;
                dateTimePicker3.Value = new DateTime(year, month + 1, day);
            }
            else
            {
                dateTimePicker3.Value = new DateTime(year, month, day + 1);
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            /*if (!textBox2.Text.Equals(""))
            {
                textBox4.Text = (Double.Parse(textBox2.Text) * Double.Parse(textBox3.Text)).ToString();
            }*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox5.Text.Equals(""))
            {
                MessageBox.Show("Вы не выбрали вид номенклатуры");
            }
            else
            {
                string counterpp = "";
                if (!comboBox4.Text.Equals(""))
                {
                    counterpp = "AND `counterparty` = '" + comboBox4.Text + "' ";
                }
                dataGridView1.Rows.Clear();
                string time = dateTimePicker3.Value.ToString();
                string[] asd = time.Split(' ');
                string[] zxc = asd[0].Split('.');
                string answer1 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];

                time = dateTimePicker2.Value.ToString();
                asd = null;
                asd = time.Split(' ');
                zxc = null;
                zxc = asd[0].Split('.');
                string answer2 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];
                string Query = "";
                if (comboBox5.Text.Equals("По всем"))
                {
                    Query = "SELECT * FROM `proxy` WHERE `date` BETWEEN '" + answer1 + "' AND '" + answer2 + "' " + counterpp + " ORDER BY `date` ASC;";
                }
                else
                {
                    Query = "SELECT * FROM `proxy` WHERE `date` BETWEEN '" + answer1 + "' AND '" + answer2 + "' " + counterpp + " AND `type` = '" + comboBox5.Text + "'ORDER BY `date` ASC;";
                }
                DB db = new DB();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
                MySqlDataReader myReader;
                //string[] names = new string[100];
                //MessageBox.Show(Query);
                try
                {
                    db.openConnection();
                    myReader = cmdDataBase.ExecuteReader();

                    int j = 0;
                    while (myReader.Read())
                    {
                        //MessageBox.Show("das");
                        dataGridView1.Rows.Add();
                        string td = myReader.GetString("date");
                        //MessageBox.Show("das");
                        string[] tdd = td.Split(' ');
                        dataGridView1[0, j].Value = tdd[0];
                        dataGridView1[1, j].Value = myReader.GetString("type");
                        dataGridView1[2, j].Value = myReader.GetString("numberDoc");
                        dataGridView1[3, j].Value = myReader.GetString("counterparty");
                        dataGridView1[4, j].Value = myReader.GetString("nameCargo");
                        dataGridView1[5, j].Value = myReader.GetString("cargoCount");
                        dataGridView1[6, j].Value = myReader.GetString("price");
                        dataGridView1[7, j].Value = myReader.GetString("sum");
                        double change = double.Parse(myReader.GetString("change"));
                        if (change < 0)
                        {
                            dataGridView1[8, j].Value = 0;
                        }
                        else
                        {
                            dataGridView1[8, j].Value = myReader.GetString("change");
                        }
                        j++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                db.closeConnection();

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dateTimePicker2.ResetText();
            dateTimePicker3.ResetText();
            comboBox4.Text = "";
            comboBox5.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < textBox2.Text.Length; i++)
            {
                if (textBox2.Text[i] == '.')
                {
                    string tar = "";
                    string[] asd = textBox2.Text.Split('.');
                    tar = asd[0] + "," + asd[1];
                    textBox2.Text = tar;
                }
            }
            for (int i = 0; i < textBox3.Text.Length; i++)
            {
                if (textBox3.Text[i] == '.')
                {
                    string tarr = "";
                    string[] asdd = textBox3.Text.Split('.');
                    tarr = asdd[0] + "," + asdd[1];
                    textBox3.Text = tarr;
                }
            }

            if (!textBox2.Text.Equals("") && !textBox3.Text.Equals(""))
            {
                textBox4.Text = Math.Round((Double.Parse(textBox2.Text) * Double.Parse(textBox3.Text)), 2).ToString();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string[] tmm = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString().Split('.');
            //MessageBox.Show(objName);
            //MessageBox.Show(tm.Length.ToString());
            dateTimePicker1.Value = new DateTime(int.Parse(tmm[2]), int.Parse(tmm[1]), int.Parse(tmm[0]));
            comboBox1.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            textBox1.Text = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            comboBox2.Text = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            comboBox3.Text = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
            textBox2.Text = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();
            textBox3.Text = dataGridView1[6, dataGridView1.CurrentRow.Index].Value.ToString();
            textBox4.Text = dataGridView1[7, dataGridView1.CurrentRow.Index].Value.ToString();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            comboBox4.Text = "";
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            comboBox5.Text = "";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string dt = dateTimePicker1.Value.ToString();
            string[] dtt = dt.Split(' ');
            //MessageBox.Show(dtt[0]);
            string[] dttt = dtt[0].Split('.');
            dt = dttt[2] + "-" + dttt[1] + "-" + dttt[0];
            string ddt = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            string[] ddtt = ddt.Split(' ');
            string[] ddttt = ddtt[0].Split('.');
            ddt = ddttt[2] + "-" + ddttt[1] + "-" + ddttt[0];
            DB db = new DB();
            string query = "UPDATE `proxy` SET `date` = '" + dt + "', `type` = '" + comboBox1.Text + "', `counterparty` = '" + comboBox2.Text + "', `numberDoc` = '" + textBox1.Text + "', `nameCargo` = '" + comboBox3.Text + "', `cargoCount` = '" + textBox2.Text + "', `price` = '" + textBox3.Text + "', `sum` = '" + textBox4.Text + "' WHERE `date` = '" + ddt + "' AND `counterparty` = '" + dataGridView1[3, dataGridView1.CurrentRow.Index].Value + "' AND `numberDoc` = '" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value + "' AND `nameCargo` = '" + dataGridView1[4, dataGridView1.CurrentRow.Index].Value + "';";
            MySqlCommand command = new MySqlCommand(query , db.getConnection());
            //MessageBox.Show(query);
            
            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            string counterpp = "";
            if (!comboBox4.Text.Equals(""))
            {
                counterpp = "AND `counterparty` = '" + comboBox4.Text + "' ";
            }
            dataGridView1.Rows.Clear();
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
            string Query = "";
            if (comboBox5.Text.Equals("По всем"))
            {
                Query = "SELECT * FROM `proxy` WHERE `date` BETWEEN '" + answer1 + "' AND '" + answer2 + "' " + counterpp + " ORDER BY `date` ASC;";
            }
            else
            {
                Query = "SELECT * FROM `proxy` WHERE `date` BETWEEN '" + answer1 + "' AND '" + answer2 + "' " + counterpp + " AND `type` = '" + comboBox5.Text + "'ORDER BY `date` ASC;";
            }
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            //string[] names = new string[100];
            try
            {
                db.openConnection();
                myReader = cmdDataBase.ExecuteReader();

                int j = 0;
                while (myReader.Read())
                {
                    dataGridView1.Rows.Add();
                    string td = myReader.GetString("date");
                    string[] tdd = td.Split(' ');
                    dataGridView1[0, j].Value = tdd[0];
                    dataGridView1[1, j].Value = myReader.GetString("type");
                    dataGridView1[2, j].Value = myReader.GetString("numberDoc");
                    dataGridView1[3, j].Value = myReader.GetString("counterparty");
                    dataGridView1[4, j].Value = myReader.GetString("nameCargo");
                    dataGridView1[5, j].Value = myReader.GetString("cargoCount");
                    dataGridView1[6, j].Value = myReader.GetString("price");
                    dataGridView1[7, j].Value = myReader.GetString("sum");
                    double change = double.Parse(myReader.GetString("change"));
                    if (change < 0)
                    {
                        dataGridView1[8, j].Value = 0;
                    }
                    else
                    {
                        dataGridView1[8, j].Value = myReader.GetString("change");
                    }
                    j++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dateTimePicker1.ResetText();
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            string ddt = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            string[] ddtt = ddt.Split(' ');
            string[] ddttt = ddtt[0].Split('.');
            ddt = ddttt[2] + "-" + ddttt[1] + "-" + ddttt[0];
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("DELETE FROM `proxy` WHERE `date` = '" + ddt + "' AND `counterparty` = '" + dataGridView1[3, dataGridView1.CurrentRow.Index].Value + "' AND `numberDoc` = '" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value + "' AND `nameCargo` = '" + dataGridView1[4, dataGridView1.CurrentRow.Index].Value + "'", db.getConnection());


            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            string counterpp = "";
            if (!comboBox4.Text.Equals(""))
            {
                counterpp = "AND `counterparty` = '" + comboBox4.Text + "' ";
            }
            dataGridView1.Rows.Clear();
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
            string Query = "";
            if (comboBox5.Text.Equals("По всем"))
            {
                Query = "SELECT * FROM `proxy` WHERE `date` BETWEEN '" + answer1 + "' AND '" + answer2 + "' " + counterpp + " ORDER BY `date` ASC;";
            }
            else
            {
                Query = "SELECT * FROM `proxy` WHERE `date` BETWEEN '" + answer1 + "' AND '" + answer2 + "' " + counterpp + " AND `type` = '" + comboBox5.Text + "'ORDER BY `date` ASC;";
            }
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            //string[] names = new string[100];
            try
            {
                db.openConnection();
                myReader = cmdDataBase.ExecuteReader();

                int j = 0;
                while (myReader.Read())
                {
                    dataGridView1.Rows.Add();
                    string td = myReader.GetString("date");
                    string[] tdd = td.Split(' ');
                    dataGridView1[0, j].Value = tdd[0];
                    dataGridView1[1, j].Value = myReader.GetString("type");
                    dataGridView1[2, j].Value = myReader.GetString("numberDoc");
                    dataGridView1[3, j].Value = myReader.GetString("counterparty");
                    dataGridView1[4, j].Value = myReader.GetString("nameCargo");
                    dataGridView1[5, j].Value = myReader.GetString("cargoCount");
                    dataGridView1[6, j].Value = myReader.GetString("price");
                    dataGridView1[7, j].Value = myReader.GetString("sum");
                    double change = double.Parse(myReader.GetString("change"));
                    if (change < 0)
                    {
                        dataGridView1[8, j].Value = 0;
                    }
                    else
                    {
                        dataGridView1[8, j].Value = myReader.GetString("change");
                    }
                    j++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dateTimePicker1.ResetText();
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }
    }
}

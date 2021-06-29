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
    public partial class PersonalStatBuy : Form
    {
        Color FlatColor;
        public PersonalStatBuy()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            FlatColor = button1.FlatAppearance.BorderColor;

            button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button6.FlatAppearance.BorderColor = FlatColor;

            button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button8.FlatAppearance.BorderColor = FlatColor;

            FillCombo1();
            FillCombo4();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox4.Text = "";
            dateTimePicker1.ResetText();
            dateTimePicker2.ResetText();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void button7_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
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

        void FillCombo1()
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
                        comboBox1.Items.Add(objName);
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
            string Query = "SELECT * FROM `counterparty` WHERE status = 'Грузополучатель/грузоотправитель' OR status = 'Грузоотправитель' ORDER BY `name` ASC;";
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

        private void button6_Click(object sender, EventArgs e)
        {
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
            string Line = "";
            if (comboBox4.Text.Equals(""))
            {
                Line = "SELECT * FROM `request` WHERE `buyer` = '" + comboBox1.Text + "' AND `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `status` = 'Исполнена' ORDER BY `object`, `id` ASC;";
            }
            else
            {
                Line = "SELECT * FROM `request` WHERE `buyer` = '" + comboBox1.Text + "' AND `sender` = '" + comboBox4.Text + "' AND `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `status` = 'Исполнена' ORDER BY `object`, `id` ASC;";
            }
            //MessageBox.Show(Line);
            DB db = new DB();
            MySqlCommand command = new MySqlCommand(Line, db.getConnection());
            db.openConnection();
            object obj = command.ExecuteScalar();

            db.closeConnection();
            if (obj != null)
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlDataReader myReader;
                dgv.Columns.Clear();
                dgv.Rows.Clear();
                dgv.Columns.AddRange(
                new DataGridViewTextBoxColumn() { Name = "object", HeaderText = "Объект покупателя" },
                new DataGridViewTextBoxColumn() { Name = "nomenclature", HeaderText = "Номенклатура" },
                new DataGridViewTextBoxColumn() { Name = "numberNom", HeaderText = "Количество" },
                new DataGridViewTextBoxColumn() { Name = "trip", HeaderText = "Рейсы" });
                //Print
                PSBUY[] ps = new PSBUY[1000];
                for(int i = 0; i < 1000; i++)
                {
                    ps[i] = new PSBUY();
                    ps[i].objectBuy = "";
                    ps[i].nomen = "";
                    ps[i].num = 0;
                    ps[i].trip = 0;
                }
                int itter = 0;
                bool f = false;
                string req = "";
                string reqm = "";
                int tripMax = 0;
                dgv.Rows.Clear();
                try
                {
                    db.openConnection();
                    myReader = command.ExecuteReader();
                    while (myReader.Read())
                    {
                        for(int i = 0; i < itter; i++)
                        {
                            if (ps[i].objectBuy.Equals(myReader.GetString("object"))){
                                if (ps[i].nomen.Equals(myReader.GetString("nameCargo"))) {
                                    ps[i].num += double.Parse(myReader.GetString("numberNomenclature"));
                                    f = true;
                                    if (!req.Equals(myReader.GetString("id")))
                                    {
                                        ps[i].trip++;
                                        tripMax++;
                                        req = myReader.GetString("id");
                                    }
                                }
                            }
                        }
                        if (!f)
                        {
                            ps[itter].objectBuy = myReader.GetString("object");
                            ps[itter].nomen = myReader.GetString("nameCargo");
                            ps[itter].num += double.Parse(myReader.GetString("numberNomenclature"));
                            ps[itter].trip = 1;
                            //tripMax++;
                            itter++;
                            if (!reqm.Equals(myReader.GetString("id")))
                            {
                                tripMax++;
                                reqm = myReader.GetString("id");
                            }
                        }
                        f = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                int ro = 0;
                int tripCount = 0;
                double nomCount = 0;
                //int tripCountSum = 0;
                double nomCountSum = 0;
                for(int i = 0; i < itter; i++)
                {
                    dgv.Rows.Add();
                    dgv[0, ro].Value = ps[i].objectBuy;
                    dgv[1, ro].Value = ps[i].nomen;
                    dgv[2, ro].Value = ps[i].num;
                    dgv[3, ro].Value = ps[i].trip;
                    tripCount += ps[i].trip;
                    nomCount += ps[i].num;
                    if((i+1 == itter) || (!ps[i].objectBuy.Equals(ps[i + 1].objectBuy)))
                    {
                        dgv.Rows.Add();
                        ro++;
                        dgv[0, ro].Value = "Итого";
                        dgv[2, ro].Value = nomCount;
                        dgv[3, ro].Value = tripCount;
                        dgv.Rows[ro].DefaultCellStyle.BackColor = Color.LightGray;
                        //tripCountSum += tripCount;
                        nomCountSum += nomCount;
                        tripCount = 0;
                        nomCount = 0;
                    }
                    ro++;
                }
                dgv.Rows.Add();
                //dgv.Rows.Add();
                dgv[0, ro].Value = "Всего";
                dgv[2, ro].Value = nomCountSum;
                dgv[3, ro].Value = tripMax;
                dgv.Rows[ro].DefaultCellStyle.BackColor = Color.LightGray;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                db.closeConnection();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            comboBox4.Text = "";
        }
    }
}

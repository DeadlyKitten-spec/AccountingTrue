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
    public partial class VaultStatement : Form
    {
        Color FlatColor;
        public VaultStatement()
        {
            InitializeComponent();
            LoadDGV3();
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button5.FlatAppearance.BorderColor = FlatColor;
        }

        void LoadDGV3()
        {
            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();
            dataGridView3.Columns.AddRange(
            new DataGridViewTextBoxColumn() { Name = "product", HeaderText = "Наименование" },
            new DataGridViewTextBoxColumn() { Name = "count", HeaderText = "Кол-во" });

            DB db = new DB();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `product` ORDER BY `name` ASC", db.getConnection());
            MySqlDataReader myReader;
            int k = 0;
            string[] names = new string[100];
            try
            {
                db.openConnection();
                myReader = command.ExecuteReader();
                int j = 0;
                while (myReader.Read())
                {
                    string objName = myReader.GetString("name");
                    if (objName.Equals("") || objName.Equals(" "))
                    {
                        continue;
                    }
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
                        dataGridView3.Rows.Add();
                        //dataGridView1[0, k].Value = k + 1;
                        dataGridView3[0, k].Value = objName;
                        dataGridView3[1, k].Value = myReader.GetString("count");
                        k++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void button12_Click(object sender, EventArgs e)
        {
            int year = dateTimePicker4.Value.Date.Year;
            int month = dateTimePicker4.Value.Date.Month;
            int day = dateTimePicker4.Value.Date.Day;
            int[] dateArr = visok(dateTimePicker4.Value.Date.Year);
            if (day == 1)
            {
                month--;
                if (month == 0)
                {
                    month = 12;
                    year--;
                }
                day = dateArr[month - 1];
                dateTimePicker4.Value = new DateTime(year, month, day);
            }
            else
            {
                dateTimePicker4.Value = new DateTime(year, month, day - 1);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int year = dateTimePicker4.Value.Date.Year;
            int month = dateTimePicker4.Value.Date.Month;
            int day = dateTimePicker4.Value.Date.Day;
            int[] dateArr = visok(dateTimePicker4.Value.Date.Year);
            if (day == dateArr[month - 1])
            {
                if (month == 12)
                {
                    month = 0;
                    year++;
                }
                day = 1;
                dateTimePicker4.Value = new DateTime(year, month + 1, day);
            }
            else
            {
                dateTimePicker4.Value = new DateTime(year, month, day + 1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.AddRange(
                new DataGridViewTextBoxColumn() { Name = "date", HeaderText = "Дата" },
                new DataGridViewTextBoxColumn() { Name = "operation", HeaderText = "Операция" },
                new DataGridViewTextBoxColumn() { Name = "product", HeaderText = "Товар" },
                new DataGridViewTextBoxColumn() { Name = "count", HeaderText = "Количество" });

            string time = dateTimePicker4.Value.ToString();
            string[] asd = time.Split(' ');
            string[] zxc = asd[0].Split('.');
            string answer1 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];

            time = dateTimePicker1.Value.ToString();
            asd = null;
            asd = time.Split(' ');
            zxc = null;
            zxc = asd[0].Split('.');
            string answer2 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];

            DB db = new DB();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `operations` WHERE `date` BETWEEN '" + answer1 + "' AND '" + answer2 + "'ORDER BY `date` ASC", db.getConnection());
            MySqlDataReader myReader;
            int k = 0;
            try
            {
                db.openConnection();
                myReader = command.ExecuteReader();
                int j = 0;
                while (myReader.Read())
                {
                    dataGridView1.Rows.Add();
                    string[] fordate = myReader.GetString("date").Split(' ');
                    dataGridView1[0, k].Value = fordate[0];
                    dataGridView1[1, k].Value = myReader.GetString("operation");
                    dataGridView1[2, k].Value = myReader.GetString("product");
                    dataGridView1[3, k].Value = myReader.GetString("count");
                    k++;
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
}

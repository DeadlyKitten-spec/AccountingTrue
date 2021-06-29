using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PagedList;
using DGVPrinterHelper;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;

namespace applications
{
    public partial class StatementBuyer : Form
    {
        Color FlatColor;
        public StatementBuyer()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            FlatColor = button1.FlatAppearance.BorderColor;

            button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = FlatColor;

            button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button6.FlatAppearance.BorderColor = FlatColor;

            button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button2.FlatAppearance.BorderColor = FlatColor;

            button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button3.FlatAppearance.BorderColor = FlatColor;

            button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button8.FlatAppearance.BorderColor = FlatColor;

            comboBox1.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";

            FillCombo1();
            FillCombo2();
            FillCombo3();
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //this.Size = new Size(600, 800);
            //this.TopMost = true;
            this.ControlBox = false;
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

            bool cm1 = false;
            bool cm2 = false;
            bool cm3 = false;
            bool cm4 = false;
            string ln1 = "";
            string ln2 = "";
            string ln3 = "";
            string ln4 = "";
            if (!comboBox2.Text.Equals(""))
            {
                cm2 = true;
                ln2 = "`deal` = '" + comboBox2.Text + "' AND ";
            }
            if (!comboBox1.Text.Equals(""))
            {
                cm1 = true;
                ln1 = "`buyer` = '" + comboBox1.Text + "' AND ";
            }
            if (!comboBox3.Text.Equals(""))
            {
                cm3 = true;
                ln3 = "`ourFirms` = '" + comboBox3.Text + "' AND ";
            }
            if (!comboBox4.Text.Equals(""))
            {
                cm4 = true;
                ln4 = "`object` = '" + comboBox4.Text + "' AND ";
            }
            string Line = "SELECT * FROM `request` WHERE " + ln2 + ln1 + ln3 + ln4 + " `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "'" + " AND `status` = 'Исполнена';";
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
                new DataGridViewTextBoxColumn() { Name = "docDate", HeaderText = "Дата" },
                //new DataGridViewTextBoxColumn() { Name = "buyer", HeaderText = "Покупатель/заказчик" },
                //new DataGridViewTextBoxColumn() { Name = "object", HeaderText = "Объект" },
                new DataGridViewTextBoxColumn() { Name = "nameCargo", HeaderText = "Наименование" },
                new DataGridViewTextBoxColumn() { Name = "numberNom", HeaderText = "Кол-во номенклатуры" },
                new DataGridViewTextBoxColumn() { Name = "numberDocTrip", HeaderText = "Номер ТТН" });
                dgv.Rows.Clear();
                try
                {
                    db.openConnection();
                    myReader = command.ExecuteReader();
                    int i = 0;
                    int sum = 0;
                    while (myReader.Read())
                    {
                        //MessageBox.Show("asd");
                        dgv.Rows.Add();
                        string datefirst = myReader.GetString("docDate");
                        string[] datesplit = datefirst.Split(' ');
                        dgv[0, i].Value = datesplit[0];
                        //dgv[0, i].Value = myReader.GetString("docDate");
                        if (!myReader.GetString("nameCargo").Equals("пусто"))
                            dgv[1, i].Value = myReader.GetString("nameCargo");
                        else
                            dgv[1, i].Value = " ";
                        if (!myReader.GetString("numberNomenclature").Equals("0"))
                            dgv[2, i].Value = myReader.GetString("numberNomenclature");
                        else
                            dgv[2, i].Value = " ";
                        if (!myReader.GetString("numberDocTrip").Equals("101"))
                            dgv[3, i].Value = myReader.GetString("numberDocTrip");
                        else
                            dgv[3, i].Value = " ";
                        sum += int.Parse(myReader.GetString("numberNomenclature"));
                        i++;
                    }
                    //dgv.Rows.Add();
                    dgv[0, i ].Value = "Всего";
                    dgv[2, i ].Value = sum;
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                db.closeConnection();
            }

        }

        void FillCombo1()
        {
            string Query = "SELECT * FROM `counterparty` ORDER BY `name` ASC";
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

        void FillCombo2()
        {
            string[] combobox1items = new string[] { "Поставка", "Перевозка" };
            comboBox2.Items.AddRange(combobox1items);
            comboBox2.SelectedIndex = 0;
        }

        void FillCombo3()
        {
            //string Query = "(SELECT * FROM `counterparty` WHERE status = 'Грузополучатель/грузоотправитель' OR status = 'Грузополучатель' ORDER BY `name` ASC) ORDER BY `count` DESC";
            string Query = "SELECT * FROM `ourFirms` ORDER BY `name` ASC";
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
                    /*bool f = true;
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
                    }*/
                    //if (f == true)
                    //{
                    //    names[j] = objName;
                    //    j++;
                        comboBox3.Items.Add(objName);
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex == 0)
            {
                label2.Text = "Покупатель";
            }
            else
            {
                label2.Text = "Заказчик";
            }
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

        private void button8_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            dateTimePicker1.ResetText();
            dateTimePicker2.ResetText();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            comboBox3.Text = "";
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            comboBox4.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            string Query = "SELECT * FROM `counterparty` WHERE `name` = '" + comboBox1.Text.ToString() + "' ORDER BY `objectName` ASC;";
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
                    comboBox4.Items.Add(objName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }

        public string printLine = "Ведомость";
        public string printLineDateFirst = "";
        public string printLineDateSecond = "";
        public string printLineSeller = "";
        public string printLineBuyer = "";
        public string printLineObject = "";
        public string printLineThing = "";

        void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
           /* e.Graphics.DrawString(printLine, new Font("Tahoma", 28), Brushes.Black, 50, 50);
            e.Graphics.DrawString(printLineDateFirst, new Font("Tahoma", 15), Brushes.Black, 100, 125);
            e.Graphics.DrawString(printLineDateSecond, new Font("Tahoma", 15), Brushes.Black, 100, 175);
            e.Graphics.DrawString(printLineSeller, new Font("Tahoma", 15), Brushes.Black, 100, 225);
            e.Graphics.DrawString(printLineBuyer, new Font("Tahoma", 15), Brushes.Black, 100, 275);
            e.Graphics.DrawString(printLineObject, new Font("Tahoma", 15), Brushes.Black, 100, 325);
            e.Graphics.DrawString(printLineThing, new Font("Tahoma", 15), Brushes.Black, 100, 375);
            e.Graphics*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("docDate", typeof(string));
            dt.Columns.Add("nameCargo", typeof(string));
            dt.Columns.Add("numberNom", typeof(string));
            dt.Columns.Add("numberDocTrip", typeof(string));
            foreach(DataGridViewRow dgv in dgv.Rows)
            {
                dt.Rows.Add(dgv.Cells[0].Value, dgv.Cells[1].Value, dgv.Cells[2].Value, dgv.Cells[3].Value);
            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("Sample.xml");
            CRForBuyer1 cr = new CRForBuyer1();
            TextObject text = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text11"];
            text.Text = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            TextObject text1 = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text12"];
            text1.Text = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            TextObject text2 = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text13"];
            text2.Text = comboBox3.Text;
            TextObject text3 = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text14"];
            text3.Text = comboBox1.Text;
            TextObject text4 = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text15"];
            text4.Text = comboBox4.Text;
            cr.SetDataSource(ds);
            CRViewer crv = new CRViewer();
            crv.crystalReportViewer1.ReportSource = cr;
            crv.Show();
        }

        private void StatementBuyer_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            /*Bitmap objBmp = new Bitmap(this.dgv.Width, this.dgv.Height);
            dgv.DrawToBitmap(objBmp, new Rectangle(100, 425, this.dgv.Width, this.dgv.Height));

            e.Graphics.DrawImage(objBmp, 250, 450);*/
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

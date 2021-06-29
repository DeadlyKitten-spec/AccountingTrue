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

            button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button2.FlatAppearance.BorderColor = FlatColor;

            button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button6.FlatAppearance.BorderColor = FlatColor;

            button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button2.FlatAppearance.BorderColor = FlatColor;

            button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button3.FlatAppearance.BorderColor = FlatColor;

            button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button8.FlatAppearance.BorderColor = FlatColor;

            button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button13.FlatAppearance.BorderColor = FlatColor;

            button15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button15.FlatAppearance.BorderColor = FlatColor;

            button16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button16.FlatAppearance.BorderColor = FlatColor;

            button17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button17.FlatAppearance.BorderColor = FlatColor;

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
            string Line = "SELECT * FROM `request` WHERE " + ln2 + ln1 + ln3 + ln4 + " `dateAccept` BETWEEN '" + answer1 + "' AND '" + answer2 + "'" + " AND `status` = 'Исполнена' ORDER BY `nameCargo`, `dateAccept` ASC;";
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
                new DataGridViewTextBoxColumn() { Name = "nameCargo", HeaderText = "Наименование" },
                new DataGridViewTextBoxColumn() { Name = "dateTTN", HeaderText = "Дата ТТН" },
                new DataGridViewTextBoxColumn() { Name = "numberDocTrip", HeaderText = "Номер ТТН" },
                //new DataGridViewTextBoxColumn() { Name = "buyer", HeaderText = "Покупатель/заказчик" },
                //new DataGridViewTextBoxColumn() { Name = "object", HeaderText = "Объект" },
                new DataGridViewTextBoxColumn() { Name = "numberNom", HeaderText = "Кол-во номенклатуры" },
                new DataGridViewTextBoxColumn() { Name = "countTrip", HeaderText = "Кол-во рейсов" });
                //Print
                string[] idArr = new string[1000];
                for(int i = 0; i < 1000; i++)
                {
                    idArr[i] = "пусто";
                }
                dgv.Rows.Clear();
                try
                {
                    db.openConnection();
                    myReader = command.ExecuteReader();
                    int i = 0;
                    double sum = 0;
                    int sumTrip = 0;
                    string id = "пусто";
                    string idLast = "пусто";
                    string cargoNow = "пусто";
                    string cargoLast = "пусто";
                    double sumCargo = 0;
                    int sumTripCargo = 0;
                    bool isId = false;
                    int itter = 0;
                    while (myReader.Read())
                    {
                        isId = false;
                        id = myReader.GetString("id");
                        cargoNow = myReader.GetString("nameCargo");
                        //dgv.Rows.Add();
                        if (!cargoLast.Equals("пусто") && !cargoNow.Equals(cargoLast))
                        {
                            dgv.Rows.Add();
                            dgv[0, i].Value = "Итого";
                            dgv[3, i].Value = sumCargo;
                            dgv[4, i].Value = sumTripCargo;
                            dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                            sumCargo = 0;
                            sumTripCargo = 0;
                            i++;
                        }
                        //MessageBox.Show("asd");
                        dgv.Rows.Add();
                        if (!myReader.GetString("nameCargo").Equals("пусто"))
                            dgv[0, i].Value = myReader.GetString("nameCargo");
                        else
                            dgv[0, i].Value = " ";
                        string datefirst = myReader.GetString("dateTTN");
                        //MessageBox.Show("nen");
                        string[] datesplit = datefirst.Split(' ');
                        dgv[1, i].Value = datesplit[0];
                        //dgv[0, i].Value = myReader.GetString("docDate");
                        if (!myReader.GetString("numberDocTrip").Equals("-1"))
                            dgv[2, i].Value = myReader.GetString("numberDocTrip");
                        else
                            dgv[2, i].Value = " ";
                        if (!myReader.GetString("numberNomenclature").Equals("0"))
                            dgv[3, i].Value = myReader.GetString("numberNomenclature");
                        else
                            dgv[3, i].Value = " ";
                        //MessageBox.Show(id);
                        for (int j = 0; j < 1000; j++)
                        {
                            if (id.Equals(idArr[j]))
                            {
                                dgv[4, i].Value = "0";
                                isId = true;
                                break;
                            }
                            else
                            {
                                if (idArr[j].Equals("пусто"))
                                {
                                    isId = false;
                                    itter = j;
                                    //dgv[4, i].Value = "1";
                                }
                            }
                        }
                        if(isId == false)
                        {
                            idArr[itter] = id;
                            dgv[4, i].Value = "1";
                        }
                        idLast = id;
                        sumCargo += double.Parse(myReader.GetString("numberNomenclature"));
                        sumTripCargo += int.Parse(dgv[4, i].Value.ToString());
                        sumTrip += int.Parse(dgv[4, i].Value.ToString());
                        sum += double.Parse(myReader.GetString("numberNomenclature"));
                        i++;
                        cargoLast = cargoNow;
                    }
                    dgv.Rows.Add();
                    dgv[0, i].Value = "Итого";
                    dgv[3, i].Value = sumCargo;
                    dgv[4, i].Value = sumTripCargo;
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                    //dgv.Rows.Add();
                    dgv[0, i+1].Value = "Всего";
                    dgv[3, i+1].Value = sum;
                    dgv[4, i+1].Value = sumTrip;
                    dgv.Rows[i+1].DefaultCellStyle.BackColor = Color.LightGray;
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

        public bool printF = true;

        private void button1_Click(object sender, EventArgs e)
        {

            /*string datefirst = dateTimePicker1.Value.ToString();
            string[] datesplit = datefirst.Split(' ');
            printLineDateFirst = "Дата с:    " + datesplit[0];
            string datefirst2 = dateTimePicker2.Value.ToString();
            string[] datesplit2 = datefirst2.Split(' ');
            printLineDateSecond = "Дата по:   " + datesplit2[0];
            printLineSeller = "Продавец:   " + comboBox3.Text;
            if (comboBox2.SelectedIndex == 0)
            {
                printLineBuyer = "Покупатель:   " + comboBox1.Text;
            }
            else
            {
                printLineBuyer = "Заказчик:   " + comboBox1.Text;
            }
            printLineObject = "Объект:   " + comboBox4.Text;
            if (comboBox2.SelectedIndex == 0)
            {
                printLineThing = "Товар/Услуга:   " + "Товар";
            }
            else
            {
                printLineThing = "Товар/Услуга:   " + "Услуга";
            }
            string subtitle = "\n" + printLineDateFirst + "\n" + printLineDateSecond + "\n" + printLineSeller + "\n" + printLineBuyer + "\n" + printLineObject + "\n" + printLineThing + "\n";
            DGVPrinter printer = new DGVPrinter();
            printer.PageNumbers = true;
            printer.Title = "Ведомость";
            printer.SubTitle = subtitle;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintDataGridView(dgv);*/


            //----------------------------------
            /*PrintBuyer prb = new PrintBuyer();
            prb.ShowDialog();
            ReportBuyer reportBuyer = new ReportBuyer();
            reportBuyer.
            reportBuyer.Parameter_dateFirstR*/
            /*documents doc = new documents();
            doc.Owner = this;
            doc.Show();*/

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("nameCargo", typeof(string));
            dt.Columns.Add("docDate", typeof(string));
            dt.Columns.Add("numberDocTrip", typeof(string));
            dt.Columns.Add("numberNom", typeof(string));
            dt.Columns.Add("countTrip", typeof(string));
            foreach (DataGridViewRow dgv in dgv.Rows)
            {
                dt.Rows.Add(dgv.Cells[0].Value, dgv.Cells[1].Value, dgv.Cells[2].Value, dgv.Cells[3].Value, dgv.Cells[4].Value);
            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("Sample2.xml");
            CRForBuyer1 cr = new CRForBuyer1();
            TextObject text = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text11"];
            text.Text = dateTimePicker1.Value.ToString("dd-MM-yyyy");
            TextObject text1 = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text12"];
            text1.Text = dateTimePicker2.Value.ToString("dd-MM-yyyy");
            TextObject text5 = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text19"];
            text5.Text = comboBox2.Text;
            TextObject text2 = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text13"];
            text2.Text = comboBox3.Text;
            TextObject text3 = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text14"];
            text3.Text = comboBox1.Text;
            TextObject text4 = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text15"];
            text4.Text = comboBox4.Text;
            cr.SetDataSource(ds);
            CRViewer crv = new CRViewer();
            crv.crystalReportViewer3.ReportSource = cr;
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

        private void button10_Click(object sender, EventArgs e)
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

        private void button9_Click(object sender, EventArgs e)
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

        private void button13_Click(object sender, EventArgs e)
        {
            dateTimePicker3.ResetText();
            dateTimePicker4.ResetText();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            documents doc = new documents();
            doc.buyer = comboBox1.Text;
            doc.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            string time = dateTimePicker4.Value.ToString();
            string[] asd = time.Split(' ');
            string[] zxc = asd[0].Split('.');
            string answer1 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];

            time = dateTimePicker3.Value.ToString();
            asd = null;
            asd = time.Split(' ');
            zxc = null;
            zxc = asd[0].Split('.');
            string answer2 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];

            string Line = "SELECT * FROM `documents` WHERE `date` BETWEEN '" + answer1 + "' AND '" + answer2 + "'";
            //MessageBox.Show(Line);
            DB db = new DB();
            MySqlCommand command = new MySqlCommand(Line, db.getConnection());
            db.openConnection();
            MySqlDataReader myReader;
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.AddRange(
            new DataGridViewTextBoxColumn() { Name = "id", HeaderText = "№" },
            new DataGridViewTextBoxColumn() { Name = "buyer", HeaderText = "Покупатель" },
            new DataGridViewTextBoxColumn() { Name = "number", HeaderText = "Номер УПД" },
            new DataGridViewTextBoxColumn() { Name = "date", HeaderText = "Дата УПД" },
            new DataGridViewTextBoxColumn() { Name = "sum", HeaderText = "Сумма УПД" });
            myReader = command.ExecuteReader();
            int itter = 0;
            double all = 0; 
            try
            {
                while (myReader.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, itter].Value = itter + 1;
                    dataGridView1[1, itter].Value = myReader.GetString("buyer");
                    dataGridView1[2, itter].Value = myReader.GetString("number");
                    string[] fordate = myReader.GetString("date").Split(' ');
                    dataGridView1[3, itter].Value = fordate[0];
                    dataGridView1[4, itter].Value = myReader.GetString("sum");
                    all += double.Parse(myReader.GetString("sum"));
                    itter++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            all = Math.Round(all, 2);
            dataGridView1.Rows.Add();
            dataGridView1[0, itter].Value = "Итого";
            dataGridView1[4, itter].Value = all;
            dataGridView1.Rows[itter].DefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            db.closeConnection();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("buyer", typeof(string));
            dt.Columns.Add("number", typeof(string));
            dt.Columns.Add("date", typeof(string));
            dt.Columns.Add("sum", typeof(string));
            foreach (DataGridViewRow dataGridView1 in dataGridView1.Rows)
            {
                dt.Rows.Add(dataGridView1.Cells[0].Value, dataGridView1.Cells[1].Value, dataGridView1.Cells[2].Value, dataGridView1.Cells[3].Value, dataGridView1.Cells[4].Value);
            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("Sample3.xml");
            CRForDoc cr = new CRForDoc();
            TextObject text = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text11"];
            text.Text = dateTimePicker4.Value.ToString("dd-MM-yyyy");
            TextObject text1 = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text12"];
            text1.Text = dateTimePicker3.Value.ToString("dd-MM-yyyy");
            cr.SetDataSource(ds);
            CRViewerDoc crvd = new CRViewerDoc();
            crvd.crystalReportViewer1.ReportSource = cr;
            crvd.Show();
        }

        string val = "";
        string number = "";
        string date = "";

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = dataGridView1.CurrentRow.Index;
            val = dataGridView1[1, idx].Value.ToString();
            number = dataGridView1[2, idx].Value.ToString();
            string[] fordate = dataGridView1[3, idx].Value.ToString().Split('.');
            date = fordate[2] + "." + fordate[1] + "." + fordate[0];
        }

        private void button17_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MessageBox.Show(val + number + date);
            MySqlCommand command = new MySqlCommand("DELETE FROM `documents` WHERE `buyer` = '" + val + "' AND `number` = '" + number + "' AND `date` = '" + date + "'", db.getConnection());

            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            string time = dateTimePicker4.Value.ToString();
            string[] asd = time.Split(' ');
            string[] zxc = asd[0].Split('.');
            string answer1 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];

            time = dateTimePicker3.Value.ToString();
            asd = null;
            asd = time.Split(' ');
            zxc = null;
            zxc = asd[0].Split('.');
            string answer2 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];

            string Line = "SELECT * FROM `documents` WHERE `date` BETWEEN '" + answer1 + "' AND '" + answer2 + "'";
            //MessageBox.Show(Line);
            command = new MySqlCommand(Line, db.getConnection());
            db.openConnection();
            MySqlDataReader myReader;
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.AddRange(
            new DataGridViewTextBoxColumn() { Name = "id", HeaderText = "№" },
            new DataGridViewTextBoxColumn() { Name = "buyer", HeaderText = "Покупатель" },
            new DataGridViewTextBoxColumn() { Name = "number", HeaderText = "Номер УПД" },
            new DataGridViewTextBoxColumn() { Name = "date", HeaderText = "Дата УПД" },
            new DataGridViewTextBoxColumn() { Name = "sum", HeaderText = "Сумма УПД" });
            myReader = command.ExecuteReader();
            int itter = 0;
            try
            {
                while (myReader.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, itter].Value = itter + 1;
                    dataGridView1[1, itter].Value = myReader.GetString("buyer");
                    dataGridView1[2, itter].Value = myReader.GetString("number");
                    string[] fordate = myReader.GetString("date").Split(' ');
                    dataGridView1[3, itter].Value = fordate[0];
                    dataGridView1[4, itter].Value = myReader.GetString("sum");
                    itter++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            db.closeConnection();
        }
    }
}

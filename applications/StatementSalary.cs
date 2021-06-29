using CrystalDecisions.CrystalReports.Engine;
using DGVPrinterHelper;
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
    public partial class StatementSalary : Form
    {
        Color FlatColor;
        public StatementSalary()
        {   
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            FlatColor = button6.FlatAppearance.BorderColor;

            FillCombo2();
            button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button6.FlatAppearance.BorderColor = FlatColor;

            button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button3.FlatAppearance.BorderColor = FlatColor;

            button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button4.FlatAppearance.BorderColor = FlatColor;
            this.ControlBox = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            string time = dateTimePicker1.Value.ToString();
            string[] asd = time.Split(' ');
            string[] zxc = asd[0].Split('.');
            string answer1 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];

            time = dateTimePicker3.Value.ToString();
            asd = null;
            asd = time.Split(' ');
            zxc = null;
            zxc = asd[0].Split('.');
            string answer2 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];

            //string Line = "SELECT * FROM `request` WHERE `docDate` = '" + answer + "';";
            bool fdriver = false;

            string Line = "";// = "SELECT * FROM `request` WHERE `status` = '" + comboBox1.Text + "';";
            if (!comboBox1.Text.Equals(""))
            {
                fdriver = true;
            }

            if (fdriver)
            {
                Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `drivers` = '" + comboBox1.Text + "' AND `status` = 'Исполнена' ORDER BY `id` ASC;";
                DB db = new DB();
                MySqlCommand command = new MySqlCommand(Line, db.getConnection());
                bool g = true;
                db.openConnection();
                object obj = command.ExecuteScalar();
                db.closeConnection();
                Salary[] salary = new Salary[1000];
                for(int i = 0; i < 1000; i++)
                {
                    salary[i] = new Salary();
                    salary[i].car = "-1";
                    salary[i].objectt = "-1";
                    salary[i].price = -1;
                    salary[i].countTrip = -1;
                    salary[i].sum = -1;
                }
                int sumTrip = 0;
                bool fobj = false;
                int itter = 0;
                int idlast = 0;
                int idmorelast = 0;
                if (obj != null)
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlDataReader myReader;
                    try
                    {
                        db.openConnection();
                        myReader = command.ExecuteReader();
                        while (myReader.Read())
                        {
                            idlast = int.Parse(myReader.GetString("id"));
                            if(idlast == idmorelast)
                            {
                                idmorelast = idlast;
                                continue;
                            }
                            if (myReader.GetString("priceSalary").Equals("пусто"))
                            {
                                continue;
                            }
                            string objectt = myReader.GetString("object");
                            for(int i = 0; i < 1000; i++)
                            {
                                //MessageBox.Show("nen");
                                if (objectt.Equals(salary[i].objectt))
                                {
                                    idmorelast = idlast;
                                    salary[i].countTrip++;
                                    break;
                                }
                                else
                                {
                                    if (salary[i].objectt.Equals("-1"))
                                    {
                                        idmorelast = idlast;
                                        fobj = true;
                                        itter = i;
                                        break;
                                    }
                                }
                            }
                            if(fobj == true)
                            {
                                salary[itter].car = myReader.GetString("cars");
                                salary[itter].objectt = myReader.GetString("object");
                                //MessageBox.Show("asd");
                                /*string priceSal = myReader.GetString("priceSalary");
                                MessageBox.Show(priceSal);
                                bool isStop = false;
                                for (int i = 0; i < priceSal.Length; i++)
                                {
                                    if (priceSal[i].Equals(','))
                                    {
                                        isStop = true;
                                    }
                                }
                                if (isStop)
                                {
                                    string[] forsplit = priceSal.Split(',');
                                    //string sad = forsplit[0] + '.' + forsplit[1];
                                    //MessageBox.Show(sad);
                                    salary[itter].price = double.Parse(sad);
                                    MessageBox.Show(salary[itter].price.ToString());
                                }
                                else
                                {
                                    salary[itter].price = int.Parse(priceSal);
                                }*/
                                //MessageBox.Show("asd");
                                //MessageBox.Show(salary[itter].ToString());
                                //MessageBox.Show(myReader.GetString("priceSalary"));
                                salary[itter].price = double.Parse(myReader.GetString("priceSalary"));
                                salary[itter].countTrip = 1;
                                fobj = false;
                                itter = -1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    db.closeConnection();
                    for (int i = 0; i < 1000; i++)
                    {
                        salary[i].sum = salary[i].countTrip * salary[i].price;
                    }
                    dgv.Columns.Clear();
                    dgv.Rows.Clear();
                    dgv.Columns.AddRange(
                    new DataGridViewTextBoxColumn() { Name = "car", HeaderText = "Автомобиль" },
                    //new DataGridViewTextBoxColumn() { Name = "buyer", HeaderText = "Покупатель/заказчик" },
                    //new DataGridViewTextBoxColumn() { Name = "object", HeaderText = "Объект" },
                    new DataGridViewTextBoxColumn() { Name = "object", HeaderText = "Название Объекта" },
                    new DataGridViewTextBoxColumn() { Name = "countTrip", HeaderText = "Кол-во рейсов" },
                    new DataGridViewTextBoxColumn() { Name = "price", HeaderText = "Расценок ЗП" },
                    new DataGridViewTextBoxColumn() { Name = "sum", HeaderText = "Сумма" });
                    dgv.Rows.Clear();
                    //dgv.Rows.Add();
                    int itterr = 0;
                    double summ = 0;
                    for(int i = 0; i < 1000; i++)
                    {
                        if (salary[i].objectt.Equals("-1"))
                        {
                            itterr = i;
                            break;
                        }
                        dgv.Rows.Add();
                        dgv[0, i].Value = salary[i].car;
                        dgv[1, i].Value = salary[i].objectt;
                        dgv[2, i].Value = salary[i].countTrip;
                        dgv[3, i].Value = salary[i].price;
                        dgv[4, i].Value = salary[i].sum;
                        summ += salary[i].sum;
                        sumTrip += salary[i].countTrip;
                    }
                    //MessageBox.Show(itterr.ToString());
                    dgv.Rows.Add();
                    dgv[0, itterr].Value = "Всего";
                    /*                    dgv[1, itterr].Value = "0";
                                        dgv[2, itterr].Value = "0";
                                        dgv[3, itterr].Value = "0";*/
                    dgv[2, itterr].Value = sumTrip;
                    dgv[4, itterr].Value = summ;
                    dgv[0, itterr + 1].Value = "ЗП водителю";
                    dgv[4, itterr + 1].Value = summ * 0.15;
                    dgv.Rows[itterr].DefaultCellStyle.BackColor = Color.LightGray;
                    dgv.Rows[itterr + 1].DefaultCellStyle.BackColor = Color.LightGray;
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
                
            }
            else
            {
                //Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "';";
                //MessageBox.Show("Выберите водителя");
                Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `driveCont` = '" + comboBox2.Text + "' AND `status` = 'Исполнена' ORDER BY `cars` ASC;";
                DB db = new DB();
                MySqlCommand command = new MySqlCommand(Line, db.getConnection());
                bool g = true;
                db.openConnection();
                object obj = command.ExecuteScalar();
                db.closeConnection();
                Salary[] salary = new Salary[1000];
                for (int i = 0; i < 1000; i++)
                {
                    salary[i] = new Salary();
                    salary[i].car = "-1";
                    salary[i].objectt = "-1";
                    salary[i].price = -1;
                    salary[i].countTrip = -1;
                    salary[i].sum = -1;
                }
                int sumTrip = 0;
                bool fobj = false;
                int itter = 0;
                int idlast = 0;
                int idmorelast = 0;
                if (obj != null)
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlDataReader myReader;
                    try
                    {
                        db.openConnection();
                        myReader = command.ExecuteReader();
                        while (myReader.Read())
                        {
                            idlast = int.Parse(myReader.GetString("id"));
                            if (idlast == idmorelast)
                            {
                                idmorelast = idlast;
                                continue;
                            }
                            if (myReader.GetString("priceSalary").Equals("пусто"))
                            {
                                continue;
                            }
                            string objectt = myReader.GetString("object");
                            for (int i = 0; i < 1000; i++)
                            {
                                //MessageBox.Show("nen");
                                if (objectt.Equals(salary[i].objectt))
                                {
                                    idmorelast = idlast;
                                    salary[i].countTrip++;
                                    break;
                                }
                                else
                                {
                                    if (salary[i].objectt.Equals("-1"))
                                    {
                                        idmorelast = idlast;
                                        fobj = true;
                                        itter = i;
                                        break;
                                    }
                                }
                            }
                            if (fobj == true)
                            {
                                salary[itter].car = myReader.GetString("cars");
                                salary[itter].objectt = myReader.GetString("object");
                                //MessageBox.Show("asd");
                                /*string priceSal = myReader.GetString("priceSalary");
                                MessageBox.Show(priceSal);
                                bool isStop = false;
                                for (int i = 0; i < priceSal.Length; i++)
                                {
                                    if (priceSal[i].Equals(','))
                                    {
                                        isStop = true;
                                    }
                                }
                                if (isStop)
                                {
                                    string[] forsplit = priceSal.Split(',');
                                    //string sad = forsplit[0] + '.' + forsplit[1];
                                    //MessageBox.Show(sad);
                                    salary[itter].price = double.Parse(sad);
                                    MessageBox.Show(salary[itter].price.ToString());
                                }
                                else
                                {
                                    salary[itter].price = int.Parse(priceSal);
                                }*/
                                //MessageBox.Show("asd");
                                //MessageBox.Show(salary[itter].ToString());
                                //MessageBox.Show(myReader.GetString("priceSalary"));
                                salary[itter].price = double.Parse(myReader.GetString("priceSalary"));
                                salary[itter].countTrip = 1;
                                fobj = false;
                                itter = -1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    db.closeConnection();
                    for (int i = 0; i < 1000; i++)
                    {
                        salary[i].sum = salary[i].countTrip * salary[i].price;
                    }
                    dgv.Columns.Clear();
                    dgv.Rows.Clear();
                    dgv.Columns.AddRange(
                    new DataGridViewTextBoxColumn() { Name = "car", HeaderText = "Автомобиль" },
                    //new DataGridViewTextBoxColumn() { Name = "buyer", HeaderText = "Покупатель/заказчик" },
                    //new DataGridViewTextBoxColumn() { Name = "object", HeaderText = "Объект" },
                    new DataGridViewTextBoxColumn() { Name = "object", HeaderText = "Название Объекта" },
                    new DataGridViewTextBoxColumn() { Name = "countTrip", HeaderText = "Кол-во рейсов" },
                    new DataGridViewTextBoxColumn() { Name = "price", HeaderText = "Расценок ЗП" },
                    new DataGridViewTextBoxColumn() { Name = "sum", HeaderText = "Сумма" });
                    dgv.Rows.Clear();
                    //dgv.Rows.Add();
                    int itterr = 0;
                    double summ = 0;
                    for (int i = 0; i < 1000; i++)
                    {
                        if (salary[i].objectt.Equals("-1"))
                        {
                            itterr = i;
                            break;
                        }
                        dgv.Rows.Add();
                        dgv[0, i].Value = salary[i].car;
                        dgv[1, i].Value = salary[i].objectt;
                        dgv[2, i].Value = salary[i].countTrip;
                        dgv[3, i].Value = salary[i].price;
                        dgv[4, i].Value = salary[i].sum;
                        summ += salary[i].sum;
                        sumTrip += salary[i].countTrip;
                    }
                    //MessageBox.Show(itterr.ToString());
                    dgv.Rows.Add();
                    dgv[0, itterr].Value = "Всего";
                    /*                    dgv[1, itterr].Value = "0";
                                        dgv[2, itterr].Value = "0";
                                        dgv[3, itterr].Value = "0";*/
                    dgv[2, itterr].Value = sumTrip;
                    dgv[4, itterr].Value = summ;
                    dgv[0, itterr + 1].Value = "ЗП водителю";
                    dgv[4, itterr + 1].Value = summ * 0.15;
                    dgv.Rows[itterr].DefaultCellStyle.BackColor = Color.LightGray;
                    dgv.Rows[itterr + 1].DefaultCellStyle.BackColor = Color.LightGray;
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
            }
            
        }



        void FillCombo2()
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
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DB db = new DB();
            comboBox1.Items.Clear();
            string Query = "SELECT * FROM `drivers` WHERE `contractor` = '" + comboBox2.Text.ToString() + "' ORDER BY `name` ASC;";
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
                    comboBox1.Items.Add(objName);
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

        private void button2_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            dateTimePicker1.ResetText();
            dateTimePicker3.ResetText();
            comboBox2.Text = "";
            comboBox1.Text = "";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*string datefirst = dateTimePicker1.Value.ToString();
            string[] datesplit = datefirst.Split(' ');
            string printLineDateFirst = "Дата с:    " + datesplit[0];
            string datefirst2 = dateTimePicker3.Value.ToString();
            string[] datesplit2 = datefirst2.Split(' ');
            string printLineDateSecond = "Дата по:   " + datesplit2[0];
            string driver = "Водитель:   " + comboBox1.Text;
            string subtitle = "\n" + printLineDateFirst + "\n" + printLineDateSecond + "\n" + driver + "\n";
            DGVPrinter printer = new DGVPrinter();
            printer.PageNumbers = true;
            printer.Title = "Ведомость";
            printer.SubTitle = subtitle;
            //printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            //printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            //printer.FooterSpacing = 15;
            printer.PrintDataGridView(dgv);*/
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("car", typeof(string));
            dt.Columns.Add("object", typeof(string));
            dt.Columns.Add("countTrip", typeof(string));
            dt.Columns.Add("price", typeof(string));
            dt.Columns.Add("sum", typeof(string));
            foreach (DataGridViewRow dgv in dgv.Rows)
            {
                dt.Rows.Add(dgv.Cells[0].Value, dgv.Cells[1].Value, dgv.Cells[2].Value, dgv.Cells[3].Value, dgv.Cells[4].Value);
            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("SampleZP.xml");
            CRForZP cr = new CRForZP();
            TextObject text = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text11"];
            text.Text = dateTimePicker1.Value.ToString("dd-MM-yyyy");
            TextObject text1 = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text12"];
            text1.Text = dateTimePicker3.Value.ToString("dd-MM-yyyy");
            TextObject text5 = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text19"];
            text5.Text = comboBox2.Text;
            TextObject text2 = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text13"];
            text2.Text = comboBox1.Text;
            cr.SetDataSource(ds);
            CRViewerZP crv = new CRViewerZP();
            crv.crystalReportViewer2.ReportSource = cr;
            crv.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
            //WorkSpace.ActiveForm.Activate();
        }
    }
}

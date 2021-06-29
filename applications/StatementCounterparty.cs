using CrystalDecisions.CrystalReports.Engine;
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
    public partial class StatementCounterparty : Form
    {
        Color FlatColor;
        public StatementCounterparty()
        {
            InitializeComponent();
            FillCombo2();
            this.WindowState = FormWindowState.Maximized;
            FlatColor = button6.FlatAppearance.BorderColor;

            //FillCombo2();
            button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button6.FlatAppearance.BorderColor = FlatColor;

            button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button3.FlatAppearance.BorderColor = FlatColor;

            button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button4.FlatAppearance.BorderColor = FlatColor;
            this.ControlBox = false;
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dateTimePicker1.ResetText();
            dateTimePicker3.ResetText();
            comboBox2.Text = "";
            comboBox1.Text = "";
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
                Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `contractor` = '" + comboBox2.Text + "' AND `cars` = '" + comboBox1.Text + "' AND `status` = 'Исполнена' ORDER BY `object` ASC;";
                DB db = new DB();
                MySqlCommand command = new MySqlCommand(Line, db.getConnection());
                bool g = true;
                db.openConnection();
                object obj = command.ExecuteScalar();
                db.closeConnection();
                ForStatCP[] salary = new ForStatCP[1000];
                for (int i = 0; i < 1000; i++)
                {
                    salary[i] = new ForStatCP();
                    salary[i].cp = "-1";
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
                            string cars = myReader.GetString("cars");
                            string objectt = myReader.GetString("object");
                            for (int i = 0; i < 1000; i++)
                            {
                                //MessageBox.Show("nen");
                                if (objectt.Equals(salary[i].objectt) && cars.Equals(salary[i].cp))
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
                                salary[itter].cp = myReader.GetString("cars");
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
                    new DataGridViewTextBoxColumn() { Name = "price", HeaderText = "Расценок" },
                    new DataGridViewTextBoxColumn() { Name = "sum", HeaderText = "Сумма" });
                    dgv.Rows.Clear();
                    //dgv.Rows.Add();
                    int itterr = 0;
                    double summ = 0;
                    double summcar = 0;
                    int sumTripcar = 0;
                    bool gggg = false;
                    int asdgh = 0;
                    for (int i = 0; i < 1000; i++)
                    {
                        if (salary[i].objectt.Equals("-1"))
                        {
                            itterr = i;
                            break;
                        }
                        dgv.Rows.Add();
                        /*if (i != 0 && (!salary[i].cp.Equals(salary[i-1].cp)))
                        {
                            dgv[0, asdgh].Value = "Итого";
                            dgv[2, asdgh].Value = sumTripcar;
                            dgv[4, asdgh].Value = summcar;
                            sumTripcar = 0;
                            summcar = 0;
                            dgv.Rows[asdgh].DefaultCellStyle.BackColor = Color.LightGray;
                            asdgh++;
                            gggg = true;
                        }*/
                        //dgv.Rows.Add();
                        dgv[0, asdgh].Value = salary[i].cp;
                        dgv[1, asdgh].Value = salary[i].objectt;
                        dgv[2, asdgh].Value = salary[i].countTrip;
                        dgv[3, asdgh].Value = salary[i].price;
                        dgv[4, asdgh].Value = salary[i].sum;
                        summcar += salary[i].sum;
                        sumTripcar += salary[i].countTrip;
                        summ += salary[i].sum;
                        sumTrip += salary[i].countTrip;
                        asdgh++;
                        /*if (gggg)
                        {
                            i--;
                            gggg = false;
                        }*/
                    }
                    //MessageBox.Show(itterr.ToString());
                    dgv.Rows.Add();
                    dgv[0, itterr].Value = "Всего";
                    /*                    dgv[1, itterr].Value = "0";
                                        dgv[2, itterr].Value = "0";
                                        dgv[3, itterr].Value = "0";*/
                    dgv[2, itterr].Value = sumTrip;
                    dgv[3, itterr].Value = summ / sumTrip;
                    dgv[4, itterr].Value = summ;
                    /*dgv[0, itterr + 1].Value = "ЗП водителю";
                    dgv[4, itterr + 1].Value = summ * 0.15;*/
                    dgv.Rows[itterr].DefaultCellStyle.BackColor = Color.LightGray;
                    //dgv.Rows[itterr + 1].DefaultCellStyle.BackColor = Color.LightGray;
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }

            }
            else
            {
                //Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "';";
                //MessageBox.Show("Выберите водителя");
                Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `contractor` = '" + comboBox2.Text + "' AND `status` = 'Исполнена' ORDER BY `cars`, `id` ASC;";
                DB db = new DB();
                MySqlCommand command = new MySqlCommand(Line, db.getConnection());
                bool g = true;
                db.openConnection();
                object obj = command.ExecuteScalar();
                db.closeConnection();
                ForStatCP[] salary = new ForStatCP[1000];
                for (int i = 0; i < 1000; i++)
                {
                    salary[i] = new ForStatCP();
                    salary[i].cp = "-1";
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
                            string cars = myReader.GetString("cars");
                            string objectt = myReader.GetString("object");
                            for (int i = 0; i < 1000; i++)
                            {
                                //MessageBox.Show("nen");
                                if (objectt.Equals(salary[i].objectt) && cars.Equals(salary[i].cp))
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
                                salary[itter].cp = myReader.GetString("cars");
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
                    new DataGridViewTextBoxColumn() { Name = "price", HeaderText = "Расценок" },
                    new DataGridViewTextBoxColumn() { Name = "sum", HeaderText = "Сумма" });
                    dgv.Rows.Clear();
                    //dgv.Rows.Add();
                    int itterr = 0;
                    double summ = 0;
                    double summcar = 0;
                    int sumTripcar = 0;
                    bool gggg = false;
                    int asdgh = 0;
                    ForStatCP[] salaryTar = new ForStatCP[1000];
                    for (int i = 0; i < 1000; i++)
                    {
                        salaryTar[i] = new ForStatCP();
                        salaryTar[i].cp = "-1";
                        salaryTar[i].objectt = "-1";
                        salaryTar[i].price = -1;
                        salaryTar[i].countTrip = -1;
                        salaryTar[i].sum = -1;
                    }
                    int k = 0;
                    for(int i = 0; i < 1000; i++)
                    {
                        if (salary[i].objectt.Equals("-1"))
                        {
                            salaryTar[k].cp = "Итого";
                            salaryTar[k].countTrip = sumTripcar;
                            salaryTar[k].sum = summcar;
                            k++;
                            salaryTar[k].cp = "Всего";
                            salaryTar[k].countTrip = sumTrip;
                            salaryTar[k].sum = summ;
                            salaryTar[k].price = summ / sumTrip;
                            itterr = i;
                            break;
                        }
                        if (i != 0 && (!salary[i].cp.Equals(salary[i - 1].cp)))
                        {
                            salaryTar[k].cp = "Итого";
                            salaryTar[k].countTrip = sumTripcar;
                            salaryTar[k].sum = summcar;
                            k++;
                            sumTripcar = 0;
                            summcar = 0;
                        }
                        salaryTar[k] = salary[i];
                        summ += salary[i].sum;
                        sumTrip += salary[i].countTrip;
                        summcar += salary[i].sum;
                        sumTripcar += salary[i].countTrip;
                        k++;
                    }
                    for(int i = 0; i < 1000; i++)
                    {
                        if (salaryTar[i].cp.Equals("-1"))
                        {
                            itterr = i;
                            break;
                        }
                        dgv.Rows.Add();
                        dgv[0, i].Value = salaryTar[i].cp;
                        dgv[1, i].Value = salaryTar[i].objectt;
                        dgv[2, i].Value = salaryTar[i].countTrip;
                        dgv[3, i].Value = salaryTar[i].price;
                        //MessageBox.Show(asdgh.ToString());
                        dgv[4, i].Value = salaryTar[i].sum;
                        if (salaryTar[i].cp.Equals("Итого"))
                        {
                            dgv[1, i].Value = "";
                            dgv[3, i].Value = "";
                            dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                        }
                        if (salaryTar[i].cp.Equals("Всего"))
                        {
                            dgv[1, i].Value = "";
                            dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                        }
                    }
                    /*MessageBox.Show("as");
                    for(int i = 0; i < dgv.Rows.Count; i++)
                    {
                        if(dgv[0, i].Value.Equals("Итого") || dgv[0, i].Value.Equals("Всего"))
                        {
                            dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                        }
                    }*/
                    /*for (int i = 0; i < 1000; i++)
                    {
                        //asdgh = i;
                        if (salary[i].objectt.Equals("-1"))
                        {
                            itterr = i;
                            break;
                        }
                        //dgv.Rows.Add();
                        //MessageBox.Show(salary[i].cp);
                        if (i != 0 && (!salary[i].cp.Equals(salary[i - 1].cp)))
                        {
                            //MessageBox.Show("asd");
                            dgv.Rows.Add();
                            dgv[0, asdgh].Value = "Итого";
                            dgv[2, asdgh].Value = sumTripcar;
                            dgv[4, asdgh].Value = summcar;
                            //MessageBox.Show(asdgh.ToString());
                            //dgv.Rows.Add();
                            dgv.Rows[asdgh].DefaultCellStyle.BackColor = Color.LightGray;
                            asdgh++;
                            dgv.Rows.Add();
                            dgv[0, asdgh].Value = salary[i].cp;
                            dgv[1, asdgh].Value = salary[i].objectt;
                            dgv[2, asdgh].Value = salary[i].countTrip;
                            dgv[3, asdgh].Value = salary[i].price;
                            //MessageBox.Show(asdgh.ToString());
                            dgv[4, asdgh].Value = salary[i].sum;
                            sumTripcar = 0;
                            summcar = salary[i].sum;
                            summ += salary[i].sum;
                            sumTrip += salary[i].countTrip;
                            //asdgh++;
                            gggg = true;
                            continue;
                        }
                        //MessageBox.Show("asd");
                        //asdgh++;
                        dgv.Rows.Add();
                        dgv[0, asdgh].Value = salary[i].cp;
                        dgv[1, asdgh].Value = salary[i].objectt;
                        dgv[2, asdgh].Value = salary[i].countTrip;
                        dgv[3, asdgh].Value = salary[i].price;
                        dgv[4, asdgh].Value = salary[i].sum;
                        summcar += salary[i].sum;
                        sumTripcar += salary[i].countTrip;
                        summ += salary[i].sum;
                        sumTrip += salary[i].countTrip;
                        asdgh++;
                    }
                    //MessageBox.Show(itterr.ToString());
                    dgv.Rows.Add();
                    dgv[0, itterr].Value = "Итого";
                    dgv[2, itterr].Value = sumTripcar;
                    dgv[4, itterr].Value = summcar;
                    dgv.Rows[itterr].DefaultCellStyle.BackColor = Color.LightGray;
                    itterr++;
                    dgv.Rows.Add();
                    dgv[0, itterr].Value = "Всего";
                    *//*                    dgv[1, itterr].Value = "0";
                                        dgv[2, itterr].Value = "0";
                                        dgv[3, itterr].Value = "0";*//*
                    dgv[2, itterr].Value = sumTrip;
                    dgv[4, itterr].Value = summ;
                    *//*dgv[0, itterr + 1].Value = "ЗП водителю";
                    dgv[4, itterr + 1].Value = summ * 0.15;*//*
                    dgv.Rows[itterr].DefaultCellStyle.BackColor = Color.LightGray;*/
                    //dgv.Rows[itterr + 1].DefaultCellStyle.BackColor = Color.LightGray;
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    /*int[] mas = new int[100];
                    int itt = 0;
                    for(int k = 0; k < 100; k++)
                    {
                        mas[k] = -1;
                    }
                    for(int k = 0; k < 100; k++)
                    {
                        if(dgv[0,k].Value == null)
                        {
                            mas[itt] = k;
                            itt++;
                        }
                    }
                    for (int k = 0; k < itt; k++)
                    {
                        dgv.Rows.RemoveAt(mas[k]);
                    }*/
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DB db = new DB();
            comboBox1.Items.Clear();
            string Query = "SELECT * FROM `cars` WHERE `contractor` = '" + comboBox2.Text.ToString() + "' ORDER BY `name` ASC;";
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

        void FillCombo2()
        {
            string Query = "SELECT * FROM `cars` ORDER BY `contractor` ASC";
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
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
            ds.WriteXmlSchema("SampleAnC.xml");
            CRForAnC cr = new CRForAnC();
            TextObject text = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text11"];
            text.Text = dateTimePicker1.Value.ToString("dd-MM-yyyy");
            TextObject text1 = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text12"];
            text1.Text = dateTimePicker3.Value.ToString("dd-MM-yyyy");
            TextObject text5 = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text19"];
            text5.Text = comboBox2.Text;
            TextObject text2 = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text13"];
            text2.Text = comboBox1.Text;
            cr.SetDataSource(ds);
            CRViewerAnC crv = new CRViewerAnC();
            crv.crystalReportViewer1.ReportSource = cr;
            crv.Show();
        }
    }
}

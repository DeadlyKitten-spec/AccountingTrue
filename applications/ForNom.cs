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
    public partial class ForNom : Form
    {
        public bool f;
        Color FlatColor;
        public bool edit = false;
        public int curRow;


        public ForNom()
        {

            this.ControlBox = false;
            InitializeComponent();
            FillCombo1();

            button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = FlatColor;

        }

        void FillCombo1()
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


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!textBox1.Text.Equals(""))
            {
                comboBox2.Items.Clear();
                string Query = "SELECT * FROM `proxy` WHERE `counterparty` = '" + comboBox1.Text + "' AND `nameCargo` = '" + comboBox6.Text + "' AND `cargoCount` = '" + textBox1.Text + "' AND  `change` > '0';";
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
                        string objName = myReader.GetString("numberDoc");
                        if (objName.Equals("пусто"))
                        {
                            continue;
                        }
                        comboBox2.Items.Add(objName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                db.closeConnection();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ForNom_Load(object sender, EventArgs e)
        {
            if (f)
            {
                comboBox6.Items.Clear();
                string Query = "SELECT * FROM `product` ORDER BY `name` ASC;";
                //string Query = "(SELECT * FROM `nameCargo` ORDER BY `name` ASC) ORDER BY `count` desc;";
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
                        if (objName.Equals("пусто"))
                        {
                            continue;
                        }
                        comboBox6.Items.Add(objName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                db.closeConnection();
            }
            else
            {
                comboBox6.Items.Clear();
                //string Query = "SELECT * FROM `product` ORDER BY `name` ASC";
                string Query = "SELECT * FROM `nameCargo` ORDER BY `name` ASC;";
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
                        if (objName.Equals("пусто"))
                        {
                            continue;
                        }
                        comboBox6.Items.Add(objName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                db.closeConnection();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Query = "SELECT * FROM `proxy` WHERE `counterparty` = '" + comboBox1.Text + "' AND `nameCargo` = '" + comboBox6.Text + "' AND `numberDoc` = '" + comboBox2.Text + "';";
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
                    string objName = myReader.GetString("date");
                    if (objName.Equals("пусто"))
                    {
                        continue;
                    }
                    //MessageBox.Show(objName);
                    string[] tm = objName.Split(' ');
                    string[] tmm = tm[0].Split('.');
                    //tmm[0] + "." + tmm[1] + "." + tmm[2]
                    dateTimePicker3.Value = new DateTime(int.Parse(tmm[2]), int.Parse(tmm[1]), int.Parse(tmm[0]));

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
            WriteRequest wr = this.Owner as WriteRequest;
            if (edit == false)
            {
                wr.dataGridView1.Rows.Add();
                if (!comboBox6.Text.Equals(""))
                {
                    wr.dataGridView1[0, wr.dataGridView1.Rows.Count - 2].Value = comboBox6.Text;
                }
                else
                {
                    wr.dataGridView1[0, wr.dataGridView1.Rows.Count - 2].Value = "пусто";
                }
                if (!comboBox1.Text.Equals(""))
                {
                    wr.dataGridView1[1, wr.dataGridView1.Rows.Count - 2].Value = comboBox1.Text;
                }
                else
                {
                    wr.dataGridView1[1, wr.dataGridView1.Rows.Count - 2].Value = "пусто";
                }
                if (!comboBox2.Text.Equals(""))
                {
                    wr.dataGridView1[2, wr.dataGridView1.Rows.Count - 2].Value = comboBox2.Text;
                }
                else
                {
                    wr.dataGridView1[2, wr.dataGridView1.Rows.Count - 2].Value = "пусто";
                }
                string dat = dateTimePicker3.Value.ToString();
                string[] datt = dat.Split(' ');
                wr.dataGridView1[3, wr.dataGridView1.Rows.Count - 2].Value = datt[0];
                string tar = textBox1.Text;
                if (!textBox1.Text.Equals(""))
                {
                    for (int i = 0; i < textBox1.Text.Length; i++)
                    {
                        if (textBox1.Text[i] == '.')
                        {
                            string[] asd = textBox1.Text.Split('.');
                            tar = asd[0] + "," + asd[1];
                            break;
                        }
                    }
                    textBox1.Text = tar;
                    wr.dataGridView1[4, wr.dataGridView1.Rows.Count - 2].Value = textBox1.Text;
                }
                else
                {
                    wr.dataGridView1[4, wr.dataGridView1.Rows.Count - 2].Value = "0";
                }
            }
            else
            {
                if (!comboBox6.Text.Equals(""))
                {
                    wr.dataGridView1[0, curRow].Value = comboBox6.Text;
                }
                else
                {
                    wr.dataGridView1[0, curRow].Value = "пусто";
                }
                if (!comboBox1.Text.Equals(""))
                {
                    wr.dataGridView1[1, curRow].Value = comboBox1.Text;
                }
                else
                {
                    wr.dataGridView1[1, curRow].Value = "пусто";
                }
                if (!comboBox2.Text.Equals(""))
                {
                    wr.dataGridView1[2, curRow].Value = comboBox2.Text;
                }
                else
                {
                    wr.dataGridView1[2, curRow].Value = "пусто";
                }
                string dat = dateTimePicker3.Value.ToString();
                string[] datt = dat.Split(' ');
                wr.dataGridView1[3, curRow].Value = datt[0];
                string tar = textBox1.Text;
                if (!textBox1.Text.Equals(""))
                {
                    for (int i = 0; i < textBox1.Text.Length; i++)
                    {
                        if (textBox1.Text[i] == '.')
                        {
                            string[] asd = textBox1.Text.Split('.');
                            tar = asd[0] + "," + asd[1];
                            break;
                        }
                    }
                    textBox1.Text = tar;
                    wr.dataGridView1[4, curRow].Value = textBox1.Text;
                }
                else
                {
                    wr.dataGridView1[4, curRow].Value = "0";
                }
            }
            wr.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!comboBox1.Text.Equals(""))
            {
                comboBox2.Items.Clear();
                string Query = "SELECT * FROM `proxy` WHERE `counterparty` = '" + comboBox1.Text + "' AND `nameCargo` = '" + comboBox6.Text + "' AND `cargoCount` = '" + textBox1.Text + "' AND  `change` > '0';";
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
                        string objName = myReader.GetString("numberDoc");
                        if (objName.Equals("пусто"))
                        {
                            continue;
                        }
                        comboBox2.Items.Add(objName);
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
}

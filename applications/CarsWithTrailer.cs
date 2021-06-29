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
    public partial class CarsWithTrailer : Form
    {
        public CarsWithTrailer()
        {
            InitializeComponent();

            //FillCombo1();
            FillDGV();
            //FillCombo2();
            FillCombo3();
            FillCombo4();
            FillCombo5();

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

            comboBox4.Text = "АвтоСтройХолдинг бортовые";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void FillDGV()
        {
            DB db = new DB();
            string Query = "SELECT * FROM `tracks`;";
            //string Query = "(SELECT * FROM `cars` WHERE `contractor` = '" + comboBox7.Text.ToString() + "' ORDER BY `name` ASC) ORDER BY `count` desc;";
            MySqlCommand cmdDataBase2 = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader2;
            int i = 0;
            try
            {
                db.openConnection();
                myReader2 = cmdDataBase2.ExecuteReader();

                while (myReader2.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1["id", i].Value = i + 1;
                    dataGridView1["car", i].Value = myReader2.GetString("car");
                    dataGridView1["model", i].Value = myReader2.GetString("model");
                    dataGridView1["driver", i].Value = myReader2.GetString("driver");
                    dataGridView1["trailer", i].Value = myReader2.GetString("trailer");
                    dataGridView1["option", i].Value = myReader2.GetString("option");
                    dataGridView1["type", i].Value = myReader2.GetString("type");
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }

        void FillCombo1()
        {
            DB db = new DB();
            string Query = "SELECT * FROM `cars` ORDER BY `name` ASC;";
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
                    if (objName.Equals("") || objName.Equals(" "))
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
            DB db = new DB();
            string Query = "SELECT * FROM `drivers` ORDER BY `name` ASC;";
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
                    if (objName.Equals("") || objName.Equals(" "))
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

        void FillCombo3()
        {
            DB db = new DB();
            string Query = "SELECT * FROM `trailer` ORDER BY `name` ASC;";
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
                    comboBox3.Items.Add(objName);
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
            MySqlCommand command = new MySqlCommand("INSERT INTO `tracks` (`car`, `model`, `driver`, `trailer`, `option`, `type`) VALUES (@car, @model, @driver, @trailer, @option, @type)", db.getConnection());

            command.Parameters.Add("@car", MySqlDbType.VarChar).Value = comboBox1.Text;
            command.Parameters.Add("@model", MySqlDbType.VarChar).Value = textBox1.Text;
            command.Parameters.Add("@driver", MySqlDbType.VarChar).Value = comboBox2.Text;
            command.Parameters.Add("@trailer", MySqlDbType.VarChar).Value = comboBox3.Text;
            command.Parameters.Add("@option", MySqlDbType.VarChar).Value = textBox2.Text;
            command.Parameters.Add("@type", MySqlDbType.VarChar).Value = textBox3.Text;
            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            comboBox1.Text = "";
            textBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            dataGridView1.Rows.Clear();
            string Query = "SELECT * FROM `tracks`;";
            //string Query = "(SELECT * FROM `cars` WHERE `contractor` = '" + comboBox7.Text.ToString() + "' ORDER BY `name` ASC) ORDER BY `count` desc;";
            MySqlCommand cmdDataBase2 = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader2;
            int i = 0;
            try
            {
                db.openConnection();
                myReader2 = cmdDataBase2.ExecuteReader();

                while (myReader2.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1["id", i].Value = i + 1;
                    dataGridView1["car", i].Value = myReader2.GetString("car");
                    dataGridView1["model", i].Value = myReader2.GetString("model");
                    dataGridView1["driver", i].Value = myReader2.GetString("driver");
                    dataGridView1["trailer", i].Value = myReader2.GetString("trailer");
                    dataGridView1["option", i].Value = myReader2.GetString("option");
                    dataGridView1["type", i].Value = myReader2.GetString("type");
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }

        string carEd = "";
        string modelEd = "";
        string driverEd = "";
        string trailerEd = "";
        string optionEd = "";
        string typeEd = "";

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = dataGridView1.CurrentRow.Index;
            comboBox1.Text = dataGridView1["car", idx].Value.ToString();
            textBox1.Text = dataGridView1["model", idx].Value.ToString();
            comboBox2.Text = dataGridView1["driver", idx].Value.ToString();
            comboBox3.Text = dataGridView1["trailer", idx].Value.ToString();
            textBox2.Text = dataGridView1["option", idx].Value.ToString();
            textBox3.Text = dataGridView1["type", idx].Value.ToString();
            carEd = dataGridView1["car", idx].Value.ToString();
            modelEd = dataGridView1["model", idx].Value.ToString();
            driverEd = dataGridView1["driver", idx].Value.ToString();
            trailerEd = dataGridView1["trailer", idx].Value.ToString();
            optionEd = dataGridView1["option", idx].Value.ToString();
            typeEd = dataGridView1["type", idx].Value.ToString();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("DELETE FROM `tracks` WHERE `car` = '" + carEd + "' AND `model` = '" + modelEd + "' AND `driver` = '" + driverEd + "' AND `trailer` = '" + trailerEd + "'", db.getConnection());

            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            command = new MySqlCommand("INSERT INTO `tracks` (`car`, `model`, `driver`, `trailer`, `option`, `type`) VALUES (@car, @model, @driver, @trailer, @option, @type)", db.getConnection());

            command.Parameters.Add("@car", MySqlDbType.VarChar).Value = comboBox1.Text;
            command.Parameters.Add("@model", MySqlDbType.VarChar).Value = textBox1.Text;
            command.Parameters.Add("@driver", MySqlDbType.VarChar).Value = comboBox2.Text;
            command.Parameters.Add("@trailer", MySqlDbType.VarChar).Value = comboBox3.Text;
            command.Parameters.Add("@option", MySqlDbType.VarChar).Value = textBox2.Text;
            command.Parameters.Add("@type", MySqlDbType.VarChar).Value = textBox3.Text;
            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            comboBox1.Text = "";
            textBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            dataGridView1.Rows.Clear();
            string Query = "SELECT * FROM `tracks`;";
            //string Query = "(SELECT * FROM `cars` WHERE `contractor` = '" + comboBox7.Text.ToString() + "' ORDER BY `name` ASC) ORDER BY `count` desc;";
            MySqlCommand cmdDataBase2 = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader2;
            int i = 0;
            try
            {
                db.openConnection();
                myReader2 = cmdDataBase2.ExecuteReader();

                while (myReader2.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1["id", i].Value = i + 1;
                    dataGridView1["car", i].Value = myReader2.GetString("car");
                    dataGridView1["model", i].Value = myReader2.GetString("model");
                    dataGridView1["driver", i].Value = myReader2.GetString("driver");
                    dataGridView1["trailer", i].Value = myReader2.GetString("trailer");
                    dataGridView1["option", i].Value = myReader2.GetString("option");
                    dataGridView1["type", i].Value = myReader2.GetString("type");
                    i++;
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
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("DELETE FROM `tracks` WHERE `car` = '" + carEd + "' AND `model` = '" + modelEd + "' AND `driver` = '" + driverEd + "' AND `trailer` = '" + trailerEd + "'", db.getConnection());

            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            comboBox1.Text = "";
            textBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            dataGridView1.Rows.Clear();

            string Query = "SELECT * FROM `tracks`;";
            //string Query = "(SELECT * FROM `cars` WHERE `contractor` = '" + comboBox7.Text.ToString() + "' ORDER BY `name` ASC) ORDER BY `count` desc;";
            MySqlCommand cmdDataBase2 = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader2;
            int i = 0;
            try
            {
                db.openConnection();
                myReader2 = cmdDataBase2.ExecuteReader();

                while (myReader2.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1["id", i].Value = i + 1;
                    dataGridView1["car", i].Value = myReader2.GetString("car");
                    dataGridView1["model", i].Value = myReader2.GetString("model");
                    dataGridView1["driver", i].Value = myReader2.GetString("driver");
                    dataGridView1["trailer", i].Value = myReader2.GetString("trailer");
                    dataGridView1["option", i].Value = myReader2.GetString("option");
                    dataGridView1["type", i].Value = myReader2.GetString("type");
                    i++;
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
            DB db = new DB();
            string Query = "SELECT * FROM `cars` WHERE `name` = '" + comboBox1.Text + "';";
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
                    if (objName.Equals("") || objName.Equals(" "))
                    {
                        continue;
                    }
                    textBox1.Text = myReader2.GetString("option");
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
            DB db = new DB();
            string Query = "SELECT * FROM `trailer` WHERE `name` = '" + comboBox3.Text + "';";
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
                    textBox2.Text = myReader2.GetString("option");
                    textBox3.Text = myReader2.GetString("type");
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
            string Query = "SELECT * FROM `drivers` ORDER BY `contractor` ASC;";
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

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            DB db = new DB();
            string Query = "SELECT * FROM `cars` WHERE `contractor` = '" + comboBox4.Text + "'ORDER BY `name` ASC;";
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
                    if (objName.Equals("") || objName.Equals(" "))
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

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            DB db = new DB();
            string Query = "SELECT * FROM `drivers` WHERE `contractor` = '" + comboBox5.Text + "'ORDER BY `name` ASC;";
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
                    if (objName.Equals("") || objName.Equals(" "))
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
        void FillCombo5()
        {
            string Query = "SELECT * FROM `drivers` ORDER BY `contractor` ASC;";
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
    }
}

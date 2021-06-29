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
    public partial class Drivers : Form
    {
        public Drivers()
        {
            InitializeComponent();

            FillCombo1();
            //FillDGV();

            plusButton.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

            int ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            int ScreenHeight = Screen.PrimaryScreen.Bounds.Height;

            this.Location = new Point((ScreenWidth / 2) - (this.Width / 2), (ScreenHeight / 2) - (this.Height / 2));
            this.ControlBox = false;
        }

        public string val = "";

        void FillCombo1()
        {
            string Query = "SELECT * FROM `drivers` ORDER BY `contractor` ASC";
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

        void FillDGV()
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `drivers`", db.getConnection());
            MySqlDataReader myReader;
            int i = 0;
            try
            {
                db.openConnection();
                myReader = command.ExecuteReader();

                while (myReader.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, i].Value = myReader.GetString("id");
                    dataGridView1[1, i].Value = myReader.GetString("name");
                    i++;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `drivers` WHERE `contractor` = '" + comboBox1.Text + "' ORDER BY `name` ASC", db.getConnection());
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
                        dataGridView1.Rows.Add();
                        dataGridView1[0, k].Value = k + 1;
                        dataGridView1[1, k].Value = objName;
                        k++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }

        private void plusButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `drivers` (`contractor`, `name`) VALUES (@contractor, @drivers)", db.getConnection());

            command.Parameters.Add("@contractor", MySqlDbType.VarChar).Value = comboBox1.Text;
            command.Parameters.Add("@drivers", MySqlDbType.VarChar).Value = textBox1.Text;
            db.openConnection();

            command.ExecuteNonQuery();


            db.closeConnection();

            textBox1.Text = "";
            dataGridView1.Rows.Clear();
            command = new MySqlCommand("SELECT * FROM `drivers` WHERE `contractor` = '" + comboBox1.Text + "' ORDER BY `name` ASC", db.getConnection());
            MySqlDataReader myReader;
            int k = 0;
            string[] names = new string[100];
            try
            {
                db.openConnection();
                myReader = command.ExecuteReader();

                while (myReader.Read())
                {
                    string objName = myReader.GetString("name");
                    if (objName.Equals("") || objName.Equals(" "))
                    {
                        continue;
                    }
                    dataGridView1.Rows.Add();
                    dataGridView1[0, k].Value = k + 1;
                    dataGridView1[1, k].Value = objName;
                    k++;
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
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("UPDATE `drivers` SET `name` = '" + textBox1.Text + "' WHERE `name` = '" + val + "'", db.getConnection());

            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            MySqlCommand command2 = new MySqlCommand("UPDATE `request` SET `drivers` = '" + textBox1.Text + "' WHERE `drivers` = '" + val + "'", db.getConnection());
            db.openConnection();

            command2.ExecuteNonQuery();

            db.closeConnection();

            textBox1.Text = "";
            dataGridView1.Rows.Clear();
            command = new MySqlCommand("SELECT * FROM `drivers` WHERE `contractor` = '" + comboBox1.Text + "' ORDER BY `name` ASC", db.getConnection());
            MySqlDataReader myReader;
            int k = 0;
            string[] names = new string[100];
            try
            {
                db.openConnection();
                myReader = command.ExecuteReader();

                while (myReader.Read())
                {
                    string objName = myReader.GetString("name");
                    if (objName.Equals("") || objName.Equals(" "))
                    {
                        continue;
                    }
                    dataGridView1.Rows.Add();
                    dataGridView1[0, k].Value = k + 1;
                    dataGridView1[1, k].Value = objName;
                    k++;
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
            MySqlCommand command = new MySqlCommand("DELETE FROM `drivers` WHERE `name` = '" + val + "'", db.getConnection());

            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            textBox1.Text = "";
            dataGridView1.Rows.Clear();
            command = new MySqlCommand("SELECT * FROM `drivers` WHERE `contractor` = '" + comboBox1.Text + "' ORDER BY `name` ASC", db.getConnection());
            MySqlDataReader myReader;
            int k = 0;
            string[] names = new string[100];
            try
            {
                db.openConnection();
                myReader = command.ExecuteReader();

                while (myReader.Read())
                {
                    string objName = myReader.GetString("name");
                    if (objName.Equals("") || objName.Equals(" "))
                    {
                        continue;
                    }
                    dataGridView1.Rows.Add();
                    dataGridView1[0, k].Value = k + 1;
                    dataGridView1[1, k].Value = objName;
                    k++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = dataGridView1.CurrentRow.Index;
            val = dataGridView1[1, idx].Value.ToString();
        }
    }
}

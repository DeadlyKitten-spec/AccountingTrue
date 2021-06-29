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
    public partial class Worker : Form
    {
        public Worker()
        {
            InitializeComponent();

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            FillDGV();
            this.ControlBox = false;
        }


        void FillDGV()
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `workers` ORDER BY `name` ASC", db.getConnection());
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
                        //dataGridView1[1, k].Value = myReader.GetString("shift");
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `workers` (`shift`, `name`) VALUES (@shift, @name)", db.getConnection());

            command.Parameters.Add("@shift", MySqlDbType.VarChar).Value = '1';
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox2.Text;
            db.openConnection();

            command.ExecuteNonQuery();


            db.closeConnection();

            textBox1.Text = "";
            textBox2.Text = "";
            dataGridView1.Rows.Clear();
            command = new MySqlCommand("(SELECT * FROM `workers` ORDER BY `shift` ASC) ORDER BY `name` ASC", db.getConnection());
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
                        //dataGridView1[1, k].Value = myReader.GetString("shift");
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("UPDATE `workers` SET `shift` = '" + '1' + "', `name` = '" + textBox2.Text + "' WHERE `name` = '" + val + "'", db.getConnection());

            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            textBox1.Text = "";
            textBox2.Text = "";
            dataGridView1.Rows.Clear();
            command = new MySqlCommand("(SELECT * FROM `workers` ORDER BY `shift` ASC) ORDER BY `name` ASC", db.getConnection());
            //command = new MySqlCommand("SELECT * FROM `workers` ORDER BY `name` ASC", db.getConnection());
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
                        //dataGridView1[1, k].Value = myReader.GetString("shift");
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

        public string val = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = dataGridView1.CurrentRow.Index;
            val = dataGridView1[1, idx].Value.ToString();
            //textBox1.Text = dataGridView1[1, idx].Value.ToString();
            textBox2.Text = dataGridView1[1, idx].Value.ToString();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("DELETE FROM `workers` WHERE `name` = '" + val + "'", db.getConnection());

            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            textBox1.Text = "";
            textBox2.Text = "";
            dataGridView1.Rows.Clear(); 
            command = new MySqlCommand("(SELECT * FROM `workers` ORDER BY `shift` ASC) ORDER BY `name` ASC", db.getConnection());
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
                        //dataGridView1[1, k].Value = myReader.GetString("shift");
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}

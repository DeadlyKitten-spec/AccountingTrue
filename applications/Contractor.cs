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
    public partial class Contractor : Form
    {
        public Contractor()
        {
            InitializeComponent();

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            FillDGV();
            this.ControlBox = false;
        }

        public string val = "";

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `cars` (`contractor`, `name`, `option`) VALUES (@contractor, @cars, @option)", db.getConnection());

            command.Parameters.Add("@contractor", MySqlDbType.VarChar).Value = textBox2.Text;
            command.Parameters.Add("@cars", MySqlDbType.VarChar).Value = " ";
            command.Parameters.Add("@option", MySqlDbType.VarChar).Value = " ";
            db.openConnection();

            command.ExecuteNonQuery();


            db.closeConnection();

            command = new MySqlCommand("INSERT INTO `drivers` (`contractor`, `name`) VALUES (@contractor, @drivers)", db.getConnection());

            command.Parameters.Add("@contractor", MySqlDbType.VarChar).Value = textBox2.Text;
            command.Parameters.Add("@drivers", MySqlDbType.VarChar).Value = " ";
            db.openConnection();

            command.ExecuteNonQuery();


            db.closeConnection();

            textBox2.Text = "";
            dataGridView1.Rows.Clear();
            command = new MySqlCommand("SELECT * FROM `cars` ORDER BY `contractor` ASC", db.getConnection());
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

        void FillDGV()
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `cars` ORDER BY `contractor` ASC", db.getConnection());
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("UPDATE `drivers` SET `contractor` = '" + textBox2.Text + "' WHERE `contractor` = '" + val + "'", db.getConnection());

            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            command = new MySqlCommand("UPDATE `cars` SET `contractor` = '" + textBox2.Text + "' WHERE `contractor` = '" + val + "'", db.getConnection());

            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            command = new MySqlCommand("UPDATE `request` SET `contractor` = '" + textBox2.Text + "' WHERE `contractor` = '" + val + "'", db.getConnection());

            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            command = new MySqlCommand("UPDATE `request` SET `driveCont` = '" + textBox2.Text + "' WHERE `driveCont` = '" + val + "'", db.getConnection());

            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            textBox2.Text = "";
            dataGridView1.Rows.Clear();
            command = new MySqlCommand("SELECT * FROM `cars` ORDER BY `contractor` ASC", db.getConnection());
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = dataGridView1.CurrentRow.Index;
            val = dataGridView1[1, idx].Value.ToString();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("DELETE FROM `drivers` WHERE `contractor` = '" + val + "'", db.getConnection());

            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            command = new MySqlCommand("DELETE FROM `cars` WHERE `contractor` = '" + val + "'", db.getConnection());

            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            textBox2.Text = "";
            dataGridView1.Rows.Clear();
            command = new MySqlCommand("SELECT * FROM `cars` ORDER BY `contractor` ASC", db.getConnection());
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
    }
}

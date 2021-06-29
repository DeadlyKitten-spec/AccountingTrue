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
    public partial class Nomenclature : Form
    {
        public Nomenclature()
        {
            InitializeComponent();
            FillDGV();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;



            int ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            int ScreenHeight = Screen.PrimaryScreen.Bounds.Height;

            this.Location = new Point((ScreenWidth / 2) - (this.Width / 2), (ScreenHeight / 2) - (this.Height / 2));
            this.ControlBox = false;
        }

        public string val = "";

        Point lastPoint;
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        void FillDGV()
        {
            string Query = "SELECT * FROM `namecargo`";
            DB db = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            string[] names = new string[100];
            int k = 0;
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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `namecargo` (`name`) VALUES (@cargo)", db.getConnection());

            command.Parameters.Add("@cargo", MySqlDbType.VarChar).Value = textBox2.Text;
            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            textBox2.Text = "";
            dataGridView1.Rows.Clear();
            command = new MySqlCommand("SELECT * FROM `namecargo`", db.getConnection());
            MySqlDataReader myReader;
            string[] names = new string[100];
            int k = 0;
            try
            {
                db.openConnection();
                myReader = command.ExecuteReader();
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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("UPDATE `namecargo` SET `name` = '" + textBox2.Text + "' WHERE `name` = '" + val + "'", db.getConnection());

            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            textBox2.Text = "";
            dataGridView1.Rows.Clear();
            command = new MySqlCommand("SELECT * FROM `namecargo`", db.getConnection());
            MySqlDataReader myReader;
            string[] names = new string[100];
            int k = 0;
            try
            {
                db.openConnection();
                myReader = command.ExecuteReader();
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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = dataGridView1.CurrentRow.Index;
            val = dataGridView1[1, idx].Value.ToString();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("DELETE FROM `namecargo` WHERE `name` = '" + val + "'", db.getConnection());

            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            textBox2.Text = "";
            dataGridView1.Rows.Clear();
            command = new MySqlCommand("SELECT * FROM `namecargo`", db.getConnection());
            MySqlDataReader myReader;
            string[] names = new string[100];
            int k = 0;
            try
            {
                db.openConnection();
                myReader = command.ExecuteReader();
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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
    }
}

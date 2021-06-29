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
    public partial class Chose : Form
    {
        public Chose()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            WriteRequest write = new WriteRequest();
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `request` WHERE `id` = '" + textBox2.Text.ToString() + "';", db.getConnection());
            MySqlDataReader myReader;
            this.Hide();
            WorkSpace.ActiveForm.Hide();
            write.Show();
            try
            {
                db.openConnection();
                myReader = command.ExecuteReader();

                while (myReader.Read())
                {
                    if (myReader.GetString("status").Equals("В работе"))
                    {
                        write.dateTimePicker1.Value = myReader.GetDateTime("docDate");
                        write.comboBox1.Text = myReader.GetString("deal");
                        write.comboBox2.Text = myReader.GetString("ourFirms");
                        write.comboBox3.Text = myReader.GetString("buyer");
                        write.comboBox4.Text = myReader.GetString("sender");
                        write.comboBox5.Text = myReader.GetString("recipient");
                        write.comboBox6.Text = myReader.GetString("object");
                        //write.comboBox7.Text = myReader.GetString("nameCargo");
                        write.textBox3.Text = myReader.GetString("numberTrip");
                    }
                    if (myReader.GetString("status").Equals("Назначена"))
                    {
                        write.dateTimePicker1.Value = myReader.GetDateTime("docDate");
                        write.comboBox1.Text = myReader.GetString("deal");
                        write.comboBox2.Text = myReader.GetString("ourFirms");
                        write.comboBox3.Text = myReader.GetString("buyer");
                        write.comboBox4.Text = myReader.GetString("sender");
                        write.comboBox5.Text = myReader.GetString("recipient");
                        write.comboBox6.Text = myReader.GetString("object");
                        //write.comboBox7.Text = myReader.GetString("nameCargo");
                        write.textBox3.Text = myReader.GetString("numberTrip");
                        write.comboBox8.Text = myReader.GetString("cars");
                        write.comboBox9.Text = myReader.GetString("drivers");
                        write.textBox5.Text = myReader.GetString("numberDocDriver");
                        write.dateTimePicker2.Text = myReader.GetString("dateDocDriver");
                        write.comboBox10.Text = myReader.GetString("fromCounterparty");
                    }
                    /*WriteRequest.Cargo[WriteRequest.CargoCount] = myReader.GetString("nameCargo");
                    WriteRequest.CargoCount++;*/
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

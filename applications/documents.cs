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
    public partial class documents : Form
    {
        Color FlatColor;
        public documents()
        {
            InitializeComponent();

            button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = FlatColor;

            button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button2.FlatAppearance.BorderColor = FlatColor;
        }

        private void button2_Click(object sender, EventArgs e)
        {
/*            StatementBuyer sb = this.Owner as StatementBuyer;
            sb.printF = false;*/
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string buyer = "";

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Equals("") || textBox2.Text.Equals(""))
            {
                MessageBox.Show("Вы ввели не все данные");
            }
            else
            {
                StatementBuyer sb = this.Owner as StatementBuyer;
                DB db = new DB();
                MySqlCommand command = new MySqlCommand("INSERT INTO `documents` (`buyer`, `number`, `date`, `sum`) VALUES (@buyer, @number, @date, @sum)", db.getConnection());

                command.Parameters.Add("@buyer", MySqlDbType.VarChar).Value = buyer;
                command.Parameters.Add("@number", MySqlDbType.VarChar).Value = textBox2.Text;
                string[] fordate = dateTimePicker3.Value.ToString().Split(' ');
                string[] fordate2 = fordate[0].Split('.');
                string tarr = fordate2[2] + "-" + fordate2[1] + "-" + fordate2[0];
                command.Parameters.Add("@date", MySqlDbType.VarChar).Value = tarr;
                string tar = "";
                for (int i = 0; i < textBox1.Text.Length; i++)
                {
                    if (textBox1.Text[i] == '.')
                    {
                        string[] asd = textBox1.Text.Split('.');
                        tar = asd[0] + "," + asd[1];
                        textBox1.Text = tar;
                        break;
                    }
                }
                command.Parameters.Add("@sum", MySqlDbType.VarChar).Value = textBox1.Text;
                db.openConnection();

                command.ExecuteNonQuery();


                db.closeConnection();
                this.Close();
            }
        }
    }
}

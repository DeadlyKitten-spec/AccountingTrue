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
    public partial class CargoRequest : Form
    {
        public CargoRequest()
        {
            InitializeComponent();

            FillCombo();

            label1.Text = "Добавить " + WriteRequest.Deal;
        }
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        void FillCombo() {
            string Query = "SELECT * FROM `nameCargo`";
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
                    comboBox1.Items.Add(objName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            WriteRequest.Cargo[WriteRequest.CargoCount] = comboBox1.Text;
            WriteRequest.numCargo[WriteRequest.CargoCount] = double.Parse(textBox1.Text);
            WriteRequest.CargoCount++;
            comboBox1.Text = "";
            textBox1.Text = "";
            MessageBox.Show(WriteRequest.Deal + " добавлен");
        }

    }
}

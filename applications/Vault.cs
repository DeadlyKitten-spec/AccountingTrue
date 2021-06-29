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
    public partial class Vault : Form
    {
        Color FlatColor;
        public Vault()
        {
            InitializeComponent();
            LoadDGV();
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = FlatColor;
            button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button2.FlatAppearance.BorderColor = FlatColor;
            button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button3.FlatAppearance.BorderColor = FlatColor;
            button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button8.FlatAppearance.BorderColor = FlatColor;
            button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button13.FlatAppearance.BorderColor = FlatColor;
            button15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button15.FlatAppearance.BorderColor = FlatColor;
        }

        void LoadDGV()
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            dgv.Columns.AddRange(
            new DataGridViewTextBoxColumn() { Name = "worker", HeaderText = "Рабочий" },
            new DataGridViewTextBoxColumn() { Name = "clock", HeaderText = "Кол-во часов" });

            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            dataGridView2.Columns.AddRange(
            new DataGridViewTextBoxColumn() { Name = "product", HeaderText = "Товар" },
            new DataGridViewTextBoxColumn() { Name = "count", HeaderText = "Кол-во" });

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.AddRange(
            new DataGridViewTextBoxColumn() { Name = "product", HeaderText = "Товар" },
            new DataGridViewTextBoxColumn() { Name = "count", HeaderText = "Кол-во" });

            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();
            dataGridView3.Columns.AddRange(
            new DataGridViewTextBoxColumn() { Name = "product", HeaderText = "Товар" },
            new DataGridViewTextBoxColumn() { Name = "count", HeaderText = "Кол-во" });
        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ForWorkers fw = new ForWorkers();
            if (comboBox1.Text.Equals("Первая"))
            {
                fw.shift = "1";
            }
            if (comboBox1.Text.Equals("Вторая"))
            {
                fw.shift = "2";
            }
            if (comboBox1.Text.Equals("Третья"))
            {
                fw.shift = "3";
            }
            if (comboBox1.Text.Equals("Все"))
            {
                fw.shift = "";
            }
            fw.Owner = this;
            //MessageBox.Show(fw.shift);
            fw.Show();
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

        private void button12_Click(object sender, EventArgs e)
        {
            int year = dateTimePicker4.Value.Date.Year;
            int month = dateTimePicker4.Value.Date.Month;
            int day = dateTimePicker4.Value.Date.Day;
            int[] dateArr = visok(dateTimePicker4.Value.Date.Year);
            if (day == 1)
            {
                month--;
                if (month == 0)
                {
                    month = 12;
                    year--;
                }
                day = dateArr[month - 1];
                dateTimePicker4.Value = new DateTime(year, month, day);
            }
            else
            {
                dateTimePicker4.Value = new DateTime(year, month, day - 1);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int year = dateTimePicker4.Value.Date.Year;
            int month = dateTimePicker4.Value.Date.Month;
            int day = dateTimePicker4.Value.Date.Day;
            int[] dateArr = visok(dateTimePicker4.Value.Date.Year);
            if (day == dateArr[month - 1])
            {
                if (month == 12)
                {
                    month = 0;
                    year++;
                }
                day = 1;
                dateTimePicker4.Value = new DateTime(year, month + 1, day);
            }
            else
            {
                dateTimePicker4.Value = new DateTime(year, month, day + 1);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count > 1)
            {
                DialogResult result = MessageBox.Show(
                        "Удалить данную позицию?",
                        "Сообщение",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1
                        //MessageBoxOptions.DefaultDesktopOnly
                        );

                if (result == DialogResult.Yes)
                {
                    dgv.Rows.RemoveAt(curRow);
                }
            }
        }
        public int curRow;
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            curRow = dgv.CurrentCell.RowIndex;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            ForProduct fp = new ForProduct();
            fp.table = 0;
            fp.Owner = this;
            fp.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 1)
            {
                DialogResult result = MessageBox.Show(
                        "Удалить данную позицию?",
                        "Сообщение",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1
                        //MessageBoxOptions.DefaultDesktopOnly
                        );

                if (result == DialogResult.Yes)
                {
                    dataGridView2.Rows.RemoveAt(curRow);
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            curRow = dataGridView2.CurrentCell.RowIndex;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                       "Введенные данные верны?",
                       "Сообщение",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Information,
                       MessageBoxDefaultButton.Button1
                       //MessageBoxOptions.DefaultDesktopOnly
                       );

            if (result == DialogResult.Yes)
            {
                for (int i = 0; i < dgv.Rows.Count-1; i++)
                {
                    for (int j = 0; j < dataGridView2.Rows.Count-1; j++)
                    {
                        DB db = new DB();
                        MySqlCommand command = new MySqlCommand("INSERT INTO `work` (`date`, `shift`, `worker`, `clock`, `product`, `count`) VALUES (@date, @shift, @worker, @clock, @product, @count)", db.getConnection());

                        command.Parameters.Add("@date", MySqlDbType.Date).Value = dateTimePicker1.Value;
                        if (comboBox1.Text.Equals("Первая")) {
                            command.Parameters.Add("@shift", MySqlDbType.Int64).Value = "1";
                        }
                        if (comboBox1.Text.Equals("Вторая"))
                        {
                            command.Parameters.Add("@shift", MySqlDbType.Int64).Value = "2";
                        }
                        command.Parameters.Add("@worker", MySqlDbType.VarChar).Value = dgv[0, i].Value;
                        string time = dgv[1, i].Value.ToString();
                        for (int p = 0; p < time.Length; p++)
                        {
                            if (time[p] == '.')
                            {
                                string[] ans = time.Split('.');
                                time = ans[0] + "," + ans[1];
                                break;
                            }
                        }
                        command.Parameters.Add("@clock", MySqlDbType.VarChar).Value = time;
                        command.Parameters.Add("@product", MySqlDbType.VarChar).Value = dataGridView2[0, j].Value;
                        string num = dataGridView2[1, j].Value.ToString();
                        for (int p = 0; p < num.Length; p++)
                        {
                            if (num[p] == '.')
                            {
                                string[] ans = num.Split('.');
                                num = ans[0] + "," + ans[1];
                                break;
                            }
                        }
                        command.Parameters.Add("@count", MySqlDbType.VarChar).Value = num;
                        db.openConnection();

                        command.ExecuteNonQuery();


                        db.closeConnection();

                    }
                }
                for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                {
                    DB db = new DB();
                    string num = dataGridView2[1, i].Value.ToString();
                    for (int p = 0; p < num.Length; p++)
                    {
                        if (num[p] == '.')
                        {
                            string[] ans = num.Split('.');
                            num = ans[0] + "," + ans[1];
                            break;
                        }
                    }
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM `product` WHERE `name` = '" + dataGridView2[0, i].Value + "'", db.getConnection());
                    MySqlDataReader myReader;
                    double count = 0;
                    try
                    {
                        db.openConnection();
                        myReader = cmd.ExecuteReader();

                        int j = 0;
                        while (myReader.Read())
                        {
                            count = double.Parse(myReader.GetString("count"));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    db.closeConnection();
                    count += double.Parse(num); 
                    MySqlCommand command = new MySqlCommand("UPDATE `product` SET `count` = '" + count + "' WHERE `name` = '" + dataGridView2[0, i].Value + "'", db.getConnection());
                    db.openConnection();

                    command.ExecuteNonQuery();


                    db.closeConnection();


                    command = new MySqlCommand("INSERT INTO `operations` (`date`, `operation`, `product`, `count`) VALUES (@date, @ope, @product, @count)", db.getConnection());

                    //string[] fordate = dateTimePicker1.Value.ToString().Split(' ');
                    command.Parameters.Add("@date", MySqlDbType.Date).Value = dateTimePicker1.Value;
                    command.Parameters.Add("@ope", MySqlDbType.VarChar).Value = "Поступление от производства";
                    command.Parameters.Add("@product", MySqlDbType.VarChar).Value = dataGridView2[0, i].Value;
                    string numm = dataGridView2[1, i].Value.ToString();
                    for (int p = 0; p < numm.Length; p++)
                    {
                        if (numm[p] == '.')
                        {
                            string[] ans = numm.Split('.');
                            numm = ans[0] + "," + ans[1];
                            break;
                        }
                    }
                    command.Parameters.Add("@count", MySqlDbType.VarChar).Value = numm;

                    //MessageBox.Show("INSERT INTO `operations` (`date`, `operation`, `product`, `count`) VALUES (" + fordate[0] + ", Поступление от производства, " + dataGridView2[0, i].Value + ", " + numm + ")");
                    db.openConnection();

                    command.ExecuteNonQuery();


                    db.closeConnection();

                }
                comboBox1.Text = "";
                dgv.Rows.Clear();
                dataGridView2.Rows.Clear();
            }
           
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ForProduct fp = new ForProduct();
            fp.table = 1;
            fp.Owner = this;
            fp.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 1)
            {
                DialogResult result = MessageBox.Show(
                        "Удалить данную позицию?",
                        "Сообщение",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1
                        //MessageBoxOptions.DefaultDesktopOnly
                        );

                if (result == DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(curRow);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                       "Введенные данные верны?",
                       "Сообщение",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Information,
                       MessageBoxDefaultButton.Button1
                       //MessageBoxOptions.DefaultDesktopOnly
                       );

            if (result == DialogResult.Yes)
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    DB db = new DB();
                    string num = dataGridView1[1, i].Value.ToString();
                    for (int p = 0; p < num.Length; p++)
                    {
                        if (num[p] == '.')
                        {
                            string[] ans = num.Split('.');
                            num = ans[0] + "," + ans[1];
                            break;
                        }
                    }
                    MySqlCommand command = new MySqlCommand("UPDATE `product` SET `count` = '" + num + "' WHERE `name` = '" + dataGridView1[0, i].Value + "'", db.getConnection());
                    db.openConnection();

                    command.ExecuteNonQuery();


                    db.closeConnection();

                    command = new MySqlCommand("INSERT INTO `operations` (`date`, `operation`, `product`, `count`) VALUES (@date, @ope, @product, @count)", db.getConnection());

                    //string[] fordate = dateTimePicker1.Value.ToString().Split(' ');
                    command.Parameters.Add("@date", MySqlDbType.Date).Value = dateTimePicker4.Value;
                    command.Parameters.Add("@ope", MySqlDbType.VarChar).Value = "Инвентаризация";
                    command.Parameters.Add("@product", MySqlDbType.VarChar).Value = dataGridView1[0, i].Value;
                    string numm = dataGridView1[1, i].Value.ToString();
                    for (int p = 0; p < numm.Length; p++)
                    {
                        if (numm[p] == '.')
                        {
                            string[] ans = numm.Split('.');
                            numm = ans[0] + "," + ans[1];
                            break;
                        }
                    }
                    command.Parameters.Add("@count", MySqlDbType.VarChar).Value = numm;

                    //MessageBox.Show("INSERT INTO `operations` (`date`, `operation`, `product`, `count`) VALUES (" + fordate[0] + ", Поступление от производства, " + dataGridView2[0, i].Value + ", " + numm + ")");
                    db.openConnection();

                    command.ExecuteNonQuery();


                    db.closeConnection();
                }
                dataGridView1.Rows.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VaultStatement vaultStatement = new VaultStatement();
            vaultStatement.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            ForProduct fp = new ForProduct();
            fp.table = 2;
            fp.Owner = this;
            fp.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count > 1)
            {
                DialogResult result = MessageBox.Show(
                        "Удалить данную позицию?",
                        "Сообщение",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1
                        //MessageBoxOptions.DefaultDesktopOnly
                        );

                if (result == DialogResult.Yes)
                {
                    dataGridView3.Rows.RemoveAt(curRow);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                       "Введенные данные верны?",
                       "Сообщение",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Information,
                       MessageBoxDefaultButton.Button1
                       //MessageBoxOptions.DefaultDesktopOnly
                       );

            if (result == DialogResult.Yes)
            {
                for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
                {
                    DB db = new DB();
                    string num = dataGridView3[1, i].Value.ToString();
                    for (int p = 0; p < num.Length; p++)
                    {
                        if (num[p] == '.')
                        {
                            string[] ans = num.Split('.');
                            num = ans[0] + "," + ans[1];
                            break;
                        }
                    }
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM `product` WHERE `name` = '" + dataGridView3[0, i].Value + "'", db.getConnection());
                    MySqlDataReader myReader;
                    double count = 0;
                    try
                    {
                        db.openConnection();
                        myReader = cmd.ExecuteReader();

                        int j = 0;
                        while (myReader.Read())
                        {
                            count = double.Parse(myReader.GetString("count"));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    db.closeConnection();
                    count -= double.Parse(num);
                    MySqlCommand command = new MySqlCommand("UPDATE `product` SET `count` = '" + count + "' WHERE `name` = '" + dataGridView3[0, i].Value + "'", db.getConnection());
                    db.openConnection();

                    command.ExecuteNonQuery();


                    db.closeConnection();

                    command = new MySqlCommand("INSERT INTO `operations` (`date`, `operation`, `product`, `count`) VALUES (@date, @ope, @product, @count)", db.getConnection());

                    //string[] fordate = dateTimePicker1.Value.ToString().Split(' ');
                    command.Parameters.Add("@date", MySqlDbType.Date).Value = dateTimePicker2.Value;
                    command.Parameters.Add("@ope", MySqlDbType.VarChar).Value = "Брак";
                    command.Parameters.Add("@product", MySqlDbType.VarChar).Value = dataGridView3[0, i].Value;
                    string numm = dataGridView3[1, i].Value.ToString();
                    for (int p = 0; p < numm.Length; p++)
                    {
                        if (numm[p] == '.')
                        {
                            string[] ans = numm.Split('.');
                            numm = ans[0] + "," + ans[1];
                            break;
                        }
                    }
                    command.Parameters.Add("@count", MySqlDbType.VarChar).Value = numm;

                    //MessageBox.Show("INSERT INTO `operations` (`date`, `operation`, `product`, `count`) VALUES (" + fordate[0] + ", Поступление от производства, " + dataGridView2[0, i].Value + ", " + numm + ")");
                    db.openConnection();

                    command.ExecuteNonQuery();


                    db.closeConnection();
                }
                dataGridView3.Rows.Clear();
            }
        }
    }
}

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
    public partial class CounterParty : Form
    {
        public CounterParty()
        {
            InitializeComponent();

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

            FillDGV();
            int ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            int ScreenHeight = Screen.PrimaryScreen.Bounds.Height;

            this.Location = new Point((ScreenWidth / 2) - (this.Width / 2), (ScreenHeight / 2) - (this.Height / 2));
            this.ControlBox = false;
        }

        public string val = "";
            

        void FillDGV()
        {
            string Query = "SELECT * FROM `counterparty` ORDER BY `name` ASC";
            DB db = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            CounterObject[] counter = new CounterObject[1000];
            for(int i = 0; i < 1000; i++)
            {
                counter[i] = new CounterObject();
                counter[i].name = "-1";
                counter[i].status = "-1";
            }
            int k = 0;
            try
            {
                db.openConnection();
                myReader = cmdDataBase.ExecuteReader();

                int j = 0;
                while (myReader.Read())
                {
                    string Name = myReader.GetString("name");
                    string objName = myReader.GetString("status");
                    bool f = true;
                    int itter = 0;
                    /*for (int i = 0; i < names.Length; i++)
                    {
                        if (names[i].Equals(Name))
                        {
                            if (namesStatus[i].Equals(objName))
                            {   
                                f = false;
                                break;
                            }
                        }
                    }*/
                    for(int i = 0; i < 1000; i++)
                    {
                        if (counter[i].name.Equals("-1"))
                        {
                            itter = i;
                            break;
                        }
                        if (counter[i].name.Equals(Name))
                        {
                            if (!counter[i].status.Equals(objName))
                            {
                                if (!objName.Equals("пусто"))
                                {
                                    counter[i].status = objName;
                                }
                            }
                            f = false;
                        }
                    }
                    if(f == true)
                    {
                        counter[itter].name = Name;
                        counter[itter].status = objName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            for (int i = 0; i < 1000; i++)
            {
                if (!counter[i].name.Equals("-1"))
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, k].Value = k + 1;
                    dataGridView1[1, k].Value = counter[i].name;
                    dataGridView1[2, k].Value = counter[i].status;
                    k++;
                }
                else
                {
                    break;
                }
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (checkBox3.CheckState == CheckState.Checked && ((checkBox1.CheckState == CheckState.Checked) || (checkBox2.CheckState == CheckState.Checked)))
            {
                MessageBox.Show("Неверный статус");
            }
            else
            {
                DB db = new DB();
                MySqlCommand command = new MySqlCommand("INSERT INTO `counterparty` (`name`, `objectName`, `price1`, `priceCount`, `status`, `priceBuyer1`, `priceBuyerCount`) VALUES (@party, @object, @price, @count, @status, @priceBuyer, @priceBuyerCount)", db.getConnection());

                command.Parameters.Add("@party", MySqlDbType.VarChar).Value = textBox2.Text;
                command.Parameters.Add("@object", MySqlDbType.VarChar).Value = "пусто";
                command.Parameters.Add("@price", MySqlDbType.VarChar).Value = "0";
                command.Parameters.Add("@count", MySqlDbType.Int32).Value = 1;
                command.Parameters.Add("@priceBuyer", MySqlDbType.VarChar).Value = "0";
                command.Parameters.Add("@priceBuyerCount", MySqlDbType.Int32).Value = 1;
                if (checkBox3.CheckState == CheckState.Checked)
                {
                    command.Parameters.Add("@status", MySqlDbType.VarChar).Value = "Диспетчер";
                }
                else
                {
                    if ((checkBox1.CheckState == CheckState.Checked) && (checkBox2.CheckState == CheckState.Checked))
                    {
                        command.Parameters.Add("@status", MySqlDbType.VarChar).Value = "Грузополучатель/грузоотправитель";
                    }
                    else
                    {
                        if (checkBox1.CheckState == CheckState.Checked)
                        {
                            command.Parameters.Add("@status", MySqlDbType.VarChar).Value = "Грузополучатель";
                        }
                        else
                        {
                            if (checkBox2.CheckState == CheckState.Checked)
                            {
                                command.Parameters.Add("@status", MySqlDbType.VarChar).Value = "Грузоотправитель";
                            }
                            if ((checkBox1.CheckState != CheckState.Checked) && (checkBox2.CheckState != CheckState.Checked))
                            {
                                command.Parameters.Add("@status", MySqlDbType.VarChar).Value = "Б/С";
                            }
                        }
                    }
                }

                db.openConnection();

                command.ExecuteNonQuery();

                db.closeConnection();

                textBox2.Text = "";
                checkBox1.CheckState = CheckState.Unchecked;
                checkBox2.CheckState = CheckState.Unchecked;
                checkBox3.CheckState = CheckState.Unchecked;
                dataGridView1.Rows.Clear();
                command = new MySqlCommand("SELECT * FROM `counterparty` ORDER BY `name` ASC", db.getConnection());
                MySqlDataReader myReader;
                CounterObject[] counter = new CounterObject[1000];
                for (int i = 0; i < 1000; i++)
                {
                    counter[i] = new CounterObject();
                    counter[i].name = "-1";
                    counter[i].status = "-1";
                }
                int k = 0;
                try
                {
                    db.openConnection();
                    myReader = command.ExecuteReader();

                    int j = 0;
                    while (myReader.Read())
                    {
                        string Name = myReader.GetString("name");
                        string objName = myReader.GetString("status");
                        bool f = true;
                        int itter = 0;
                        /*for (int i = 0; i < names.Length; i++)
                        {
                            if (names[i].Equals(Name))
                            {
                                if (namesStatus[i].Equals(objName))
                                {   
                                    f = false;
                                    break;
                                }
                            }
                        }*/
                        for (int i = 0; i < 1000; i++)
                        {
                            if (counter[i].name.Equals("-1"))
                            {   
                                itter = i;
                                break;
                            }
                            if (counter[i].name.Equals(Name))
                            {
                                if (!counter[i].status.Equals(objName))
                                {
                                    if (!objName.Equals("пусто"))
                                    {
                                        counter[i].status = objName;
                                    }
                                }
                                f = false;
                            }
                        }
                        if (f == true)
                        {
                            counter[itter].name = Name;
                            counter[itter].status = objName;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                db.closeConnection();
                for (int i = 0; i < 1000; i++)
                {
                    if (!counter[i].name.Equals("-1"))
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1[0, k].Value = k + 1;
                        dataGridView1[1, k].Value = counter[i].name;
                        dataGridView1[2, k].Value = counter[i].status;
                        k++;
                    }
                    else
                    {
                        break;
                    }
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        Point lastPoint;
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


        private void panel2_MouseDown_1(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel2_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = dataGridView1.CurrentRow.Index;
            val = dataGridView1[1, idx].Value.ToString();
            if(dataGridView1[2, idx].Value != null)
            {
                if (dataGridView1[2, idx].Value.Equals("Диспетчер"))
                {
                    checkBox1.CheckState = CheckState.Unchecked;
                    checkBox2.CheckState = CheckState.Unchecked;
                    checkBox3.CheckState = CheckState.Checked;
                }
                if (dataGridView1[2, idx].Value.Equals("Грузополучатель/грузоотправитель"))
                {
                    checkBox1.CheckState = CheckState.Checked;
                    checkBox2.CheckState = CheckState.Checked;
                    checkBox3.CheckState = CheckState.Unchecked;
                }
                if (dataGridView1[2, idx].Value.Equals("Грузополучатель"))
                {
                    checkBox1.CheckState = CheckState.Checked;
                    checkBox2.CheckState = CheckState.Unchecked;
                    checkBox3.CheckState = CheckState.Unchecked;
                }
                if (dataGridView1[2, idx].Value.Equals("Б/С"))
                {
                    checkBox1.CheckState = CheckState.Unchecked;
                    checkBox2.CheckState = CheckState.Unchecked;
                    checkBox3.CheckState = CheckState.Unchecked;
                }
                if (dataGridView1[2, idx].Value.Equals("Грузоотправитель"))
                {
                    checkBox1.CheckState = CheckState.Unchecked;
                    checkBox2.CheckState = CheckState.Checked;
                    checkBox3.CheckState = CheckState.Unchecked;
                }
            }
            textBox2.Text = dataGridView1[1, idx].Value.ToString();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (checkBox3.CheckState == CheckState.Checked && ((checkBox1.CheckState == CheckState.Checked) || (checkBox2.CheckState == CheckState.Checked)))
            {
                MessageBox.Show("Неверный статус");
            }
            else
            {
                DB db = new DB();
                String Line = "";
                if (!textBox2.Text.Equals(""))
                {
                    if (checkBox3.CheckState == CheckState.Checked)
                    {
                        Line = "UPDATE `counterparty` SET `name` = '" + textBox2.Text + "', `status` = 'Диспетчер' WHERE `name` = '" + val + "'";
                    }
                    else
                    {
                        if ((checkBox1.CheckState == CheckState.Checked) && (checkBox2.CheckState == CheckState.Checked))
                        {
                            Line = "UPDATE `counterparty` SET `name` = '" + textBox2.Text + "', `status` = 'Грузополучатель/грузоотправитель' WHERE `name` = '" + val + "'";
                        }
                        else
                        {
                            if (checkBox1.CheckState == CheckState.Checked)
                            {
                                Line = "UPDATE `counterparty` SET `name` = '" + textBox2.Text + "', `status` = 'Грузополучатель' WHERE `name` = '" + val + "'";
                            }
                            else
                            {
                                if (checkBox2.CheckState == CheckState.Checked)
                                {
                                    Line = "UPDATE `counterparty` SET `name` = '" + textBox2.Text + "', `status` = 'Грузоотправитель' WHERE `name` = '" + val + "'";
                                }
                            }
                        }
                    }
                    if(checkBox1.CheckState == CheckState.Unchecked && checkBox2.CheckState == CheckState.Unchecked && checkBox3.CheckState == CheckState.Unchecked)
                    {
                        Line = "UPDATE `counterparty` SET `name` = '" + textBox2.Text + "', `status` = 'Б/С' WHERE `name` = '" + val + "'";
                    }
                }
                else
                {
                    /*if (checkBox3.CheckState == CheckState.Checked)
                    {
                        Line = "UPDATE `counterparty` SET `status` = 'Диспетчер' WHERE `name` = '" + val + "'";
                    }
                    else
                    {
                        if ((checkBox1.CheckState == CheckState.Checked) && (checkBox2.CheckState == CheckState.Checked))
                        {
                            Line = "UPDATE `counterparty` SET `status` = 'Грузополучатель/грузоотправитель' WHERE `name` = '" + val + "'";
                        }
                        else
                        {
                            if (checkBox1.CheckState == CheckState.Checked)
                            {
                                Line = "UPDATE `counterparty` SET `status` = 'Грузополучатель' WHERE `name` = '" + val + "'";
                            }
                            else
                            {
                                if (checkBox2.CheckState == CheckState.Checked)
                                {
                                    Line = "UPDATE `counterparty` SET `status` = 'Грузоотправитель' WHERE `name` = '" + val + "'"; ;
                                }
                            }
                        }
                    }*/
                    MessageBox.Show("Вы не ввели название контрагенту");
                }
                MySqlCommand command = new MySqlCommand(Line, db.getConnection());

                db.openConnection();

                command.ExecuteNonQuery();

                db.closeConnection();

                if (!textBox2.Text.Equals(""))
                {
                    MySqlCommand command2 = new MySqlCommand("UPDATE `request` SET `buyer` = '" + textBox2.Text + "' WHERE `buyer` = '" + val + "'", db.getConnection());
                    db.openConnection();

                    command2.ExecuteNonQuery();

                    db.closeConnection();

                    command2 = new MySqlCommand("UPDATE `request` SET `sender` = '" + textBox2.Text + "' WHERE `sender` = '" + val + "'", db.getConnection());
                    db.openConnection();

                    command2.ExecuteNonQuery();

                    db.closeConnection();

                    command2 = new MySqlCommand("UPDATE `request` SET `recipient` = '" + textBox2.Text + "' WHERE `recipient` = '" + val + "'", db.getConnection());
                    db.openConnection();

                    command2.ExecuteNonQuery();

                    db.closeConnection();
                    
                    command2 = new MySqlCommand("UPDATE `request` SET `fromCounterparty` = '" + textBox2.Text + "' WHERE `fromCounterparty` = '" + val + "'", db.getConnection());
                    db.openConnection();

                    command2.ExecuteNonQuery();

                    db.closeConnection();
                }

                textBox2.Text = "";
                checkBox1.CheckState = CheckState.Unchecked;
                checkBox2.CheckState = CheckState.Unchecked;
                checkBox3.CheckState = CheckState.Unchecked;
                dataGridView1.Rows.Clear();
                command = new MySqlCommand("SELECT * FROM `counterparty` ORDER BY `name` ASC", db.getConnection());
                MySqlDataReader myReader;
                CounterObject[] counter = new CounterObject[1000];
                for (int i = 0; i < 1000; i++)
                {
                    counter[i] = new CounterObject();
                    counter[i].name = "-1";
                    counter[i].status = "-1";
                }
                int k = 0;
                try
                {
                    db.openConnection();
                    myReader = command.ExecuteReader();

                    int j = 0;
                    while (myReader.Read())
                    {
                        string Name = myReader.GetString("name");
                        string objName = myReader.GetString("status");
                        bool f = true;
                        int itter = 0;
                        /*for (int i = 0; i < names.Length; i++)
                        {
                            if (names[i].Equals(Name))
                            {
                                if (namesStatus[i].Equals(objName))
                                {   
                                    f = false;
                                    break;
                                }
                            }
                        }*/
                        for (int i = 0; i < 1000; i++)
                        {
                            if (counter[i].name.Equals("-1"))
                            {
                                itter = i;
                                break;
                            }
                            if (counter[i].name.Equals(Name))
                            {
                                if (!counter[i].status.Equals(objName))
                                {
                                    if (!objName.Equals("пусто"))
                                    {
                                        counter[i].status = objName;
                                    }
                                }
                                f = false;
                            }
                        }
                        if (f == true)
                        {
                            counter[itter].name = Name;
                            counter[itter].status = objName;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                db.closeConnection();
                for (int i = 0; i < 1000; i++)
                {
                    if (!counter[i].name.Equals("-1"))
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1[0, k].Value = k + 1;
                        dataGridView1[1, k].Value = counter[i].name;
                        dataGridView1[2, k].Value = counter[i].status;
                        k++;
                    }
                    else
                    {
                        break;
                    }
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("DELETE FROM `counterparty` WHERE `name` = '" + val + "'", db.getConnection());

            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            textBox2.Text = "";
            checkBox1.CheckState = CheckState.Unchecked;
            checkBox2.CheckState = CheckState.Unchecked;
            checkBox3.CheckState = CheckState.Unchecked;
            dataGridView1.Rows.Clear();
            command = new MySqlCommand("SELECT * FROM `counterparty` ORDER BY `name` ASC", db.getConnection());
            MySqlDataReader myReader;
            CounterObject[] counter = new CounterObject[1000];
            for (int i = 0; i < 1000; i++)
            {
                counter[i] = new CounterObject();
                counter[i].name = "-1";
                counter[i].status = "-1";
            }
            int k = 0;
            try
            {
                db.openConnection();
                myReader = command.ExecuteReader();

                int j = 0;
                while (myReader.Read())
                {
                    string Name = myReader.GetString("name");
                    string objName = myReader.GetString("status");
                    bool f = true;
                    int itter = 0;
                    /*for (int i = 0; i < names.Length; i++)
                    {
                        if (names[i].Equals(Name))
                        {
                            if (namesStatus[i].Equals(objName))
                            {   
                                f = false;
                                break;
                            }
                        }
                    }*/
                    for (int i = 0; i < 1000; i++)
                    {
                        if (counter[i].name.Equals("-1"))
                        {
                            itter = i;
                            break;
                        }
                        if (counter[i].name.Equals(Name))
                        {
                            if (!counter[i].status.Equals(objName))
                            {
                                if (!objName.Equals("пусто"))
                                {
                                    counter[i].status = objName;
                                }
                            }
                            f = false;
                        }
                    }
                    if (f == true)
                    {
                        counter[itter].name = Name;
                        counter[itter].status = objName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            for (int i = 0; i < 1000; i++)
            {
                if (!counter[i].name.Equals("-1"))
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, k].Value = k + 1;
                    dataGridView1[1, k].Value = counter[i].name;
                    dataGridView1[2, k].Value = counter[i].status;
                    k++;
                }
                else
                {
                    break;
                }
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
    }
}

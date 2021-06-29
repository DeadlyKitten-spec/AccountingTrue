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
    public partial class Price : Form
    {
        public Price()
        {
            InitializeComponent();
            FillCombo1();

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public string val = "";

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        void FillCombo1()
        {
            string Query = "SELECT * FROM `counterparty`";
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
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            string Query = "SELECT * FROM `counterparty` WHERE `name` = '" + comboBox1.Text.ToString() + "' ORDER BY `objectName` ASC;";
            DB db = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            int k = 0;
            string[] namess = new string[100];
            try
            {
                db.openConnection();
                myReader = cmdDataBase.ExecuteReader();

                while (myReader.Read())
                {
                    string objName = myReader.GetString("objectName");
                    if (objName.Equals(""))
                    {
                        continue;
                    }
                    bool f = true;
                    for (int i = 0; i < namess.Length; i++)
                    {
                        if (namess[i] != null)
                        {
                            if (namess[i].Equals(objName))
                            {
                                f = false;
                                break;
                            }
                        }
                    }
                    if (f == true)
                    {
                        namess[k] = objName;
                        comboBox2.Items.Add(objName);
                        k++;
                    }
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            dataGridView1.Rows.Clear();
            Query = "SELECT * FROM `counterparty` WHERE `name` = '" + comboBox1.Text.ToString() + "'ORDER BY `price` DESC;";
            cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader2;
            k = 0;
            ForPrice[] forPrice = new ForPrice[100];
            for (int i = 0; i < forPrice.Length; i++)
            {
                forPrice[i] = new ForPrice();
            }
            int itter = 0;
            bool g = true;
            try
            {
                db.openConnection();
                myReader2 = cmdDataBase.ExecuteReader();

                while (myReader2.Read())
                {
                    string objName = myReader2.GetString("objectName");
                    string objPrice = myReader2.GetString("price");
                    if (objName.Equals("") || objName.Equals(" "))
                    {
                        continue;
                    }
                    for(int i = 0; i < itter; i++)
                    {
                        //MessageBox.Show("nen");
                        g = true;
                        if (objName.Equals(forPrice[i].objectt))
                        {
                            g = false;
                            break;
                        }
                    }
                    if (g)
                    {
                        forPrice[itter].counterparty = myReader2.GetString("name");
                        forPrice[itter].objectt = objName;
                        forPrice[itter].price = objPrice;
                        itter++;
                        //MessageBox.Show(forPrice[k].price);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            for (int i = 0; i < itter; i++)
            {
                //MessageBox.Show(forPrice[i].price);
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = i + 1;
                dataGridView1[1, i].Value = forPrice[i].counterparty;
                dataGridView1[2, i].Value = forPrice[i].objectt;
                dataGridView1[3, i].Value = forPrice[i].price;
            }
            /*bool check = true;
            while (true)
            {
                check = true;
                if (k > 1)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int l = i + 1; l < dataGridView1.Rows.Count; l++)
                        {
                            if (!dataGridView1[2, i].Value.Equals(dataGridView1[2, l].Value))
                            {
                                break;
                            }
                            else
                            {
                                if (dataGridView1[3, i].Value.Equals(dataGridView1[3, l].Value))
                                {
                                    dataGridView1.Rows.RemoveAt(i);
                                    for (int j = i; j < dataGridView1.Rows.Count; j++)
                                    {
                                        dataGridView1[0, j].Value = j + 1;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int l = i + 1; l < dataGridView1.Rows.Count; l++)
                        {
                            if (dataGridView1[2, i].Value.Equals(dataGridView1[2, l].Value))
                            {
                                //dataGridView1[3, i].Value.Equals(" ") || dataGridView1[3, i].Value.Equals("") || dataGridView1[3, i].Value.Equals(null)
                                if (!dataGridView1[3, i].Value.Equals(dataGridView1[3, l].Value))
                                {
                                    MessageBox.Show("dasd");
                                    dataGridView1.Rows.RemoveAt(i);
                                    for (int j = i; j < dataGridView1.Rows.Count; j++)
                                    {
                                        dataGridView1[0, j].Value = j + 1;
                                    }
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = i + 1; j < dataGridView1.Rows.Count; i++)
                        {
                            if (!dataGridView1[2, j].)
                            {
                                if (dataGridView1[2, i].Value.Equals(dataGridView1[2, j].Value))
                                {
                                    check = false;
                                }
                            }
                        }
                    }
                    if (check)
                    {
                        break;
                    }
                }
            }
            *//*if(!dataGridView1[2, dataGridView1.Rows.Count - 1].Value.Equals(null)) {
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
            }*/
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            MySqlCommand command = new MySqlCommand("INSERT INTO `counterparty` (`name`, `objectName`, `price`) VALUES (@name, @object, @price)", db.getConnection());

            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = comboBox1.Text;
            command.Parameters.Add("@object", MySqlDbType.VarChar).Value = comboBox2.Text;
            string num = textBox1.Text;
            for (int j = 0; j < num.Length; j++)
            {
                if (num[j] == '.')
                {
                    string[] ans = num.Split('.');
                    num = ans[0] + "," + ans[1];
                    break;
                }
            }
            command.Parameters.Add("@price", MySqlDbType.VarChar).Value = num;
            db.openConnection();
            command.ExecuteNonQuery();
            db.closeConnection();
            dataGridView1.Rows.Clear();
            string Query = "SELECT * FROM `counterparty` WHERE `name` = '" + comboBox1.Text.ToString() + "'ORDER BY `price` DESC;";
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader2;
            ForPrice[] forPrice = new ForPrice[100];
            for (int i = 0; i < forPrice.Length; i++)
            {
                forPrice[i] = new ForPrice();
            }
            int itter = 0;
            bool g = true;
            try
            {
                db.openConnection();
                myReader2 = cmdDataBase.ExecuteReader();

                while (myReader2.Read())
                {
                    string objName = myReader2.GetString("objectName");
                    string objPrice = myReader2.GetString("price");
                    if (objName.Equals("") || objName.Equals(" "))
                    {
                        continue;
                    }
                    for (int i = 0; i < itter; i++)
                    {
                        //MessageBox.Show("nen");
                        g = true;
                        if (objName.Equals(forPrice[i].objectt))
                        {
                            g = false;
                            break;
                        }
                    }
                    if (g)
                    {
                        forPrice[itter].counterparty = myReader2.GetString("name");
                        forPrice[itter].objectt = objName;
                        forPrice[itter].price = objPrice;
                        itter++;
                        //MessageBox.Show(forPrice[k].price);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            for (int i = 0; i < itter; i++)
            {
                //MessageBox.Show(forPrice[i].price);
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = i + 1;
                dataGridView1[1, i].Value = forPrice[i].counterparty;
                dataGridView1[2, i].Value = forPrice[i].objectt;
                dataGridView1[3, i].Value = forPrice[i].price;
            }
            db.closeConnection();

            textBox1.Text = "";
        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("UPDATE `counterparty` SET `price` = '" + textBox1.Text + "' WHERE `price` = '" + val + "'", db.getConnection());

            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            dataGridView1.Rows.Clear();
            string Query = "SELECT * FROM `counterparty` WHERE `name` = '" + comboBox1.Text.ToString() + "'ORDER BY `price` DESC;";
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader2;
            int k = 0;
            ForPrice[] forPrice = new ForPrice[100];
            for (int i = 0; i < forPrice.Length; i++)
            {
                forPrice[i] = new ForPrice();
            }
            int itter = 0;
            bool g = true;
            try
            {
                db.openConnection();
                myReader2 = cmdDataBase.ExecuteReader();

                while (myReader2.Read())
                {
                    string objName = myReader2.GetString("objectName");
                    string objPrice = myReader2.GetString("price");
                    if (objName.Equals("") || objName.Equals(" "))
                    {
                        continue;
                    }
                    for (int i = 0; i < itter; i++)
                    {
                        //MessageBox.Show("nen");
                        g = true;
                        if (objName.Equals(forPrice[i].objectt))
                        {
                            g = false;
                            break;
                        }
                    }
                    if (g)
                    {
                        forPrice[itter].counterparty = myReader2.GetString("name");
                        forPrice[itter].objectt = objName;
                        forPrice[itter].price = objPrice;
                        itter++;
                        //MessageBox.Show(forPrice[k].price);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            for (int i = 0; i < itter; i++)
            {
                //MessageBox.Show(forPrice[i].price);
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = i + 1;
                dataGridView1[1, i].Value = forPrice[i].counterparty;
                dataGridView1[2, i].Value = forPrice[i].objectt;
                dataGridView1[3, i].Value = forPrice[i].price;
            }

            textBox1.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = dataGridView1.CurrentRow.Index;
            val = dataGridView1[3, idx].Value.ToString();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("DELETE FROM `counterparty` WHERE `price` = '" + val + "'", db.getConnection());

            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            dataGridView1.Rows.Clear();
            string Query = "SELECT * FROM `counterparty` WHERE `name` = '" + comboBox1.Text.ToString() + "'ORDER BY `price` DESC;";
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader2;
            int k = 0;
            ForPrice[] forPrice = new ForPrice[100];
            for (int i = 0; i < forPrice.Length; i++)
            {
                forPrice[i] = new ForPrice();
            }
            int itter = 0;
            bool g = true;
            try
            {
                db.openConnection();
                myReader2 = cmdDataBase.ExecuteReader();

                while (myReader2.Read())
                {
                    string objName = myReader2.GetString("objectName");
                    string objPrice = myReader2.GetString("price");
                    if (objName.Equals("") || objName.Equals(" "))
                    {
                        continue;
                    }
                    for (int i = 0; i < itter; i++)
                    {
                        //MessageBox.Show("nen");
                        g = true;
                        if (objName.Equals(forPrice[i].objectt))
                        {
                            g = false;
                            break;
                        }
                    }
                    if (g)
                    {
                        forPrice[itter].counterparty = myReader2.GetString("name");
                        forPrice[itter].objectt = objName;
                        forPrice[itter].price = objPrice;
                        itter++;
                        //MessageBox.Show(forPrice[k].price);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            for (int i = 0; i < itter; i++)
            {
                //MessageBox.Show(forPrice[i].price);
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = i + 1;
                dataGridView1[1, i].Value = forPrice[i].counterparty;
                dataGridView1[2, i].Value = forPrice[i].objectt;
                dataGridView1[3, i].Value = forPrice[i].price;
            }
        }
    }
}

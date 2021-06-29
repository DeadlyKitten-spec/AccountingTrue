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
    public partial class ObjectCounterparty : Form
    {
        public ObjectCounterparty()
        {
            InitializeComponent();

            int ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            int ScreenHeight = Screen.PrimaryScreen.Bounds.Height;

            this.Location = new Point((ScreenWidth / 2) - (this.Width / 2), (ScreenHeight / 2) - (this.Height / 2));

            FillCombo1();

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            label4.Text = "Расценок \nпокупателю";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            this.ControlBox = false;
        }

        public string val = "";

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        void FillCombo1()
        {
            string Query = "SELECT * FROM `counterparty` ORDER BY `name` ASC";
            DB db = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            string[] names = new string[1000];
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
            dataGridView1.Rows.Clear();
            string Query = "SELECT * FROM `counterparty` WHERE `name` = '" + comboBox1.Text.ToString() + "';";
            DB db = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            ForPrice[] frprice = new ForPrice[1000];
            for(int i = 0; i < 1000; i++)
            {
                frprice[i] = new ForPrice();
                frprice[i].counterparty = "-1";
                frprice[i].objectt = "-1";
                frprice[i].price = "-1";
                frprice[i].pricebuy = "-1";
                frprice[i].pricebuycount = "-1";
                frprice[i].pricecount = "-1";
            }
            int k = 0;
            try
            {
                db.openConnection();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    /*int priceCount = myReader.GetInt32("priceCount");
                    string objName = myReader.GetString("objectName");
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
                    }*/
                    string name = myReader.GetString("name");
                    string objName = myReader.GetString("objectName");
                    string priceCount = myReader.GetString("priceCount");
                    string priceCountBuy = myReader.GetString("priceBuyerCount");
                    int itter = 0;
                    //MessageBox.Show("обнуление");
                    if (objName.Equals("пусто"))
                    {
                        //MessageBox.Show("проверкапустоты");
                        continue;
                    }
                    for (int i = 0; i < 1000; i++)
                    {
                        //MessageBox.Show("цикл");
                        if (frprice[i].counterparty.Equals("-1"))
                        {
                            itter = i;
                            break;
                        }
                    }
                    frprice[itter].counterparty = name;
                    frprice[itter].objectt = objName;
                    frprice[itter].pricebuycount = priceCountBuy;
                    frprice[itter].pricecount = priceCount;
                    frprice[itter].price = myReader.GetString("price" + priceCount);
                    frprice[itter].pricebuy = myReader.GetString("priceBuyer" + priceCountBuy);
                    /*if (f == true)
                    {
                        names[k] = objName;
                        dataGridView1.Rows.Add();
                        dataGridView1[0, k].Value = k + 1;
                        dataGridView1[1, k].Value = myReader.GetString("name");
                        dataGridView1[2, k].Value = objName;
                        string pricec = "price" + priceCount;
                        dataGridView1[3, k].Value = myReader.GetString(pricec);
                        k++;
                    }*/
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            for (int i = 0; i < 1000; i++)
            {
                if (!frprice[i].counterparty.Equals("-1"))
                {
                    //MessageBox.Show("таблица");
                    dataGridView1.Rows.Add();
                    dataGridView1[0, k].Value = k + 1;
                    dataGridView1[1, k].Value = frprice[i].counterparty;
                    dataGridView1[2, k].Value = frprice[i].objectt;
                    dataGridView1[3, k].Value = frprice[i].price;
                    dataGridView1[4, k].Value = frprice[i].pricebuy;
                    k++;
                }
                else
                {
                    break;
                }
            }
        }


        


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            MySqlCommand command = new MySqlCommand("INSERT INTO `counterparty` (`name`, `objectName`, `status`, `price1`, `priceCount`, `priceBuyer1`, `priceBuyerCount`) VALUES (@name, @object, @status, @price, @priceCount, @priceBuyer, @priceBuyerCount)", db.getConnection());

            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = comboBox1.Text;
            command.Parameters.Add("@object", MySqlDbType.VarChar).Value = textBox2.Text;
            string num = "0";
            if (!textBox1.Text.Equals(""))
            {
                num = textBox1.Text;
                for (int j = 0; j < num.Length; j++)
                {
                    if (num[j] == '.')
                    {
                        string[] ans = num.Split('.');
                        num = ans[0] + "," + ans[1];
                        break;
                    }
                }
            }
            string numbuy = "0";
            if (!textBox3.Text.Equals(""))
            {
                numbuy = textBox3.Text;
                for (int j = 0; j < numbuy.Length; j++)
                {
                    if (numbuy[j] == '.')
                    {
                        string[] ans = numbuy.Split('.');
                        numbuy = ans[0] + "," + ans[1];
                        break;
                    }
                }
            }
            command.Parameters.Add("@status", MySqlDbType.VarChar).Value = "пусто";
            command.Parameters.Add("@price", MySqlDbType.VarChar).Value = num;
            command.Parameters.Add("@priceCount", MySqlDbType.Int32).Value = 1;
            command.Parameters.Add("@priceBuyer", MySqlDbType.VarChar).Value = numbuy;
            command.Parameters.Add("@priceBuyerCount", MySqlDbType.Int32).Value = 1;
            db.openConnection();
            command.ExecuteNonQuery();
            db.closeConnection();
            dataGridView1.Rows.Clear();
            string Query = "SELECT * FROM `counterparty` WHERE `name` = '" + comboBox1.Text.ToString() + "' ORDER BY `name` ASC;";
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            int k = 0;
            ForPrice[] frprice = new ForPrice[1000];
            for (int i = 0; i < 1000; i++)
            {
                frprice[i] = new ForPrice();
                frprice[i].counterparty = "-1";
                frprice[i].objectt = "-1";
                frprice[i].price = "-1";
                frprice[i].pricebuy = "-1";
                frprice[i].pricebuycount = "-1";
                frprice[i].pricecount = "-1";
            }
            try
            {
                db.openConnection();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    /*int priceCount = myReader.GetInt32("priceCount");
                    string objName = myReader.GetString("objectName");
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
                    }*/
                    string name = myReader.GetString("name");
                    string objName = myReader.GetString("objectName");
                    string priceCount = myReader.GetString("priceCount");
                    string priceCountBuy = myReader.GetString("priceBuyerCount");
                    int itter = 0;
                    //MessageBox.Show("обнуление");
                    if (objName.Equals("пусто"))
                    {
                        //MessageBox.Show("проверкапустоты");
                        continue;
                    }
                    for (int i = 0; i < 1000; i++)
                    {
                        //MessageBox.Show("цикл");
                        if (frprice[i].counterparty.Equals("-1"))
                        {
                            itter = i;
                            break;
                        }
                    }
                    frprice[itter].counterparty = name;
                    frprice[itter].objectt = objName;
                    frprice[itter].pricebuycount = priceCountBuy;
                    frprice[itter].pricecount = priceCount;
                    frprice[itter].price = myReader.GetString("price" + priceCount);
                    frprice[itter].pricebuy = myReader.GetString("priceBuyer" + priceCountBuy);
                    /*if (f == true)
                    {
                        names[k] = objName;
                        dataGridView1.Rows.Add();
                        dataGridView1[0, k].Value = k + 1;
                        dataGridView1[1, k].Value = myReader.GetString("name");
                        dataGridView1[2, k].Value = objName;
                        string pricec = "price" + priceCount;
                        dataGridView1[3, k].Value = myReader.GetString(pricec);
                        k++;
                    }*/
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            for (int i = 0; i < 1000; i++)
            {
                if (!frprice[i].counterparty.Equals("-1"))
                {
                    //MessageBox.Show("таблица");
                    dataGridView1.Rows.Add();
                    dataGridView1[0, k].Value = k + 1;
                    dataGridView1[1, k].Value = frprice[i].counterparty;
                    dataGridView1[2, k].Value = frprice[i].objectt;
                    dataGridView1[3, k].Value = frprice[i].price;
                    dataGridView1[4, k].Value = frprice[i].pricebuy;
                    k++;
                }
                else
                {
                    break;
                }
            }

            textBox2.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            string Query2 = "SELECT * FROM `counterparty` WHERE `name` = '" + comboBox1.Text.ToString() + "' ORDER BY `name` ASC;";
            MySqlCommand cmdDataBase2 = new MySqlCommand(Query2, db.getConnection());
            MySqlDataReader myReader2;
            db.openConnection();
            myReader2 = cmdDataBase2.ExecuteReader();
            myReader2.Read();
            int priceCountt = myReader2.GetInt32("priceCount");
            int priceBuyerCount = myReader2.GetInt32("priceBuyerCount");
            string pricec = "price" + priceCountt;
            string pricecbuy = "priceBuyer" + priceCountt;
            bool d = false;
            bool dbuy = false;
            if (!myReader2.GetString(pricec).Equals("0"))
            {
                priceCountt++;
                pricec = "price" + priceCountt;
                d = true;
            }
            if (!myReader2.GetString(pricecbuy).Equals("0"))
            {
                priceBuyerCount++;
                pricecbuy = "priceBuyer" + priceBuyerCount;
                dbuy = true;
            }
            string num = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            if (!textBox1.Text.Equals(""))
            {
                num = textBox1.Text;
                for (int j = 0; j < num.Length; j++)
                {
                    if (num[j] == '.')
                    {
                        string[] ans = num.Split('.');
                        num = ans[0] + "," + ans[1];
                        break;
                    }
                }
            }
            string numbuy = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
            if (!textBox3.Text.Equals(""))
            {
                numbuy = textBox3.Text;
                for (int j = 0; j < numbuy.Length; j++)
                {
                    if (numbuy[j] == '.')
                    {
                        string[] ans = numbuy.Split('.');
                        numbuy = ans[0] + "," + ans[1];
                        break;
                    }
                }
            }
            string objectName = "";
            if (textBox2.Text.Equals(""))
            {
                objectName = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            }
            else
            {
                objectName = textBox2.Text;
            }
            db.closeConnection();
            myReader2.Close();
            MySqlCommand command = new MySqlCommand();
            /*if (d)
            {
                if (!textBox1.Text.Equals("") && !textBox2.Text.Equals(""))
                {
                    command = new MySqlCommand("UPDATE `counterparty` SET `objectName` = '" + textBox2.Text + "' , `" + pricec + "` = '" + num + "' , `priceCount` = '" + priceCount + "' WHERE `objectName` = '" + val + "'", db.getConnection());
                }
                else
                {
                    if (!textBox2.Text.Equals(""))
                    {
                        command = new MySqlCommand("UPDATE `counterparty` SET `objectName` = '" + textBox2.Text + "' , `priceCount` = '" + priceCount + "' WHERE `objectName` = '" + val + "'", db.getConnection());
                    }
                    else
                    {
                        if (!textBox1.Text.Equals(""))
                        {
                            command = new MySqlCommand("UPDATE `counterparty` SET `" + pricec + "` = '" + num + "' , `priceCount` = '" + priceCount + "' WHERE `objectName` = '" + val + "'", db.getConnection());
                        }
                    }
                }
            }
            else
            {
                if (!textBox1.Text.Equals("") && !textBox2.Text.Equals(""))
                {
                    command = new MySqlCommand("UPDATE `counterparty` SET `objectName` = '" + textBox2.Text + "' , `" + pricec + "` = '" + num + "' WHERE `objectName` = '" + val + "'", db.getConnection());
                }
                else
                {
                    if (!textBox2.Text.Equals(""))
                    {
                        command = new MySqlCommand("UPDATE `counterparty` SET `objectName` = '" + textBox2.Text + "' WHERE `objectName` = '" + val + "'", db.getConnection());
                    }
                    else
                    {
                        if (!textBox1.Text.Equals(""))
                        {
                            command = new MySqlCommand("UPDATE `counterparty` SET `" + pricec + "` = '" + num + "' WHERE `objectName` = '" + val + "'", db.getConnection());
                        }
                    }
                }
            }*/
            command = new MySqlCommand("UPDATE `counterparty` SET `objectName` = '" + objectName + "' , `" + pricec + "` = '" + num + "', `" + pricecbuy + "` = '" + numbuy + "' WHERE `objectName` = '" + val + "'", db.getConnection());
            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            MySqlCommand command2 = new MySqlCommand("UPDATE `request` SET `object` = '" + objectName + "' WHERE `object` = '" + val + "'", db.getConnection());
            db.openConnection();

            command2.ExecuteNonQuery();

            db.closeConnection();

            command2 = new MySqlCommand("UPDATE `request` SET `objectArrive` = '" + objectName + "' WHERE `objectArrive` = '" + val + "'", db.getConnection());
            db.openConnection();

            command2.ExecuteNonQuery();

            db.closeConnection();

            command2 = new MySqlCommand("UPDATE `request` SET `objectSend` = '" + objectName + "' WHERE `objectSend` = '" + val + "'", db.getConnection());
            db.openConnection();

            command2.ExecuteNonQuery();

            db.closeConnection();

            textBox2.Text = "";
            dataGridView1.Rows.Clear();
            string Query = "SELECT * FROM `counterparty` WHERE `name` = '" + comboBox1.Text.ToString() + "' ORDER BY `name` ASC;";
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            int k = 0;
            ForPrice[] frprice = new ForPrice[1000];
            for (int i = 0; i < 1000; i++)
            {
                frprice[i] = new ForPrice();
                frprice[i].counterparty = "-1";
                frprice[i].objectt = "-1";
                frprice[i].price = "-1";
                frprice[i].pricebuy = "-1";
                frprice[i].pricebuycount = "-1";
                frprice[i].pricecount = "-1";
            }
            try
            {
                db.openConnection();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    /*int priceCount = myReader.GetInt32("priceCount");
                    string objName = myReader.GetString("objectName");
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
                    }*/
                    string name = myReader.GetString("name");
                    string objName = myReader.GetString("objectName");
                    string priceCount = myReader.GetString("priceCount");
                    string priceCountBuy = myReader.GetString("priceBuyerCount");
                    int itter = 0;
                    //MessageBox.Show("обнуление");
                    if (objName.Equals("пусто"))
                    {
                        //MessageBox.Show("проверкапустоты");
                        continue;
                    }
                    for (int i = 0; i < 1000; i++)
                    {
                        //MessageBox.Show("цикл");
                        if (frprice[i].counterparty.Equals("-1"))
                        {
                            itter = i;
                            break;
                        }
                    }
                    frprice[itter].counterparty = name;
                    frprice[itter].objectt = objName;
                    frprice[itter].pricebuycount = priceCountBuy;
                    frprice[itter].pricecount = priceCount;
                    frprice[itter].price = myReader.GetString("price" + priceCount);
                    frprice[itter].pricebuy = myReader.GetString("priceBuyer" + priceCountBuy);
                    /*if (f == true)
                    {
                        names[k] = objName;
                        dataGridView1.Rows.Add();
                        dataGridView1[0, k].Value = k + 1;
                        dataGridView1[1, k].Value = myReader.GetString("name");
                        dataGridView1[2, k].Value = objName;
                        string pricec = "price" + priceCount;
                        dataGridView1[3, k].Value = myReader.GetString(pricec);
                        k++;
                    }*/
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            for (int i = 0; i < 1000; i++)
            {
                if (!frprice[i].counterparty.Equals("-1"))
                {
                    //MessageBox.Show("таблица");
                    dataGridView1.Rows.Add();
                    dataGridView1[0, k].Value = k + 1;
                    dataGridView1[1, k].Value = frprice[i].counterparty;
                    dataGridView1[2, k].Value = frprice[i].objectt;
                    dataGridView1[3, k].Value = frprice[i].price;
                    dataGridView1[4, k].Value = frprice[i].pricebuy;
                    k++;
                }
                else
                {
                    break;
                }
            }


            db.closeConnection();

            textBox2.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = dataGridView1.CurrentRow.Index;
            val = dataGridView1[2, idx].Value.ToString();
            /*textBox1.Text = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            textBox2.Text = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            textBox3.Text = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();*/
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("DELETE FROM `counterparty` WHERE `objectName` = '" + val + "'", db.getConnection());

            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            textBox2.Text = "";
            dataGridView1.Rows.Clear();
            string Query = "SELECT * FROM `counterparty` WHERE `name` = '" + comboBox1.Text.ToString() + "' ORDER BY `name` ASC;";
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            int k = 0;
            ForPrice[] frprice = new ForPrice[1000];
            for (int i = 0; i < 1000; i++)
            {
                frprice[i] = new ForPrice();
                frprice[i].counterparty = "-1";
                frprice[i].objectt = "-1";
                frprice[i].price = "-1";
                frprice[i].pricebuy = "-1";
                frprice[i].pricebuycount = "-1";
                frprice[i].pricecount = "-1";
            }
            try
            {
                db.openConnection();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    /*int priceCount = myReader.GetInt32("priceCount");
                    string objName = myReader.GetString("objectName");
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
                    }*/
                    string name = myReader.GetString("name");
                    string objName = myReader.GetString("objectName");
                    string priceCount = myReader.GetString("priceCount");
                    string priceCountBuy = myReader.GetString("priceBuyerCount");
                    int itter = 0;
                    //MessageBox.Show("обнуление");
                    if (objName.Equals("пусто"))
                    {
                        //MessageBox.Show("проверкапустоты");
                        continue;
                    }
                    for (int i = 0; i < 1000; i++)
                    {
                        //MessageBox.Show("цикл");
                        if (frprice[i].counterparty.Equals("-1"))
                        {
                            itter = i;
                            break;
                        }
                    }
                    frprice[itter].counterparty = name;
                    frprice[itter].objectt = objName;
                    frprice[itter].pricebuycount = priceCountBuy;
                    frprice[itter].pricecount = priceCount;
                    frprice[itter].price = myReader.GetString("price" + priceCount);
                    frprice[itter].pricebuy = myReader.GetString("priceBuyer" + priceCountBuy);
                    /*if (f == true)
                    {
                        names[k] = objName;
                        dataGridView1.Rows.Add();
                        dataGridView1[0, k].Value = k + 1;
                        dataGridView1[1, k].Value = myReader.GetString("name");
                        dataGridView1[2, k].Value = objName;
                        string pricec = "price" + priceCount;
                        dataGridView1[3, k].Value = myReader.GetString(pricec);
                        k++;
                    }*/
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            for (int i = 0; i < 1000; i++)
            {
                if (!frprice[i].counterparty.Equals("-1"))
                {
                    //MessageBox.Show("таблица");
                    dataGridView1.Rows.Add();
                    dataGridView1[0, k].Value = k + 1;
                    dataGridView1[1, k].Value = frprice[i].counterparty;
                    dataGridView1[2, k].Value = frprice[i].objectt;
                    dataGridView1[3, k].Value = frprice[i].price;
                    dataGridView1[4, k].Value = frprice[i].pricebuy;
                    k++;
                }
                else
                {
                    break;
                }
            }


            db.closeConnection();
        }
    }
}

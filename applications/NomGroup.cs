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
    public partial class NomGroup : Form
    {
        public NomGroup()
        {
            InitializeComponent();
            label2.Text = "Вид \nноменклатуры";
            //FillDGV();
            comboBox1.Text = "Груз";

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public string val = "";
        void FillDGV()
        {
            dataGridView1.Rows.Clear();
            if (comboBox1.Text.Equals("Груз"))
            {
                string Query = "SELECT * FROM `namecargo` ORDER BY `group` ASC";
                DB db = new DB();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
                MySqlDataReader myReader;
                CounterObject[] counter = new CounterObject[1000];
                for (int i = 0; i < 1000; i++)
                {
                    counter[i] = new CounterObject();
                    counter[i].name = "-1";
                    //counter[i].status = "-1";
                }
                int k = 0;
                try
                {
                    db.openConnection();
                    myReader = cmdDataBase.ExecuteReader();

                    int j = 0;
                    while (myReader.Read())
                    {
                        string Name = myReader.GetString("group");
                        //string objName = myReader.GetString("status");
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
                                /*if (!counter[i].status.Equals(objName))
                                {
                                    if (!objName.Equals("пусто"))
                                    {
                                        counter[i].status = objName;
                                    }
                                }*/
                                f = false;
                            }
                        }
                        if (f == true)
                        {
                            counter[itter].name = Name;
                            //counter[itter].status = objName;
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
                        //dataGridView1[2, k].Value = counter[i].status;
                        k++;
                    }
                    else
                    {
                        break;
                    }
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            else
            {
                comboBox1.Text = "Товар";
                string Query = "SELECT * FROM `product` ORDER BY `group` ASC";
                DB db = new DB();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
                MySqlDataReader myReader;
                CounterObject[] counter = new CounterObject[1000];
                for (int i = 0; i < 1000; i++)
                {
                    counter[i] = new CounterObject();
                    counter[i].name = "-1";
                    //counter[i].status = "-1";
                }
                int k = 0;
                try
                {
                    db.openConnection();
                    myReader = cmdDataBase.ExecuteReader();

                    int j = 0;
                    while (myReader.Read())
                    {
                        string Name = myReader.GetString("group");
                        //string objName = myReader.GetString("status");
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
                                /*if (!counter[i].status.Equals(objName))
                                {
                                    if (!objName.Equals("пусто"))
                                    {
                                        counter[i].status = objName;
                                    }
                                }*/
                                f = false;
                            }
                        }
                        if (f == true)
                        {
                            counter[itter].name = Name;
                            //counter[itter].status = objName;
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
                        //dataGridView1[2, k].Value = counter[i].status;
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals("Груз"))
            {
                DB db = new DB();
                MySqlCommand command = new MySqlCommand("INSERT INTO `namecargo` (`group`, `name`) VALUES (@group, @name)", db.getConnection());
                command.Parameters.Add("@group", MySqlDbType.VarChar).Value = textBox2.Text;
                command.Parameters.Add("@name", MySqlDbType.VarChar).Value = "пусто";
                db.openConnection();

                command.ExecuteNonQuery();

                db.closeConnection();

                textBox2.Text = "";
                dataGridView1.Rows.Clear();
                command = new MySqlCommand("SELECT * FROM `namecargo` ORDER BY `group` ASC", db.getConnection());
                MySqlDataReader myReader;
                CounterObject[] counter = new CounterObject[1000];
                for (int i = 0; i < 1000; i++)
                {
                    counter[i] = new CounterObject();
                    counter[i].name = "-1";
                    //counter[i].status = "-1";
                }
                int k = 0;
                try
                {
                    db.openConnection();
                    myReader = command.ExecuteReader();

                    int j = 0;
                    while (myReader.Read())
                    {
                        string Name = myReader.GetString("group");
                        //string objName = myReader.GetString("status");
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
                                /*if (!counter[i].status.Equals(objName))
                                {
                                    if (!objName.Equals("пусто"))
                                    {
                                        counter[i].status = objName;
                                    }
                                }*/
                                f = false;
                            }
                        }
                        if (f == true)
                        {
                            counter[itter].name = Name;
                            //counter[itter].status = objName;
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
                        //dataGridView1[2, k].Value = counter[i].status;
                        k++;
                    }
                    else
                    {
                        break;
                    }
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            else
            {
                DB db = new DB();
                MySqlCommand command = new MySqlCommand("INSERT INTO `product` (`group`, `name`) VALUES (@group, @name)", db.getConnection());
                command.Parameters.Add("@group", MySqlDbType.VarChar).Value = textBox2.Text;
                command.Parameters.Add("@name", MySqlDbType.VarChar).Value = "пусто";
                db.openConnection();

                command.ExecuteNonQuery();

                db.closeConnection();

                textBox2.Text = "";
                dataGridView1.Rows.Clear();
                command = new MySqlCommand("SELECT * FROM `product` ORDER BY `group` ASC", db.getConnection());
                MySqlDataReader myReader;
                CounterObject[] counter = new CounterObject[1000];
                for (int i = 0; i < 1000; i++)
                {
                    counter[i] = new CounterObject();
                    counter[i].name = "-1";
                    //counter[i].status = "-1";
                }
                int k = 0;
                try
                {
                    db.openConnection();
                    myReader = command.ExecuteReader();

                    int j = 0;
                    while (myReader.Read())
                    {
                        string Name = myReader.GetString("group");
                        //string objName = myReader.GetString("status");
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
                                /*if (!counter[i].status.Equals(objName))
                                {
                                    if (!objName.Equals("пусто"))
                                    {
                                        counter[i].status = objName;
                                    }
                                }*/
                                f = false;
                            }
                        }
                        if (f == true)
                        {
                            counter[itter].name = Name;
                            //counter[itter].status = objName;
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
                        //dataGridView1[2, k].Value = counter[i].status;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals("Груз"))
            {
                dataGridView1.Rows.Clear();
                string Query = "SELECT * FROM `namecargo` ORDER BY `group` ASC";
                DB db = new DB();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
                MySqlDataReader myReader;
                CounterObject[] counter = new CounterObject[1000];
                for (int i = 0; i < 1000; i++)
                {
                    counter[i] = new CounterObject();
                    counter[i].name = "-1";
                    //counter[i].status = "-1";
                }
                int k = 0;
                try
                {
                    db.openConnection();
                    myReader = cmdDataBase.ExecuteReader();

                    int j = 0;
                    while (myReader.Read())
                    {
                        string Name = myReader.GetString("group");
                        //string objName = myReader.GetString("status");
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
                                /*if (!counter[i].status.Equals(objName))
                                {
                                    if (!objName.Equals("пусто"))
                                    {
                                        counter[i].status = objName;
                                    }
                                }*/
                                f = false;
                            }
                        }
                        if (f == true)
                        {
                            counter[itter].name = Name;
                            //counter[itter].status = objName;
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
                        //dataGridView1[2, k].Value = counter[i].status;
                        k++;
                    }
                    else
                    {
                        break;
                    }
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            else
            {
                dataGridView1.Rows.Clear();
                string Query = "SELECT * FROM `product` ORDER BY `group` ASC";
                DB db = new DB();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
                MySqlDataReader myReader;
                CounterObject[] counter = new CounterObject[1000];
                for (int i = 0; i < 1000; i++)
                {
                    counter[i] = new CounterObject();
                    counter[i].name = "-1";
                    //counter[i].status = "-1";
                }
                int k = 0;
                try
                {
                    db.openConnection();
                    myReader = cmdDataBase.ExecuteReader();

                    int j = 0;
                    while (myReader.Read())
                    {
                        string Name = myReader.GetString("group");
                        //string objName = myReader.GetString("status");
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
                                /*if (!counter[i].status.Equals(objName))
                                {
                                    if (!objName.Equals("пусто"))
                                    {
                                        counter[i].status = objName;
                                    }
                                }*/
                                f = false;
                            }
                        }
                        if (f == true)
                        {
                            counter[itter].name = Name;
                            //counter[itter].status = objName;
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
                        //dataGridView1[2, k].Value = counter[i].status;
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals("Груз"))
            {
                DB db = new DB();
                string Query = "UPDATE `namecargo` SET `group` = '" + textBox2.Text + "' WHERE `group` = '" + val + "'";
                MySqlCommand command = new MySqlCommand(Query, db.getConnection());

                db.openConnection();

                command.ExecuteNonQuery();

                db.closeConnection();
                dataGridView1.Rows.Clear();
                Query = "SELECT * FROM `namecargo` ORDER BY `group` ASC";
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
                MySqlDataReader myReader;
                CounterObject[] counter = new CounterObject[1000];
                for (int i = 0; i < 1000; i++)
                {
                    counter[i] = new CounterObject();
                    counter[i].name = "-1";
                    //counter[i].status = "-1";
                }
                int k = 0;
                try
                {
                    db.openConnection();
                    myReader = cmdDataBase.ExecuteReader();

                    int j = 0;
                    while (myReader.Read())
                    {
                        string Name = myReader.GetString("group");
                        //string objName = myReader.GetString("status");
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
                                /*if (!counter[i].status.Equals(objName))
                                {
                                    if (!objName.Equals("пусто"))
                                    {
                                        counter[i].status = objName;
                                    }
                                }*/
                                f = false;
                            }
                        }
                        if (f == true)
                        {
                            counter[itter].name = Name;
                            //counter[itter].status = objName;
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
                        //dataGridView1[2, k].Value = counter[i].status;
                        k++;
                    }
                    else
                    {
                        break;
                    }
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            else
            {
                DB db = new DB();
                string Query = "UPDATE `product` SET `group` = '" + textBox2.Text + "' WHERE `group` = '" + val + "'";
                MySqlCommand command = new MySqlCommand(Query, db.getConnection());

                db.openConnection();

                command.ExecuteNonQuery();

                db.closeConnection();
                dataGridView1.Rows.Clear();
                Query = "SELECT * FROM `product` ORDER BY `group` ASC";
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
                MySqlDataReader myReader;
                CounterObject[] counter = new CounterObject[1000];
                for (int i = 0; i < 1000; i++)
                {
                    counter[i] = new CounterObject();
                    counter[i].name = "-1";
                    //counter[i].status = "-1";
                }
                int k = 0;
                try
                {
                    db.openConnection();
                    myReader = cmdDataBase.ExecuteReader();

                    int j = 0;
                    while (myReader.Read())
                    {
                        string Name = myReader.GetString("group");
                        //string objName = myReader.GetString("status");
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
                                /*if (!counter[i].status.Equals(objName))
                                {
                                    if (!objName.Equals("пусто"))
                                    {
                                        counter[i].status = objName;
                                    }
                                }*/
                                f = false;
                            }
                        }
                        if (f == true)
                        {
                            counter[itter].name = Name;
                            //counter[itter].status = objName;
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
                        //dataGridView1[2, k].Value = counter[i].status;
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = dataGridView1.CurrentRow.Index;
            val = dataGridView1[1, idx].Value.ToString();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals("Груз"))
            {
                DB db = new DB();
                MySqlCommand command = new MySqlCommand("DELETE FROM `namecargo` WHERE `group` = '" + val + "'", db.getConnection());

                db.openConnection();

                command.ExecuteNonQuery();

                db.closeConnection();
                dataGridView1.Rows.Clear();
                string Query = "SELECT * FROM `namecargo` ORDER BY `group` ASC";
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
                MySqlDataReader myReader;
                CounterObject[] counter = new CounterObject[1000];
                for (int i = 0; i < 1000; i++)
                {
                    counter[i] = new CounterObject();
                    counter[i].name = "-1";
                    //counter[i].status = "-1";
                }
                int k = 0;
                try
                {
                    db.openConnection();
                    myReader = cmdDataBase.ExecuteReader();

                    int j = 0;
                    while (myReader.Read())
                    {
                        string Name = myReader.GetString("group");
                        //string objName = myReader.GetString("status");
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
                                /*if (!counter[i].status.Equals(objName))
                                {
                                    if (!objName.Equals("пусто"))
                                    {
                                        counter[i].status = objName;
                                    }
                                }*/
                                f = false;
                            }
                        }
                        if (f == true)
                        {
                            counter[itter].name = Name;
                            //counter[itter].status = objName;
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
                        //dataGridView1[2, k].Value = counter[i].status;
                        k++;
                    }
                    else
                    {
                        break;
                    }
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            else
            {
                DB db = new DB();
                MySqlCommand command = new MySqlCommand("DELETE FROM `product` WHERE `group` = '" + val + "'", db.getConnection());

                db.openConnection();

                command.ExecuteNonQuery();

                db.closeConnection();
                dataGridView1.Rows.Clear();
                string Query = "SELECT * FROM `product` ORDER BY `group` ASC";
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
                MySqlDataReader myReader;
                CounterObject[] counter = new CounterObject[1000];
                for (int i = 0; i < 1000; i++)
                {
                    counter[i] = new CounterObject();
                    counter[i].name = "-1";
                    //counter[i].status = "-1";
                }
                int k = 0;
                try
                {
                    db.openConnection();
                    myReader = cmdDataBase.ExecuteReader();

                    int j = 0;
                    while (myReader.Read())
                    {
                        string Name = myReader.GetString("group");
                        //string objName = myReader.GetString("status");
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
                                /*if (!counter[i].status.Equals(objName))
                                {
                                    if (!objName.Equals("пусто"))
                                    {
                                        counter[i].status = objName;
                                    }
                                }*/
                                f = false;
                            }
                        }
                        if (f == true)
                        {
                            counter[itter].name = Name;
                            //counter[itter].status = objName;
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
                        //dataGridView1[2, k].Value = counter[i].status;
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
}

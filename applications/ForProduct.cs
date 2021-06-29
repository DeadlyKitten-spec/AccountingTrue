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
    public partial class ForProduct : Form
    {
        Color FlatColor;
        public ForProduct()
        {
            InitializeComponent();

            button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = FlatColor;
            FillCombo6();
        }

        void FillCombo6()
        {
            string Query = "SELECT * FROM `product` " + /*WHERE status = 'Грузополучатель/грузоотправитель' OR status = 'Диспетчер'*/  "ORDER BY `name` ASC;";
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
                        comboBox6.Items.Add(objName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public int table = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            if (table == 0)
            {
                Vault vault = this.Owner as Vault;
                vault.dataGridView2.Rows.Add();
                vault.dataGridView2[0, vault.dataGridView2.Rows.Count - 2].Value = comboBox6.Text;
                vault.dataGridView2[1, vault.dataGridView2.Rows.Count - 2].Value = textBox1.Text;
                vault.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                this.Close();
            }
            else
            {   if (table == 1)
                {
                    Vault vault = this.Owner as Vault;
                    vault.dataGridView1.Rows.Add();
                    vault.dataGridView1[0, vault.dataGridView1.Rows.Count - 2].Value = comboBox6.Text;
                    vault.dataGridView1[1, vault.dataGridView1.Rows.Count - 2].Value = textBox1.Text;
                    vault.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.Close();
                }
                else
                {
                    Vault vault = this.Owner as Vault;
                    vault.dataGridView3.Rows.Add();
                    vault.dataGridView3[0, vault.dataGridView3.Rows.Count - 2].Value = comboBox6.Text;
                    vault.dataGridView3[1, vault.dataGridView3.Rows.Count - 2].Value = textBox1.Text;
                    vault.dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.Close();
                }
            }
        }
    }
}

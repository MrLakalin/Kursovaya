using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursovay
{
    public partial class dobPOS : Form
    {
        public dobPOS()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    Poset main = this.Owner as Poset;
                    if (main != null)
                    {
                        if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text))
                        {
                            MessageBox.Show("Пожалуйста, заполните все поля.");
                            return;
                        }

                        string dbPath = Path.Combine(Application.StartupPath, "db.accdb");
                        string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};Persist Security Info=False;";

                        using (OleDbConnection connection = new OleDbConnection(connectionString))
                        {
                            connection.Open();

                            OleDbCommand command = new OleDbCommand("INSERT INTO [Посетители] ([Имя], [Фамилия], [Возраст], [Рост], [Баланс]) VALUES (?, ?, ?, ?, ?)", connection);
                            command.Parameters.AddWithValue("@Имя", textBox1.Text);
                            command.Parameters.AddWithValue("@Фамилия", textBox2.Text);
                            command.Parameters.AddWithValue("@Возвраст", textBox3.Text);
                            command.Parameters.AddWithValue("@Рост", textBox4.Text);
                            command.Parameters.AddWithValue("@Баланс", textBox5.Text);
                            command.ExecuteNonQuery();
                        }

                        DataRow nRow = main.dbPOS.Tables[0].NewRow();
                        nRow[1] = textBox1.Text;
                        nRow[2] = textBox2.Text;
                        nRow[3] = textBox3.Text;
                        nRow[4] = textBox4.Text;
                        nRow[5] = textBox4.Text;

                        main.dbPOS.Tables[0].Rows.Add(nRow);
                        main.dbPOS.Tables[0].AcceptChanges();
                        main.dataGridView1.Refresh();

                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
                }
            }
        }

        private void dobPOS_Load(object sender, EventArgs e)
        {

        }
    }
}

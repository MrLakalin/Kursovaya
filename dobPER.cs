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
    public partial class dobPER : Form
    {
        public dobPER()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    Personal main = this.Owner as Personal;
                    if (main != null)
                    {
                        if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace (textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox2a.Text))
                        {
                            MessageBox.Show("Пожалуйста, заполните все поля.");
                            return;
                        }

                        string dbPath = Path.Combine(Application.StartupPath, "db.accdb");
                        string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};Persist Security Info=False;";

                        using (OleDbConnection connection = new OleDbConnection(connectionString))
                        {
                            connection.Open();

                            OleDbCommand command = new OleDbCommand("INSERT INTO [Персонал] ([Имя], [Фамилия], [Телефон], [Должность], [Зарплата]) VALUES (?, ?, ?, ?, ?)", connection);
                            command.Parameters.AddWithValue("@Имя", textBox1.Text);
                            command.Parameters.AddWithValue("@Фамилия", textBox2a.Text);
                            command.Parameters.AddWithValue("@Телефон", textBox2.Text);
                            command.Parameters.AddWithValue("@Должность", textBox3.Text);
                            command.Parameters.AddWithValue("@Зарплата", textBox4.Text);
                            command.ExecuteNonQuery();
                        }

                        DataRow nRow = main.dbPER.Tables[0].NewRow();
                        nRow[1] = textBox1.Text;
                        nRow[2] = textBox2a.Text;
                        nRow[3] = textBox2.Text;
                        nRow[4] = textBox3.Text;
                        nRow[5] = textBox4.Text;


                        main.dbPER.Tables[0].Rows.Add(nRow);
                        main.dbPER.Tables[0].AcceptChanges();
                        main.dataGridView1.Refresh();

                        textBox1.Text = "";
                        textBox2a.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
                }
            }
        }

        private void dobPER_Load(object sender, EventArgs e)
        {

        }
    }
}

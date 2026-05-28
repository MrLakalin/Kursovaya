using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursovay
{
    public partial class Form1 : Form
    {
        private const string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=db.accdb;";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string loginuser = textBox1.Text;
            string password = textBox2.Text;

            DataTable table = new DataTable();

            using (OleDbConnection connection = new OleDbConnection(ConnectionString))
            {
                string query = "SELECT * FROM [admin] WHERE [логин] = @ul AND [пароль] = @up";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ul", loginuser);
                    command.Parameters.AddWithValue("@up", password);

                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    adapter.Fill(table);
                }
            }

            if (table.Rows.Count > 0)
            {
                if (table.Rows.Count > 0)
                {
                    string userRole = table.Rows[0]["роль"].ToString().Trim();

                    switch (userRole.ToLower())
                    {
                        case "admin":
                            Osnova adminForm = new Osnova();
                            adminForm.Owner = this;
                            adminForm.Show();
                            this.Hide();
                            break;
                        case "user":
                            UserOsnova userForm = new UserOsnova();
                            userForm.Owner = this;
                            userForm.Show();
                            this.Hide();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка при вводе данных аккаунта.");
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Reg af = new Reg();
            af.Owner = this;
            this.Hide();
            af.Show();
        }
    }
}
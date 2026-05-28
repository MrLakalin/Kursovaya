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
    public partial class Reg : Form
    {
        private const string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=db.accdb;";
        public Reg()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string loginuser = textBox1.Text.Trim();      // Логин
            string password = textBox2.Text.Trim();       // Пароль
            string confirmPassword = textBox3.Text.Trim(); // Подтверждение пароля

            // Проверка на пустые поля
            if (string.IsNullOrEmpty(loginuser) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните логин и пароль!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка совпадения паролей
            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Clear();
                textBox3.Clear();
                textBox2.Focus();
                return;
            }

            // Проверка длины пароля (минимум 3 символа)
            if (password.Length < 3)
            {
                MessageBox.Show("Пароль должен содержать минимум 3 символа!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверяем, существует ли такой логин
            if (CheckUserExists(loginuser))
            {
                MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Clear();
                textBox1.Focus();
                return;
            }

            // Добавляем нового пользователя (роль автоматически = "user")
            if (RegisterUser(loginuser, password))
            {
                MessageBox.Show("Регистрация успешно завершена!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Очищаем поля
                ClearFields();

                // Автоматически входим в систему как обычный пользователь
                AutoLogin(loginuser);
            }
        }

        private bool CheckUserExists(string login)
        {
            using (OleDbConnection connection = new OleDbConnection(ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM [admin] WHERE [логин] = @login";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                    return count > 0;
                }
            }
        }

        private bool RegisterUser(string login, string password)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(ConnectionString))
                {
                    // Роль автоматически "user"
                    string query = "INSERT INTO [admin] ([логин], [пароль], [роль]) VALUES (@login, @password, 'user')";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@password", password);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        connection.Close();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка базы данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void AutoLogin(string login)
        {
            // После регистрации сразу открываем форму для обычного пользователя
            UserOsnova userForm = new UserOsnova();
            userForm.Owner = this;
            userForm.Show();
            this.Hide();
        }

        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox1.Focus();
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 af = new Form1();
            af.Owner = this;
            this.Hide();
            af.Show();
        }
    }
}
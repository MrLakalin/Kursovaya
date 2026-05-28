using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursovay
{
    public partial class Personal : Form
    {
        public Personal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Osnova af = new Osnova();
            af.Owner = this;
            af.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bilet af2 = new Bilet();
            af2.Owner = this;
            af2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Personal af3 = new Personal();
            af3.Owner = this;
            af3.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Poset af4 = new Poset();
            af4.Owner = this;
            af4.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
                    }
                }
            }
        }

        private void Personal_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbPER.Персонал". При необходимости она может быть перемещена или удалена.
            this.персоналTableAdapter.Fill(this.dbPER.Персонал);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.Персонал". При необходимости она может быть перемещена или удалена.

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.персоналTableAdapter.Update(this.dbPER.Персонал);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dobPER af5 = new dobPER();
            af5.Owner = this;
            af5.Show();
        }
    }
}

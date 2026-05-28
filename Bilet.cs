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
    public partial class Bilet : Form
    {
        public Bilet()
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

        private void Bilet_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbBIL.Билет". При необходимости она может быть перемещена или удалена.
            this.билетTableAdapter.Fill(this.dbBIL.Билет);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.Билет". При необходимости она может быть перемещена или удалена.

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.билетTableAdapter.Update(this.dbBIL.Билет);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dobBIL af5 = new dobBIL();
            af5.Owner = this;
            af5.Show();
        }
    }
}

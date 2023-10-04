using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace exam
{
    public partial class Form1 : Form
    {
        SimDAO simDAO = new SimDAO();

        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = simDAO.DocListSim();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int text = Convert.ToInt32(textBox1.Text);

                if (text.ToString().Length != 10)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                
            }

            List<Sim> sim = simDAO.TimKiem(textBox1.Text);

            if (sim.Count == 0)
                MessageBox.Show("Không tìm được");
            else
                simDAO.Delete(textBox1.Text.ToString());

            dataGridView1.DataSource = simDAO.DocListSim();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string text = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            textBox1.Text = text;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Sim> sim = simDAO.TimKiem(textBox1.Text);

            if (sim.Count == 0)
                MessageBox.Show("Không tìm được");
            else
                dataGridView1.DataSource = simDAO.TimKiem(textBox1.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            simDAO.ChuyenCSV();

            MessageBox.Show("Đã chuyển vào file CSV");
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                List<Sim> sim = simDAO.TimKiem(textBox1.Text);

                if (sim.Count == 0)
                    MessageBox.Show("Không tìm được");
                else
                    dataGridView1.DataSource = simDAO.TimKiem(textBox1.Text);
            }

            if (!char.IsNumber((char)e.KeyCode))
            {
                Console.Beep();
                e.SuppressKeyPress = true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LA_Shtokal
{
    public partial class Visible : Form
    {
        string code="";
        public Visible()
        {
            InitializeComponent();
        }

        private void Visible_Load(object sender, EventArgs e)
        {
            code = System.IO.File.ReadAllText(@"D:\kpi\3_kurs\ОРТ\program.txt"); 
            textBox1.Text = code;
        }

        private void Run()
        {
            Analiz test = new Analiz(code);
            if(test.error!=null)
            {
                MessageBox.Show("Помилка! Рядок " + test.error.row.ToString() + ". Колонка " + test.error.column.ToString()+"\n"+test.error.message);
                return;
            }
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            for (int i=0;i<test.rezultTable.Count;i++)
            {
                dataGridView1.Rows.Add(test.rezultTable[i].row, test.rezultTable[i].lexem, test.rezultTable[i].code, test.rezultTable[i].indexConst==0?"": test.rezultTable[i].indexConst.ToString());
            }

            for (int i = 0; i < test.identifiersTable.Count; i++)
            {
                dataGridView2.Rows.Add(test.identifiersTable[i].idn, test.identifiersTable[i].index);
            }

            for (int i = 0; i < test.constTable.Count; i++)
            {
                dataGridView3.Rows.Add(test.constTable[i].constant, test.constTable[i].index);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            code = textBox1.Text;
            Run();
        }
    }
}

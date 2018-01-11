using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void texboxEmpt_Valing(object sender, CancelEventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                textBox1.BackColor = Color.Red;
            }
            else
            {
                textBox1.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar<48 ||e.KeyChar>57)&&e.KeyChar!=8)
            {
                e.Handled = true;
            }
        }
    }
}

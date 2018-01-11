using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ThreadTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Thread th1;
        private int i;
        public delegate void myDel();
        public myDel delObj;
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            delObj = new myDel(showMessage);
            th1 = new Thread(new ThreadStart(incr));
            th1.IsBackground = true;
            th1.Start();
        }

        private void incr()
        {
            while (i < 100)//此次需要为一个循环，否则点一次才执行一次
            {
                i += 1;
                this.BeginInvoke(delObj);
                Thread.Sleep(1000);
            }
            if (i>=100)
            {
                i = 0;
            }

        }

        private void showMessage()
        {
            label1.Text = i.ToString(); 
        }

    }
}

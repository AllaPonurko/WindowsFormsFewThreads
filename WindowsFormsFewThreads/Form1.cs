using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsFewThreads
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        private void btnShow_Click(object sender, EventArgs e)
        {MyThreads my = new MyThreads();
            if(txtStart.Text.Length==0||txtEnd.Text.Length==0||txtCount.Text.Length==0)
            {
                MessageBox.Show("Нет входящих параметров");
            }
            if (txtStart.Text.Length != 0 & txtEnd.Text.Length != 0 & txtCount.Text.Length != 0)
            {
                MyThreads.start = Convert.ToInt32(txtStart.Text);
                MyThreads.end = Convert.ToInt32(txtEnd.Text);
                MyThreads.count = Convert.ToInt32(txtCount.Text);
                try
                { Mutex mutex = new Mutex();
                    mutex.WaitOne();
                    lstNumber.Items.Clear();
                    my.T_Random.Start();
                    Thread.Sleep(1000);
                    foreach(var item in MyThreads.vs)
                    {
                        lstNumber.Items.Add(item);
                    }
                    my.SaveList();
                    mutex.ReleaseMutex();
                    mutex.WaitOne();
                    my.T_Prime.Start();
                    Thread.Sleep(2000);
                    lstPrimeNumber.Items.Clear();
                    foreach(var item in MyThreads.prime)
                    {
                        lstPrimeNumber.Items.Add(item);
                    }
                    mutex.ReleaseMutex();
                    mutex.WaitOne();
                    my.T_Seven.Start();
                    Thread.Sleep(2000);
                    lstEndOnSeven.Items.Clear();
                    foreach(var item in MyThreads.seven)
                    {
                        lstEndOnSeven.Items.Add(item);
                    }
                    mutex.ReleaseMutex();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}

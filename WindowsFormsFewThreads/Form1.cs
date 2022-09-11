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
            vs = new List<int>();
            prime = new List<int>();
            seven = new List<int>();
        }
        static private int start;
        static private int end;
        static private int count;
        static private List<int> vs;
        static private List<int> prime;
        static private List<int> seven;
        static private void MethodSeven()
        {
            seven.Clear();
            foreach(var item in vs)
            {
                if (item % 10 == 7)
                    seven.Add(item);
            }
        }
        static private void MethodPrimeNumber()
        {
            prime.Clear();
            foreach(var item in vs)
            {
                bool b = true;
                for (int j = 2; j < item; j++)
                {
                    if (item % j == 0 & item % 1 == 0)
                    {
                        b = false;
                    }
                }
                if (b) prime.Add(item);
            }
        }
        static private void MethodRandomNum()
        {
            vs.Clear();
            Random random = new Random();
            for(int i=0;i<count;i++)
            {if (start < end)
                { 
                    vs.Add(random.Next(start, end)); 
                }
                else
                {
                    vs.Add(random.Next(end, start));
                }
            }

        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            if(txtStart.Text.Length==0||txtEnd.Text.Length==0||txtCount.Text.Length==0)
            {
                MessageBox.Show("Нет входящих параметров");
            }
            if (txtStart.Text.Length != 0 & txtEnd.Text.Length != 0 & txtCount.Text.Length != 0)
            {
                start =Convert.ToInt32(txtStart.Text);
                end = Convert.ToInt32(txtEnd.Text);
                count = Convert.ToInt32(txtCount.Text);
                try
                { Mutex mutex = new Mutex();
                    mutex.WaitOne();
                    lstNumber.Items.Clear();
                    ThreadStart Start = new ThreadStart(MethodRandomNum);
                    Thread thread = new Thread(Start);
                    thread.Start();
                    Thread.Sleep(1000);
                    foreach(var item in vs)
                    {
                        lstNumber.Items.Add(item);
                    }
                    mutex.ReleaseMutex();
                    mutex.WaitOne();
                    ThreadStart Start1 = new ThreadStart(MethodPrimeNumber);
                    Thread thread1 = new Thread(Start1);
                    thread1.Start();
                    Thread.Sleep(2000);
                    lstPrimeNumber.Items.Clear();
                    foreach(var item in prime)
                    {
                        lstPrimeNumber.Items.Add(item);
                    }
                    mutex.ReleaseMutex();
                    mutex.WaitOne();
                    ThreadStart Start2 = new ThreadStart(MethodSeven);
                    Thread thread2 = new Thread(Start2);
                    thread2.Start();
                    Thread.Sleep(2000);
                    lstEndOnSeven.Items.Clear();
                    foreach(var item in seven)
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

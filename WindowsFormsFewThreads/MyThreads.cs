using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WindowsFormsFewThreads
{
   public class MyThreads
    {
        public MyThreads()
        {
            vs = new List<int>();
            prime = new List<int>();
            seven= new List<int>();
            T_Random = new Thread(StartRandom);
            T_Prime = new Thread(StartPrime);
            T_Seven = new Thread(StartSeven);
        }
        public ThreadStart StartRandom = new ThreadStart(MethodRandomNum);
        public Thread T_Random;
        public ThreadStart StartPrime = new ThreadStart(MethodPrimeNumber);
        public Thread T_Prime;
        public ThreadStart StartSeven = new ThreadStart(MethodSeven);
        public Thread T_Seven;
        static public int start { get; set; }
        static public int end { get; set; }
        static public int count { get; set; }
        static public List<int> vs { get; set; }
        static public List<int> prime { get; set; }
        static public List<int> seven { get; set; }
        static private void MethodSeven()
        {
            seven.Clear();
            foreach (var item in vs)
            {
                if (item % 10 == 7)
                    seven.Add(item);
            }
        }
        static private void MethodPrimeNumber()
        {
            prime.Clear();
            if(GetList()!=null)
            foreach (var item in GetList())
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
            else
            {
                MessageBox.Show("Ошибка! Объект не имеет значений!");
            }
        }
        static private void MethodRandomNum()
        {
            vs.Clear();
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                if (start < end)
                {
                    vs.Add(random.Next(start, end));
                }
                else
                {
                    vs.Add(random.Next(end, start));
                }
            }

        }
        public void SaveList()
        {
            try
            {XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<int>));
            using (FileStream fs = new FileStream("listNumbers.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, vs);
                MessageBox.Show("Object has been serialized");
            }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        static public List<int> GetList()
        {
            try
            {
               List<int> _vs = new List<int>();  
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<int>));
            using (FileStream fs = new FileStream("listNumbers.xml", FileMode.Open))
            {
                _vs = (List<int>)xmlSerializer.Deserialize(fs);
                MessageBox.Show("Object has been deserialized");
            }
            return _vs;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

    }
}

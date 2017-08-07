using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraduateWork
{
    class WorkWithFile
    {

        /*
        private int count_digit(double n)
        {
            if (n < 1) return 1;
            int i = 1;
            while (n >= 1)
            {
                n = n / 10;
                i*=10;
            }
            return i;
        }

        private string ParseValue(string s)
        {
            double n;
            if (double.TryParse(s,
                System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo,
                out n)) return (n/ count_digit(n)).ToString();
            switch (s)
            {
                case "yes": return "0,1";
                case "no": return "0,2";
                case "normal": return "0,1";
                case "abnormal": return "0,2";
                case "present": return "0,1";
                case "notpresent": return "0,2";
                case "good": return "0,1";
                case "poor": return "0,2";
                case "ckd": return "0";
                case "notckd": return "1";
                case "?": return "0";
                default: return "0";

            }
        }

        public void ProcessData(string inPath, string outPath)
        {
            string filepath = "../../Data/cur.txt";
            File.Delete(outPath);
            var sourceStream = new System.IO.StreamReader(System.IO.File.Open(inPath, System.IO.FileMode.Open));
            var resultWriter = new System.IO.StreamWriter(System.IO.File.Open(filepath, System.IO.FileMode.OpenOrCreate));
            string s = "";

            while (!s.Contains("data"))
                s = sourceStream.ReadLine();

            StringBuilder sb = new StringBuilder();

            while ((s = sourceStream.ReadLine()) != null)
            {
                var arr = s.Split(',');
                foreach (var value in arr)
                {
                    sb.Append(ParseValue(value));
                    sb.Append(' ');
                }
                resultWriter.WriteLine(sb.ToString());
                sb = new StringBuilder();
            }

            sourceStream.Close();
            resultWriter.Close();
            
            var text = System.IO.File.ReadAllLines(filepath, Encoding.Default).Select(x => x.Trim());
            resultWriter = new System.IO.StreamWriter(System.IO.File.Open(outPath, System.IO.FileMode.OpenOrCreate));
            
            var ckd = text.Where(x => x.Last() == '0').ToArray();
            var notckd = text.Where(x => x.Last() == '1').ToArray();
            int k = 0;
            int count=0;
            for(int i=0; i <notckd.Length; i++)
            {
                resultWriter.WriteLine(notckd[i]);
                resultWriter.WriteLine(ckd[k]);
                k++;
                if(i %2==0)
                {
                    resultWriter.WriteLine(ckd[k]);
                    k++;
                }
                if (i%6==0)
                {
                    resultWriter.WriteLine(ckd[k]);
                    k++;
                }
            }
            File.Delete(filepath);
            resultWriter.Close();
            
        }*/

        private double ParseValue(string s)
        {
            double n;
            if (double.TryParse(s,
                System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo,
                out n)) return n;
            switch (s.Trim())
            {
                case "yes": return 1;
                case "no": return 2;
                case "normal": return 1;
                case "abnormal": return 2;
                case "present": return 1;
                case "notpresent": return 2;
                case "good": return 1;
                case "poor": return 2;
                case "ckd": return 0;
                case "notckd": return 1;
                case "?": return 0;
                default: return 0;

            }
        }

        public void ProcessData(string inPath, string outPath)
        {
            string filepath = "../../Data/cur.txt";
            File.Delete(outPath);
            var sourceStream = new System.IO.StreamReader(System.IO.File.Open(inPath, System.IO.FileMode.Open));

            string s = "";
            while (!s.Contains("data"))
                s = sourceStream.ReadLine();

            StringBuilder sb = new StringBuilder();
            List<List<double>> data = new List<List<double>>();

            while ((s = sourceStream.ReadLine()) != null)
            {
                var arr = s.Split(',');
                List<double> temp = new List<double>();
                foreach (var value in arr)
                    temp.Add(ParseValue(value));
                data.Add(temp);
            }

            sourceStream.Close();


            var attributes = data[0].Count - 1;
            List<double> maximums = new List<double>(attributes), minimums = new List<double>(attributes);
            for (int column = 0; column < attributes; column++)
            {
                double min = 0, max = 0;
                for (int row = 0; row < data.Count; row++)
                {
                    if (data[row][column] > max)
                        max = data[row][column];
                    if (data[row][column] < min)
                        min = data[row][column];
                }
                maximums.Add(max);
                minimums.Add(min);

            }

            File.Delete(filepath);
            var resultWriter = new System.IO.StreamWriter(System.IO.File.Open(filepath, System.IO.FileMode.OpenOrCreate));

            for (int row = 0; row < data.Count; row++)
            {
                for (int column = 0; column < attributes; column++)
                {
                    double value = (data[row][column] - minimums[column]) / (maximums[column] - minimums[column]);
                    sb.Append(value.ToString());
                    sb.Append(' ');
                }
                sb.Append(data[row][attributes]);
                sb.Append(' ');
                resultWriter.WriteLine(sb.ToString());
                sb = new StringBuilder();
            }


            
            resultWriter.Close();

            
            var text = System.IO.File.ReadAllLines(filepath, Encoding.Default).Select(x => x.Trim());
            resultWriter = new System.IO.StreamWriter(System.IO.File.Open(outPath, System.IO.FileMode.OpenOrCreate));

            var ckd = text.Where(x => x.Last() == '0').ToArray();
            var notckd = text.Where(x => x.Last() == '1').ToArray();
            int k = 0;
            int count = 0;
            for (int i = 0; i < notckd.Length; i++)
            {
                resultWriter.WriteLine(notckd[i]);
                resultWriter.WriteLine(ckd[k]);
                k++;
                if (i % 2 == 0)
                {
                    resultWriter.WriteLine(ckd[k]);
                    k++;
                }
                if (i % 6 == 0)
                {
                    resultWriter.WriteLine(ckd[k]);
                    k++;
                }
            }
            File.Delete(filepath);
            resultWriter.Close();
             

        }

        public void DivideFile(string inPath, string trainPath, string testPath, int count)
        {
            count = count / 2;
            var text = System.IO.File.ReadAllLines(inPath, Encoding.Default).Select(x=>x.Trim());

            var ckd = text.Where(x => x.Last() == '0');
            var notckd = text.Where(x => x.Last() == '1');

            var training = ckd.Take(ckd.Count() - count).Concat(notckd.Take(notckd.Count() - count));
            var test= ckd.Skip(ckd.Count() - count).Concat(notckd.Skip(notckd.Count() - count));

            System.IO.File.WriteAllLines(@trainPath, training);
            System.IO.File.WriteAllLines(@testPath, test);

        }

        public void GetData(ref double[][] data, ref double[][] answer, ref Form1 form, string filepath)
        {
            var text = System.IO.File.ReadAllLines(filepath, Encoding.Default);
            List<double[]> d = new List<double[]>();
            List<double[]> a = new List<double[]>();
            foreach (var str in text)
            {
                var arr = str.Trim().Split(' ').Select(x => double.Parse(x));
                List<double> get_checked = new List<double>();
                for (int i = 0; i < arr.Count() - 2; i++)
                {
                    var control = (CheckBox)form.Controls["attribute" + i.ToString()];
                    if (control.Checked)
                        get_checked.Add(arr.ElementAt(i));
                }
                d.Add(get_checked.ToArray());
                a.Add(new[] { arr.Last() });
            }
            data = d.ToArray();
            answer = a.ToArray();
        }

    }
}

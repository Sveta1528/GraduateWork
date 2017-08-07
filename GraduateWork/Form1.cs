using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace GraduateWork
{
    public partial class Form1 : MetroForm
    {
        private double[][] all_data;
        private double[][] all_answer;
        private double[][] data;
        private double[][] answer;
        private double[][] test_data;
        private double[][] test_answer;
        private int count_attributes;
        private WorkWithFile work_file = new WorkWithFile();
        private GenerateObjects generate =new GenerateObjects();
        private int count_records;
        private int count_tests;
        private WorkWithNet work_net;

        public Form1()
        {
            InitializeComponent();
            Form1 form = this;
            
            generate.GenerateAttributes(ref form,InFilepath.Text,ref count_attributes, 5, 
                new Tuple<int, int>(230,50), new Tuple<int, int>(65,20)); 
                
        }


        private void ProcessData_Click(object sender, EventArgs e)
        {
            work_file.ProcessData(InFilepath.Text, OutFilepath.Text);
        }

        private void Divide_Click(object sender, EventArgs e)
        {
            count_tests = Int32.Parse(Percent.Text);
            work_file.DivideFile(OutFilepath.Text, "../../Data/training.txt", "../../Data/test.txt", count_tests);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form1 form = this;
            generate.GenerateLayers(ref form, 550, 100, 25, 80);
        }

        private void Default_CheckedChanged(object sender, EventArgs e)
        {
            layers.Enabled = false;
            Form1 form = this;
            generate.DeleteLayers(ref form);
        }

        private void metroRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            layers.Enabled = true ;
        }


        private void Learn_Click(object sender, EventArgs e)
        {
            work_net = new WorkWithNet();
            Form1 form = this;
            work_file.GetData(ref all_data, ref all_answer, ref form, OutFilepath.Text);
            var k =Int32.Parse(Percent.Text) / 100.0;
            int count = (int)(all_data.Length* k);
            List<double[]> for_data=new List<double[]>();
            List<double[]> for_test = new List<double[]>();
            List<double[]> for_data_answ = new List<double[]>();
            List<double[]> for_test_answ = new List<double[]>();

            for (int i = 0; i < count; i++)
            {
                for_data.Add(all_data[i]);
                for_data_answ.Add(all_answer[i]);
            }

            for (int i = count; i < all_data.Length; i++)
            {
                for_test.Add(all_data[i]);
                for_test_answ.Add(all_answer[i]);
            }

            data = for_data.ToArray();
            test_data = for_test.ToArray();
            answer = for_data_answ.ToArray();
            test_answer = for_test_answ.ToArray();
            System.Diagnostics.Stopwatch sw = new Stopwatch();
            sw.Start();
            work_net.Learn(ref form, ref data, ref answer, 50000000 );
            Test.Enabled = true;
            sw.Stop();
            textBox1.Text = (sw.ElapsedMilliseconds / 1000.0).ToString();
        }

        private void Test_Click(object sender, EventArgs e)
        {
            Form1 form = this;
            //work_net.Test(ref form, ref test_data, ref test_answer,  "../../Data/result.txt");
           // work_net.Test(ref form, ref data, ref answer, "../../Data/result2.txt");
           work_net.Test2(ref form, ref data, ref answer, ref test_data, ref test_answer);
        }

        private void DeleteCheck_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < count_attributes; i++)
            {
                var control = (CheckBox)this.Controls["attribute" + i.ToString()];
                control.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var text = System.IO.File.ReadAllLines(OutFilepath.Text, Encoding.Default);
            List<double[]> d = new List<double[]>();
            List<double[]> a = new List<double[]>();
            foreach (var str in text)
            {
                var arr = str.Trim().Split(' ').Select(x => double.Parse(x));
                List<double> get_checked = new List<double>();
                for (int i = 0; i < arr.Count() - 2; i++)
                {
                    var control = (CheckBox)Controls["attribute" + i.ToString()];
                    if (control.Checked)
                        get_checked.Add(arr.ElementAt(i));
                }
                d.Add(get_checked.ToArray());
                a.Add(new[] { arr.Last() });
            }
            data = d.ToArray();
            answer = a.ToArray();
            File.Delete("attributes.txt");
            StringBuilder sb = new StringBuilder();
            var resultWriter = new System.IO.StreamWriter(System.IO.File.Open("attributes.txt", System.IO.FileMode.OpenOrCreate));
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    sb.Append(data[i][j]);
                    sb.Append(" ");
                }
                sb.Append(answer[i][0]);
                resultWriter.WriteLine(sb.ToString());
                sb=new StringBuilder();
            }
            resultWriter.Close();
        }
    }
}

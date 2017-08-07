using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduateWork
{
    class WorkWithNet
    {
        private NeuralNetwork net;

        private int[] GetCountsNeuron(ref Form1 form, ref double[][] data, ref double[][] answer)
        {
            int n = Int32.Parse(form.layers.Text) + 2;
            var result = new int[n];
            int countInput = result[0] = result[1] = data[0].Length;
            int countOutput = result[n - 1] = answer[0].Length;

            if (form.Default.Checked) return result;

            for (int i = 0; i < n - 2; i++)
            {
                var control = form.Controls["layer" + i.ToString()];
                result[i+1] = Int32.Parse(control.Text);
            }

            return result;
        }

        public void Learn(ref Form1 form, ref double[][] data, ref double[][] answer, int iterations)
        {
            var thresh = Math.Pow(10, -Int32.Parse(form.Accuracy.Text));
            int[] neuronsArr = GetCountsNeuron(ref form, ref data, ref answer);
            double speed = double.Parse(form.Speed.Text);
            net = new NeuralNetwork(neuronsArr.Length, neuronsArr, speed);
            form.Logs.Text = "";
            int count = data.Length;
            // Начинаем обучение сети
            long i;
            var sb = new StringBuilder();
            for (i = 0; i < iterations; i++)
            {

                // Запускаем обучение
                net.BackPropogate(data[i % count], answer[i % count]);

                // Проверяем среднюю квадратичную ошибку
                var error = net.mse(answer[i % count]);
                if (error < thresh)
                {

                    form.Logs.Text += "Сеть обучена. Выполнено " + i + " повтореий" + Environment.NewLine;
                    form.Logs.Text += "Средняя квадратичная ошибка:  " + error + Environment.NewLine;
                    break;
                }


                // Выводим статус каждые 10% прогресса
                if (i % (iterations / 10) == 0)
                {
                    error = net.mse(answer[i % count]);
                    form.Logs.Text += "Средняя квадратичная ошибка:  " + error + Environment.NewLine;
                    form.Logs.Text += "... Идет обучение..." + Environment.NewLine;
                }

            }

            // Если цикл дошел до предела, вместо того, чтобы средняя квадротичная ошибка стала приемлимой
            if (i == iterations)
            {
                form.Logs.Text += "Истекло количество повторений" + Environment.NewLine;
                form.Logs.Text += "Средняя квадратичная ошибка:  " + net.mse(answer[i % count]) + Environment.NewLine;
            }
        }

        public void Test(ref Form1 form, ref double [][] data, ref double[][] answer,  string outPath)
        {
            File.Delete(outPath);
            var resultWriter =
                new System.IO.StreamWriter(System.IO.File.Open(outPath, System.IO.FileMode.OpenOrCreate));

            StringBuilder sb = new StringBuilder();
            int right = 0;
            for (int i = 0; i < data.Length; i++)
            {
                net.FeedForwards(data[i]);
                var result = net.Out();
                if (Math.Round(result[0]) == answer[i][0]) right++;
            }
            sb.AppendLine();
            sb.AppendLine("Количество верных ответов: " + right + " из " + data.Length + 
                " ( " + right / ((double)data.Length / 100) +
                          "% )");

            resultWriter.Write(sb.ToString());
            resultWriter.Close();
        }

        public void Test2(ref Form1 form, ref double[][] data, ref double[][] answer, ref double[][] test_data, ref double[][] test_answer)
        {


            int right = 0;
            for (int i = 0; i < data.Length; i++)
            {
                net.FeedForwards(data[i]);
                var result = net.Out();
                if (Math.Round(result[0]) == answer[i][0]) right++;
            }
            form.l_count.Text = right.ToString();
            form.l_percent.Text = (right / ((double)data.Length / 100)).ToString();

            right = 0;
            for (int i = 0; i < test_data.Length; i++)
            {
                net.FeedForwards(test_data[i]);
                var result = net.Out();
                if (Math.Round(result[0]) == test_answer[i][0]) right++;
            }
            form.test_count.Text = right.ToString();
            form.test_percent.Text = (right / ((double)test_data.Length / 100)).ToString();


        }
    }
}

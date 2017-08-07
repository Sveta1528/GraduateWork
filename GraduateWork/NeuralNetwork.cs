using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduateWork
{
    class NeuralNetwork
    {
        private int layers; // Количество слоев
        double beta;  // Скорость обучения
        int  [] neuronCnt; // Вектор количества нейронов на каждом слое
        double [][] outNneuron;
        double [][] delta;
        double [][][] weight;
        double [][][] prevDwt;

         public NeuralNetwork(int nl, int [] sz, double b)
         {
             beta = b;

            // Сопоставление слоев и их размеров
            layers = nl; // Кол-во слоев
            neuronCnt = new int[layers];
             for (int i = 0; i < layers; i++)
                 neuronCnt[i] = sz[i]; // Кол-во нейроннов в слое i. neuronCnt[id слоя]

             // Выделяем память для кажого выходного нейрона
            outNneuron = new double [layers][];
             for (int i = 0; i < layers; i++)
                 outNneuron[i] = new double[neuronCnt[i]];
                     // Значение на выходе нейрона. outNneuron[id слоя][id нейрона]

             // Выделяем память для delta
            delta = new double [layers][];
             for (int i = 1; i < layers; i++)
                 delta[i] = new double[neuronCnt[i]];

             // Выделяем память для весов связей неронов
            // weight[id слоя нейрона][id нейрона][id предыдущего нейрона, с которым установленна связь]; 
            weight = new double [layers][][];
             for (int i = 1; i < layers; i++)
                 weight[i] = new double[neuronCnt[i]][];
             for (int i = 1; i < layers; i++)
             for (int j = 0; j < neuronCnt[i]; j++)
                 weight[i][j] = new double[neuronCnt[i - 1] + 1];

             // Выделяем память для предыдущего веса нейрона
            prevDwt = new double [layers][][];
             for (int i = 1; i < layers; i++)
                 prevDwt[i] = new double[neuronCnt[i]][];

             for (int i = 1; i < layers; i++)
             for (int j = 0; j < neuronCnt[i]; j++)
                 prevDwt[i][j] = new double[neuronCnt[i - 1] + 1];

             // Заполнить веса нейроннов случайными значениями
            //srand((unsigned)(time(NULL)));
            Random rnd = new Random();
            for (int i = 1; i < layers; i++)
             for (int j = 0; j < neuronCnt[i]; j++)
             for (int k = 0; k < neuronCnt[i - 1] + 1; k++)
                 weight[i][j][k] = rnd.NextDouble() * 2 -1;

         }


        // Сигмоид функция
        double sigmoid(double x) => (double)(1 / (1 + Math.Exp(-x)));


        // Средняя квадратическая ошибка
        public double mse(double [] tgt) {
            double mse = 0.0;
            for (int i = 0; i < neuronCnt[layers - 1]; i++)
                mse += (tgt[i] - outNneuron[layers - 1][i]) * (tgt[i] - outNneuron[layers - 1][i]);
            return mse / 2.0;
        }



        // Возвращает выход сети
        public double [] Out() => outNneuron[layers - 1];

        // Просчитать один слой
         public void FeedForwards(double [] x)
        {
            int curLayer, curNneuron;

            // Назначить содержание для первого слоя (входного)
            for (int i = 0; i < neuronCnt[0]; i++)
                outNneuron[0][i] = x[i];

            // --- Проходимся по каждому слою, пропуская входной (curLayer = 1)
            for (curLayer = 1; curLayer < layers; curLayer++)
            // --- Проходимся по каждому нейронну в слое
            for (curNneuron = 0; curNneuron < neuronCnt[curLayer]; curNneuron++)
            {
                // --- Проходимся по всем нейроннам предыдущего слоя
                var sum = 0.0;
                int prevNneuron;
                for (prevNneuron = 0; prevNneuron < neuronCnt[curLayer - 1]; prevNneuron++)
                        // Умножаем вход на вес и прибавляем к сумме нейрона
                        sum += outNneuron[curLayer - 1][prevNneuron] * weight[curLayer][curNneuron][prevNneuron]; 
                
                // Добавляем смещение
                sum += weight[curLayer][curNneuron][neuronCnt[curLayer - 1]];
                // Обновляем значение выода нейрона. Применяем функцию сжатия
                outNneuron[curLayer][curNneuron] = sigmoid(sum);
            }
        }

        // Обратное распространение ошибки
        public void BackPropogate(double [] input, double [] result)
        {
            // Обновляем выходы сети
            FeedForwards(input);

            // дельта_к=о_к(1-о_k)(t_k-o_k)
            // Найти дельту для выходного слоя
            for (int i = 0; i < neuronCnt[layers - 1]; i++)
                delta[layers - 1][i] = outNneuron[layers - 1][i] * (1 - outNneuron[layers - 1][i]) *
                                       (result[i] - outNneuron[layers - 1][i]);

            // дельта_j = o_j(1-o_j) сумма дельта_k w_j,k.
            // Найти дельту для скрытых слоев
            for (int i = layers - 2; i > 0; i--)
            for (int j = 0; j < neuronCnt[i]; j++)
            {
                var sum = 0.0;
                for (int k = 0; k < neuronCnt[i + 1]; k++)
                    sum += delta[i + 1][k] * weight[i + 1][k][j];
                delta[i][j] = outNneuron[i][j] * (1 - outNneuron[i][j]) * sum;
            }

            // Коректировать веса связей
            for (int i = 1; i < layers; i++)
            for (int j = 0; j < neuronCnt[i]; j++)
            {
                
                for (int k = 0; k < neuronCnt[i - 1]; k++)
                {
                    //  /\w_i,j=beta*дельта_j*o_j.
                    prevDwt[i][j][k] = beta * delta[i][j] * outNneuron[i - 1][k];
                    weight[i][j][k] += prevDwt[i][j][k];
                }
                prevDwt[i][j][neuronCnt[i - 1]] = beta * delta[i][j];
                weight[i][j][neuronCnt[i - 1]] += prevDwt[i][j][neuronCnt[i - 1]];
            }
        }

    }
}

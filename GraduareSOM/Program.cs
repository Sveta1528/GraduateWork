using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace SOM_Grad
{
    class Map
    {
        private Neuron[,] weights;  // Collection of weights.
        private int iteration;      // Current iteration.
        private int size;        // Side size of output grid.
        private int dimensions;    // Number of input dimensions.
        private Random rnd = new Random();

        private List<string> labels = new List<string>();
        private List<double[]> patterns = new List<double[]>();
        private List<double[]> data= new List<double[]>();
        private List<double[]> test = new List<double[]>();
        private List<string> all_labels = new List<string>();
        private List<string> test_labels = new List<string>();
        private List<string> data_labels = new List<string>();

        static void Main(string[] args)
        {
            new Map(24, 30, "process_data.txt");
            Console.ReadLine();
        }

        public Map(int dimensions, int size, string file)
        {
            this.size = size;
            this.dimensions = dimensions;
                Initialise();
                LoadData(file, 0.75);
                //NormalisePatterns();
                Train(0.000001);
                //Train(0.01);
                //SaveWeights();
                //LoadWeights();
                DumpCoordinates();

             
        }

        private void Initialise()
        {
            weights = new Neuron[size, size];
            for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
            {
                weights[i, j] = new Neuron(i, j, size) {Weights = new double[dimensions]};

                for (int k = 0; k < dimensions; k++)
                    weights[i, j].Weights[k] = rnd.NextDouble();
            }
        }

        private void SaveWeights()
        {
            File.Delete("weights.txt");
            var resultWriter = new System.IO.StreamWriter(System.IO.File.Open("weights.txt", System.IO.FileMode.OpenOrCreate));
            for (int i=0; i<size; i++)
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < weights[i, j].Weights.Length; k++)
                    resultWriter.Write(weights[i, j].Weights[k] + " ");
                resultWriter.WriteLine();
            }
            resultWriter.Close();

        }

        private void LoadWeights()
        {
            var sourceStream = new System.IO.StreamReader(System.IO.File.Open("weights.txt", System.IO.FileMode.Open));

            int i = 0;
            int j = 0;
            string s = "";
            while ((s = sourceStream.ReadLine()) != null)
            {
                var arr = s.Trim().Split(' ');
                for (int k = 0; k < arr.Length; k++)
                    weights[i, j].Weights[k] = double.Parse(arr[k]);
                j++;
                if (j == size)
                {
                    j = 0;
                    i++;
                }

            }
            sourceStream.Close();

        }

        private void LoadData(string file,double percent)
        {
            StreamReader reader = File.OpenText(file);
            while (!reader.EndOfStream)
            {
                string[] line = reader.ReadLine().Split(' ');
                all_labels.Add(line.Last());
                double[] inputs = new double[dimensions];
                for (int i = 0; i < dimensions; i++)
                {
                    inputs[i] = double.Parse(line[i]);
                }
                data.Add(inputs);
            }
            reader.Close();
            int count = (int)(all_labels.Count*percent);
            labels = all_labels.Take(count).ToList();
            test_labels = all_labels.Skip(count).ToList();
            data_labels = all_labels.Take(count).ToList();
            patterns = data.Take(count).ToList();
            test = data.Skip(count).ToList();
        }


        private void NormalisePatterns()
        {
            for (int j = 0; j < dimensions; j++)
            {
                double sum = 0;
                for (int i = 0; i < patterns.Count; i++)
                {
                    sum += patterns[i][j];
                }
                double average = sum / patterns.Count;
                for (int i = 0; i < patterns.Count; i++)
                {
                    patterns[i][j] = patterns[i][j] / average;
                }
            }
        }

        private void Train(double maxError)
        {
            double currentError = double.MaxValue;
            while (currentError > maxError)
            {
                currentError = 0;
                List<double[]> TrainingSet = new List<double[]>();
                foreach (double[] pattern in patterns)
                {
                    TrainingSet.Add(pattern);
                }
                for (int i = 0; i < patterns.Count; i++)
                {
                    double[] pattern = TrainingSet[rnd.Next(patterns.Count - i)];
                    currentError += TrainPattern(pattern);
                    TrainingSet.Remove(pattern);
                }
                //Console.WriteLine(currentError.ToString("0.000000000"));
            }
        }

        private double TrainPattern(double[] pattern)
        {
            double error = 0;
            Neuron winner = Winner(pattern);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    error += weights[i, j].UpdateWeights(pattern, winner, iteration);
                }
            }
            iteration++;
            return Math.Abs(error / (size * size));
        }

        private void DumpCoordinates()
        {

            string[][] matr = new string[size][];
            for (int i = 0; i < matr.Length; i++)
            {
                matr[i] = new string[matr.Length];
                for (int j = 0; j < matr[i].Length; j++)
                    matr[i][j] = "-";
            }

            List<string>[][] m = new List<string>[size][];
            for (int i = 0; i < m.Length; i++)
            {
                m[i] = new List<string>[m.Length];
                for (int j = 0; j < m[i].Length; j++)
                    m[i][j] = new List<string>();
            } ;

            for (int i = 0; i < patterns.Count; i++)
            {
                Neuron n = Winner(patterns[i]);
                matr[n.X][n.Y] = labels[i];
                m[n.X][n.Y].Add(labels[i]);
            }
            

            for (int i = 0; i < m.Length; i++)
            {
                for (int j = 0; j < m.Length; j++)
                {
                    if((m[i][j].Count() ==0)) continue;
                    if (m[i][j].Where(x => x == "0").Count() > m[i][j].Where(x => x == "1").Count())
                        matr[i][j] = "0";
                    else
                        matr[i][j] = "1";
                }
            }


            int radius = 2;
            int left, right, top, bot;
            int count = 0, count_empty = 0;
            List<Tuple<int,int,string>> empty = new List<Tuple<int, int,string>>();
            for (int i = 0; i < test.Count; i++)
            {
                Neuron n = Winner(test[i]);
                if (matr[n.X][n.Y] == "-")
                {
                    
                    count_empty++;
                    empty.Add(new Tuple<int, int, string>(n.X, n.Y, test_labels[i]));
                    if (n.X == 2)
                        left = 0;
                    left = n.Y - radius;
                    left = left < 0 ? 0 : left;

                    right= n.Y + radius;
                    right = right >= size ? size-1 : right;

                    top= n.X- radius;
                    top = top < 0 ? 0 : top;

                    bot = n.X + radius;
                    bot = bot >= size ? size-1 : bot;

                    int zero = 0, one = 0;
                    int chek = 0;
                    //Console.WriteLine("top " + top + " bot " + bot + " left " + left + " right " + right);
                    for (int k =top; k<=bot; k++)
                    for (int l = left; l <= right; l++)
                    {
                        chek++;
                        if (matr[k][l] == "0") zero++;
                        if (matr[k][l] == "1") one++;
                        //Console.Write(matr[k][l] + " ");
                    }
                    //Console.WriteLine("Вошли " + chek);
                    string res = zero > one ? "0" : "1";
                    //Console.WriteLine("Ожидалось {0}; Получилось {1}", test_labels[i], res);
                    if (test_labels[i] == res) count++;


                }
                else
                {
                    if (test_labels[i] == matr[n.X][n.Y]) count++;
                }
            }

            Console.WriteLine(count);
            Console.WriteLine("На тестовой:" + (count / ((double)test.Count / 100)).ToString());

            
            count = 0;
            count_empty = 0;
            for (int i = 0; i < patterns.Count; i++)
            {
                Neuron n = Winner(patterns[i]);
                if (matr[n.X][n.Y] == "-")
                {

                    count_empty++;
                    empty.Add(new Tuple<int, int, string>(n.X, n.Y, data_labels[i]));
                    if (n.X == 2)
                        left = 0;
                    left = n.Y - radius;
                    left = left < 0 ? 0 : left;

                    right = n.Y + radius;
                    right = right >= size ? size - 1 : right;

                    top = n.X - radius;
                    top = top < 0 ? 0 : top;

                    bot = n.X + radius;
                    bot = bot >= size ? size - 1 : bot;

                    int zero = 0, one = 0;
                    int chek = 0;
                    //Console.WriteLine("top " + top + " bot " + bot + " left " + left + " right " + right);
                    for (int k = top; k <= bot; k++)
                        for (int l = left; l <= right; l++)
                        {
                            chek++;
                            if (matr[k][l] == "0") zero++;
                            if (matr[k][l] == "1") one++;
                            //Console.Write(matr[k][l] + " ");
                        }
                    //Console.WriteLine("Вошли " + chek);
                    string res = zero > one ? "0" : "1";
                    //Console.WriteLine("Ожидалось {0}; Получилось {1}", test_labels[i], res);
                    if (data_labels[i] == res) count++;


                }
                else
                {
                    if (data_labels[i] == matr[n.X][n.Y]) count++;
                }
            }
            Console.WriteLine(count);
            Console.WriteLine("На обучающей:" + (count / ((double)patterns.Count / 100)).ToString());
            Console.WriteLine("####################################");
            
            /*
            Console.WriteLine("Count empty " + count_empty);
            foreach (var VARIABLE in empty)
            {
                Console.WriteLine("{0} {1} {2}", VARIABLE.Item1,VARIABLE.Item2, VARIABLE.Item3);
            }
            */
            /*

            for (int i = 0; i < matr.Length; i++)
            {
                for (int j = 0; j < matr.Length; j++)
                {
                    Console.Write(matr[i][j] + " ");
                }
                Console.WriteLine();
            }
             */


        }

        private Neuron Winner(double[] pattern)
        {
            Neuron winner = null;
            double min = double.MaxValue;
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    double d = Distance(pattern, weights[i, j].Weights);
                    if (d < min)
                    {
                        min = d;
                        winner = weights[i, j];
                    }
                }
            return winner;
        }

        private double Distance(double[] vector1, double[] vector2)
        {
            double value = 0;
            for (int i = 0; i < vector1.Length; i++)
                value += Math.Pow((vector1[i] - vector2[i]), 2);
            return Math.Sqrt(value);
        }
    }

    public class Neuron
    {
        public double[] Weights;
        public int X;
        public int Y;
        private int size;
        private double nf;

        public Neuron(int x, int y, int size)
        {
            X = x;
            Y = y;
            this.size = size;
            nf = 1000 / Math.Log(size);
        }

        private double Gauss(Neuron win, int it)
        {
            double distance = Math.Sqrt(Math.Pow(win.X - X, 2) + Math.Pow(win.Y - Y, 2));
            return Math.Exp(-Math.Pow(distance, 2) / (Math.Pow(Strength(it), 2)));
        }

        private double LearningRate(int it)
        {
            return Math.Exp(-it / 1000) * 0.1;
        }

        private double Strength(int it)
        {
            return Math.Exp(-it / nf) * size;
        }

        public double UpdateWeights(double[] pattern, Neuron winner, int it)
        {
            double sum = 0;
            for (int i = 0; i < Weights.Length; i++)
            {
                double delta = LearningRate(it) * Gauss(winner, it) * (pattern[i] - Weights[i]);
                Weights[i] += delta;
                sum += delta;
            }
            return sum / Weights.Length;
        }
    }
}

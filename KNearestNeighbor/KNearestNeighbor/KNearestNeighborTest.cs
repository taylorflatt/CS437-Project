using System;
using System.Windows.Forms;

namespace KNearestNeighbor
{
    class KNearestNeighborTest
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Wizard());

            //Here we define 10 cars that have a specific set of classification data.
            double[][] inputs =
            {
                new double[] { -5, -2, 1, 2, 9},
                new double[] { -5, -5, 2, 5, 2},
                new double[] {  2, 1, 7, 8, 2},
                new double[] {  1, 1, 8, 1, 2},
                new double[] {  1, 2, 2, 4, 1},
                new double[] {  3, 1, 2, 0, 1},
                new double[] { 11, 5, 1, 2, 1},
                new double[] { 15, 5, 2, 5, 9},
                new double[] { 10, 5, 2, 1, 0},
                new double[] { 10, 5, 2, 1, 0},
            };

            int[] outputs =
            {
                0,0,0,0,0,
                1,1,1,
                2,2
            };

            // Now we will create the K-Nearest Neighbors algorithm. We need to decide upon a k value that fits 
            //our data. Depending on how many inputs Marty can pull, k=sqrt(inputs). The smaller the data set the 
            //smaller our k but the larger our error. If we make k super large on a small data set then the results 
            //will be biased and we won't get a real answer. But if we make it too small then we might not get an 
            //accurate representation of then data. So we need to have a LARGE data set in order to have truly 
            //accurate results.
            KNearestNeighborAlgorithm knn = new KNearestNeighborAlgorithm(k: 3, inputs: inputs, outputs: outputs);


            // After the algorithm has been created, we can classify a new car instance so we can see which 
            //car is our competitor.
            double[] data = { 9, 5, 2, 1, 0 };
            int answer = knn.Compute(data); // answer will depend on the k-value since data set is small.

            //Display (debug) information
            int count = 0;
            bool flag = false;
            Console.WriteLine("Inputs: ");
            foreach (var set in inputs)
            {
                foreach (var element in set)
                {
                    if (count % 2 == 0 && flag == true)
                        Console.WriteLine("");

                    Console.Write(+element + ", ");
                    count++;
                    flag = true;
                }
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Class: " + answer);
        }
    }
}
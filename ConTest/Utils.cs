using System;
using Accord.Math;
using Accord.Statistics;

namespace temputils {

    class utils {
        public double [,] FeatureNormalization(in double[,] localmatrix)
        {
            double [] Mu;
            double[] Sigma = Measures.StandardDeviation(localmatrix); // Calculate the standard deviation

            Mu = Mean(localmatrix);       // Calculate the column average

            for (int row = 0; row < localmatrix.GetLength(0); row++)
            {
                for (int col = 0; col < localmatrix.GetLength(1); col++)
                {
                    localmatrix[row, col] = localmatrix[row, col] - Mu[col];
                    localmatrix[row, col] = localmatrix[row, col] / Sigma[col];
                }
            }
            return (localmatrix);
        }

        private static double[] Mean(in double[,] input)
        {
            // Multidimensional array method

            const int ColumnSums = 0;

            double[] Mu = input.Mean(dimension: ColumnSums); // Calculates the column means
            return Mu;
        }
    }
}
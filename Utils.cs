using System;

namespace utils
{

    public class Utils
    {
        public Class1()
        {
        }

        public FeatureNormalization(in double[,] localmatrix)
        {
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
            Normalized = localmatrix;
        }
    }
}

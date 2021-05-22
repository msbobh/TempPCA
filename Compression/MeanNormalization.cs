using System;
using System.Linq;
using System.Collections.Generic;
using Accord.Statistics;
using Accord.Math;

namespace Compression
{
    public class MeanNormalization
    {
        /*
         * Performs mean normalization over a matrix, an average for each feature columne
         * The average (mean) is then divided by the standard deviation.
         * StdDev is store in a vector Sigma, the calculated mean is available via the 
         * "Mu" property
         *  
         */

        // Public class properties
        
        public double[,] Normalized;
        public double[] Mu;                 // Calculated normalizaton vector

               
              
        // each (X(i) - Mu(i)) / Sigma(i)
        public MeanNormalization(in double[,] localmatrix)
        {
            double[] Sigma = Measures.StandardDeviation(localmatrix); // Calculate the standard deviation
            
            Mu = Mean(localmatrix);       // Calculate the column average
            
            for (int row = 0; row < localmatrix.GetLength(0); row++)
            {
                for (int col = 0; col < localmatrix.GetLength(1); col++)
                {
                    localmatrix[row,col] = localmatrix[row,col] - Mu[col];
                    localmatrix[row, col] = localmatrix[row, col] / Sigma[col];
                }
            }
            Normalized = localmatrix;
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

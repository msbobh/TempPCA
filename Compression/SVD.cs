using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Accord.Math.Decompositions;
using Accord.Math;
using Accord.Statistics.Models.Regression.Linear;
using UtilityFuncs;


namespace Compression
{
    public class SingleValueDecomposition
    {
        /*
         * Principal Component Analysis (PCA) algorithm
         * Reduce data from n-dimensions to k-dimensions
         * 
         * Need compute the eigen vectors of covariance matrix sigma
         * sigma = (X * X) (transpose)
         * X will be an n x n square matrix and will calculate 
         * the following matrices [U,S,V], we want the first k
         * vectors of the U matrix. Call this U(reduce), then
         * to generate the Z matrix we compute U(reduce) transpose * X
         */

        // Public class properties
        public double[][] NormalizedJagged { get; set; } //Normalized feature matrix
        public double[,] Ureduced { get; }
        public double[,] UAccordreduced { get; }
        

        public  SingleValueDecomposition (in double [,] FeatureMatrix)
        {
            // need to calculate Sigma Covariance Matrix from the feature matrix after
            // it has been Normalized
            double [,] CovarMatrix = GenerateCovarianceMatrix(FeatureMatrix);
            //EigenvalueDecomposition EVD = new EigenvalueDecomposition(CovarMatrix);
            //Ureduced = EVD.Eigenvectors;

            SingularValueDecomposition SVN = new SingularValueDecomposition(FeatureMatrix, computeRightSingularVectors: true, computeLeftSingularVectors: false);
            UAccordreduced = SVN.RightSingularVectors;

           
        }
                
        public double [,] GenerateCovarianceMatrix (in double [,] FeatureMatrix)
        {
            double m = FeatureMatrix.GetLength(0); // Number of samples

            MeanNormalization MNData = new MeanNormalization(FeatureMatrix);
            double[,] NormalizedFeatures = MNData.Normalized; // Normalize the data

            // Calculate the Covariance Matrix
            NormalizedFeatures = NormalizedFeatures.TransposeAndDot(NormalizedFeatures);
            // Divide by 1/M
            NormalizedFeatures = NormalizedFeatures.Multiply(1.0 / m);
            return NormalizedFeatures;
        }

        /*
         * this is not ready for use, outputs a jagged array when the code assumes
         * a multi dimemsional array.
         * 
         */
         public double [][] AccordPCA (in double [,] FeatureMatrix)
        {
            double [][] data = externalFunc.convertToJaggedArray(FeatureMatrix);
            
            var pca = new Accord.Statistics.Analysis.PrincipalComponentAnalysis()
            {
                Method = Accord.Statistics.Analysis.PrincipalComponentMethod.Center,
                Whiten = false
            };

            // Now we can learn the linear projection from the data
            MultivariateLinearRegression transform = pca.Learn(data);

            // Finally, we can project all the data
            double[][] output1 = pca.Transform(data);

            // Or just its first components by setting 
            // NumberOfOutputs to the desired components:
            pca.NumberOfOutputs = 1;

            // And then calling transform again:
            double[][] output2 = pca.Transform(data);
            return output2;
        }

        public double [,] Reduce (double [,] MatrixTobeReduced, int Dimension, double [,] eigenVectors)
        {
            double[,] resultMatrix = MatrixTobeReduced.Get(startRow: 0, endRow: MatrixTobeReduced.GetLength(0),
                                                            startColumn: 0, endColumn: Dimension);
            return (MatrixTobeReduced.Dot(eigenVectors));
        }
    }
}

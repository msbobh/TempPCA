using System;
using Compression;
using Accord.Math;
using Accord.IO;
namespace ConTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // ***************** File read stuff

            double[,] Rawdata;
            double[,] labelsdata;
            int k = 1;

            Console.WriteLine("Principal Components Analysis compression\n");
            string testFname = args[0];

            using (CsvReader reader = new CsvReader(testFname, hasHeaders: false))
            {
                Rawdata = reader.ToMatrix();
            }
            Console.WriteLine(" Read input CSV");

            System.Diagnostics.Stopwatch elapsed = new System.Diagnostics.Stopwatch();
            elapsed.Start();
            // Create a new PCA object and cacluate eigenvectors
            SingleValueDecomposition PCA = new SingleValueDecomposition(Rawdata);

            elapsed.Stop();
            Console.WriteLine("Created new SVD Object, time = {0}",elapsed.Elapsed.ToString());
            Console.WriteLine(" First few eigen Vectors");
            for (int row = 0; row < 1; row++)
            {
                for (int col = 0; col < PCA.UAccordreduced.GetLength(1); col++)
                {
                    Console.Write("{0:N4}  ", PCA.UAccordreduced[row, col]);
                }
                Console.WriteLine();
            }

            // Create submatrix of k eigen vectors
            double[,] dimreduced = PCA.UAccordreduced.Get(startRow: 0, endRow: PCA.UAccordreduced.GetLength(0),
                                                          startColumn: 0, endColumn: k);
            // Re obtain the unchanged input matrix and convert to n x k

            double [,] projectionData;
            using (CsvReader reader = new CsvReader(testFname, hasHeaders: false))
            {
                projectionData = reader.ToMatrix();
            }
            //projectionData =

            double[,] FacesZ = PCA.Reduce(projectionData, Dimension: k, eigenVectors: dimreduced);
            
            Console.WriteLine("Done Compressing");
            Console.WriteLine("Z Matrix");

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < FacesZ.GetLength(1); j++)
                {
                    Console.Write("{0:N4}  ", FacesZ[i, j]);
                }
                Console.WriteLine();
            }

            double[,] zz = FacesZ.Dot(dimreduced);
            

            
                        
        }
    }
}

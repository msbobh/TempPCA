using System;
using Compression;
using Accord.Math;
using Accord.IO;
using temputils;

namespace ConTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // ***************** File read stuff

            double[,] Rawdata;
            double[,] labelsdata;
            int k = 100;

            Console.WriteLine("Principal Components Analysis compression\n");
            string testFname = args[0];

            using (CsvReader reader = new CsvReader(testFname, hasHeaders: false))
            {
                Rawdata = reader.ToMatrix();
            }
                        
            // Create a new PCA object and cacluate eigenvectors
            // Calls two differnt routines and provides timing
            SingleValueDecomposition PCA = new SingleValueDecomposition(Rawdata);
                        
            //Console.WriteLine(" First few eigen Vectors");
            /*for (int row = 0; row < 1; row++)
            {
                for (int col = 0; col < PCA.UAccordreduced.GetLength(1); col++)
                {
                    Console.Write("{0:N4}  ", PCA.UAccordreduced[row, col]);
                }
                Console.WriteLine();
            }*/

            // Create submatrix of k eigen vectors
            double[,] dimreduced = PCA.UAccordreduced.Get(startRow: 0, endRow: PCA.UAccordreduced.GetLength(0),
                                                          startColumn: 0, endColumn: k);
            // Re obtain the unchanged input matrix and convert to n x k

            double [,] projectionData;
            using (CsvReader reader = new CsvReader(testFname, hasHeaders: false))
            {
                projectionData = reader.ToMatrix();
            }
            projectionData = temputils.utils.FeatureNormalization(projectionData);
            // Write out Eigen Vectors
            using (CsvWriter writer = new CsvWriter("Uforoctave.csv"))
            {
                writer.Write(PCA.Ureduced);
            }
            double[,] FacesZ = PCA.Reduce(projectionData, Dimension: k, eigenVectors: dimreduced);
            
            Console.WriteLine("Done Compressing");
            

            double[,] zz = FacesZ.Dot(dimreduced);
            string filename = "PCAFaces.csv";
            using (CsvWriter writer = new CsvWriter(filename))
            {
                writer.Write(FacesZ);
            }
            




        }
    }
}

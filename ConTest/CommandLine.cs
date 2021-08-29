using System;
using UtilityFuncs;

namespace ConTest
{
    public class CommandLine
    {
        public int numargs { get; }
        public int K { get; }
        //public string [] CommandLineArgs;

        public bool usage = true; //True if proper usage 
        public bool FileOpenError = false;

        public CommandLine(string[] CommandLineArgs)
        {
            /* 
             * Process command line arguments, return variables and validate existence of files
             */

            /* Max of 2 parameters:
             * Input Matrix for for creating the Singlar Value Decomposition
             * K number of vectors to select for projecting data on a reduced space
             *
             */
            numargs = CommandLineArgs.Length;
            if (numargs > 2 | numargs < 1) { usage = false; return; }

            string InputMatrix = CommandLineArgs[0]; // Training file

            // Perform some file checking                        

            if (!externalFunc.checkFile(InputMatrix))
            {
                Console.WriteLine("Error opening file{0}", InputMatrix);
                FileOpenError = true;

            }
            if (!int.TryParse(CommandLineArgs[1], out int temp ))
            {
                Console.WriteLine("Error opening file {0}", CommandLineArgs[1]);
                FileOpenError = true;
            }
            K = temp;
        }
    }
}

using System.Net;
using System.Xml.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            var puzzleInput = puzzleInputToList (Constants.INPUT_FILENAME);
            Intcode i = new Intcode(puzzleInput);
            var outputString = String.Empty;
            while(!i.hasFinished)
            {
                var output = i.Run();
                var convertedOutput = asciiToChar(output);
                outputString = String.Concat(outputString, convertedOutput);
            }
            var test = outputString.Split("\n");
            var height = test.Length-2;
            char[][] chArray  = new char[height-1][];

            var sum = 0;
            var yCoordStart = 0;
            
            for (int j=0; j<height-1; j++)
            {
                chArray[j] = test[j].ToCharArray();
            }

            /*for (int y=1; y<chArray.Length-1; y++)
            {
                for (int x=1; x<chArray[0].Length-1; x++)
                {
                    if (chArray[y][x]=='#')
                    {
                        if (chArray[y-1][x]=='#' & 
                            chArray[y+1][x]=='#' &
                            chArray[y][x+1]=='#' &
                            chArray[y][x-1]=='#')
                            sum += (x*y);
                            //chArray[y][x] = 'O';
                    }
                }
            }*/
            foreach (var p in chArray)
                Console.WriteLine(String.Join("", p));

            //Console.WriteLine(sum);

            // Identify coordinates where scaffold char is surrounded by other scaffold chars
        }

        static List<Int64> puzzleInputToList (string inputFilePath) {
            var str = File.ReadLines (inputFilePath).First ();
            var listOfInts = str.Split (',').Select (Int64.Parse).ToList ();
            return listOfInts;
        }

        static char asciiToChar(long inputToConvert)
        {
            // Convert ASCII to corresponding character
            return (Convert.ToChar(inputToConvert)); //.ToString();
        }
    }
}

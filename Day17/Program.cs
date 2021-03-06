﻿using System;
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
            
            for (int j=0; j<height-1; j++)
            {
                chArray[j] = test[j].ToCharArray();
            }

            foreach (var p in chArray)
                Console.WriteLine(String.Join("", p));
            // Console.WriteLine(calculateSumForPartOne(chArray));
            
            // Identify coordinates where scaffold char is surrounded by other scaffold chars

            // Print out coordinate of current position marker
            var tupleCurrPosn = currPosnMarker(chArray);
            Console.WriteLine($"{tupleCurrPosn.Item1}, {tupleCurrPosn.Item2}");

        }

        static Tuple<int, int> currPosnMarker(char[][] chArray)
        {
            var tupleToReturn = new Tuple<int, int>(0, 0);
            for (int ycoord=0; ycoord<chArray.Length-1; ycoord++)
            {
                if (chArray[ycoord].Contains('^')|chArray[ycoord].Contains('<')|chArray[ycoord].Contains('>'))
                {
                    Console.WriteLine("Yes");
                    for (int xcoord=0; xcoord<chArray[ycoord].Length-1; xcoord++)
                    {
                        if (chArray[ycoord][xcoord]=='^')
                        {
                            tupleToReturn = new Tuple<int, int>(xcoord, ycoord);
                        } 
                    }
                }
            }
        return tupleToReturn;
        }

        static int calculateSumForPartOne(char[][] chArray)
        {
            // Code below is for calculating sum for part 1
            var sum = 0;
            for (int y=1; y<chArray.Length-1; y++)
            {
                for (int x=1; x<chArray[0].Length-1; x++)
                {
                    if (chArray[y][x]==Constants.SCAFFOLD)
                    {
                        if (chArray[y-1][x]==Constants.SCAFFOLD & 
                            chArray[y+1][x]==Constants.SCAFFOLD &
                            chArray[y][x+1]==Constants.SCAFFOLD &
                            chArray[y][x-1]==Constants.SCAFFOLD)
                            sum += (x*y);
                            //chArray[y][x] = 'O';
                    }
                }
            }
            return sum;
        }

        static List<Int64> puzzleInputToList (string inputFilePath) {
            var str = File.ReadLines (inputFilePath).First ();
            return str.Split (',').Select (Int64.Parse).ToList ();
        }

        static char asciiToChar(long inputToConvert)
        {
            // Convert ASCII to corresponding character
            return (Convert.ToChar(inputToConvert)); //.ToString();
        }
    }
}

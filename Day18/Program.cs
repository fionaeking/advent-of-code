using System.Net;
using System.Xml.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Day18
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputAsArray = readInputAsCoords(Constants.INPUT_FILENAME);

            var currLoc = new Tuple<int, int>(0, 0);
            //foreach (var line in inputAsArray)
            for(int y=0; y<inputAsArray.Length; y++)
            {
                for(int x=0; x<inputAsArray[0].Length; x++)
                {
                    if (inputAsArray[y][x]=='@')
                        currLoc = new Tuple<int, int>(x, y);

                }
            }
            Console.WriteLine(currLoc.Item1 + " " + currLoc.Item2);

            var ch = checkDirection('U', currLoc, inputAsArray);
            if (ch!='#')
            {
                if (Char.IsUpper(ch))
                {
                    // Check if have key 
                }
                else if (Char.IsLower(ch))
                {
                    // Collect key
                }
                else
                {
                    //Will be a full stop
                }
            }
            checkDirection('D', currLoc, inputAsArray);
            checkDirection('L', currLoc, inputAsArray);
            checkDirection('R', currLoc, inputAsArray);

            // Check UDRL from currLoc


            /*foreach (var line in inputAsArray)
            {
                foreach (var ch in line)
                {
                    if (Char.IsUpper(ch))
                    {
                        // Check if have key 
                    }
                    else if (Char.IsLower(ch))
                    {
                        // Collect key
                    }
                    else switch(ch)
                    {
                        case '@': break; //we are at location
                        case '#': break;
                        case '.': break;
                        default: break;
                    }
                }
            }*/
        }

        static char checkDirection(char dir, Tuple<int, int> currLoc, string[] inputAsArray)
        {
            switch(dir)
            {
                case 'U': 
                return inputAsArray[currLoc.Item2-1][currLoc.Item1];
                case 'D': 
                return inputAsArray[currLoc.Item2+1][currLoc.Item1];
                case 'L': 
                return inputAsArray[currLoc.Item2][currLoc.Item1-1];
                case 'R': 
                return inputAsArray[currLoc.Item2][currLoc.Item1+1];
                default: return 0;
            }
        }

        static string[] readInputAsCoords(string inputFilePath) 
        {
            return File.ReadLines(inputFilePath).ToArray();
            //var puzzleAsStringArray = File.ReadLines (inputFilePath);
            //return puzzleAsStringArray;
            /*foreach (var line in puzzleAsStringArray)
            {

            }*/

        }
    }
}

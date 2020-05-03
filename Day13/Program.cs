using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            Intcode i = new Intcode(puzzleInputToList(Constants.INPUT_FILENAME));
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //int[] tileCounter = new int[5];
            Dictionary<Tuple<int, int>, int> toDraw = new Dictionary<Tuple<int, int>, int>();
            int score = 0;
            while (!i.hasFinished)
            {
                // Declaring separate variables to improve readability
                var xPosn = Convert.ToInt32(i.Run());
                var yPosn = Convert.ToInt32(i.Run());
                var tile = Convert.ToInt32(i.Run());
                if(xPosn==-1 & yPosn==0)
                {
                    score = tile;
                }
                else
                {
                    toDraw[new Tuple<int, int>(xPosn, yPosn)] = tile;
                    switch(tile)
                    {
                        case 3: 
                        i.paddlePosn = xPosn; 
                        break;
                        case 4:
                        i.ballPosn = xPosn;
                        break;
                        case 0:
                        case 1:
                        case 2:
                        break;
                        default:
                        return;  // End of program 
                    }
                }
                drawGame(toDraw, score);
            }
        }

        static void drawGame(Dictionary<Tuple<int, int>, int> pointsToDraw, int score)
        {
            // Use StringBuilder to reduce console flicker
            StringBuilder s = new StringBuilder();
            Console.Clear();
            Console.SetWindowSize(43, 21);
            Console.CursorVisible = false;
            var sortedList = pointsToDraw.OrderBy(key => key.Key.Item2).ThenBy(key => key.Key.Item1);
            int maxY = Int32.MinValue;
            int minX = Int32.MaxValue;
            foreach (var kvp in sortedList)
            {
                // This seems inefficient - could remove maxY but not minX
                maxY = Math.Max(kvp.Key.Item2, maxY);
                minX = Math.Min(kvp.Key.Item1, minX);
            }
            
            int currY = maxY;
            int currX = minX;
            List<List<string>> finalImage = new List<List<string>>();
            List<string> lineOfImage = new List<string>();
            foreach (var kvp in sortedList)
            {
                if(kvp.Key.Item2!=currY)
                {
                    finalImage.Add(lineOfImage);
                    //Reset variables
                    lineOfImage = new List<string>();
                    currX = minX;
                    currY = kvp.Key.Item2;
                }
                lineOfImage.Add(getTile(kvp.Value));
                currX++;
            }

            finalImage.Add(lineOfImage);
            foreach (var line in finalImage)
            {
                s.Append(String.Join("", line));
                s.Append("\n");
            }
            s.Append("\nScore: " + score);
            Console.WriteLine(s);
        }

        static string getTile(int tile)
        {
            switch(tile)
            {
                case 0: return " ";  //empty
                case 1: return $"{"\u25A8"}"; //wall
                case 2: return $"{"\u25A1"}";  //block
                case 3: return "_";  //horizontal paddle
                case 4: return "o";  // ball
                default: return " ";
            }
        }

        static List<Int64> puzzleInputToList (string inputFilePath) 
        {
            var str = File.ReadLines (inputFilePath).First ();
            return str.Split (',').Select (Int64.Parse).ToList ();
        }
    }
}
using System.Xml.Linq;
using System.Text;
using System.Diagnostics;
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
         var puzzleInput = puzzleInputToList (Constants.INPUT_FILENAME);
            Intcode i = new Intcode(puzzleInput);
            int[] tileCounter = new int[5];
            Dictionary<Tuple<int, int>, int> toDraw = new Dictionary<Tuple<int, int>, int>();
            long score = 0;
            int iter = 0;
            while (!i.hasFinished)
            {
                // Declaring separate variables to improve readability
                var xPosn = i.Run ();
                var yPosn = i.Run();
                var tile = i.Run();
                if(xPosn==-1 & yPosn==0)
                {
                    score = tile;
                }
                else
                {
                    toDraw[new Tuple<int, int>(Convert.ToInt32(xPosn), Convert.ToInt32(yPosn))] = Convert.ToInt32(tile);
                    if (0<=tile & tile<=4)
                    {
                        tileCounter[tile] += 1;
                        if (tile==3)
                        {
                            i.paddlePosn = xPosn;
                        }
                        else if (tile==4)
                        {
                            i.ballPosn = xPosn;
                        }
                    }
                    else
                    {
                        // End of program
                        break;
                    }
                }
                //if (iter ==2)
                //{
                drawGame(toDraw, Convert.ToInt32(score));
                //Console.WriteLine("\nScore: " + score);
                //iter = 0;
                //}
                //else
                //    iter++;
                
            }
            //drawGame(toDraw);
            //Console.WriteLine(tileCounter[2]);
        }

        static void drawGame(Dictionary<Tuple<int, int>, int> pointsToDraw, int score)
        {
            Console.Clear();
            Console.SetWindowSize(43, 21);

            StringBuilder s = new StringBuilder();

            //Console.SetCursorPosition(0, 0); //1);
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
                //Console.WriteLine(String.Join("", line));
            }
            //Console.WriteLine("\nScore: " + score);
            s.Append("\nScore: " + score);
            Console.WriteLine(s);
        }

        static string getTile(int tile)
        {
            switch(tile)
            {
                case 0:
                //empty
                return " ";
                case 1:
                //wall
                return "W";
                case 2:
                //block
                return "+";
                case 3:
                //horizontal paddle
                return "_";
                case 4:
                // ball
                return "o";
                default:
                return " ";
            }
        }

        static List<Int64> puzzleInputToList (string inputFilePath) {
            var str = File.ReadLines (inputFilePath).First ();
            var listOfInts = str.Split (',').Select (Int64.Parse).ToList ();
            return listOfInts;
        }
    }
}
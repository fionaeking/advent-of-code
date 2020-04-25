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
            Intcode i = new Intcode (puzzleInput);
            int[] tileCounter = new int[5];
            List<List<int>> toDraw = new List<List<int>>();
            while (!i.hasFinished)
            {
                var xPosn = i.Run ();
                var yPosn = i.Run();
                var tile = i.Run(); 
                toDraw.Add(new List<int>(){Convert.ToInt32(xPosn), Convert.ToInt32(yPosn), Convert.ToInt32(tile)});
                tileCounter[tile] += 1;       
            }
            var sortedList = toDraw.OrderBy(key => key[1]).ThenBy(key => key[0]);


            int maxY = Int32.MinValue;
        int minX = Int32.MaxValue;
        foreach (var kvp in sortedList)
        {
            // This seems inefficient - could remove maxY but not minX
            maxY = Math.Max(kvp[1], maxY);
            minX = Math.Min(kvp[0], minX);
        }
        int currY = maxY;
        int xCount = minX;
        List<List<string>> finalImage = new List<List<string>>();
        List<string> lineOfImage = new List<string>();
        foreach (var kvp in sortedList)
        {
            if(kvp[1]!=currY)
            {
                finalImage.Add(lineOfImage);
                //Reset variables
                lineOfImage = new List<string>();
                xCount = minX;
                currY = kvp[1];
            }
            /*while (kvp[1] > xCount)
            {
                lineOfImage.Add(0);
                xCount++;
            }*/

            lineOfImage.Add(getTile(kvp[2]));
            xCount++;
        }
        finalImage.Add(lineOfImage); // Add final line (N.B. I forgot this initially!)

        foreach (var line in finalImage)
        {
            Console.WriteLine(String.Join("", line)); //.Replace('0', ' '));
        }


        //Console.WriteLine(tileCounter[2]);
            
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
                return "#";
                case 3:
                //horizontal padde
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
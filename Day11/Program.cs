using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            var puzzleInput = puzzleInputToList (Constants.INPUT_FILENAME);
            Intcode i = new Intcode (puzzleInput);
            Robot r = new Robot();
            while (!i.hasFinished)
            {
                var firstOutput = i.Run ();
                var secondOutput = i.Run();
                // We can convert as the values will only be 0 or 1
                i.nextInput = r.newPointandDirection(Convert.ToInt32(firstOutput), Convert.ToInt32(secondOutput));
            }
            Console.WriteLine("Number of panels painted: " + r.getNumberPanelsPainted());
            
        }

            static List<Int64> puzzleInputToList (string inputFilePath) {
            var str = File.ReadLines (inputFilePath).First ();
            var listOfInts = str.Split (',').Select (Int64.Parse).ToList ();
            return listOfInts;
        }
    }
}

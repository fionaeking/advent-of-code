using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var puzzleInput = puzzleInputToList(Constants.INPUT_FILENAME);
            Intcode i = new Intcode(puzzleInput);
            i.Run();
        }

        static List<int> puzzleInputToList(string inputFilePath)
        {
            var str = File.ReadLines(inputFilePath).First();
            var listOfInts = str.Split(',').Select(int.Parse).ToList();
            return listOfInts;
        }
    }
}

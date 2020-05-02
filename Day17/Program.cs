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
            Console.WriteLine(outputString);
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

using System.Net;
using System.Xml.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day22
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputRange = 10;
            var inputList = Enumerable.Range(0, inputRange).ToArray();
            IEnumerable<string> inputInstructions = puzzleInputToList("Input.txt");

            var dealer = new Dealer(inputList);
            foreach(var instr in inputInstructions)
            {
                performShufflingTechnique(dealer, instr);
            }
            dealer.printOutCardDeck();
            //Console.WriteLine(dealer.getPositionOfValue(2019));
        }

        static IEnumerable<string> puzzleInputToList (string inputFilePath) 
        {
            return File.ReadLines (inputFilePath);
        }

        static void performShufflingTechnique(Dealer d, string instr)
        {
            if (instr.StartsWith("cut"))
            {
                d.cut(Int32.Parse(instr.Replace("cut ", "")));
            }
            else if (instr.EndsWith("stack"))
            {
                d.dealIntoNewStack();
            }
            else
            {
                d.dealWithIncrement(Int32.Parse(instr.Replace("deal with increment ", "")));
            }
        }
    
    }

}

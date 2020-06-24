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
            //var inputList = new int[]{0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            var inputList = Enumerable.Range(0, 10007).ToArray();
            var d = new Dealer(inputList);
            IEnumerable<string> i = puzzleInputToList("Input.txt");

            foreach(var t in i)
            {
                if (t.StartsWith("cut"))
                {
                    d.cut(Int32.Parse(t.Replace("cut ", "")));
                }
                else if (t.EndsWith("stack"))
                {
                    d.dealIntoNewStack();
                }
                else
                {
                    d.dealWithIncrement(Int32.Parse(t.Replace("deal with increment ", "")));
                }
            }
            
            Console.WriteLine(d.getPositionOfValue(2019));
        }

        static IEnumerable<string> puzzleInputToList (string inputFilePath) 
        {
            var str = File.ReadLines (inputFilePath);
            return str;
        }
    
    }

}

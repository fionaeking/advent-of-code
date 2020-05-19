using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            var puzzleInput = puzzleInputToList(Constants.INPUT_FILENAME);
            var startingList = new List<List<int>>(){new List<int>(){1}, 
                                                              new List<int>(){2}, 
                                                              new List<int>(){3}, 
                                                              new List<int>(){4}};
            var count = fewestMovesToOxygen(puzzleInput, startingList);
            Console.WriteLine("Min value is " + count);
            puzzleInput = puzzleInputToList(Constants.INPUT_FILENAME);
            forPartTwo(puzzleInput);
        }

        static void addElement(List<List<int>> listOfMoves, List<int> elem, int numToAdd)
        {
            listOfMoves.Add(new List<int>(elem).Concat(new List<int>(){numToAdd}).ToList());
        }

        static void buildUpFunction(List<List<int>> listOfMoves)
        {
            // Need to use copy of input
            foreach (var elem in new List<List<int>>(listOfMoves))
            {
                switch(elem.Last())
                {
                    case 1:
                    case 2:
                    addElement(listOfMoves, elem, elem.Last());
                    addElement(listOfMoves, elem, 3);
                    addElement(listOfMoves, elem, 4);
                    break;
                    case 3:
                    case 4:
                    addElement(listOfMoves, elem, 1);
                    addElement(listOfMoves, elem, 2);
                    addElement(listOfMoves, elem, elem.Last());   
                    break;
                    default:
                    break;
                }
                listOfMoves.Remove(elem);
            }
        }

        static List<Int64> puzzleInputToList (string inputFilePath) 
        {
            var str = File.ReadLines (inputFilePath).First ();
            return str.Split (',').Select (Int64.Parse).ToList ();
        }

        static int fewestMovesToOxygen(List<long> puzzleInput, List<List<int>> startingList)
        {
            long xPosn = 0;
            int count = 0;
            while(xPosn!=2)
            {
                buildUpFunction(startingList);
                foreach (var inputValue in new List<List<int>>(startingList))
                {       
                    count = 0;
                    Intcode i = new Intcode(puzzleInput, inputValue);
                    while (!i.hasFinished)
                    {
                        xPosn = i.Run();
                        count++;
                        if (xPosn!=1)
                        {
                            if (!i.hasFinished)  // This is required if xPosn is 0
                                startingList.Remove(inputValue);
                            if (xPosn==2)
                            {
                                Console.WriteLine(String.Join(",", inputValue));
                            }
                            break;
                        }
                    }
                }
            }
            return count;
        }

        static void forPartTwo(List<long> puzzleInput) //, List<List<int>> startingList)
        {
            
            int count = 0;
            Dictionary<Tuple<int, int>, int> outputAsDict = new Dictionary<Tuple<int, int>, int>();
            long outputReturned = 0;
            Tuple<int, int> coOrd = new Tuple<int, int>(0,0);
            //while(xPosn!=2)
            //{
                //buildUpFunction(startingList);
                //foreach (var inputValue in new List<List<int>>(startingList))
                //{
                    //List<int> inputValue = new List<int>(){1,1,1,1,4,4,1,1,4,4,2,2,2,2,4,4,4,4,2,2,3,3,3,3,3,3,2,2,4,4,4,4,2,2,2,2,2,2,4,4,2,2,3,3,2,2,4,4,2,2,3,3,2,2,2,2,3,3,1,1,3,3,1,1,4,4,1,1,1,1,3,3,1,1,3,3,1,1,1,1,1,1,3,3,1,1,3,3,1,1,4,4,1,1,1,1,3,3,2,2,3,3,3,3,1,1,1,1,1,1,4,4,4,4,2,2,4,4,1,1,4,4,2,2,4,4,4,4,1,1,3,3,1,1,4,4,4,4,1,1,4,4,2,2,4,4,1,1,4,4,1,1,3,3,1,1,3,3,1,1,4,4,4,4,4,4,4,4,4,4,2,2,2,2,3,3,1,1,3,3,2,2,2,2,2,2,2,2,3,3,2,2,4,4,4,4,4,4,2,2,3,3,3,3,3,3,2,2,3,3,2,2,4,4,2,2,2,2,3,3,3,3,2,2,4,4,4,4,4,4,1,1,4,4,2,2,4,4,2,2,3,3,2,2,4,4,2,2,3,3,3,3,3,3,1,1,3,3,2,2,2,2,2,2,4,4,2,2,2,2,4,4,1,1,1,1,1,1,4,4,4,4,2,2,3,3,2,2,4,4,2,2,3,3};        
                    //List<int> inputValue = new List<int>(){1,1,1,1,3,2,2,2,2,3,1,1,1,1,3,2,2,2,2,3,1,1,1,1,3,2,2,2,2,3,1,1,1,1,3,2,2,2,2,3,1,1,1,1,3,2,2,2,2,3,1,1,1,1,3,2,2,2,2};
                    List<int> inputValue = new List<int>(){}; //4,4,4,4,1,3,3,3,3,3,1,4,4,4,4,4,1,3,3,3,3,3,1,4,4,4,4,4};
                    inputValue.AddRange(Enumerable.Repeat(4, 10));
                    inputValue.Add(1);
                    inputValue.AddRange(Enumerable.Repeat(3, 10));
                    inputValue.Add(1);
                    inputValue.AddRange(Enumerable.Repeat(4, 10));
                    inputValue.Add(1);
                    inputValue.AddRange(Enumerable.Repeat(3, 10));
                    inputValue.Add(1);
                    inputValue.AddRange(Enumerable.Repeat(4, 10));
                    // Need to track when input value has been used (as this dictates movement)
                    // but also what the output value is (as this dictates the output symbol at that location)
                    count = 0;
                    Intcode i = new Intcode(puzzleInput, inputValue);
                    while (!i.hasFinished | inputValue.Count>1)
                    {
                        //var movement = i.newList.FirstOrDefault();
                        //outputReturned = i.Run();
                        count++;
                        //Console.WriteLine(inputValue.First());
                        if (inputValue.Count<1) break;
                        coOrd = updateLocation(inputValue.First(), coOrd);
                        //Console.WriteLine(inputValue.Count);
                        inputValue.RemoveAt(0);
                        outputAsDict[coOrd] = Convert.ToInt32(i.Run()); //outputReturned;
                        /*if (xPosn!=1)
                        {
                            if (!i.hasFinished)  // This is required if xPosn is 0
                                startingList.Remove(inputValue);
                            break;
                        }*/
                    }
                //}
            //}
            StringBuilder s = new StringBuilder();
            var sortedList = outputAsDict.OrderBy(key => key.Key.Item2).ThenBy(key => key.Key.Item1);
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
                lineOfImage.Add(Convert.ToString(kvp.Value)); //TODO - change
                currX++;
            }
            finalImage.Add(lineOfImage);
            foreach (var line in finalImage)
            {
                s.Append(String.Join("", line));
                s.Append("\n");
            }
            Console.WriteLine(s);
            //return count;
        }

        static Tuple<int, int> updateLocation(int num, Tuple<int, int> prevCoord)
        {
            switch(num)
            {
                case 1:
                //north
                return new Tuple<int, int>(prevCoord.Item1, prevCoord.Item2+1);
                case 2:
                //south
                return new Tuple<int, int>(prevCoord.Item1, prevCoord.Item2-1);
                case 3:
                //west
                return new Tuple<int, int>(prevCoord.Item1-1, prevCoord.Item2);
                case 4:
                //east
                return new Tuple<int, int>(prevCoord.Item1+1, prevCoord.Item2);
                default:
                //Console.WriteLine(num);
                throw new Exception("Unrecognised direction");
            }
        }
    }
}
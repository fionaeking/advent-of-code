using System.Net;
using System.Xml.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day22 
{
        
class Dealer { 

        private int[] inputArray;
        public Dealer(int[] inputList)
        {
            inputArray = inputList;
        }

        public void dealIntoNewStack() 
        {
            Array.Reverse(inputArray);
        }

        public void cut(int N) 
        {
            int numToSkip = (N>=0) ? N : inputArray.Length + N;
            var tempArray = inputArray.Skip(numToSkip).ToArray(); 
            inputArray.Take(numToSkip).ToArray().CopyTo(inputArray, tempArray.Length);
            tempArray.CopyTo(inputArray, 0);
        }

        public void dealWithIncrement(int N)
        {
            var tempArray = new int[inputArray.Length];
            int indexToWrite;
            for (int i=0; i<inputArray.Length; i++)
            {
                indexToWrite = (i * N) % (inputArray.Length);
                tempArray[indexToWrite] = inputArray[i];
            }
            inputArray = tempArray;
        }

        public void printOutCardDeck()
        {
            Console.WriteLine(String.Join(",", inputArray));
        }

        public int getPositionOfValue(int val)
        {
            return Array.IndexOf(inputArray, val);
        }
    }
}
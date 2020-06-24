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
            var outputArray = new int[inputArray.Length];
            int numToSkip = (N>=0) ? N : inputArray.Length + N;

            var x = inputArray.Skip(numToSkip).ToArray(); 
            inputArray.Take(numToSkip).ToArray().CopyTo(outputArray, x.Length);
            x.CopyTo(outputArray, 0);

            inputArray = outputArray;
        }

        public void dealWithIncrement(int N)
        {
            var outputArray = new int[inputArray.Length];
            int indexToWrite;
            for (int i=0; i<inputArray.Length; i++)
            {
                indexToWrite = (i * N) % (inputArray.Length);
                outputArray[indexToWrite] = inputArray[i];
            }
            inputArray = outputArray;
        }

        public int getPositionOfValue(int val)
        {
            return Array.IndexOf(inputArray, val);
        }
    }
}
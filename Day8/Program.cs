using System;
using System.Collections.Generic;
using System.IO;

namespace Day8
{
    class Program //1995 is too high
    {
        static void Main(string[] args)
        {
            string inputString = File.ReadAllText("Input.txt");
            var width = 25;
            var tall = 6;
            var layers = getLayers(inputString, width, tall);
            var answer = returnOneTimesTwoDigits(layers);
            Console.WriteLine(answer);
        }


        static List<List<string>> getLayers(string inputString, int width, int tall)
        {
            var layers = new List<List<string>>();
            var firstList = new List<string>();
            var count = 0;
            for (int i = 0; i < inputString.Length; i += width)
            {
                count++;
                firstList.Add(inputString.Substring(i, width));
                if (count % tall == 0)
                {
                    //New layer
                    layers.Add(firstList);
                    firstList = new List<string>();
                }
            }
            return layers;
        }

        static int returnOneTimesTwoDigits(List<List<string>> layers)
        {
            var zeroCount = 0;
            var minCount = Int32.MaxValue;
            var oneCount = 0;
            var twoCount = 0;
            var answer = 0;

            foreach (var l in layers)
            {
                foreach (var p in l)
                {
                    foreach (var s in p)
                    {
                        if (s == '0')
                            zeroCount++;
                        else if (s == '1')
                            oneCount++;
                        else if (s == '2')
                            twoCount++;
                    }
                }

                if (zeroCount < minCount)
                {
                    minCount = zeroCount;
                    answer = oneCount * twoCount;
                }
                zeroCount = 0;
                oneCount = 0;
                twoCount = 0;
            }
            return answer;
        }

    }
}

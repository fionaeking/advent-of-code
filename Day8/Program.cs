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
            //string inputString = "0222112222120000";
            var width = 25;
            var tall = 6;
            var layers = getLayers(inputString, width, tall);

            // Part 1
            // var answer = returnOneTimesTwoDigits(layers);
            // Console.WriteLine(answer);

            List<char> finalImage = new List<char>();

            for (int str = 0; str < layers[0].Count; str++)
            {
                for (int ch = 0; ch < layers[0][0].Length; ch++)
                {
                    for (int layer = 0; layer < layers.Count; layer++)
                    {
                        // FIrst 0 - Select list for first layer
                        // Second 0 - Select first string in that list
                        // Third 0 -  Select first character in that string
                        if (layers[layer][str][ch] != '2')
                        {
                            finalImage.Add(layers[layer][str][ch]);
                            Console.WriteLine(layers[layer][str][ch]);
                            break;
                        }
                    }
                }
            }
            string finalImageAsString = String.Join("", finalImage);

            for (int i = 0; i < finalImageAsString.Length; i += width)
            {
                Console.WriteLine(finalImageAsString.Substring(i, width).Replace('0', ' '));
            }
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

            foreach (var layer in layers)
            {
                foreach (var str in layer)
                {
                    foreach (var ch in str)
                    {
                        if (ch == '0')
                            zeroCount++;
                        else if (ch == '1')
                            oneCount++;
                        else if (ch == '2')
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

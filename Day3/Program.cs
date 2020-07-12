using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Program to find the intersection point closest to the origin of two wires
// N.B. Very over-complicated!

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tuple<int, int>> coordsWireOne = getPuzzleInputCoordinates(Constants.inputOne);
            List<Tuple<int, int>> coordsWireTwo = getPuzzleInputCoordinates(Constants.inputTwo);
            outputManhattanDistance(coordsWireOne, coordsWireTwo);
        }

        static List<Tuple<int, int>> getPuzzleInputCoordinates(string inputFilepath)
        {
            List<string> inputList = puzzleInputToList(inputFilepath);
            return getCoordinates(inputList);
        }

        static List<string> puzzleInputToList(string inputFilePath)
        {
            var str = File.ReadLines(inputFilePath).First();
            return str.Split(',').ToList();
        }

        static List<Tuple<int, int>> getCoordinates(List<string> inputList)
        {
            var listOfCoords = new List<Tuple<int, int>> { Tuple.Create(0, 0) };
            string nextInstruction;
            for (int i = 0; i < inputList.Count; i++)
            {
                nextInstruction = inputList[i];
                Tuple<int, int> nextCoord = applyInstruction(nextInstruction, listOfCoords.Last());
                listOfCoords.Add(nextCoord);
            }
            return listOfCoords;
        }

        static Tuple<int, int> applyInstruction(string instruction, Tuple<int, int> previousCoords)
        {
            char direction = instruction[0];
            int magnitude;
            Int32.TryParse(instruction.TrimStart(direction).ToString(), out magnitude);
            switch (direction)
            {
                case 'U': return Tuple.Create(previousCoords.Item1, previousCoords.Item2 + magnitude);
                case 'D': return Tuple.Create(previousCoords.Item1, previousCoords.Item2 - magnitude);
                case 'L': return Tuple.Create(previousCoords.Item1 - magnitude, previousCoords.Item2);
                case 'R': return Tuple.Create(previousCoords.Item1 + magnitude, previousCoords.Item2);
                default: throw new Exception("Unrecognised input");
            }
        }

        static Tuple<int, int> returnIntersection(Tuple<int, int> A, Tuple<int, int> B, Tuple<int, int> C, Tuple<int, int> D)
        {
            // I have definitely overcomplicated! Should have used info that y = c or x = c
            if ((D.Item2 < B.Item2 & D.Item2 < A.Item2 & C.Item2 < B.Item2 & C.Item2 < A.Item2)
            | (D.Item2 > B.Item2 & D.Item2 > A.Item2 & C.Item2 > B.Item2 & C.Item2 > A.Item2)
            | (D.Item1 < B.Item1 & D.Item1 < A.Item1 & C.Item1 < B.Item1 & C.Item1 < A.Item1)
            | (D.Item1 > B.Item1 & D.Item1 > A.Item1 & C.Item1 > B.Item1 & C.Item1 > A.Item1))
            {
                return null;
            }

            int A1 = B.Item2 - A.Item2;
            int B1 = A.Item1 - B.Item1;
            int C1 = A1 * A.Item1 + B1 * A.Item2;

            int A2 = D.Item2 - C.Item2;
            int B2 = C.Item1 - D.Item1;
            int C2 = A2 * C.Item1 + B2 * C.Item2;

            int delta = A1 * B2 - A2 * B1;
            if (delta == 0)
            {
                return null;
            }
            else
            {
                int x = (B2 * C1 - B1 * C2) / delta;
                int y = (A1 * C2 - A2 * C1) / delta;
                return ((x == 0 & y == 0) | x < 0 | y < 0) ? null : Tuple.Create(x, y);
            }
        }

        static void outputManhattanDistance(List<Tuple<int, int>> coordsWireOne, List<Tuple<int, int>> coordsWireTwo)
        {
            var minDistance = int.MaxValue;
            var minSteps = int.MaxValue;
            var stepCount = 0;
            var stepsOne = 0;
            for (int i = 0; i < coordsWireOne.Count - 1; i++)
            {
                var stepsTwo = 0;
                for (int j = 0; j < coordsWireTwo.Count - 1; j++)
                {
                    var a = returnIntersection(coordsWireOne[i], coordsWireOne[i + 1], coordsWireTwo[j], coordsWireTwo[j + 1]);
                    if (a != null)
                    {
                        stepCount = stepsOne + stepsTwo + Math.Abs(a.Item1 - coordsWireOne[i].Item1) + Math.Abs(a.Item1 - coordsWireTwo[j].Item1)
                        + Math.Abs(a.Item2 - coordsWireOne[i].Item2) + Math.Abs(a.Item2 - coordsWireTwo[j].Item2);
                        //Console.WriteLine("Intersection");
                        //intersections.Add(a);
                        minDistance = Math.Min(Math.Abs(a.Item1 + a.Item2), minDistance);
                        minSteps = Math.Min(stepCount, minSteps);
                    }
                    stepsTwo += Math.Abs(coordsWireTwo[j + 1].Item1 - coordsWireTwo[j].Item1);
                    stepsTwo += Math.Abs(coordsWireTwo[j + 1].Item2 - coordsWireTwo[j].Item2);
                }
                stepsOne += Math.Abs(coordsWireOne[i + 1].Item1 - coordsWireOne[i].Item1);
                stepsOne += Math.Abs(coordsWireOne[i + 1].Item2 - coordsWireOne[i].Item2);
            }
            Console.WriteLine($"The Manhattan distance is {minDistance}");
            Console.WriteLine($"The best steps value is {minSteps}");
        }

    }
}

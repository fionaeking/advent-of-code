using System;
using System.Collections.Generic;
using System.IO;

// Jupiter's Moons

// --- Part 2 ---
// Each axis changes independently of the other axes
// Find period of oscillation for each axis
// Then find Least Common Multiple of those 3 periods
// This will be timestep where the state matches the first state

// Lessons learned - need to REALLY think how you can reduce complexity! (think outside the box)


namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] answers = new long[3];

            for (int posn=0; posn<3; posn++)
            {
                List<List<int>> inputList = puzzleInputToList(Constants.INPUT_FILENAME);
                Moon[] listOfMoons = new Moon[4];
                listOfMoons[0] = new Moon(inputList[0]);
                listOfMoons[1] = new Moon(inputList[1]);
                listOfMoons[2] = new Moon(inputList[2]);
                listOfMoons[3] = new Moon(inputList[3]);

                string firstVals = addFirstMoonValsToList(listOfMoons, posn);
                for (long timestep=0; timestep < Constants.TIMESTEP; timestep++)
                {
                    //Compare pairs - 6 combinations
                    callUpdateVelocities(listOfMoons, posn);
                    for (int j=0; j<4; j++)
                    {
                        listOfMoons[j].updatePosition();
                    }

                    string moonValsFirst = addFirstMoonValsToList(listOfMoons, posn);
                    if (firstVals == moonValsFirst){
                        Console.WriteLine("here timestamp: " + (timestep+1));
                        answers[posn] = timestep+1;
                        break;
                    }
                }
            }

            var startVal = Math.Max(answers[0], answers[1]);
            startVal = Math.Max(startVal, answers[2]);
            var currVal = startVal;

            while(true)
            {
                if (currVal%answers[0]==0 & currVal%answers[1]==0 & currVal%answers[2]==0)
                {
                    Console.WriteLine("LCM is " + currVal);
                    break;
                }
                currVal += startVal;
            }
            //var totalEnergy = returnTotalEnergy(listOfMoons);
            //Console.WriteLine("Total energy: " + totalEnergy);
        }

        static string addFirstMoonValsToList(Moon[] moonList, int j)
        {
            var str = "";
            for (int i=0; i<4; i++)
            {
                str = String.Concat(str, String.Join(' ', moonList[i].returnFirstAsList(j)));
                str = str + " ";
            }
            return str;
        }

        static string addValuesToList(Moon[] moonList)
        {
            var str = "";
            for (int i=0; i<4; i++)
            {
                str = str.Insert(str.Length, String.Join(',',moonList[i].returnValuesAsList()));
            }
            return str;
        }

        static void callUpdateVelocities(Moon[] moonList, int k)
        {
            for (int i=0; i<moonList.Length; i++)
            {
                for (int j = i+1; j<moonList.Length; j++) 
                {
                    updateVelocities(moonList[i], moonList[j], k);
                }
            }
        }

        static void updateVelocities(Moon moonOne, Moon moonTwo, int i)
        {
            Tuple<int, int>[] posn = new Tuple<int, int>[3];
            posn[0] = new Tuple<int, int>(moonOne.Px, moonTwo.Px);
            posn[1] = new Tuple<int, int>(moonOne.Py, moonTwo.Py);
            posn[2] = new Tuple<int, int>(moonOne.Pz, moonTwo.Pz);
            
            if (posn[i].Item1!=posn[i].Item2)
            {
                if (posn[i].Item1 < posn[i].Item2)
                {
                    moonOne.updateVelocity(i, 1);
                    moonTwo.updateVelocity(i, -1);
                }
                else if (posn[i].Item1 > posn[i].Item2)
                {
                    moonOne.updateVelocity(i, -1);
                    moonTwo.updateVelocity(i, 1);
                }
            }   
        }

        static int returnTotalEnergy(Moon[] listOfMoons)
        {
            return listOfMoons[0].calcTotalEnergy() + listOfMoons[1].calcTotalEnergy() 
                    + listOfMoons[2].calcTotalEnergy() + listOfMoons[3].calcTotalEnergy();
        }

        static List<List<int>> puzzleInputToList (string inputFilePath) {
            List<List<int>> allMoonPosns = new List<List<int>>();
            foreach (var str in File.ReadLines(inputFilePath))
            {
                var substr = str.Remove(str.Length-1).Split(',');
                List<int> oneMoonPosn = new List<int>();
                foreach (var each in substr)
                {
                    oneMoonPosn.Add(Convert.ToInt32(each.Split('=')[1]));
                }
                allMoonPosns.Add(oneMoonPosn);
            }
            return allMoonPosns;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

// Emergency hull painting robot

// Input: Colour to paint, direction to turn
// Output: Colour of current panel

//Functionality:
//Move, detect colour and paint
//Move forward one panel if turned

class Robot
{
    Tuple<int, int> currPoint;
    int currDirection;

    Dictionary<Tuple<int, int>, int> panelsPainted;
    public Robot()
    {
        currPoint = new Tuple<int, int>(0,0);
        currDirection = Constants.UPZERO;
        panelsPainted = new Dictionary<Tuple<int, int>, int>();
        panelsPainted.Add(new Tuple<int, int>(0,0), 1);
    }

    void paintPanel(int inputVal)
    {
        panelsPainted[currPoint] = inputVal;
    }

    int directionToTurn(int inputVal)
    {
        return (inputVal==0) ? -90 : 90;
    }

    void calculateNewPoint()
    {
        switch(currDirection)
        {
            case Constants.UPZERO:
            case Constants.UP360:
                currPoint = new Tuple<int, int>(currPoint.Item1, currPoint.Item2 + 1);
                break;
            case Constants.LEFT:
                currPoint = new Tuple<int, int>(currPoint.Item1 - 1, currPoint.Item2);
                break;
            case Constants.DOWN:
                currPoint = new Tuple<int, int>(currPoint.Item1, currPoint.Item2 - 1);
                break;
            case Constants.RIGHT:
                currPoint = new Tuple<int, int>(currPoint.Item1 + 1, currPoint.Item2);
                break;
            default:
                throw new Exception("Error - unrecognised direction");
        }
    }

    int colourOfCurrPanel()
    {
        return panelsPainted.ContainsKey(currPoint) ? panelsPainted[currPoint] : 0; //black - default
    }

    public int newPointandDirection(int inputVal, int inputValTwo)
    {
        paintPanel(inputVal);
        currDirection += directionToTurn(inputValTwo) + 360;
        currDirection %= 360;
        calculateNewPoint();
        return colourOfCurrPanel();
    }

    public int getNumberPanelsPainted()
    {
        return panelsPainted.Count;
    }

    public void printOutPanels()
    {
        var listAnglesOrdered = panelsPainted.OrderBy(key => key.Key); //.ToDictionary(key => key.Key);
        int maxY = 0;
        int minY = 1000;
        int maxX = 0;
        int minX = 1000;
        foreach (var kvp in listAnglesOrdered)
        {
            maxY = Math.Max(kvp.Key.Item2, maxY);
            maxX = Math.Max(kvp.Key.Item1, maxX);
            minY = Math.Min(kvp.Key.Item2, minY);
            minX = Math.Min(kvp.Key.Item1, minX);
        }

        int prevX = minX;
        int yCount = minY;
        List<List<int>> finalImage = new List<List<int>>();
        List<int> imageLine = new List<int>();
        foreach (var kvp in listAnglesOrdered)
        {
            if(kvp.Key.Item1!=prevX)
            {
                //Console.WriteLine(kvp.Key.Item1);
                //check length of imageLine
                while (imageLine.Count < (maxX-minX))
                {
                    imageLine.Add(0);
                }
                finalImage.Add(imageLine);
                imageLine = new List<int>();
                yCount = minY;
                prevX = kvp.Key.Item1;
            }
            while (kvp.Key.Item2 != yCount)
            {
                imageLine.Add(0); //chcek what default should be
                yCount++;
            }
            imageLine.Add(kvp.Value);
        }

        foreach (var line in finalImage)
        {
            Console.WriteLine(String.Join("", line).Replace('0', ' '));
        }
        //Console.WriteLine(maxY);
        
          /*List<char> finalImage = new List<char>();

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
            }*/
    }
   
}
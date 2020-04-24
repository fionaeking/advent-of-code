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
        var listAnglesOrdered = panelsPainted.OrderByDescending(key => key.Key.Item2).ThenBy(key => key.Key.Item1);; //.ToDictionary(key => key.Key);
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

        foreach (var kvp in listAnglesOrdered)
        {
            Console.WriteLine(kvp.Key);
        }

        int prevY = maxY;
        int xCount = minX;
        List<List<int>> finalImage = new List<List<int>>();
        List<int> imageLine = new List<int>();
        foreach (var kvp in listAnglesOrdered)
        {
            if(kvp.Key.Item2!=prevY)
            {
                //check length of imageLine
                while (imageLine.Count < (maxX-minX))
                {
                    Console.WriteLine("Pad");
                    imageLine.Add(0);
                }
                finalImage.Add(imageLine);
                imageLine = new List<int>();
                xCount = minX;
                prevY = kvp.Key.Item2;
            }
            while (kvp.Key.Item1 > xCount)
            {
                imageLine.Add(0);
                xCount++;
            }
            imageLine.Add(kvp.Value);
            xCount++;
        }
        finalImage.Add(imageLine);

        foreach (var line in finalImage)
        {
            Console.WriteLine(String.Join("", line).Replace('0', ' '));
        }
    }
   
}
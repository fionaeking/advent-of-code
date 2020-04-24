using System;
using System.Collections.Generic;

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
        Console.WriteLine("initial: "+ currPoint);
        currDirection = Constants.UPZERO;
        panelsPainted = new Dictionary<Tuple<int, int>, int>();
    }

    void paintPanel(int inputVal)
    {
        Console.WriteLine(currPoint.Item1);
        Console.WriteLine(currPoint.Item2);
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
        Console.WriteLine("update");
        foreach (var key in panelsPainted.Keys)
        {
            Console.WriteLine(key.Item1);
            Console.WriteLine(key.Item2);
        }
        return panelsPainted.Count;
    }
}
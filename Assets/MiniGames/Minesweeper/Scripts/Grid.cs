// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: noobtuts.com
// Contributors: David W. Corso
// Start: 05/20/2018
// Last:  08/13/2018

// Info
public class Grid
{
    public static int w = 13; // this is the width
    public static int h = 13; // this is the height
    public static Element[,] elements = new Element[w, h];

    public static void uncoverMines()
    {
        foreach (Element elem in elements)
        {
            if (elem.bIsMine)
            {
                elem.LoadTexture(0);
            }
        }
    }

    public static bool mineAt(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < w && y < h)
        {
            return elements[x, y].bIsMine;
        }
        else
        {
            return false;
        }
    }

    public static int adjacentMines(int x, int y)
    {
        int count = 0;

        if (mineAt(x,   y+1)) ++count; // top
        if (mineAt(x+1, y+1)) ++count; // top-right
        if (mineAt(x+1, y  )) ++count; // right
        if (mineAt(x+1, y-1)) ++count; // bottom-right
        if (mineAt(x,   y-1)) ++count; // bottom
        if (mineAt(x-1, y-1)) ++count; // bottom-left
        if (mineAt(x-1, y  )) ++count; // left
        if (mineAt(x-1, y+1)) ++count; // top-left

        return count;
    }

    // Flood Fill empty elements
    public static void FFuncover(int x, int y, bool[,] visited)
    {
        if (x >= 0 && y >= 0 && x < w && y < h)
        {
            // visited already?
            if (visited[x, y])
                return;

            // uncover element
            elements[x, y].LoadTexture(adjacentMines(x, y));

            // close to a mine? then no more work needed here
            if (adjacentMines(x, y) > 0)
                return;

            // set visited flag
            visited[x, y] = true;

            // recursion
            FFuncover(x-1, y  , visited); // left
            FFuncover(x+1, y  , visited); // right
            FFuncover(x,   y-1, visited); // bottom
            FFuncover(x,   y+1, visited); // up
            FFuncover(x-1, y+1, visited); // top-left
            FFuncover(x+1, y+1, visited); // top-right
            FFuncover(x-1, y-1, visited); // bottom-left
            FFuncover(x+1, y-1, visited); // bottom-right
        }
    }

    public static bool bIsFinished()
    {
        // Try to find a covered element that is no mine
        foreach (Element elem in elements)
        {
            if (elem.bIsCovered() && !elem.bIsMine)
            {
                return false;
            }
        }
            
        // There are none => all are mines => game won.
        return true;
    }
}

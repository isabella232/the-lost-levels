using System;
using System.Collections.Generic;


// Creating a custom Point class that has coordinates and a way to recognize the parent
public class Point
{
    public Point(int X, int Y, Point Parent)
    { x = X; y = Y; parent = Parent; }
    public int x; public int y; public int label; public Point parent;

    public override bool Equals(Point point)
    {
        return (this.x == point.x && this.y == point.y);
    }
}


public class PathFinder
{
	public PathFinder()
	{
	}


    // findPath essentially does a BFS on a graph while "building" it
    // start and ened should have parent=null
    public static List<Point> findPath(int[][] matrix, Point start, Point end)
    {
        // Initializing Data Structures
        Queue<Point> queue = new Queue<Point>();
        Dictionary<Point, int> nodes = new Dictionary<Point,int>();

        // Enqueue and Add to Dictionary, and creating a temporary Point t
        queue.Enqueue(start);
        nodes.Add(start, 1);
        Point t = queue.Dequeue();

        // Checking 8 tiles around
        for (int i = -1; i < 2; i++ )
        {
            for (int j = -1; j < 2; j++)
            {
                // Making sure we don't cause an ArrayOutOfBounds Exception
                if (t.x + i >= 0 && t.x + i < matrix.Length && t.y + j >= 0 && t.y < matrix[t.y].Length)
                {
                    // If Walkable
                    if (matrix[t.x + i][t.y + j] == 1)
                    {
                        //Create Point with parent t
                        Point e = new Point(t.x + i, t.y + j, t);

                        if (e.Equals(end))
                        {
                            return getPath(e);
                        }

                        // Add e to the queue and dictionary
                        queue.Enqueue(e);
                        nodes.Add(e, 1);
                    }
                }
            }
        }

        // BFS algorithm is the same as above, except it runs until the queue is empty,
        // and checks if a node has been queued already through the Dictionary
        while (queue.Count != 0)
        {
            t = queue.Dequeue();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (t.x + i >= 0 && t.x + i < matrix.Length && t.y + j >= 0 && t.y < matrix[t.y].Length)
                    {
                        if (matrix[t.x + i][t.y + j] == 1)
                        {
                            Point e = new Point(t.x + i, t.y + j, t);
                            if (e.Equals(end))
                            {
                                return getPath(e);
                            }

                            // Checking if we already iterated through this node
                            if (nodes.ContainsKey(e)) { }
                            else
                            { queue.Enqueue(e); nodes.Add(e, e.label); };
                        }
                    }
                }
            }
        }

        return new List<Point>();
    }

    private static List<Point> getPath(Point end)
    {
        List<Point> path = new List<Point>();
        Point curr = end;
        Point next = end.parent;

        // Adding the parent nodes until the start, which should have a null parent
        while (curr != null)
        {
            path.Add(curr);
            curr = curr.parent;
        }

        // Reverse the indices in order to have the start point at the beginning
        path.Reverse();
        return path;
    }

}

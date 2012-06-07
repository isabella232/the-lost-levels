using System;
using System.Collections.Generic;



// Creating a custom BFSNode class that has coordinates and a way to recognize the parent
public class BFSNode
{
    public BFSNode(int X, int Y, BFSNode Parent)
    { x = X; y = Y; parent = Parent; }
    public int x; public int y; public int label; public BFSNode parent;

    public override bool Equals(Object obj)
    {
        BFSNode other = obj as BFSNode;
        return (this.x == other.x && this.y == other.y);
    }

    public override int GetHashCode()
    {
        String s = this.x + ", " + this.y;
        String st = String.Empty;
        foreach (char c in s)
            st += (int)c;
        return Int32.Parse(st);
    }

    public override String ToString()
    {
        String s = x + ", " + y;
        return s;
    }
}


public class PathFinder
{
	public PathFinder()
	{
	}


    // findPath essentially does a BFS on a graph while "building" it
    // start and end should have parent=null
    public static List<BFSNode> findPath(int[][] matrix, BFSNode start, BFSNode end)
    {
        // Initializing Data Structures
        Queue<BFSNode> queue = new Queue<BFSNode>();
        Dictionary<BFSNode, int> nodes = new Dictionary<BFSNode,int>();

        // Enqueue and Add to Dictionary, and creating a temporary BFSNode t
        queue.Enqueue(start);
        nodes.Add(start, 1);
        BFSNode t = queue.Dequeue();

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
                        //Create BFSNode with parent t
                        BFSNode e = new BFSNode(t.x + i, t.y + j, t);

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
                            BFSNode e = new BFSNode(t.x + i, t.y + j, t);
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

        return new List<BFSNode>();
    }

    private static List<BFSNode> getPath(BFSNode end)
    {
        List<BFSNode> path = new List<BFSNode>();
        BFSNode curr = end;
        BFSNode next = end.parent;

        // Adding the parent nodes until the start, which should have a null parent
        while (curr != null)
        {
            path.Add(curr);
            curr = curr.parent;
        }

        // Reverse the indices in order to have the start BFSNode at the beginning
        path.Reverse();
        return path;
    }

}
/*
class Tester
{
    public static void Main()
    {
        int[][] matrix = new int[5][];
        matrix[0] = new int[] { 1, 1, 1, 1, 0 };
        matrix[1] = new int[] { 1, 0, 0, 1, 0 };
        matrix[2] = new int[] { 0, 1, 0, 1, 0 };
        matrix[3] = new int[] { 0, 1, 0, 1, 0 };
        matrix[4] = new int[] { 0, 0, 0, 0, 0 };

        BFSNode start = new BFSNode(2, 1, null);
        BFSNode end = new BFSNode(2, 3, null);
        List<BFSNode> path = PathFinder.findPath(matrix, start, end);


        System.Diagnostics.Debug.WriteLine("STARTING");
        foreach (BFSNode b in path)
            System.Diagnostics.Debug.WriteLine(b);
    }

}
*/
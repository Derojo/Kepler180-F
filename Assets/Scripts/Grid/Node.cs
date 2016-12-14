using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Node
{
    public int x;
    public int z;
    public List<Node> neighbours;

    public Node()
    {
        neighbours = new List<Node>();
    }
}
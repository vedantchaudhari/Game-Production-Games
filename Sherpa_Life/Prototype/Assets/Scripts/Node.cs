using UnityEngine;
using System.Collections.Generic;

public class Node
{
	public List<Node> neighbors;
	public int x;
	public int y;

	public Node()
	{
		neighbors = new List<Node> ();
	}

	public float distanceTo(Node node)
	{
		if (node == null) 
		{
			Debug.Log ("Node is null");
		}

        return Vector2.Distance(new Vector2(x, y), new Vector2(node.x, node.y));
	}
}
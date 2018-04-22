using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour {

    public GameObject player;
    public TileData[] tileData;

    int[,] tiles;
	Node[,] graph;

    public int mapSizeX = 10;
    public int mapSizeY = 10;

	void Start () {
        player.GetComponent<Player>().tileX = (int)player.transform.position.x;
        player.GetComponent<Player>().tileY = (int) player.transform.position.y;
        player.GetComponent<Player>().tileMap = this;

        generateData();
		generateGraph();
        generateMap();
    }

    void generateData()
    {
        tiles = new int[mapSizeX, mapSizeY];

        // Initialize map tiles
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
				tiles [x, y] = 0;
            }
        }

        // SOULJA BOUUUUUUUUUUUUUUUUUUUUUUUUUUUUUI
        tiles[4, 4] = 2;
        tiles[5, 4] = 2;
        tiles[6, 4] = 2;
        tiles[7, 4] = 2;
        tiles[8, 4] = 2;
        tiles[4, 5] = 2;
        tiles[4, 6] = 2;
        tiles[8, 5] = 2;
        tiles[8, 6] = 2;
    }

	void generateGraph()
	{
		graph = new Node[mapSizeX, mapSizeY];

		for (int x = 0; x < mapSizeX; x++) 
		{
			for (int y = 0; y < mapSizeY; y++) 
			{
				graph [x, y] = new Node ();
				graph [x, y].x = x;
				graph [x, y].y = y;
			}
		}

		for (int x = 0; x < mapSizeX; x++) 
		{
            for (int y = 0; y < mapSizeY; y++)
            {
                if(x > 0) 
                {
                    graph[x,y].neighbors.Add( graph[x-1, y] );
                    if(y > 0)
                        graph[x,y].neighbors.Add( graph[x-1, y-1] );
                    if(y < mapSizeY - 1)
                        graph[x,y].neighbors.Add( graph[x-1, y+1] );
                }

                // Try Right
                if(x < mapSizeX - 1) 
                {
                    graph[x,y].neighbors.Add( graph[x+1, y] );
                    if(y > 0)
                        graph[x,y].neighbors.Add( graph[x+1, y-1] );
                    if(y < mapSizeY-1)
                        graph[x,y].neighbors.Add( graph[x+1, y+1] );
                }

                // Try straight up and down
                if (y > 0)
                {
                    graph[x, y].neighbors.Add(graph[x, y - 1]);
                }
                if (y < mapSizeY - 1)
                {
                    graph[x, y].neighbors.Add(graph[x, y + 1]);
                } 
            }
		}
	}

    void generateMap()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TileData type = tileData[tiles[x, y]];
                GameObject tile = Instantiate(type.tilePrefab, new Vector3(x, y, 0), Quaternion.identity);

                ClickHandler clickHandler = tile.GetComponent<ClickHandler>();
                clickHandler.tileX = x; // Set map tile position x
                clickHandler.tileY = y; // Set map tile position y
                clickHandler.map = this;
            }
        }
    }
	
    public Vector3 tilePositionToWorldPosition(int x, int y)
    {
        return new Vector3(x, y, 0);
    }

    public float tileCost(int sourceX, int sourceY, int targetX, int targetY)
    {
        TileData tData = tileData[tiles[targetX, targetY]];
        float cost = tData.cost;

        if (playerCanEnter(targetX, targetY) == false)
        {
            return Mathf.Infinity;
        }
        if (sourceX != targetX && sourceY != targetY)
        {
            cost += 0.001f;
        }

        return cost;
    }

    public bool playerCanEnter(int targetX, int targetY)
    {
        return tileData[tiles[targetX, targetY]].isWalkableTile;
    }

    public void move(int x, int y)
    {
        //player.GetComponent<Player>().tileX = x;
        //player.GetComponent<Player>().tileY = y;
        //player.transform.position = tilePositionToWorldPosition(x, y);
        player.GetComponent<Player>().currentPath = null;

        if (playerCanEnter(x, y) == false)
        {
            return;
        }

		Dictionary<Node, float> distance = new Dictionary<Node, float>();
		Dictionary<Node, Node> previous = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();
        List<Node> currentPath = new List<Node>();

		Node source = graph[player.GetComponent<Player> ().tileX, player.GetComponent<Player> ().tileY];
        Node target = graph[x, y];
        Node current = target;

		distance [source] = 0; // Where the player is located currently
		previous [source] = null; // Nothing before the player hehe

        foreach (Node node in graph)
        {
            if (node != source)
            {
                distance[node] = Mathf.Infinity;
                previous[node] = null;
            }

            unvisited.Add(node);
        }

        while (unvisited.Count > 0)
        {
            Node unvisitedNode = null; // Stores unvisited node with the lowest distance

            foreach (Node possibleUnvisitedNode in unvisited)
            {
                if (unvisitedNode == null || distance[possibleUnvisitedNode] < distance[unvisitedNode])
                {
                    unvisitedNode = possibleUnvisitedNode;
                }
            }

            if (unvisitedNode == target)
            {
                break;
            }

            unvisited.Remove(unvisitedNode);

            foreach (Node node in unvisitedNode.neighbors)
            {
                //float alt = distance[unvisitedNode] + unvisitedNode.distanceTo(node);
                float alt = distance[unvisitedNode] + tileCost(unvisitedNode.x, unvisitedNode.y, node.x, node.y);

                if (alt < distance[node])
                {
                    distance[node] = alt;
                    previous[node] = unvisitedNode;
                }
            }
        }

        if (previous[target] == null)
        {
            return; // No Route between the target and the source
        }

        while (current != null)
        {
            currentPath.Add(current);
            current = previous[current];
        }

        currentPath.Reverse();

        player.GetComponent<Player>().currentPath = currentPath;
    }
}

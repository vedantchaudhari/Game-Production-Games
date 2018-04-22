using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public List<Node> currentPath = null;
    public TileMap tileMap;
    public int tileX;
    public int tileY;
    public int movementSpeed = 3;

    void Update()
    {
        if (currentPath != null) // Make sure there is a currentPath to display
        {
            int currentNode = 0;

            while (currentNode < currentPath.Count - 1)
            {
                Vector3 start = tileMap.tilePositionToWorldPosition(currentPath[currentNode].x, currentPath[currentNode].y) + new Vector3(0, 0, -1f);
                Vector3 end = tileMap.tilePositionToWorldPosition(currentPath[currentNode + 1].x, currentPath[currentNode + 1].y) + new Vector3(0, 0, -1f);

                Debug.DrawLine(start, end, Color.red);
                currentNode++;
            }
        }
    }

    public void moveNextTile()
    {
        float remainingMomvement = movementSpeed;

        while (remainingMomvement > 0)
        {
            if (currentPath == null)
            {
                Debug.Log("ERROR: There is no current path");
                return;
            }

            remainingMomvement -= tileMap.tileCost(currentPath[0].x, currentPath[0].y, currentPath[1].x, currentPath[1].y);

            tileX = currentPath[1].x;
            tileY = currentPath[1].y;

            transform.position = tileMap.tilePositionToWorldPosition(tileX, tileY);

            currentPath.RemoveAt(0);

            if (currentPath.Count == 1)
            {
                currentPath = null;
            }
        }
    }
//}
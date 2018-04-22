using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour
{
    public TileMap tileMap;
    public int x;
    public int y;

    void Start()
    {
        setPosition();
    }

    void setPosition()
    {
        Vector3 position = tileMap.tilePositionToWorldPosition(x, y);

        this.transform.position = position + new Vector3(0, 0, -1);
    }
}
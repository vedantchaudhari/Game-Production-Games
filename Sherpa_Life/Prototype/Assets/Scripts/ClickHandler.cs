using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour {

    public int tileX;
    public int tileY;
    public TileMap map;

    void OnMouseUp()
    {
        map.move(tileX, tileY);
    }
}

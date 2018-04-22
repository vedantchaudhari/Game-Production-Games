using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData {
    public string type;
    public GameObject tilePrefab;

    public bool isWalkableTile = true;
    public float cost = 1;
}
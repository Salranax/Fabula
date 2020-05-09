using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public GridZone[] gridZones = new GridZone[4];
    [SerializeField]
    public float liningSpace = 1.56f;
    [SerializeField]
    public float offsetX = -7.4f, offsetY = -4f;

    public float getLiningSpace(){
        return liningSpace;
    }

    public Vector2 getOffset(){
        return new Vector2(offsetX, offsetY);
    }

    public Vector2 calculateCoord(Vector2Int _gridCoord){
        return new Vector2(_gridCoord.x * liningSpace + offsetX, _gridCoord.y * liningSpace + offsetY);
    }
}

enum GridType
{
    wall,
    empty
}

enum CubeType
{
    movable,
    wall,
    entrance,
    exit
}

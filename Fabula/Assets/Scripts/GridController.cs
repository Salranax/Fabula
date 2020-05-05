using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public GridZone[] gridZones = new GridZone[4];
    private GridZone activeGrid;
    [SerializeField]
    public float liningSpace = 1.56f;
    [SerializeField]
    public float offsetX = -7.4f, offsetY = -4f;

    // Start is called before the first frame update
    void Start()
    {
        activeGrid = gridZones[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GridZone getActiveZone(){
        return activeGrid;
    }

    public float getLiningSpace(){
        return liningSpace;
    }

    public Vector2 getOffset(){
        return new Vector2(offsetX, offsetY);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridZone : MonoBehaviour
{   
    public int zoneIndex;
    private GameObject[] gridObjects;
    private GridType[] gridTypes;
    public List<Cube> movables = new List<Cube>();
    public List<GameObject> indicators = new List<GameObject>();

    public void setSwipe(SwipeData _data){
        
    }

    public void addMovable(Cube _script){
        movables.Add(_script);
    }

    public GameObject[] getGridObjects(){
        return gridObjects;
    }

    public List<Cube> getMovableCubes(){
        return movables;
    }
}

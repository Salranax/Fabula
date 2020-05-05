using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridZone : MonoBehaviour
{
    private GameObject[] gridObjects;
    private GridType[] gridTypes;
    public List<Cube> movables = new List<Cube>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

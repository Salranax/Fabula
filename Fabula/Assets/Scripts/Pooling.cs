using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public GameObject wall;
    public GameObject movableCube;
    public GameObject indicatorPrefab;

    public List<GameObject> wallPool;
    public List<GameObject> movableCubePool;
    public List<GameObject> indicatorPool;

    public GameObject getWall(){
        if(wallPool.Count == 0){
            GameObject _tmpWall = Instantiate(wall);
            return _tmpWall;
        }
        else{
            GameObject _tmpWall = wallPool[0];
            wallPool.RemoveAt(0);
            _tmpWall.SetActive(true);
            return _tmpWall;
        }
    }

    public GameObject getMovableCube(){
        if(movableCubePool.Count == 0){
            GameObject _tmpWall = Instantiate(movableCube);
            return _tmpWall;
        }
        else{
            GameObject _tmpWall = movableCubePool[0];
            movableCubePool.RemoveAt(0);
            _tmpWall.SetActive(true);
            return _tmpWall;
        }
    }

    public GameObject getIndicator(){
        if(indicatorPool.Count == 0){
            GameObject _tmpIndicator = Instantiate(indicatorPrefab);
            return _tmpIndicator;
        }
        else{
            GameObject _tmpIndicator = indicatorPool[0];
            indicatorPool.RemoveAt(0);
            _tmpIndicator.SetActive(true);
            return _tmpIndicator;
        }
    }
}

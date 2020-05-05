using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField]
    public GridController _GridController;
    [SerializeField]
    public Vector2Int gridCoord;

    public void cubeSetup(GridController _gridControllerDependency, Vector2Int _gridCoord){
        _GridController = _gridControllerDependency;
        gridCoord = _gridCoord;
    }

    public void moveToCoord(Vector2 _newCoord, Vector2Int _gridCoord){
        gridCoord = _gridCoord;
        transform.localPosition = _newCoord;
    }

    IEnumerator moveCoroutine(Vector2Int _finalPos){
        yield return new WaitForEndOfFrame();
    }
}

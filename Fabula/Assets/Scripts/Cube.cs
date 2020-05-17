using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField]
    public LevelController _LevelController;
    public CubeSkin skinController;
    public Vector2Int gridCoord;
    private float speed = 0.4f;

    public void cubeSetup(LevelController _levelControllerDependency, Vector2Int _gridCoord){
        _LevelController = _levelControllerDependency;
        gridCoord = _gridCoord;
    }

    public void moveToCoord(Vector2 _newCoord, Vector2Int _gridCoord){
        if(gridCoord != _gridCoord){
            gridCoord = _gridCoord;
            StartCoroutine(moveCoroutine(_newCoord));
        }
        else{
            _LevelController.coroutineFinished();
        }
    }

    public void setSkin(Sprite _skin){
        skinController.setSkin(_skin);
    }

    IEnumerator moveCoroutine(Vector2 _finalPos){
        float step = 0;
        Vector2 _startPos = transform.localPosition;

        while (Vector2.Distance(transform.localPosition, _finalPos) >= 0.2)
        {
            step += speed * Time.deltaTime; 
            this.transform.localPosition = Vector2.Lerp(transform.localPosition, _finalPos, step);
            
            yield return new WaitForEndOfFrame();
        }

        this.transform.localPosition = _finalPos;

        _LevelController.coroutineFinished();
        
        yield return new WaitForEndOfFrame();
    }
}

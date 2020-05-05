using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Dependencies")]
    public GridController _GridController;
    public LevelGenerator _LevelGenerator;

    [Header("Setup")]
    public LayerMask cubeMask;

    [Header("Level Datas")]
    public LevelData[] levelDataSet;

    // Start is called before the first frame update
    void Start()
    {
        generateLevel(1, levelDataSet[0].dataSet);
    }

    public void generateLevel(int _lvlNo, Texture2D[] _lvlDatas){
        for (int i = 0; i < 4; i++)
        {
           _LevelGenerator.GenerateLevel(_lvlDatas[i], _GridController.gridZones[i]); 
        }
    }

    public void setSwipe(SwipeData _data){
        GridZone _zone = _GridController.getActiveZone();
        List<Cube> _cubeGrid = new List<Cube>(_zone.getMovableCubes());

        Cube[] _orderedCubes = orderCubeList(_data, _cubeGrid); //Order cubes

        decideMovement(_data ,_orderedCubes);   //Calculate coordinate to move for each cube
    }

    //Calculates the coordinates for each cube to move
    //Then move them to that coordinate
    private void decideMovement(SwipeData _data, Cube[] _cubes){
        Vector2 _rayDirection;
        Vector2Int _coordinateOffset;

        if(_data.Direction == SwipeDirection.Left){
            _rayDirection = Vector2.left;
            _coordinateOffset = new Vector2Int(1,0);
        }
        else if(_data.Direction == SwipeDirection.Right){
            _rayDirection = Vector2.right;
            _coordinateOffset = new Vector2Int(-1,0);
        }
        else if(_data.Direction == SwipeDirection.Up){
            _rayDirection = Vector2.up;
            _coordinateOffset = new Vector2Int(0,-1);
        }
        else{
            _rayDirection = Vector2.down;
            _coordinateOffset = new Vector2Int(0,1);
        }

        foreach (Cube item in _cubes)
        {
            Vector2Int _coord = rayToDir(_rayDirection,item.transform) + _coordinateOffset;
            item.moveToCoord(new Vector2(
                _coord.x * _GridController.getLiningSpace() + _GridController.getOffset().x, 
                _coord.y * _GridController.getLiningSpace() + _GridController.getOffset().y), 
                _coord
            );
        }
    }

    //Cast a ray to the swipe direction and gets coordinates of collided object
    private Vector2Int rayToDir(Vector2 _rayDirection, Transform _RayStartPos){
        Vector2Int _gridCoord = new Vector2Int(0,0);
        
        RaycastHit2D hit = Physics2D.Raycast(_RayStartPos.position, _rayDirection,cubeMask);

        if(hit.collider != null){
            _gridCoord = hit.collider.gameObject.GetComponent<Cube>().gridCoord;
        }

        return _gridCoord;
    }

    //CUBE ORDERING CALCULATIONS
    private Cube[] orderCubeList(SwipeData _data, List<Cube> _grid){
        List<Cube> _cubeGrid = new List<Cube>(_grid);

        Cube[] _orderedCubeGrid = new Cube[_cubeGrid.Count];

        for (int i = 0; i < _orderedCubeGrid.Length; i++)
        {
            Cube _tmpCube = findSmallorBig(_cubeGrid ,_data.Direction);
            _orderedCubeGrid[i] = _tmpCube;
            _cubeGrid.Remove(_tmpCube);
        }

        return _orderedCubeGrid;
    }

    //Find the Cube with biggest/or smallest x/y value depend on swipe direction
    //Example Use: 
    //for Cubes that are in the same row and swiping to right, cubes on the right must have the 
    //calculations first so cube on its left can have correct calculation
    private Cube findSmallorBig(List<Cube> _cubeList, SwipeDirection _dir){
        Cube _tmpCube = _cubeList[0];

        foreach (Cube item in _cubeList)
        {   
            if(_dir == SwipeDirection.Left){
                if(Mathf.Max(_tmpCube.gridCoord.x, item.gridCoord.x) == item.gridCoord.x){
                    _tmpCube = item;
                }
            }
            else if(_dir == SwipeDirection.Right){
                if(Mathf.Min(_tmpCube.gridCoord.x, item.gridCoord.x) == item.gridCoord.x){
                    _tmpCube = item;
                }
            }
            else if(_dir == SwipeDirection.Down){
                if(Mathf.Max(_tmpCube.gridCoord.y, item.gridCoord.y) == item.gridCoord.y){
                    _tmpCube = item;
                }
            }
            else if(_dir == SwipeDirection.Up){
                if(Mathf.Min(_tmpCube.gridCoord.y, item.gridCoord.y) == item.gridCoord.y){
                    _tmpCube = item;
                }
            }

        }
        return _tmpCube;
    }
}

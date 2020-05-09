using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Dependencies")]
    public GridController _GridController;
    public LevelGenerator _LevelGenerator;
    public CameraController _CameraController;

    [Header("Setup")]
    public LayerMask cubeMask;

    [Header("Level Datas")]
    public LevelData[] levelDataSet;
    public StageController[] levelStageDatas;

    private GridZone activeGrid;
    private int zoneIndex;
    private int stageIndex = 0;
    private int levelNo = 1;

    //Cube Movement
    private float speed;
    private int activeCoroutines = 0;
    private bool isSwipeAvailable = true;

    // Start is called before the first frame update
    void Start()
    {   
        zoneIndex = 0;
        activeGrid = _GridController.gridZones[zoneIndex];
        generateLevel(1, levelDataSet[0].dataSet);
    }

    //Generates level
    public void generateLevel(int _lvlNo, Texture2D[] _lvlDatas){
        for (int i = 0; i < 4; i++)
        {
            //gets level data for zone and generates level on that zone. Iterates for each zone
           _LevelGenerator.GenerateLevel(_lvlDatas[i], _GridController.gridZones[i], this); 
        }

        //sets indicators of first stage of first zone as a start
        _LevelGenerator.SetIndicators(levelStageDatas[levelNo - 1].GetCubeDatas(stageIndex, activeGrid), activeGrid);
    }

    //gets swipe data from swipemanager and decides what to do
    public void setSwipe(SwipeData _data){
        if(isSwipeAvailable){
            deactivateSwipe();
            GridZone _zone = activeGrid;
            List<Cube> _cubeGrid = new List<Cube>(_zone.getMovableCubes());

            Cube[] _orderedCubes = orderCubeList(_data, _cubeGrid); //Order cubes

            decideMovement(_data ,_orderedCubes);   //Calculate coordinate to move for each cube
        }
    }

    //After each cobe move to its final position, this method calls and checks that all cubes finish the movement
    public void coroutineFinished(){
        activeCoroutines --;

        //if all cubes finish its movement checks that is stage complete and enables swipes again
        if(activeCoroutines == 0){
            
            if(checkCubeStatus(levelStageDatas[levelNo - 1].GetCubeDatas(stageIndex, activeGrid), activeGrid)){
                checkProgress(levelStageDatas[levelNo - 1].GetStageLength(activeGrid));
            }
            else
            {
                activateSwipe();
            }
        }
    }

    //Calculates the coordinates for each cube to move
    //Then move them to that coordinate
    private void decideMovement(SwipeData _data, Cube[] _cubes){
        Vector2 _rayDirection = Vector2.left;
        Vector2Int _coordinateOffset;

        if(_data.Direction == SwipeDirection.Left){
            if(zoneIndex == 0){
                _rayDirection = Vector2.left;   
            }
            else if(zoneIndex == 1){
                _rayDirection = Vector2.down;
            }
            else if(zoneIndex == 2){
                _rayDirection = Vector2.right;
            }
            else if(zoneIndex == 3){
                _rayDirection = Vector2.up;
            }
            _coordinateOffset = new Vector2Int(1,0);
        }
        else if(_data.Direction == SwipeDirection.Right){
            if(zoneIndex == 0){
                _rayDirection = Vector2.right;
                
            }
            else if(zoneIndex == 1){
                _rayDirection = Vector2.up;
            }
            else if(zoneIndex == 2){
                _rayDirection = Vector2.left;
            }
            else if(zoneIndex == 3){
                _rayDirection = Vector2.down;
            }
            _coordinateOffset = new Vector2Int(-1,0);
        }
        else if(_data.Direction == SwipeDirection.Up){
            if(zoneIndex == 0){
                _rayDirection = Vector2.up;
            }
            else if(zoneIndex == 1){
                _rayDirection = Vector2.left;
            }
            else if(zoneIndex == 2){
                _rayDirection = Vector2.down;
            }
            else if(zoneIndex == 3){
                _rayDirection = Vector2.right;
            }
            _coordinateOffset = new Vector2Int(0,-1);
        }
        else{
            if(zoneIndex == 0){
                _rayDirection = Vector2.down;
            }
            else if(zoneIndex == 1){
                _rayDirection = Vector2.right;
            }
            else if(zoneIndex == 2){
                _rayDirection = Vector2.up;
            }
            else if(zoneIndex == 3){
                _rayDirection = Vector2.left;
            }
            _coordinateOffset = new Vector2Int(0,1);
        }

        activeCoroutines = _cubes.Length;

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
            if(_dir == SwipeDirection.Right){
                if(Mathf.Max(_tmpCube.gridCoord.x, item.gridCoord.x) == item.gridCoord.x){
                    _tmpCube = item;
                }
            }
            else if(_dir == SwipeDirection.Left){
                if(Mathf.Min(_tmpCube.gridCoord.x, item.gridCoord.x) == item.gridCoord.x){
                    _tmpCube = item;
                }
            }
            else if(_dir == SwipeDirection.Up){
                if(Mathf.Max(_tmpCube.gridCoord.y, item.gridCoord.y) == item.gridCoord.y){
                    _tmpCube = item;
                }
            }
            else if(_dir == SwipeDirection.Down){
                if(Mathf.Min(_tmpCube.gridCoord.y, item.gridCoord.y) == item.gridCoord.y){
                    _tmpCube = item;
                }
            }

        }
        return _tmpCube;
    }

    //Compare all cube coordinates by true positions of stage
    //return true if all cube coordinates match by true positions which means stage completed
    //else return false which means stage is not completed
    private bool checkCubeStatus(StageCubeData _cubeData,GridZone _zone){
        int _trueCount = 0;

        foreach (Vector2Int _trueCoord in _cubeData.cubePlacements)
        {
            foreach (var _cube in _zone.getMovableCubes())
            {
                if(_trueCoord == _cube.gridCoord){
                    _trueCount ++;
                }
            }
        }

        if(_trueCount == _cubeData.cubePlacements.Length){
            return true;
        }
        else{
            return false;
        }
    }

    private void checkProgress(int _dataLength){
        Debug.Log(stageIndex + " / " + _dataLength);
        if(stageIndex + 1 == _dataLength){
            moveToNextZone();
        }
        else{
            stageIndex ++;
            _LevelGenerator.SetIndicators(levelStageDatas[levelNo - 1].GetCubeDatas(stageIndex, activeGrid), activeGrid);
            activateSwipe();
        }
    }

    private void moveToNextZone(){
        zoneIndex ++;
        stageIndex = 0;
        _CameraController.moveToZoneByIndex(zoneIndex);
        activeGrid = _GridController.gridZones[zoneIndex];
        _LevelGenerator.SetIndicators(levelStageDatas[levelNo - 1].GetCubeDatas(stageIndex, activeGrid), activeGrid);
        Invoke("activateSwipe", 2.1f);
    }

    private void activateSwipe(){
        isSwipeAvailable = true;
    }

    private void deactivateSwipe(){
        isSwipeAvailable = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GridController _GridController;
    public Pooling _pool;
    public ColorToPrefab[] colorMappings;
    [SerializeField]
    public float liningSpace;
    [SerializeField]
    public float offsetX, offsetY;
    public delegate void MovementCallback();

    public void GenerateLevel(Texture2D _mapdata, GridZone _zone, LevelController _levelControllerDependency){
        offsetX = _GridController.getOffset().x;
        offsetY = _GridController.getOffset().y;
        liningSpace = _GridController.getLiningSpace();

        
        for (int x = 0; x < _mapdata.width; x++)
        {
            for (int y = 0; y < _mapdata.height; y++)
            {
                GenerateTile(x,y, _mapdata, _zone, _levelControllerDependency);
            }
        }
    }

    public void SetIndicators(StageCubeData _cubeData,GridZone _zone){
        foreach (GameObject item in _zone.indicators)
        {
            _pool.retireIndicator(item);
        }

        generateIndicators(_cubeData, _zone);
        
        for (int i = 0; i < _cubeData.cubePlacements.Length; i++)
        {
            _zone.indicators[i].transform.localPosition = _GridController.calculateCoord(_cubeData.cubePlacements[i]);
        }
    }

    private void generateIndicators(StageCubeData _cubeData, GridZone _zone){
        foreach (Vector2Int _coord in _cubeData.cubePlacements)
        {
            GameObject _tmp = _pool.getIndicator();
            _zone.indicators.Add(_tmp);
            _tmp.transform.SetParent(_zone.transform);
            _tmp.transform.rotation = Quaternion.identity;
        }
    }

    //Color Codes
    //0 is WALL
    //1 is MOVABLE CUBE
    //2 is DOOR

    void GenerateTile(int x, int y, Texture2D _mapdata, GridZone _zone, LevelController _levelControllerDependency){
        Color32 pixelColor = _mapdata.GetPixel(x,y);

        if(colorMappings[0].color.Equals(pixelColor)){
            GameObject _tmpWall = _pool.getWall();

            _tmpWall.transform.SetParent(_zone.transform);
            _tmpWall.transform.localPosition = new Vector2(x * liningSpace + offsetX, y * liningSpace + offsetY);

            Cube _cubeScript = _tmpWall.GetComponent<Cube>();
            _cubeScript.gridCoord = new Vector2Int(x,y);
            _tmpWall.transform.rotation = Quaternion.identity;
        }
        else if(colorMappings[1].color.Equals(pixelColor)){
            GameObject _tmpMovable = _pool.getMovableCube();

            _tmpMovable.transform.SetParent(_zone.transform);
            _tmpMovable.transform.localPosition = new Vector2(x * liningSpace + offsetX, y * liningSpace + offsetY);

            Cube _cubeScript = _tmpMovable.GetComponent<Cube>();
            _cubeScript.cubeSetup(_levelControllerDependency, new Vector2Int(x,y));
            _zone.addMovable(_cubeScript);
            _cubeScript.transform.rotation = Quaternion.identity;
        }
        else if(colorMappings[2].color.Equals(pixelColor)){
            GameObject _tmpMovable = _pool.getMovableCube();

            _tmpMovable.transform.SetParent(_zone.transform);
            _tmpMovable.transform.localPosition = new Vector2(x * liningSpace + offsetX, y * liningSpace + offsetY);

            Cube _cubeScript = _tmpMovable.GetComponent<Cube>();
            _cubeScript.cubeSetup(_levelControllerDependency, new Vector2Int(x,y));
            //_zone.addMovable(_cubeScript);
            _cubeScript.transform.rotation = Quaternion.identity;
        }
    }

}

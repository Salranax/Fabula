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

    public void GenerateLevel(Texture2D _mapdata, GridZone _zone){
        offsetX = _GridController.getOffset().x;
        offsetY = _GridController.getOffset().y;
        liningSpace = _GridController.getLiningSpace();
        
        for (int x = 0; x < _mapdata.width; x++)
        {
            for (int y = 0; y < _mapdata.height; y++)
            {
                GenerateTile(x,y, _mapdata, _zone);
            }
        }
    }

    //Color Codes
    //0 is WALL
    //1 is MOVABLE CUBE

    void GenerateTile(int x, int y, Texture2D _mapdata, GridZone _zone){
        Color32 pixelColor = _mapdata.GetPixel(x,y);

        if(colorMappings[0].color.Equals(pixelColor)){
            GameObject _tmpWall = _pool.getWall();

            _tmpWall.transform.SetParent(_zone.transform);
            _tmpWall.transform.localPosition = new Vector2(x * liningSpace + offsetX, y * liningSpace + offsetY);

            Cube _cubeScript = _tmpWall.GetComponent<Cube>();
            _cubeScript.gridCoord = new Vector2Int(x,y);
        }
        if(colorMappings[1].color.Equals(pixelColor)){
            GameObject _tmpMovable = _pool.getMovableCube();

            _tmpMovable.transform.SetParent(_zone.transform);
            _tmpMovable.transform.localPosition = new Vector2(x * liningSpace + offsetX, y * liningSpace + offsetY);

            Cube _cubeScript = _tmpMovable.GetComponent<Cube>();
            _cubeScript.gridCoord = new Vector2Int(x,y);
            _zone.addMovable(_cubeScript);
        }

    }

}

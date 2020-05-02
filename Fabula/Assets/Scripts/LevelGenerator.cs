using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Pooling _pool;
    public ColorToPrefab[] colorMappings;

    public void GenerateLevel(Texture2D _mapdata, GridZone _zone){

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
            _tmpWall.transform.localPosition = new Vector2(x, y);
        }

    }

}

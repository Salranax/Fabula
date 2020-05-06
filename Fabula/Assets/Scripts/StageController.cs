using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [Header("Datas For Zone 1")]
    public StageCubeData[] stageDatas1;
    [Header("Datas For Zone 2")]
    public StageCubeData[] stageDatas2;
    [Header("Datas For Zone 3")]
    public StageCubeData[] stageDatas3;
    [Header("Datas For Zone 4")]
    public StageCubeData[] stageDatas4;

    public StageCubeData GetCubeDatas(int _stageIndex, GridZone _zone){
        if(_zone.zoneIndex == 0){
            return stageDatas1[_stageIndex];
        }
        else if(_zone.zoneIndex == 1){
            return stageDatas2[_stageIndex];
        }
        else if(_zone.zoneIndex == 2){
            return stageDatas3[_stageIndex];
        }
        else if(_zone.zoneIndex == 3){
            return stageDatas4[_stageIndex];
        }
        else{
            return null;
        }
    }
}

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
    [Header("Movement Sequences")]
    [Header("Sequences For Zone 1")]
    public MovementSequence[] stage1Sequence;
    [Header("Sequences For Zone 2")]
    public MovementSequence[] stage2Sequence;
    [Header("Sequences For Zone 3")]
    public MovementSequence[] stage3Sequence;
    [Header("Sequences For Zone 4")]
    public MovementSequence[] stage4Sequence;


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

    public int GetStageLength(GridZone _zone){
        if(_zone.zoneIndex == 0){
            return stageDatas1.Length;
        }
        else if(_zone.zoneIndex == 1){
            return stageDatas2.Length;
        }
        else if(_zone.zoneIndex == 2){
            return stageDatas3.Length;
        }
        else if(_zone.zoneIndex == 3){
            return stageDatas4.Length;
        }
        else{
            return 0;
        }
    }

    public MovementSequence getSequence(int _index, GridZone _zone){
        if(_zone.zoneIndex == 0){
            return stage1Sequence[_index];
        }
        else if(_zone.zoneIndex == 1){
            return stage2Sequence[_index];
        }
        else if(_zone.zoneIndex == 2){
            return stage3Sequence[_index];
        }
        else if(_zone.zoneIndex == 3){
            return stage4Sequence[_index];
        }
        else{
            return null;
        }
    }
}

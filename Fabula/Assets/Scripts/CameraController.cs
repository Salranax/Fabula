using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    public CinemachineVirtualCamera[] cameras = new CinemachineVirtualCamera[4];
    private int activePriority = 10;
    private int passivePriority = 9;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveToZoneByIndex(int _index){
        foreach (CinemachineVirtualCamera _cam in cameras)
        {
            _cam.Priority = passivePriority;
        }

        cameras[_index].Priority = activePriority;
    }
}

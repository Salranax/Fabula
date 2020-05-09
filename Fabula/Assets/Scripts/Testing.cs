using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public LevelController TestModule;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void testCubeSort(){
        SwipeData swipeData = new SwipeData()
        {
            Direction = SwipeDirection.Left,
            StartPosition = Vector3.zero,
            EndPosition = Vector3.zero
        };
        TestModule.setSwipe(swipeData);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSkin : MonoBehaviour
{
    public SpriteRenderer skin;

    public void setSkin(Sprite _skin){
        skin.sprite = _skin;
    }
}

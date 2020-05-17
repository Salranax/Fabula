using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    public GameObject winPopup;

    public void winCondition(){
        winPopup.SetActive(true);
    }
}

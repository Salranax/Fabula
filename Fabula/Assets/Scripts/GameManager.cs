using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PopupController _PopupController;
    public LevelController _LevelController;

    public void winCondition(){
        _PopupController.winCondition();
    }

    public void reload(){
        SceneManager.LoadScene(0);
    }
}

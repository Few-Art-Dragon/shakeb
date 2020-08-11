using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{


    [SerializeField]
    private GameObject _gameOverMenu;

    private void OnEnable()
    {
        Score.finishBottle += OpenGameOverMenu;
    }

    private void OnDisable()
    {
        Score.finishBottle -= OpenGameOverMenu;
    }

    private void OpenGameOverMenu()
    {
        _gameOverMenu.SetActive(true);
    }

}

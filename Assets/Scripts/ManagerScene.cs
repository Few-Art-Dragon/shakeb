using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class ManagerScene : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _buttons;

    [SerializeField]
    private Sprite _soundOn, _soundOff, _pause, _play;
    [SerializeField]
    private Image _soundButton, _pauseButton;

    private bool isSoundOn, isPause;

    [SerializeField]
    private GameObject _gameOverMenu;
    private void Start()
    {
        isSoundOn = true;
    }

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
        ChangeTimeScale();
        _gameOverMenu.SetActive(true);
    }

    private void EnableOrDisableButtons()
    {
        
        foreach (var item in _buttons)
        {
            item.SetActive(isPause);
        }
    }

    // Pause game
    public void MakePause()
    {
        if (!isPause)
        {
            isPause = !isPause;
            Time.timeScale = 0;
            _pauseButton.sprite = _play;
        }
        else
        {
            
            isPause = !isPause;
            Time.timeScale = 1;
            _pauseButton.sprite = _pause;
        }
        EnableOrDisableButtons();
    }

    // Restart game
    public void RestartGame()
    {
        ChangeTimeScale();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 
    private void ChangeTimeScale()
    {
        Time.timeScale = 1;
    }

    // Turn on or off sound
    public void ChangeSound()
    {
        if (isSoundOn)
        {
            isSoundOn = !isSoundOn;
            _soundButton.sprite = _soundOff;
        }
        else
        {
            isSoundOn = !isSoundOn;
            _soundButton.sprite = _soundOn;
        }
    }

    // Go Main menu
    public void GoMainMenu()
    {
        ChangeTimeScale();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

}

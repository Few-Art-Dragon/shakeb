using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class ManagerScene : MonoBehaviour
{
    public delegate void MusicDelegate();
    public static event MusicDelegate GoMenuEvent, GoPauseEvent, GoGameEvent;

    [SerializeField]
    private GameObject[] _buttons;

    [SerializeField]
    private Sprite _soundOn, _soundOff, _pause, _play;
    [SerializeField]
    private Image _pauseButton;
    [SerializeField]
    private Image[] _soundButton;

    private bool isSoundOn, isPause;

    [SerializeField]
    private GameObject _gameOverMenu, _pauseMenu, _textScore, _textDescription;
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

        _textDescription.transform.DOLocalJump(new Vector3(0f, 0f, 0f), 1f, 1, 1f);

        _pauseMenu.transform.DOLocalJump(new Vector3(0f, -800f, 0f), 1f, 1, 1f);

        _gameOverMenu.transform.DOLocalJump(new Vector3(0f, 0f, 0f), 1f, 1, 1f);


        _textScore.transform.DOLocalJump(new Vector3(0f, 200f, 0f) , 2f, 1, 1f);
        _textDescription.SetActive(true);
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
            GoPauseEvent();
            isPause = !isPause;
            Time.timeScale = 0;
            _pauseButton.sprite = _play;
        }
        else
        {
            GoGameEvent();
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
            _soundButton[0].sprite = _soundOff;
            _soundButton[1].sprite = _soundOff;
        }
        else
        {
            isSoundOn = !isSoundOn;
            _soundButton[0].sprite = _soundOn;
            _soundButton[1].sprite = _soundOn;
        }
    }

    // Go Main menu
    public void GoMainMenu()
    {
        GoMenuEvent();
        ChangeTimeScale();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

}

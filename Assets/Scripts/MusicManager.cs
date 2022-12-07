using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class MusicManager : MonoBehaviour
{
    private static GameObject _musicManagerObject;
    [SerializeField]
    private AudioMixerSnapshot _mainMenuAudioMixer;
    [SerializeField]
    private AudioMixerSnapshot _firstGameModeAudioMixer;
    [SerializeField]
    private AudioMixerSnapshot _pauseAudioMixer;

    private void InitMusicManager()
    {
        if (_musicManagerObject == null)
        {
            _musicManagerObject = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void GoPause()
    {
        _pauseAudioMixer.TransitionTo(0.01f);
    }

    public void GoGame()
    {
        _firstGameModeAudioMixer.TransitionTo(1f);
    }

    public void GoMainMenu()
    {
        _mainMenuAudioMixer.TransitionTo(1f);
    }

    private void OnEnable()
    {
        MainMenu.GoGameOneEvent += GoGame;
        ManagerScene.GoMenuEvent += GoMainMenu;
        ManagerScene.GoGameEvent += GoGame;
        ManagerScene.GoPauseEvent += GoPause;
    }

    private void Awake()
    {
        InitMusicManager();
    }

    private void OnDisable()
    {
        MainMenu.GoGameOneEvent -= GoGame;
        ManagerScene.GoMenuEvent -= GoMainMenu;
        ManagerScene.GoGameEvent -= GoGame;
        ManagerScene.GoPauseEvent -= GoPause;
    }
}

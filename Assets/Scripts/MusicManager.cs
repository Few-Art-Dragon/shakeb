using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class MusicManager : MonoBehaviour
{
    

    private static GameObject _gameObject;
    [SerializeField]
    private AudioMixerSnapshot _mainmenu, _game1, _pause;
    // Start is called before the first frame update

    private void OnEnable()
    {
        MainMenu.GoGameOneEvent += GoGame;
        ManagerScene.GoMenuEvent += GoMainMenu;
        ManagerScene.GoGameEvent += GoGame;
        ManagerScene.GoPauseEvent += GoPause;
    }
    private void Awake()
    {
        if (_gameObject == null)
        {
            _gameObject = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    public void GoPause()
    {
        _pause.TransitionTo(0.01f);
    }

    public void GoGame()
    {
        
        _game1.TransitionTo(1f);
    }


    public void GoMainMenu()
    {
        _mainmenu.TransitionTo(1f);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public delegate void MusicDelegate();
    public static event MusicDelegate GoGameOneEvent;

    [SerializeField]
    [Range(0f,1)]
    private float _duraction;
    [SerializeField]
    private float _placeA;
    [SerializeField]
    private float _placeB;

    [SerializeField]
    private RectTransform _mainMenu, _gameMenu, _shopMenu, _bottleShopMenu, _leadboardMenu, _achievementMenu, _lines;

    public void GoGameMenu()
    {
        _mainMenu.DOMoveX(_placeB, _duraction);
        _gameMenu.DOMoveX(_placeA, _duraction);
    }

    //public void GoShopMenu()
    //{

    //    _mainMenu.DOMoveX(_placeB, _duraction);
    //    _shopMenu.DOMoveX(_placeA, _duraction);
    //}

    public void GoBottleShopMenu()
    {
        _lines.DOMoveX(_placeA, _duraction);
        _mainMenu.DOMoveX(_placeB, _duraction);
        _bottleShopMenu.DOMoveX(_placeA, _duraction);
    }

    //public void GoLeadBoardMenu()
    //{
    //    _mainMenu.DOMoveX(_placeB, _duraction);
    //    _leadboardMenu.DOMoveX(_placeA, _duraction);
        
    //}

    //public void GoAchievementMenu()
    //{
    //    _mainMenu.DOMoveX(_placeB, _duraction);
    //    _achievementMenu.DOMoveX(_placeA, _duraction);
    //}

    public void BackMainMenu()
    {
        _lines.DOMoveX(-_placeB, _duraction);
        _mainMenu.DOMoveX(_placeA, _duraction);
        _gameMenu.DOMoveX(_placeB, _duraction);
        _shopMenu.DOMoveX(_placeB, _duraction);
        _bottleShopMenu.DOMoveX(_placeB, _duraction);
        _leadboardMenu.DOMoveX(_placeB, _duraction);
        _achievementMenu.DOMoveX(_placeB, _duraction);
    }

    public void ShowAchievement()
    {
        PlayServices.instance.ShowAchievement();
    }

    public void ShowLederboard()
    {
        PlayServices.instance.ShowLeaderboard();
    }

    public void GoGameOne()
    {
        GoGameOneEvent();
        SceneManager.LoadScene("LVL1", LoadSceneMode.Single);
    }

    public void GoGameTwo()
    {
        SceneManager.LoadScene("LVL2", LoadSceneMode.Single);
    }

    public void GoExit()
    {
        Application.Quit();
    }
}

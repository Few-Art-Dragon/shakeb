using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public delegate void FinishBottle();
    public static event FinishBottle finishBottle;

    [SerializeField]
    private Camera _camera;

    private bool beginStart;

    [SerializeField]
    private SaveAndLoad _saveAndLoad;

    private Transform _transformText;

    private Vector3 _currentPos, _prevPos;

    [SerializeField]
    private Text _scoreText;

    private int _score;

    private void CheckPositionBottle()
    {
        _currentPos = _transformText.position;
        if (_currentPos.y >= _prevPos.y)
        {
            _prevPos = _currentPos;
        }
        else if (_currentPos.y < _prevPos.y)
        {
            beginStart = false;
            finishBottle();
            _saveAndLoad.Save(_score);
            StopAllCoroutines();
        }
    }

    private void AddScore()
    {
        StartCoroutine(IAddScore());
    }

    private void OnEnable()
    {
        Controller.startBottle += AddScore;
    }

    private void Start()
    {
        _transformText = GetComponent<Transform>();
        _currentPos = _transformText.position;
        _prevPos = _currentPos;
        _score = 0;

        Debug.Log(_camera.WorldToScreenPoint(_transformText.position));

        // _scoreText.rectTransform.position = _camera.ScreenToWorldPoint(_transformText.position);// WorldToScreenPoint(_transformText.position);
    }

    // Update is called once per frame
    private void Update()
    {
        if (beginStart)
        {
            CheckPositionBottle();
        }
    }

    private void OnDisable()
    {
        Controller.startBottle -= AddScore;
    }

    IEnumerator IAddScore()
    {
        beginStart = true;
        while (true)
        {
            _scoreText.GetComponent<RectTransform>().DOScale(1.3f, 0.25f);
            yield return new WaitForSeconds(0.25f);
            _scoreText.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 0.9f);
            _score += 1;
            _scoreText.text = _score.ToString();
        }
    }
}

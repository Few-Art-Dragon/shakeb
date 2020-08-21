using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    private bool beginStart;

    [SerializeField]
    private SaveAndLoad _saveAndLoad;

    public delegate void FinishBottle();
    public static event FinishBottle finishBottle;

    private Transform _transform;

    private Vector3 _currentPos, _prevPos;

    [SerializeField]
    private Text _scoreText;

    private int _score;
    private void OnEnable()
    {
        Controller.startBottle += AddScore;
    }
    private void OnDisable()
    {
        Controller.startBottle -= AddScore;
    }

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _currentPos = _transform.position;
        _prevPos = _currentPos;
        _score = 0;

        Debug.Log(_camera.WorldToScreenPoint(_transform.position));

       // _scoreText.rectTransform.position = _camera.ScreenToWorldPoint(_transform.position);// WorldToScreenPoint(_transform.position);


    }

    // Update is called once per frame
    void Update()
    {
        if (beginStart)
        {
            CheckPositionBottle();
            
        }

        

    }
    private void CheckPositionBottle()
    {
        _currentPos = _transform.position;
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

    IEnumerator IAddScore()
    {
        beginStart = true;
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            _score += 1;
            _scoreText.text = _score.ToString();
        }
    }
}

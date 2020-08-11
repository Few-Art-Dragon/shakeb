using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public delegate void FinishBottle();
    public static event FinishBottle finishBottle;

    private Transform _transform;
    [SerializeField]
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
    }

    // Update is called once per frame
    void Update()
    {
        CheckPositionBottle();

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
            finishBottle();
            StopAllCoroutines();
        }
    }

    private void AddScore()
    {
        StartCoroutine(IAddScore());
    }

    IEnumerator IAddScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            _score += 1;
            _scoreText.text = _score.ToString();
        }
    }
}

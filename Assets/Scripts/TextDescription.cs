using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDescription : MonoBehaviour
{
    private Text _text;
    [SerializeField]
    private string[] _textDescription;

    private int randomNum;

    private void Start()
    {
        _text = GetComponent<Text>();
        randomNum = Random.Range(0, _textDescription.Length + 1);
        GetText();
    }

    private void GetText()
    {
        for (int i = 0; i < _textDescription.Length; i++)
        {
            if (i == randomNum)
            {
                _text.text = _textDescription[i];
            }
        }
    }
}

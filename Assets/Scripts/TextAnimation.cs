using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TextAnimation : MonoBehaviour
{
    private RectTransform _transform;
    Sequence mySequence;
    void Start()
    {
        _transform = GetComponent<RectTransform>();
        mySequence = DOTween.Sequence();
        mySequence.Append(_transform.DOScale(1f, 0.9f));
        mySequence.Append(_transform.DOScale(0.7f, 0.6f));
        mySequence.SetLoops(-1);

    }
}

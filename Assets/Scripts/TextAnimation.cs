using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TextAnimation : MonoBehaviour
{
    private RectTransform _transformText;
    private Sequence _sequnceDOTween;
    private void Start()
    {
        _transformText = GetComponent<RectTransform>();
        _sequnceDOTween = DOTween.Sequence();
        _sequnceDOTween.Append(_transformText.DOScale(1f, 0.9f));
        _sequnceDOTween.Append(_transformText.DOScale(0.7f, 0.6f));
        _sequnceDOTween.SetLoops(-1);
    }
}

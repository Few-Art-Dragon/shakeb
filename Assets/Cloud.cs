using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(IDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckOnVisible()
    {
        if (!_spriteRenderer.isVisible)
        {
            gameObject.SetActive(false);
            StopAllCoroutines();
        }
        
    }

    IEnumerator IDestroy()
    {
        yield  return new WaitForSeconds(1f);
        CheckOnVisible();
    }
}

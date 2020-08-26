using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private Vector3 _target;
    private bool _canMove;
    private float _speedCloud;
    [SerializeField]
    private float minRand, maxRand;
    [SerializeField]
    private float distanceMoveCloud;
    private Transform _transform;

    private void OnEnable()
    {
        StartCoroutine(ICheckPositionNow());
    }
    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (_canMove)
        {
            MoveCloud();
        }
    }

    private void RandomSpeedCloud()
    {
        _speedCloud = Random.Range(minRand, maxRand);
    }
    private void MoveCloud()
    {
        _transform.position = Vector3.MoveTowards(_transform.position, _target, Time.deltaTime * _speedCloud);
    }

    private Vector3 CheckWhereMove()
    {
        Vector3 x = new Vector3(0f, 0f, 0f);
        if (_transform.position.x > -6.1f && _transform.position.x < 0f)
        {
            x = new Vector3(distanceMoveCloud, _transform.position.y, 0f);
        }
        else if (_transform.position.x > 0 && _transform.position.x < 6.1f)
        {
            x = new Vector3(-distanceMoveCloud, _transform.position.y, 0f);
        }
        else if (_transform.position.x == 0)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                x = new Vector3(distanceMoveCloud, _transform.position.y, 0f);
            }
            else if (rand == 1)
            {
                x = new Vector3(-distanceMoveCloud, _transform.position.y, 0f);
            }
        }
        return x;
    }

    private void CheckOnVisible()
    {
        if (transform.position.x < -6 || transform.position.x > 6)
        {
            _canMove = false;
            StopCoroutine(ICheckPositionNow());
            StopCoroutine(IDestroy());
            gameObject.SetActive(false);

        }
        //if (!_sprite.isVisible)
        //{
        //    _canMove = false;
        //    StopCoroutine(ICheckPositionNow());
        //    StopCoroutine(IDestroy());
        //    gameObject.SetActive(false);
        //}

    }
    

    IEnumerator ICheckPositionNow()
    {
        yield return new WaitForSeconds(0.5f);
        RandomSpeedCloud();
        _transform = GetComponent<Transform>();
        _target = CheckWhereMove();
        _canMove = true;
        StartCoroutine(IDestroy());
    }
    IEnumerator IDestroy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            CheckOnVisible();
        }

    }
}

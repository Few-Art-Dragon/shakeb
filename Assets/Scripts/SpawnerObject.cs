using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObject : MonoBehaviour
{


    private Transform _transform;
    [SerializeField]
    private float _minPosX, _maxPosX, _timeSpawn; 

    [SerializeField]
    private int _chanceProcent;

    [SerializeField]
    private GameObject[] _objects;

    private void OnEnable()
    {
        Controller.startBottle += SpawnObjects;
        Score.finishBottle += DisableSpawner;
    }

    private void OnDisable()
    {
        Controller.startBottle -= SpawnObjects;
        Score.finishBottle -= DisableSpawner;
    }

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private Vector3 RandomPositionForObject()
    {
        Vector3 positionObject = new Vector3(Random.Range(_minPosX, _maxPosX), _transform.position.y, _transform.position.z);
        return positionObject;
    }

    private GameObject CheckOnDisable()
    {
        GameObject _gameObject = null;
        foreach (var item in _objects)
        {
            if (item.active == false)
            {
                _gameObject = item;
                break;
            }
        }
        return _gameObject;
    }
    private int CheckChanceOnSpawnObject()
    {
        int chance = Random.Range(1, 101);
        return chance;
    }

    private void EnableObject()
    {
        GameObject _gameObject = null;
        _gameObject = CheckOnDisable();
        if (_gameObject != null)
        {
            _gameObject.SetActive(true);
            _gameObject.transform.position = RandomPositionForObject();
        }
    }

    private void SpawnObjects()
    {
        StartCoroutine(ISpawnObject());
    }

    private void DisableSpawner()
    {
        StopAllCoroutines();
    }

    IEnumerator ISpawnObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeSpawn);
            if (CheckChanceOnSpawnObject() > _chanceProcent)
            {
                EnableObject();
            }
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObject : MonoBehaviour
{
    public SideState ActiveState = SideState.Vertical;

    [SerializeField]
    private int _limitSpawnObject;
    [SerializeField]
    private bool _disableSpawnerWhenFinish;
    private Transform _transformObject;
    [SerializeField]
    private float _minPosX;
    private float _maxPosX;
    private float _intervalTimeSpawn;
    [SerializeField]
    private int _chanceProcent;
    [SerializeField]
    private bool _enableRandomOrderLayer;
    [SerializeField]
    private GameObject[] _listObjects;

    //Spawner begin work
    private void StartCoroutineSpawnObjects()
    {
        StartCoroutine(ISpawnObjects());
    }
    private int CheckCountSpawnerObject()
    {
        int count = 0;
        foreach (var i in _listObjects)
        {
            if (i.activeSelf)
            {
                count++;
            }
        }
        return count;
    }

    //Check chance in spawn object
    private int CheckChanceOnSpawnObject()
    {
        int chance = Random.Range(1, 101);
        return chance;
    }

    //Enable object
    private void EnableObject()
    {
        if (CheckOnEnableObject())
        {
            GameObject _gameObject = null;
            _gameObject = CheckOnDisable();
            if (_gameObject != null)
            {
                _gameObject.SetActive(true);
                _gameObject.transform.position = RandomPositionForObject();
                if (_enableRandomOrderLayer)
                {
                    _gameObject.GetComponent<SpriteRenderer>().sortingOrder = RandomOrderLayerForObject();
                }
            }
        }
    }

    //Check objects on active, if all objects not active, spawner don't work
    private bool CheckOnEnableObject()
    {
        bool i = false;
        foreach (var item in _listObjects)
        {
            if (item.activeSelf == false)
            {
                i = true;
                break;

            }
            else
            {
                i = false;
            }

        }
        return i;
    }

    //If object not active, it begin active
    private GameObject CheckOnDisable()
    {
        int num = RandomObjectForSpawn();
        GameObject _gameObject = null;

        if (_listObjects[num].activeSelf == false)
        {
            _gameObject = _listObjects[num];
        }
        else
        {
            return CheckOnDisable();
        }
        return _gameObject;
    }

    //Random object in array
    private int RandomObjectForSpawn()
    {
        int randomObj = Random.Range(0, _listObjects.Length);

        return randomObj;
    }

    //Random position for spawn object
    private Vector3 RandomPositionForObject()
    {
        Vector3 positionObject = new Vector3(0f, 0f, 0f);
        if (ActiveState == SideState.Vertical)
        {
            positionObject = new Vector3(_transformObject.position.x + Random.Range(_minPosX, _maxPosX), _transformObject.position.y, _transformObject.position.z);
        }
        else if (ActiveState == SideState.Horizontal)
        {
            positionObject = new Vector3(_transformObject.position.x, _transformObject.position.y + Random.Range(_minPosX, _maxPosX), _transformObject.position.z);
        }

        return positionObject;
    }

    //Random Order layer for object
    private int RandomOrderLayerForObject()
    {
        int num = Random.Range(0, 101);
        if (num % 2 == 0)
        {
            num = -1;
        }
        else
        {
            num = 1;
        }
        return num;
    }

    //Spawner finish work
    private void DisableSpawner()
    {
        StopAllCoroutines();
    } 

    private void OnEnable()
    {
        Controller.startBottle += StartCoroutineSpawnObjects;
        if (_disableSpawnerWhenFinish)
        {
            Score.finishBottle += DisableSpawner;
        }
    }

    private void Start()
    {
        _transformObject = GetComponent<Transform>();
    }

    private void OnDisable()
    {
        Controller.startBottle -= StartCoroutineSpawnObjects;
        if (_disableSpawnerWhenFinish)
        {
            Score.finishBottle -= DisableSpawner;
        }
    }

    IEnumerator ISpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(_intervalTimeSpawn);
            if (CheckCountSpawnerObject() < _limitSpawnObject)
            {
                if (CheckChanceOnSpawnObject() > _chanceProcent)
                {
                    EnableObject();
                }
            }  
        }
    }
    public enum SideState
    {
        Horizontal, 
        Vertical 
    };
}

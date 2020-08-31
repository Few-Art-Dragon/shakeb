using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SpawnerObject : MonoBehaviour
{
    [SerializeField]
    private int _limitSpawnObject;
    public enum SideState { Horizontal, Vertical};
    public SideState ActiveState = SideState.Vertical;

    [SerializeField]
    private bool _disableSpawnerWhenFinish;

    private Transform _transform;
    [SerializeField]
    private float _minPosX, _maxPosX, _timeSpawn; 

    [SerializeField]
    private int _chanceProcent;

    [SerializeField]
    private bool _enableRandomOrderLayer;

    [SerializeField]
    private GameObject[] _objects;

    private void OnEnable()
    {
        Controller.startBottle += SpawnObjects;
        if (_disableSpawnerWhenFinish)
        {
            Score.finishBottle += DisableSpawner;
        }

    }

    private void OnDisable()
    {
        Controller.startBottle -= SpawnObjects;
        if (_disableSpawnerWhenFinish)
        {
            Score.finishBottle -= DisableSpawner;
        }
    }

    private void Start()
    {
        _transform = GetComponent<Transform>();
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

    //Random position for spawn object
    private Vector3 RandomPositionForObject()
    {
        Vector3 positionObject = new Vector3(0f, 0f, 0f);
        if (ActiveState == SideState.Vertical)
        {
            positionObject = new Vector3(_transform.position.x + Random.Range(_minPosX, _maxPosX), _transform.position.y, _transform.position.z);
        }
        else if (ActiveState == SideState.Horizontal)
        {
            positionObject = new Vector3(_transform.position.x, _transform.position.y + Random.Range(_minPosX, _maxPosX), _transform.position.z);
        }
        
        return positionObject;
    }

    //Random object in array
    private int RandomObjectForSpawn()
    {
        int randomObj = Random.Range(0, _objects.Length);

        return randomObj;
    }

    //If object not active, it begin active
    private GameObject CheckOnDisable()
    {
        int num = RandomObjectForSpawn();
        GameObject _gameObject = null;

        if (_objects[num].active == false)
        {
            _gameObject = _objects[num];
        }
        else 
        {
            return CheckOnDisable();
        }
        return _gameObject;
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
                if(_enableRandomOrderLayer)
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
        foreach (var item in _objects)
        {
            if (item.active == false)
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

    //Spawner begin work
    private void SpawnObjects()
    {
        StartCoroutine(ISpawnObject());
    }

    //Spawner finish work
    private void DisableSpawner()
    {
        StopAllCoroutines();
    }

    private int CheckCountSpawnerObject()
    {
        int count = 0;
        foreach (var i in _objects)
        {
            if (i.activeSelf)
            {
                count++;
            }
        }
        return count;

    }



    IEnumerator ISpawnObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeSpawn);
            if (CheckCountSpawnerObject() < _limitSpawnObject)
            {
                if (CheckChanceOnSpawnObject() > _chanceProcent)
                {
                    EnableObject();
                }
            }  
        }
    }
}

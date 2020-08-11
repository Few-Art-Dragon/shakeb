using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    [SerializeField]
    private GameObject _particleWater;

    private void OnEnable()
    {
        _particleWater.SetActive(false);
        Controller.startBottle += StartWater;
    }
    private void OnDisable()
    {
        _particleWater.SetActive(false);
        Controller.startBottle -= StartWater;
    }

    private void StartWater()
    {
        _particleWater.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

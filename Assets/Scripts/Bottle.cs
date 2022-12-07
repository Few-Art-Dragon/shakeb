using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    [SerializeField]
    private float _speedSpriteBoom;
    [SerializeField]
    private GameObject _spriteBoom;
    [SerializeField]
    private float _timeDropCap, _timeDisableBoom, _timeWaitingStopCamera;
    [SerializeField]
    private Rigidbody2D _rigidbody2DCap;

    private GameObject _cinemachineCam;

    [SerializeField]
    private GameObject _particleWater;
    [SerializeField]
    private ParticleSystem _water;

    private void StartWater()
    {
        _water.Play();
        StartCoroutine(IDropCap());
    }

    private void StopWater()
    {
        _water.Stop();
    }

    private void StopCamera()
    {
        StartCoroutine(IWaitingStopCamera());
    }

    private void DestroyCoin(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            collision.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        StopWater();
        Controller.startBottle += StartWater;

        Score.finishBottle += StopCamera;
        Score.finishBottle += StopWater;
    }

    private void Start()
    {
        _cinemachineCam = GameObject.Find("CM vcam1");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DestroyCoin(collision);
    }

    private void OnDisable()
    {
        _particleWater.SetActive(false);
        Controller.startBottle -= StartWater;

        Score.finishBottle -= StopCamera;
        Score.finishBottle -= StopWater;
    }

    IEnumerator IDropCap()
    {
        yield return new WaitForSeconds(_timeDropCap);
        
        _spriteBoom.SetActive(true);
        _rigidbody2DCap.bodyType = RigidbodyType2D.Dynamic;
        _spriteBoom.transform.DOScale(new Vector3(0.4f, 0.4f, 0.4f), _speedSpriteBoom);
        StartCoroutine(IDisableBoom());
    }
    IEnumerator IDisableBoom()
    {
        yield return new WaitForSeconds(_timeDisableBoom);
        _spriteBoom.SetActive(false);
    }

    IEnumerator IWaitingStopCamera()
    {
        yield return new WaitForSeconds(_timeWaitingStopCamera);
        _cinemachineCam.GetComponent<CinemachineVirtualCamera>().enabled = false;
        //_cinemachineCam.SetActive(false);
    }
}

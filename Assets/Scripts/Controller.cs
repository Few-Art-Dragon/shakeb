using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Controller : MonoBehaviour
{
    public delegate void StartBottle();
    public static event StartBottle startBottle;


    private Vector3 accelerator = Vector3.zero;

    [SerializeField]
    private int countShake;
    
    private int nowTimer;

    [SerializeField]
    private float speed;

    private bool gameOver;
    private bool isFirstShake;
    private bool isShaked;

    [SerializeField]
    private Rigidbody2D _rg2D;

    void Start()
    {
        _rg2D = GetComponent<Rigidbody2D>();
        isFirstShake = true;
        accelerator = Input.acceleration;
        countShake = 12;
        nowTimer = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            ShakeBottle();
        }
    }
    private void OnMouseDown()
    {
        startBottle();
        PushBottle();
    }

    private void ShakeBottle()
    {
        accelerator = Input.acceleration;
        if (isFirstShake && !isShaked && accelerator.sqrMagnitude >= 5f )
        {
            StartCoroutine(ITimer());
            isFirstShake = false;
            countShake++;
        }
        else if (accelerator.sqrMagnitude >= 5f && !isShaked && !isFirstShake)
        {
            isShaked = true;
            countShake++;
        }
        else if (accelerator.sqrMagnitude < 5f)
        {
            isShaked = false;
        }
    }

    private void PushBottle()
    {
        _rg2D.AddForce(transform.up * countShake * speed, ForceMode2D.Impulse);
        countShake = 0;
    }


    IEnumerator ITimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            nowTimer++;
            if (nowTimer >= 5)
            {
                Handheld.Vibrate();
                gameOver = true;
                isFirstShake = true;
                nowTimer = 0;
                startBottle();
                PushBottle();
                StopAllCoroutines();
            }
        }
    }

}

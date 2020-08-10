using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public Text text;

    private Vector3 accelerator = Vector3.zero;
    [SerializeField]
    private int countShake;
    
    private int nowTimer;

    public float speed;

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
        StartCoroutine(IStart());

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            ShakeBottle();
        }
        


        //Vector3 dir = Vector3.zero;

        //dir.x = -Input.acceleration.y;
        //dir.z = Input.acceleration.x;

        //if (dir.sqrMagnitude > 1)
        //    dir.Normalize();

        //dir *= Time.deltaTime;

        //transform.Translate(dir * speed);
        //text.text = "X: " + dir.x.ToString("0.00") + " Y: " + dir.y.ToString("0.00") + " Z: " + dir.z.ToString("0.00");



    }
    private void OnMouseDown()
    {
        StartBottle();
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


        //text.text = countShake.ToString();

    }

    private void StartBottle()
    {
        _rg2D.AddForce(transform.up * countShake * speed, ForceMode2D.Impulse);
    }
    IEnumerator IStart()
    {

        yield return new WaitForSeconds(2f);
        
        StartBottle();
    }

    IEnumerator ITimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            nowTimer++;
            if (nowTimer >= 5)
            {
                gameOver = true;
                isFirstShake = true;
                nowTimer = 0;
                countShake = 0;
                StopCoroutine(ITimer());
            }
        }
    }

}

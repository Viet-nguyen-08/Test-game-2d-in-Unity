using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPro : MonoBehaviour
{
     
    public float moveTimeInterpo = 0.5f;
    public float dashInterpo = 5;
    private float timerInterpo;
    private Vector3 startPos, targetPos;
    private bool isDash;

    
    void Start()
    {
        
    }

   
    void Update()
    {
        //GetComponent<PlayerMove>().flip();
        if (isDash)
        {
            timerInterpo += Time.deltaTime;
            float t = timerInterpo / moveTimeInterpo;
            
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            if(t >= moveTimeInterpo)
            {
                transform.position = Vector3.Lerp(targetPos, startPos, t);
                if(t >= moveTimeInterpo*2) isDash = false;

            }
            Debug.Log("bool 'isDash' is start");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            dash();
            Debug.Log("button H is start");
        }
    }
    void dash()
    {
        isDash = true;
        timerInterpo = 0f;
        startPos = transform.position;
        float dir = Mathf.Sign(transform.position.x);
        targetPos = startPos + new Vector3(dir * dashInterpo, 0f, 0f);
        Debug.Log("function dash() was called");
    }
}

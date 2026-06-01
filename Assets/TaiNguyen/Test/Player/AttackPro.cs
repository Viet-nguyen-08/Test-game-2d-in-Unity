using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPro : MonoBehaviour
{  
    public float moveTimeInterpo = 0.5f;
    public float dashInterpo = 5;
    private float timerInterpo, t;
    private Vector3 startPos, targetPos;
    private bool goOut, goBack, oneHit;
    private int dem;
    public float dam = 2f;
    public Transform PointAttPr;
    public LayerMask enemyLayer;
    public Vector2 boxsize = new Vector2(2f, 1f);
    
    
    void Start()
    {
        
    }

   
    void Update()
    {
        if (goOut)
        {
            timerInterpo += Time.deltaTime;
            t = timerInterpo / moveTimeInterpo;
            transform.localPosition = Vector3.Lerp(startPos, targetPos, t);
            damCollider();
            if(t >= 1f)
            {
                goOut = false;
                goBack = true;
                t = 0f;
            }
        }
        if (goBack)
        {
            timerInterpo += Time.deltaTime;
            t = timerInterpo / moveTimeInterpo;
            transform.localPosition = Vector3.Lerp(targetPos, startPos, t);
            if(t >= 1f)
            {
                goBack = false;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.H))
        {
            dash();

        }
        
    }
    void dash()
    {
        goOut = true;
        goBack = false;
        timerInterpo = 0f;
        startPos = new Vector3(0f, 0f, 0f);
        float dir = Mathf.Sign(transform.localScale.x);
        targetPos = startPos + new Vector3(dir * dashInterpo, 0f, 0f);
    }
    void damCollider()
    {
        Collider2D[] colli = Physics2D.OverlapBoxAll(PointAttPr.position, boxsize, 0f, enemyLayer);
        for(int i = 0; i < colli.Length; i++)
        {
            colli[i].GetComponent<EnemyScript>(). TakeDamage(dam);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(PointAttPr.position, boxsize);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
   [SerializeField] private float distance = 5f;
   public float health;
   public float move, move2 = 0.5f;
   private Animator anim;
   public CamScript CamShake;
   private float deltaTime1;
   public float deltaTime2;
   private Vector3 startPos;
   private bool movingRight = true;
   void Start()
   {
      anim = GetComponent<Animator> ();
      startPos = transform.position;
   }  
   void Update()
   {
      float moveR = startPos.x + distance;
      float moveL = startPos.x - distance;
      if (movingRight)
      {
         transform.Translate(Vector2.right * move * Time.deltaTime);
         transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
         if(transform.position.x >= moveR)
         {
            movingRight = false;
         }
      }
      else
      {
         transform.Translate(Vector2.left * move * Time.deltaTime);
         transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f);
         if(transform.position.x <= moveL)
         {
            movingRight = true;
         }
      }
      if(deltaTime1 <= 0)
      {
         move = move2;
      }
      else
      {
         move = 0;
         deltaTime1 -= Time.deltaTime;
      }
   }
   public void TakeDamage(float damage)
   {
      deltaTime1 = deltaTime2;
      CamShake.Shake();
      anim.SetTrigger("Damaged");
      health -= damage;
      Debug.Log("enemy is " + health + " hp");
      if(health <= 0) Debug.Log("enemy is dead");
   }
}

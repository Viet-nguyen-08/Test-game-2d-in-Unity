using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
   private int hehehe;
   public float health;
   public float move;
   private Animator anim;
   public CamScript CamShake;
   private float deltaTime1;
   public float deltaTime2;
   void Start()
   {
      anim = GetComponent<Animator> ();
   }  
   void Update()
   {
      transform.Translate(Vector2.left * move * Time.deltaTime);
      if(deltaTime1 <= 0)
      {
         move = 0.5f;
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

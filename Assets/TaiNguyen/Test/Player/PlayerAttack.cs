using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float PunchDamage = 5;                       //sát thương player tùy chỉnh
    public float radius;                                // chiều rộng của vùng gây damage
    public Transform attackPoint;                       // điểm gây damage
    public LayerMask enemyLayer;                        // lớp layer để tương tác
    private Animator anim;
    private bool isAtt = false;   
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
       PunchAttack();      
    }
    void PunchAttack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetBool("isAtt", !isAtt);                       
        }       
    }
    void getAttackPounch()                                      // được gọi trong animation event
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position , radius, enemyLayer);// cú pháp ở đây là điểm gây dame +position, chiều rộng, lớp tương tác
            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyScript> ().TakeDamage(PunchDamage);
            }
    }
    void endAttackPounch()                                  // được gọi trong animation event
    {
        anim.SetBool("isAtt", isAtt);
    }
    void OnDrawGizmosSelected()                             // một phương thức riêng để vẽ hình ra
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, radius);    // điểm + position, chiều rộng
    }
}

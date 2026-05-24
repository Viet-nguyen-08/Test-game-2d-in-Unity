using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // what thẻ hell
    public float damage = 5;    //sát thương player tùy chỉnh
    public float radius;    // chiều rộng của vùng gây damage
    public Transform attackPoint;   // điểm gây damage
    public LayerMask enemyLayer;    // lớp layer để tương tác
    private Animator anim;
    private bool isAtt = false;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
       attack();
    }
    void attack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetBool("isAtt", !isAtt);
                       
        }       
    }
    void getAttack()
    {
        // cú pháp ở đây là điểm gây dame +position, chiều rộng, lớp 
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position , radius, enemyLayer);
            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyScript> ().TakeDamage(damage);
            }
    }
    void endAttack()
    {
        anim.SetBool("isAtt", isAtt);
    }
    void OnDrawGizmosSelected()     // một phương thức riêng để hiện hình ra
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, radius);    // điểm + position, chiều rộng
    }
}

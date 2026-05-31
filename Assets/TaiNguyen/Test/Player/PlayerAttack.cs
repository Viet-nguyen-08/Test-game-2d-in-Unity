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
    private bool isAtt;   
    private float timer;
    private int hit;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("isAttacking");
            isAtt = true;
            timer = 0f;
            hit = 0;
        }
            if (isAtt)
            {
                timer += Time.deltaTime;
                if(timer >= 0.3f)
                {
                    hit++;
                    getAttackPounch();
                    timer = 0f;
                    if(hit >= 4)
                    {
                        isAtt = false;
                    }
                }
            }             
    }
    void getAttackPounch()                                      
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position , radius, enemyLayer);// cú pháp ở đây là điểm gây dame +position, chiều rộng, lớp tương tác
            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyScript>().TakeDamage(PunchDamage);
            }
    }
    void OnDrawGizmosSelected()                                 // một phương thức riêng để vẽ hình ra
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, radius);    // điểm + position, chiều rộng
    }
}

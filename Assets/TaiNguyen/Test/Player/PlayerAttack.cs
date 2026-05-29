using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // ồ nô hay ho thật đấy
    public float PunchDamage = 5, TeleDamage = 2;       //sát thương player tùy chỉnh
    public float radius;                                // chiều rộng của vùng gây damage
    public Vector2 boxsize = new Vector2(2f, 1f);
    public Transform attackPoint, telePoint;            // điểm gây damage
    public LayerMask enemyLayer;                        // lớp layer để tương tác
    private Animator anim;
    private bool isAtt = false;
    private float teleTime = 0f;
    
    
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
    void getAttackPounch()                                  // được gọi trong animation event
    {
                                                            // cú pháp ở đây là điểm gây dame +position, chiều rộng, lớp tương tác
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position , radius, enemyLayer);
            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyScript> ().TakeDamage(PunchDamage);
            }
    }
    void endAttackPounch()                                  // được gọi trong animation event
    {
        anim.SetBool("isAtt", isAtt);
    }
   
    public void getAtackTele()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(telePoint.position, boxsize, 0f, enemyLayer);
        for(int i = 0; i < enemies.Length; i++) enemies[i].GetComponent<EnemyScript> ().TakeDamage(TeleDamage);
    }
    void OnDrawGizmosSelected()                             // một phương thức riêng để vẽ hình ra
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, radius);    // điểm + position, chiều rộng
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(telePoint.position, boxsize);
    }
}

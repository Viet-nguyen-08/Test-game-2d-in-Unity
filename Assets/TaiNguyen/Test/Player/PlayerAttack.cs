using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // ồ nô hay ho thật đấy
    public float PunchDamage = 5, TeleDamage = 2;    //sát thương player tùy chỉnh
    public float radius, radius2;     // chiều rộng của vùng gây damage
    public Transform attackPoint, telePoint;    // điểm gây damage
    public LayerMask enemyLayer;    // lớp layer để tương tác
    private Animator anim;
    private bool isAtt = false;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
       PunchAttack();
        TeleAttack();
    }
    void PunchAttack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetBool("isAtt", !isAtt);
                       
        }       
    }
    void getAttackPounch()
    {
        // cú pháp ở đây là điểm gây dame +position, chiều rộng, lớp 
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position , radius, enemyLayer);
            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyScript> ().TakeDamage(PunchDamage);
            }
    }
    void endAttackPounch()
    {
        anim.SetBool("isAtt", isAtt);
    }
    void TeleAttack()
    {
        if(Input.GetKeyDown(KeyCode.J)) getAtackTele();
    }
    void getAtackTele()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(telePoint.position, radius2, enemyLayer);
        for(int i = 0; i < enemies.Length; i++) enemies[i].GetComponent<EnemyScript> ().TakeDamage(TeleDamage);
    }
    void OnDrawGizmosSelected()     // một phương thức riêng để hiện hình ra
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, radius);    // điểm + position, chf roj
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(telePoint.position, radius2);
    }
}

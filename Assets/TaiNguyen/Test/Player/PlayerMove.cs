using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float move, jump; 
    public float deltaTimeDamF, deltaTimeDamH;                   // sát thương tùy chỉnh
    public float dashDistance = 3f, moveTimeInterpo = 1f;              // khoảng cách lướt và chia thời gian
    [SerializeField] private  Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded, isLerping, isDashAttack;           
    private Rigidbody2D rb;
    private Animator ani;
    private float moveInput, deltaTime3, timerInterpo;      
    private Vector3 startPos, targetPos;                        // nội suy
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        ani = GetComponent<Animator> ();
    }
    void Update()
    {     
        if(!isLerping)
        {
            moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(move * moveInput, rb.velocity.y);
            if(moveInput > 0) transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            if(moveInput < 0) transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f);
            bool run = Mathf.Abs(rb.velocity.x) > 0.1f;
            ani.SetBool("isRuning", run);       
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jump);
                Debug.Log("wtf bth mà");
            }
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer); 
        }         
        if(deltaTime3 <= 0) move = 5;
        else
        {
            move = 0;
            deltaTime3 -= Time.deltaTime;                               // thuật toán đơn giản để khóa di chuyển
        }              
        if (isLerping)
        {
            timerInterpo += Time.deltaTime;
            float f = timerInterpo / moveTimeInterpo;
            transform.position = Vector3.Lerp(startPos, targetPos, f);
            if(f >= 1)
            {
                isLerping = false;
                isDashAttack = false;
            }            
        }
        if(Input.GetKeyDown(KeyCode.F))
        {            
            ani.SetTrigger("isAttacking");
            deltaTime3 = deltaTimeDamF;
        }  
        if (Input.GetKeyDown(KeyCode.J)) StartDash();
    }
    public void flip()
    {
        if(moveInput > 0) transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        if(moveInput < 0) transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f); 
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(isDashAttack && collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyScript>(). TakeDamage(2);
        }
    }
    void StartDash()
    {
        isLerping = true;
        isDashAttack = true;
        timerInterpo = 0f;
        startPos = transform.position;
        float dir = Mathf.Sign(transform.localScale.x);
        targetPos = startPos + new Vector3(dir * dashDistance, 0f, 0f);
    }
}

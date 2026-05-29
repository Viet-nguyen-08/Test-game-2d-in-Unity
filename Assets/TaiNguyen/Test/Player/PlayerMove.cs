using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float move, jump, deltaTimeDam, dashDistance = 3f, dashTime = 0.3f,  moveTime = 1f;

    [SerializeField] private  Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded, isLerping, isDashAttack;              
    private Rigidbody2D rb;
    private Animator ani;
    private float moveInput, deltaTime3, timer, teleTime;      
    private Vector3 startPos, targetPos;
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
            deltaTime3 -= Time.deltaTime;   // thuật toán đơn giản để khóa di chuyển
        } 
        if(Input.GetKeyDown(KeyCode.F)) deltaTime3 = deltaTimeDam;
        
        if (isLerping)
        {
            timer += Time.deltaTime;
            float f = timer / moveTime;
            transform.position = Vector3.Lerp(startPos, targetPos, f);
            if(f >= 1)
            {
                isLerping = false;
                isDashAttack = false;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.J)) StartDash();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(isDashAttack && collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyScript> ().TakeDamage(2);
        }
    }
    void StartDash()
    {
        isLerping = true;
        isDashAttack = true;
        timer = 0f;
        startPos = transform.position;
        float dir = Mathf.Sign(transform.localScale.x);
        targetPos = startPos + new Vector3(dir * dashDistance, 0f, 0f);
    }
    
    
}

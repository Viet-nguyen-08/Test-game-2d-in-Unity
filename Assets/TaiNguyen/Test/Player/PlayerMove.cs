using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float move;
    public float jump;
    [SerializeField] private  Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded ;
    private Rigidbody2D rb;
    private Animator ani;
    private float moveInput;
    private float deltaTime3;
    public float deltaTime4;
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        ani = GetComponent<Animator> ();
    }
    void Update()
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
        if(deltaTime3 <= 0) move = 5;
        else
        {
            move = 0;
            deltaTime3 -= Time.deltaTime;   // thuật toán đơn giản để khóa di chuyển
        } 
        if(Input.GetKeyDown(KeyCode.F)) deltaTime3 = deltaTime4;     
    }
    
}

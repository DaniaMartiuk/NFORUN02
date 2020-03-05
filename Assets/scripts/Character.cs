using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{
    [SerializeField]
    private float speedRun;
    [SerializeField]
    private float jumpForce;
    private bool isGround = false;
    private Vector2 sizeCollider;
    private Vector2 offsetColloder;
    private Rigidbody2D RB;
    private Animator animator;
    private SpriteRenderer SR;
    new private CapsuleCollider2D collider2D;
    private void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            Run();
        }
        else if(isGround && !Input.GetButton("Vertical"))
        {
            State = CharacterState.Stop;
        }
        if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")>0 && isGround)
        {
            Jump();
        }
        if (Input.GetButton("Vertical") && Input.GetAxis("Vertical") < 0 && isGround)
        {
            Crouch();
        }
        else
        {
            collider2D.size = sizeCollider;
            collider2D.offset = offsetColloder;
        }

    }
    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SR = GetComponentInChildren<SpriteRenderer>();
        collider2D = GetComponent<CapsuleCollider2D>();
        jumpForce = 10f;
        speedRun = 7f;
        offsetColloder = collider2D.offset;
        sizeCollider = collider2D.size;
    }
    private void FixedUpdate()
    {
        CheckGround();
    }
    private CharacterState State
    {
        get { return (CharacterState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }
    
    private void Crouch()
    {
        State = CharacterState.Down;
        collider2D.offset.Set(collider2D.offset.x, 0.55F);
        collider2D.size.Set(collider2D.size.x, 1.2f);

    }
    private void Run()
    {
        if (isGround)
            State = CharacterState.Run;
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.Lerp(transform.position, transform.position + direction, speedRun * Time.deltaTime);
        SR.flipX = (direction.x < 0);
    }
    private void Jump()
    {
        RB.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    private void CheckGround()
    {   
        isGround = Physics2D.OverlapCircleAll(transform.position, 0.1f).Length > 1;
        if (!isGround)
            State = CharacterState.Jump;
    }

}
public enum CharacterState
{
    Stop,//0
    Run,//1
    Jump,//2
    Down//3

}

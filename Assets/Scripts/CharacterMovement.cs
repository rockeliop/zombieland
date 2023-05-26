using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float maxSpeed = 6.0f;
    public float moveDirection;
    public float jumpSpeed = 600.0f;
    public float groundRadius = 0.2f;
    public bool grounded;
    public bool facingRight = true;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private Animator anim;
    private Rigidbody rgbdy;

    public Transform characterContainer;

    // Start is called before the first frame update
    void Start()
    {
        rgbdy = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();


    }
    void FixedUpdate()
    {
        rgbdy.velocity = new Vector2(moveDirection * maxSpeed, rgbdy.velocity.y);
         grounded = Physics.Raycast(transform.position, -Vector3.up ,groundRadius,whatIsGround);

        if (moveDirection > 0.0f && !facingRight)
        {
            Flip();
        } else if (moveDirection < 0.0f && facingRight)
        {
            Flip();

        }

        anim.SetFloat("Speed", Mathf.Abs(moveDirection));
        anim.SetBool("Ground", grounded);
    }
    void Flip()
    {
        facingRight = !facingRight;
        characterContainer.Rotate(Vector3.up, 180.0f, Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = Input.GetAxis("Horizontal");

         if(grounded && Input.GetButtonDown("Jump")){
            rgbdy.AddForce(new Vector2(0, jumpSpeed));
        }

    }
}

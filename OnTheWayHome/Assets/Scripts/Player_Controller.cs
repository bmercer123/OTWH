using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    //start variables
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;

    public int cherries = 0;

    //Finite State Machine
    private enum State {idle, running, jumping, falling};
    private State state = State.idle;

    //Inspector variables
    [SerializeField]private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 25f;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
        AnimationState();
        anim.SetInteger("state", (int)state); //Set animation based on enumerator state
    }

    //running trigger colision for colliders
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectible")
        {
            Destroy(collision.gameObject);
            cherries += 1;
        }
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        //Character movement (Ex. Press A key or left arrow and the character goes left )

        //Moving Left
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            // Transform so that the sprite position changes with the movement of the character
            transform.localScale = new Vector2(-1, 1);
        }

        //Moving Right
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

        //Jumping
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.jumping;
        }
    }

    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling) 
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }


        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            //Moving 
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }
}

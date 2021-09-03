using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private enum State {idle, running, jumping};
    private State state = State.idle;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float hDirection = Input.GetAxis("Horizontal");
        //Character movement (Ex. Press A key or left arrow and the character goes left )

        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
            // Transform so that the sprite position changes with the movement of the character
            transform.localScale = new Vector2(-1, 1);
        }


        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

        else 
        {
             
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
            state = State.jumping;
        }
        VelocityState();
        anim.SetInteger("state", (int)state);

    }

    private void VelocityState()
    {
        if (state == State.jumping)
        {

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField]private float leftCap;
    [SerializeField]private float rightCap;

    [SerializeField] private float jumpLength = 10f;
    [SerializeField] private float jumpHeight = 20f;
    [SerializeField] private LayerMask ground;
    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator anim;

    private bool facingLeft = true;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Transit form Jumping to Falling
        if (anim.GetBool("Jumping")) 
        {
            if (rb.velocity.y < .1)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }

        //Transit from Falling to Idle
        {
            if (coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
            {
                anim.SetBool("Falling", false);
            }
        }
    }

    private void eMovement()
    {
        if (facingLeft)
        {
            //Test to see if beyond leftCap
            if (transform.position.x > leftCap)
            {
                //Check for sprite facing direction, if not correct, then face correct direction
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                //Check if object on the ground, if so, jump
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = false;
            }
        }
        else
        {
            {
                //Test to see if beyond leftCap
                if (transform.position.x < rightCap)
                {
                    //Check for sprite facing direction, if not correct, then face correct direction
                    if (transform.localScale.x != -1)
                    {
                        transform.localScale = new Vector3(-1, 1);
                    }
                    //Check if object on the ground, if so, jump
                    if (coll.IsTouchingLayers(ground))
                    {
                        rb.velocity = new Vector2(jumpLength, jumpHeight);
                        anim.SetBool("Jumping", true);
                    }
                }
                else
                {
                    facingLeft = true;
                }
            }
        }
    }

    public void JumpedOn()
    {
        anim.SetTrigger("Death");
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}

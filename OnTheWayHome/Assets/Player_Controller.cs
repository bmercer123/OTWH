using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        //Character movement (Ex. Press A key or left arrow and the character goes left )

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
            // Transform so that the sprite position changes with the movement of the character
            transform.localScale = new Vector2(-1, 1);
            anim.SetBool("running", true);
        }


        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            anim.SetBool("running", true);
        }

        else 
        {
            anim.SetBool("running", false); 
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }
    }
}

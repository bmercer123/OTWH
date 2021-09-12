using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{

    //start variables
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    

    //Finite State Machine
    private enum State {idle, running, jumping, falling, hurt};
    private State state = State.idle;

    //Inspector variables
    [SerializeField]private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 25f;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private AudioSource cherry;
    [SerializeField] private AudioSource footstep;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        //Using perm from the perm script 
        PermanentUI.perm.healthAmount.text = PermanentUI.perm.health.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        if (state != State.hurt)
        {
            Movement();
        }
        AnimationState();
        anim.SetInteger("state", (int)state); //Set animation based on enumerator state

    }

    //running trigger colision for colliders
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectible")
        {
            cherry.Play();
            Destroy(collision.gameObject);
            PermanentUI.perm.cherries += 1;
            PermanentUI.perm.cText.text = PermanentUI.perm.cherries.ToString();
        }
        if (collision.tag == "PowerUp")
        {
            cherry.Play();
            Destroy(collision.gameObject);
            jumpForce = 30;
            GetComponent<SpriteRenderer>().color = Color.green;
            StartCoroutine(ResetPower());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy") 
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            //destroying an enemy object when falling on them 
            if (state == State.falling)
            {
                enemy.JumpedOn();
                Jump();
            }
            else
            {
                // hurt state
                state = State.hurt;
                PermanentUI.perm.health -= 1;
                HealthManager();// deals with health reduction and if health less then zero will load new game
                if (PermanentUI.perm.health <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                }
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    //Enemy is to the players right, meaning damage on player should move him left
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    //Enemy is to the left, so player moves right on damage
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }
    }

    private void HealthManager()
    {
        PermanentUI.perm.healthAmount.text = PermanentUI.perm.health.ToString();
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
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }

    private void AnimationState()
    {
        if (state == State.jumping)
        {
            //when y velocity is very close to 0, start falling state
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            //if while falling we touch ground go to idle state
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt) 
        {
            //when hurt and x absolute value almost 0, go to idle state
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        //when velocity on x is over 2, start running state
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

    private void FootStep() {
        footstep.Play();

    }
    private IEnumerator ResetPower() {
        yield return new WaitForSeconds(15);
        jumpForce = 20;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
  

    private float jumptimeCounter; 
    public float jumpForce; 
    public float jumptime; 

    public LayerMask whatIsGround;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;



    private Rigidbody2D rb;
    private Animator anim;

    private bool isKicking;
    private bool isRunning;
    private bool isSliding;
    private bool isHurt;
    private bool isJumping;

    private bool isJetPack_hurt;
    private bool isGun_hurt;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        // This script is made for the purpose of previewing the animations only, you must modify it or create your own character controller script that fits your need, I'm an artist / animator, I'm only selling the character and its animation ;)

 


        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump_takeOff");
            isJumping = true;
            jumptimeCounter = jumptime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (isGrounded == true)
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }

       

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetTrigger("Walk");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("Run");
        }


     

        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("isSliding", true);
        }
        else
        {
            anim.SetBool("isSliding", false);
        }


        if (Input.GetKeyDown(KeyCode.H))
        {
            anim.SetBool("isHurt", true);
        }
        else
        {
            anim.SetBool("isHurt", false);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetTrigger("Defeat");
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            anim.SetTrigger("Idle_gun");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            anim.SetTrigger("Walk_gun");
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            anim.SetTrigger("Run_gun");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            anim.SetBool("isGun_hurt", true);
        }
        else
        {
            anim.SetBool("isGun_hurt", false);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetTrigger("JetPack_idle_gun");
        }
     

        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetTrigger("JetPack_motion_gun");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            anim.SetBool("isJetPack_hurt", true);
        }
        else
        {
            anim.SetBool("isJetPack_hurt", false);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("isLookingUp", true);
        }
        else
        {
            anim.SetBool("isLookingUp", false);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetBool("isCrouching", true);
        }
        else
        {
            anim.SetBool("isCrouching", false);
        }

        if (Input.GetKey(KeyCode.F))
        {
            anim.SetBool("isKicking", true);
        }
        else
        {
            anim.SetBool("isKicking", false);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("Idle");
        }
    }
}

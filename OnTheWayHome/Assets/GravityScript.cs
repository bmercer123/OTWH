using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<Player_Controller>().enabled = false;
        }
    }
}

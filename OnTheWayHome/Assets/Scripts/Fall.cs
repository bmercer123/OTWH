using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fall : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            PermanentUI.perm.health -= 1;
            PermanentUI.perm.healthAmount.text = PermanentUI.perm.health.ToString();
            PermanentUI.perm.Reset();
        }
        /*if (PermanentUI.perm.health <= 0)
        {
            //when player health reaches zero, go to the Game Over scene
            //Fix issue with method calling --  An object reference is required for the non-static field, method, or property
            Player_Controller.loadEnd("GameOver");

        }*/
    }
}

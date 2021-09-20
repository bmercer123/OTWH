using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    
    [SerializeField] private string SceneName;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneName);
        }
    }
    public void PlayGame() {
        SceneManager.LoadScene("SampleScene");
    }
    public void Guide()
    {
        SceneManager.LoadScene("Game Guide");
    }
    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PermanentUI : MonoBehaviour
{
    // PLayer Stats that carry over from previous level
    public int cherries = 0;
    public int health = 5;
    public Text cText;
    public Text healthAmount;
    //Making PermanentUI public so it can be accessed from each scene that uses the variables
    public static PermanentUI perm;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        //Singleton patter, only 1 instance of this object should exist
        //If there is no perm -> use this one, if there is -> destroy it
        if(!perm)
        {
            perm = this;
        }
        else
        {
            Destroy(gameObject);
        }
}
    public void Reset()
    {
        cherries = 0;
        cText.text = cherries.ToString();
    }
}

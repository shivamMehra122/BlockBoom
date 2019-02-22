using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public void onRetryButtonPress()
    {
        SceneManager.LoadScene("game");
    }


     public void onPlayButtonPress()
    {
        SceneManager.LoadScene("game");
    }

    public void onQuit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();


        Application.Quit();
    }

   public void onMenuBackButton()
    {
        SceneManager.LoadScene("Main");
    }

}

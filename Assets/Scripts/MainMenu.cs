using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    //N.Nkosi
    public int PlayLevelIndex;


    public void PlayGame()
    {
        SceneManager.LoadScene("Interrogate");
    }


    public void QuitGame()
    {
        print("Quit");
        UnityEngine.Application.Quit();
    }

    /*  Brackeys
       Start Menu in Unity
       Nov 29, 2017
       https://www.youtube.com/watch?v=zc8ac_qUXQY
   */
}

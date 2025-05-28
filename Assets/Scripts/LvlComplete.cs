using UnityEngine.SceneManagement;
using UnityEngine;

public class LvlComplete : MonoBehaviour
{ 
    public int MenuLevelIndex;

    public void GoToMenu()
    {
        SceneManager.LoadScene(MenuLevelIndex);
    }

   
}

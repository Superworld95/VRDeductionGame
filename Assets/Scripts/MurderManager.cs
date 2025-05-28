using UnityEngine;
using UnityEngine.SceneManagement;

public class MurdererManager : MonoBehaviour
{
    public enum SuspectName { Brad, Sara, Tom }

    private SuspectName actualMurderer;

    [Header("Scene Names")]
    public string winSceneName = "WinScene";
    public string loseSceneName = "GameOverScene";

    void Start()
    {
        RandomizeMurderer();
    }

    void RandomizeMurderer()
    {
        actualMurderer = (SuspectName)Random.Range(0, System.Enum.GetValues(typeof(SuspectName)).Length);
        Debug.Log("Murderer has been chosen (secret)."); // Don’t reveal to player!
    }

    // Call this method when player makes their guess (e.g. from UI button)
    public void PlayerGuess(string suspectGuess)
    {
        SuspectName guessedMurderer;
        if (System.Enum.TryParse(suspectGuess, out guessedMurderer))
        {
            if (guessedMurderer.ToString().Equals(actualMurderer.ToString(), System.StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log("Correct! You win!");
                SceneManager.LoadScene(winSceneName);
            }
            else
            {
                Debug.Log($"Wrong! The murderer was {actualMurderer}. You lose.");
                SceneManager.LoadScene(loseSceneName);
            }
        }
        else
        {
            Debug.LogError("Invalid suspect guess.");
        }
    }
}

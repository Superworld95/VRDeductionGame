using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Collections;

public class Camtrol : MonoBehaviour
{
    public Button start, exit;
    public Rigidbody playerToFollow;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.transform.position = playerToFollow.transform.position + new Vector3(0, 2, -10);
        //start = GetComponent<Button>();
        //exit = GetComponent<Button>();
        if (start != null)
        {
            start.onClick.AddListener(StartGame);
        }
        if (exit != null)
        {
            exit.onClick.AddListener(ExitGame);
            Debug.Log("Listening!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = playerToFollow.transform.position + new Vector3(0, 2, -10);
        cam.transform.position = new Vector3(0, 2, cam.transform.position.z);
        //If rounds mod 3, move camera overhead.
    }
    void StartGame()
    {
        SceneManager.LoadScene("The Scene");
    }
    void ExitGame()
    {
        Application.Quit();
    }

}

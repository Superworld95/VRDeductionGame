using Meta.XR.ImmersiveDebugger.UserInterface.Generic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using Button = UnityEngine.UI.Button;

[System.Serializable]
public class Weapon
{
    public string name;
    public GameObject weaponPrefab;
    public string forensicHint;
    public bool isMurderWeapon;
}

[System.Serializable]
public class Suspect
{
    public string name;
    public string alibi;
    public string motive;
    public bool isKiller;
}

public class GameData : MonoBehaviour
{
    public List<SuspectData> suspects;
    public List<Weapon> weapons;
    public List<InterrogationStation> stations;

    public SuspectData killer;
    public Weapon murderWeapon;

    public Button suspect1, suspect2, suspect3;
    void Start()
    {
        RandomizeMurder();
        // AssignStations(); // Optional: Only use if manual assignment isn't done in Inspector
        Debug.Log("Killer: " + killer.name);
        Debug.Log("Weapon: " + murderWeapon.name);

        if(suspect1 != null)
        {
            suspect1.onClick.AddListener(promptAPressed);
            suspect2.onClick.AddListener(promptBPressed);
            suspect3.onClick.AddListener(promptCPressed);
            Debug.Log("Hi?");
        }
    }

    void RandomizeMurder()
    {
        killer = suspects[Random.Range(0, suspects.Count)];
        killer.isKiller = true;

        murderWeapon = weapons[Random.Range(0, weapons.Count)];
        murderWeapon.isMurderWeapon = true;

        GenerateClues();
    }

    void GenerateClues()
    {
        string[] innocentAlibis =
        {
            "I was home watching Netflix.",
            "I was runnng in the park.",
            "I was shopping for some supplies.",
            "I was at the coffee shop with friends.",
            "I was on a video call with my mom.",
            "I was working late.",
            "I was at the gym.",
            "I was packing up my music equipment at the club.",
            "I was Locked in the bathroom.",
        };

        string[] innocentMotives =
        {
            "We barely spoke so there is no reason.",
            "No reason at all.",
            "We had a good relationship.",
            "Elena Was my best friend id never.",
            "Elena was my roomate id never.",
            "We had a disagreement once, but that's it.",
            "She seemed fine to me.",
            "I asked her out at the club so why would I hurt her?",
            "No."
        };

        foreach (SuspectData s in suspects)
        {
            if (s == killer)
            {
                s.alibi = "I was alone in my apartment. No one saw me.";
                s.motive = "They got the promotion I deserved.";
            }
            else
            {
                s.alibi = innocentAlibis[Random.Range(0, innocentAlibis.Length)];
                s.motive = innocentMotives[Random.Range(0, innocentMotives.Length)];
            }
        }
    }

    void AssignStations()
    {
        for (int i = 0; i < stations.Count; i++)
        {
            if (i < suspects.Count && stations[i].assignedSuspect == null)
            {
                stations[i].assignedSuspect = suspects[i];
            }
        }
    }
    public void promptAPressed()
    {
        if (killer.name == "Suspect_Brad")
        {
            SceneManager.LoadScene("Case Closed");
        } else
        {
            SceneManager.LoadScene("You Are Fired");
        }
    }
    public void promptBPressed()
    {
        if (killer.name == "Suspect_Sara")
        {
            SceneManager.LoadScene("Case Closed");
        }
        else
        {
            SceneManager.LoadScene("You Are Fired");
        }
    }
    public void promptCPressed()
    {
        if (killer.name == "Suspect_Tom")
        {
            SceneManager.LoadScene("Case Closed");
        }
        else
        {
            SceneManager.LoadScene("You Are Fired");
        }
    }
}

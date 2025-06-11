using UnityEngine;

[CreateAssetMenu(fileName = "New Suspect", menuName = "Game/Suspect")]
public class SuspectData : ScriptableObject
{
    public string suspectName;
    public string alibi;
    public string motive;
    public bool isKiller;
}

using UnityEngine;

public class TabletUIManager : MonoBehaviour
{
    public GameObject suspectTab;     // Panel containing suspect buttons/info
    public GameObject evidenceTab;    // Panel containing evidence buttons/info
    public GameObject suspect1Tab;
    public GameObject suspect2Tab;
    public GameObject suspect3Tab;
    public GameObject weaponsTab;
    public GameObject otherEvidenceTab;

    // Call this from your UI buttons
    public void ShowSuspectTab()
    {
        suspectTab.SetActive(true);
        evidenceTab.SetActive(false);
    }

    public void ShowSuspect1Tab()
    {
        suspect1Tab.SetActive(true);
        suspect2Tab.SetActive(false);
        suspect3Tab.SetActive(false);
    }

    public void ShowSuspect2Tab()
    {
        suspect1Tab.SetActive(false);
        suspect2Tab.SetActive(true);
        suspect3Tab.SetActive(false);
    }

    public void ShowSuspect3Tab()
    {
        suspect1Tab.SetActive(false);
        suspect2Tab.SetActive(false);
        suspect3Tab.SetActive(true);
    }

    public void ShowWeaponsTab()
    {
        weaponsTab.SetActive(true);
        otherEvidenceTab.SetActive(false);
    }

    public void ShowOtherEvidenceTab()
    {
        weaponsTab.SetActive(false);
        otherEvidenceTab.SetActive(true);
    }

    public void ShowEvidenceTab()
    {
        suspectTab.SetActive(false);
        evidenceTab.SetActive(true);
    }

    public void CloseAllTabs()
    {
        suspectTab.SetActive(false);
        evidenceTab.SetActive(false);
    }
}

using UnityEngine;
using System.Collections.Generic;
using System.Linq; // Required for LINQ methods like .ToList()

public class MurderWeaponRandomizer : MonoBehaviour
{
    public enum WeaponType { Knife, Wrench, Syringe }

    [System.Serializable]
    public class Suspect
    {
        public string suspectName;
        public GameObject characterObject; // Optional: the character GameObject
        [HideInInspector] public WeaponType assignedWeapon; // Hide in Inspector as it's assigned by script
    }

    public List<Suspect> suspects = new List<Suspect>();

    // Public properties to access the results
    public WeaponType MurderWeapon { get; private set; }
    public Suspect ActualMurderer { get; private set; }

    void Start()
    {
        InitializeMystery();
    }

    public void InitializeMystery()
    {
        if (suspects.Count == 0)
        {
            Debug.LogWarning("No suspects assigned! Cannot initialize the mystery.");
            return;
        }

        // --- Assign the Murder Weapon ---
        // Get all possible weapon types
        WeaponType[] allWeaponTypes = (WeaponType[])System.Enum.GetValues(typeof(WeaponType));

        // Randomly choose the murder weapon dynamically based on enum count
        MurderWeapon = allWeaponTypes[Random.Range(0, allWeaponTypes.Length)];

        // --- Assign the Murderer ---
        // Randomly choose the murderer from the list
        int murdererIndex = Random.Range(0, suspects.Count);
        ActualMurderer = suspects[murdererIndex];
        ActualMurderer.assignedWeapon = MurderWeapon; // Assign the murder weapon to the murderer

        // --- Assign non-murder weapons to other suspects ---
        // Create a list of available weapons, excluding the murder weapon
        List<WeaponType> availableNonMurderWeapons = allWeaponTypes.Where(w => w != MurderWeapon).ToList();

        for (int i = 0; i < suspects.Count; i++)
        {
            if (i == murdererIndex) continue; // Skip the murderer

            // Assign a random non-murder weapon from the available list
            // Ensure there are enough non-murder weapons to assign
            if (availableNonMurderWeapons.Count > 0)
            {
                int randomWeaponIndex = Random.Range(0, availableNonMurderWeapons.Count);
                suspects[i].assignedWeapon = availableNonMurderWeapons[randomWeaponIndex];
            }
            else
            {
                // Fallback in case there are not enough distinct weapons
                // (e.g., only one weapon type exists)
                Debug.LogWarning($"Not enough distinct non-murder weapons to assign to suspect {suspects[i].suspectName}.");
            }
        }

        Debug.Log($"<color=red>Murder Weapon:</color> {MurderWeapon}");
        Debug.Log($"<color=green>Actual Murderer:</color> {ActualMurderer.suspectName} with {ActualMurderer.assignedWeapon}");
    }
}
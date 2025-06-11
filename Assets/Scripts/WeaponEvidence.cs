using UnityEngine;

public class WeaponEvidence : MonoBehaviour
{
    public Weapon weapon;

    void OnGrab()
    {
        if (weapon.isMurderWeapon)
        {
            Debug.Log("This weapon has fresh blood stains...");
        }
        else
        {
            Debug.Log("This weapon looks clean.");
        }

        // Optionally show weapon.forensicHint via UI or audio
    }
}

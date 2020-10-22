using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{

    public bool hasUziWeapon;
    public void SwitchWeapon(int weaponIndex)
    {
        if (!hasUziWeapon) return;

        int childcount = transform.childCount -1 ;
        for (int i = 0; i <= childcount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(weaponIndex - 1).gameObject.SetActive(true);
    }




}

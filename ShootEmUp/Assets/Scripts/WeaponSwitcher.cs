using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{


    public void SwitchWeapon(int weaponIndex)
    {
        int childcount = transform.childCount -1 ;
        for (int i = 0; i <= childcount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(weaponIndex - 1).gameObject.SetActive(true);
    }




}

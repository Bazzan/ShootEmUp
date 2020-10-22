using UnityEngine;

public class ActivateUziGun : MonoBehaviour
{



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.GetComponentInChildren<WeaponSwitcher>().hasUziWeapon = true;
            Destroy(gameObject);
        }
    }

}

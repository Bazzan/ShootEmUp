using System.Collections;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    

    [SerializeField] private GameObject grenadePrefab;
    [SerializeField] private float throwLength;
    [SerializeField] private float throwHight;
    [SerializeField] private float throwCooldown;

    private bool isThrowable;
    private GameObject grenadeGO;
    private Rigidbody grenadeBody;

    private void Awake()
    {
        grenadeGO = Instantiate(grenadePrefab, transform.position, transform.rotation);
        grenadeBody = grenadeGO.GetComponent<Rigidbody>();
        Physics.IgnoreCollision(GetComponentInParent<Collider>(), grenadeGO.GetComponent<Collider>());
        grenadeGO.GetComponent<Grenade>().GrenadeThrower = this;
        grenadeGO.SetActive(false);
        isThrowable = true;
        
    }

    public void ThrowGrenade()
    {
        if (!isThrowable) return;
        isThrowable = false;
        grenadeGO.SetActive(true);
        grenadeGO.transform.position = transform.position + (transform.forward * 2);
        grenadeBody.AddForce((transform.forward * throwLength) + Vector3.up * throwHight ,ForceMode.Impulse);
        StartCoroutine(ThrowCooldown());
    }


    private IEnumerator ThrowCooldown()
    {
        yield return new WaitForSeconds(throwCooldown);
        isThrowable = true;
    }

}

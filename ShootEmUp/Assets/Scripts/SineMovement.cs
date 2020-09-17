using System.Collections;
using UnityEngine;

public class SineMovement : MonoBehaviour
{
    private Transform sphere;
    private void Awake()
    {
        sphere = transform.GetChild(0);
    }



    private void OnEnable()
    {
        StartCoroutine(ShootSphere());
    }
    IEnumerator ShootSphere()
    {
        yield return new WaitForSeconds(1f);

       Transform tempGO = Instantiate(sphere, transform.position, Quaternion.identity);
        Transform tempGO1 = Instantiate(sphere, transform.position, Quaternion.identity);


        tempGO.GetComponent<SphereMovement>().direction = Vector3.right;
        tempGO1.GetComponent<SphereMovement>().direction = -Vector3.right;
        StartCoroutine(ShootSphere());
    }


}

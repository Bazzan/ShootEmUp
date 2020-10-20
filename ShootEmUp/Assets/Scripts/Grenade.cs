using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Grenade : MonoBehaviour
{
    [HideInInspector] public GrenadeThrower GrenadeThrower;


    [SerializeField] private float timeToExplode;
    [SerializeField] private float damage;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionPushback;
    [SerializeField] private LayerMask layerMask;
    private Collider[] enemyColliders;
    private Rigidbody grenadeBody;

    private void Awake()
    {
        grenadeBody = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(Explode());
        grenadeBody.velocity *= 0.5f;
        //grenadeBody.AddForce(Vector3.up *2, ForceMode.Impulse);
    }

    private IEnumerator Explode()
    {


        yield return new WaitForSeconds(timeToExplode);

        enemyColliders = Physics.OverlapSphere(transform.position, explosionRadius, layerMask);

        for (int i = 0; i < enemyColliders.Length; i++)
        {
            enemyColliders[i].GetComponent<EnemyAttribute>().TakeDamage(damage);
            Vector3 pushbackDirection = enemyColliders[i].transform.position - transform.position;

            enemyColliders[i].GetComponent<NavMeshAgent>().velocity = (pushbackDirection.normalized * 4) + (Vector3.up * 2f);
        }
        grenadeBody.velocity = Vector3.zero;

        gameObject.SetActive(false);

    }

}

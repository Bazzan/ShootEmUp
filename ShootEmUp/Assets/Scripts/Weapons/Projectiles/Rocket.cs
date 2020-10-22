using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class Rocket : MonoBehaviour
{
    [HideInInspector] public RocketLauncher rocketLauncher;

    [SerializeField] private float Damage;
    [SerializeField] private float pushback;
    [SerializeField] private float explotionRaidus;
    [SerializeField] private float travelSpeed;

    [SerializeField] private ParticleSystem explotionVFX;
    [SerializeField] private LayerMask damageableLayerMask;
    private Collider collider;
    private Rigidbody rocketBody;
    private Vector3 shootDirection;
    private Collider[] damageableColliders;

    private void Awake()
    {

        rocketBody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        Physics.IgnoreCollision(
            GameManager.instance.PlayerTransform.GetComponent<Collider>(), collider);

    }
    private void FixedUpdate()
    {
        rocketBody.MovePosition(transform.position + (transform.forward * travelSpeed * Time.deltaTime));
    }

    public void SetPositionAndRotation(Vector3 position, Quaternion direction)
    {
        transform.position = position;
        transform.rotation = direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        ParticleSystem vfx = Instantiate(explotionVFX, transform.position, Quaternion.identity);
        Destroy(vfx.gameObject, 1.5f);
        vfx.Play();

        damageableColliders = Physics.OverlapSphere(transform.position, explotionRaidus, damageableLayerMask);

        rocketLauncher.DeActivateRocket(gameObject);
        if (damageableColliders.Length == 0) yield break;

        for (int i = 0; i < damageableColliders.Length; i++)
        {
            damageableColliders[i].GetComponent<IKillabel>().TakeDamage(Damage);
            Vector3 pushbackDirection = damageableColliders[i].transform.position - transform.position;
            damageableColliders[i].GetComponent<NavMeshAgent>().velocity = (pushbackDirection.normalized * pushback) + Vector3.up * 2f;
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explotionRaidus);
    }



}

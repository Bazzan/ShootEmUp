using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Rocket : MonoBehaviour
{
    [HideInInspector] private RocketLauncher rocketLauncher;

    [SerializeField] private float Damage;
    [SerializeField] private float pushback;
    [SerializeField] private float explotionRaidus;
    [SerializeField] private float travelSpeed;

    [SerializeField] private ParticleSystem explotionVFX = default;
    [SerializeField] private LayerMask damageableLayerMask = default;
    private Collider collider = default;
    private Rigidbody rocketBody = default;
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
        Explode();
    }
    private void Explode()
    {
        ParticleSystem vfx = Instantiate(explotionVFX, transform.position, Quaternion.identity);
        Destroy(vfx.gameObject, 1.5f);
        vfx.Play();

        damageableColliders = Physics.OverlapSphere(transform.position, explotionRaidus, damageableLayerMask);

        for (int i = 0; i < damageableColliders.Length; i++)
        {
            Collider collider = damageableColliders[i];
            collider.GetComponent<IDamage>().TakeDamage(Damage);
            Vector3 pushbackDirection = damageableColliders[i].transform.position - transform.position;
            pushbackDirection.y = 0f;
            collider.GetComponent<IStagger>().Stagger(StaggerType.Pushback, (pushbackDirection.normalized * pushback) + (Vector3.up * 3));
        }
        RocketLauncher.DeActivateRocket(gameObject);
        
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explotionRaidus);
    }
}

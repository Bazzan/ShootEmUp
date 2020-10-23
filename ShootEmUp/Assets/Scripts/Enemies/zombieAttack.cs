using System.Collections;
using UnityEngine;

public class zombieAttack : MonoBehaviour
{
    public float damage;
    public float pushBackForce;
    public float timeToHit;
    public float timeBetweenPathCalc;
    public LayerMask layerMask;
    

    private const float radius = 0.5f;
    private RaycastHit rayHit;
    private EnemyMovement enemyMovement;
    private Transform zombieTransform;

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        zombieTransform = transform;
    }


    private void OnEnable()
    {
        StartCoroutine(CheckForPlayer());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }


    IEnumerator CheckForPlayer()
    {
        if (Physics.SphereCast(zombieTransform.position, radius, transform.forward, out rayHit, 1f, layerMask, QueryTriggerInteraction.Ignore))
        {
            StartCoroutine(ZombieHit());
        }
        else
        {
            yield return new WaitForSeconds(timeBetweenPathCalc);
            StartCoroutine(CheckForPlayer());
        }

    }


    IEnumerator ZombieHit()
    {
        Debug.Log("enemy trying to hit");
        enemyMovement.agent.isStopped = true;
        enemyMovement.agent.velocity /= 2f;

        yield return new WaitForSeconds(timeToHit);
     
        if (Physics.SphereCast(zombieTransform.position, radius, transform.forward, out rayHit, 1f, layerMask, QueryTriggerInteraction.Ignore))
        {
            rayHit.collider.GetComponent<PlayerAttribute>().TakeDamage(damage);
            rayHit.collider.GetComponent<Rigidbody>().AddForce((transform.forward + Vector3.up) * pushBackForce, ForceMode.Impulse);
        }
        enemyMovement.agent.isStopped = false;
        
        StartCoroutine(CheckForPlayer());
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.forward, radius);
    }
}

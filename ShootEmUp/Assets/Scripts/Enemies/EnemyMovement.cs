using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IStagger
{

    public NavMeshAgent agent;
    private Transform agentTransform;
    private Transform playerTransform;
    private Collider enemyCollider;
    private Vector3 directionToPlayer;

    public enum EnemyStaggerType{
        Stagger, 
        Pushback
    }

    private void Awake()
    {
        EnemyStaggerType currentStaggerType;
        agent = GetComponent<NavMeshAgent>();
        agentTransform = agent.transform;
        playerTransform = GameManager.instance.PlayerTransform;
        enemyCollider = GetComponent<Collider>();


        foreach (Collider collider in GameManager.instance.BoarderColliders)
        {
            Physics.IgnoreCollision(enemyCollider, collider);
        }


    }



    private void Start()
    {
        OnSpawn();
    }

    private void LateUpdate()
    {
        if (agent.velocity.magnitude > 4f) return;
        if (!agent.isStopped) return;
        if (playerTransform == null) return;
        transform.LookAt(playerTransform.position, Vector3.up);

    }


    public void OnSpawn()
    {

        StartCoroutine(CalcPath());
    }





    private IEnumerator CalcPath()
    {

        agent.SetDestination(playerTransform.position);



        yield return new WaitForSeconds(0.3f);


        StartCoroutine(CalcPath());

    }

    public void Stagger(StaggerType staggerType, Vector3 force)
    {
        if (staggerType == StaggerType.Stagger)
            agent.velocity = Vector3.zero;
        else if (staggerType == StaggerType.Pushback)
            agent.velocity = force;

    }
}

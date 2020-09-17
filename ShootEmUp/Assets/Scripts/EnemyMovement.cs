using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public NavMeshAgent agent;
    private Transform agentTransform;
    private Transform playerTransform;
    private Collider enemyCollider;
    private Vector3 directionToPlayer;
    private void Awake()
    {
        
        agent = GetComponent<NavMeshAgent>();
        agentTransform = agent.transform;
        playerTransform = GameManager.instance.PlayerTransform;
        enemyCollider = GetComponent<Collider>();


        foreach  (Collider collider in GameManager.instance.BoarderColliders)
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

    public void StaggerEnemy()
    {
        agent.velocity = Vector3.zero;
    }

    

    private IEnumerator CalcPath()
    {
        if (!agent.isStopped)
        {
            agent.SetDestination(playerTransform.position);

            yield return new WaitForSeconds(0.3f);

        }

        StartCoroutine(CalcPath());

    }



}

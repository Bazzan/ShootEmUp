using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    private NavMeshAgent agent;
    private Transform agentTransform;
    private Transform playerTransform;
    private Collider enemyCollider;

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
        agent.SetDestination(playerTransform.position);

        yield return new WaitForSeconds(0.75f);

        StartCoroutine(CalcPath());

    }



}

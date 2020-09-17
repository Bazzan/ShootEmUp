using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class BigEnemyShoot : MonoBehaviour
{
    public float ShootCooldown;
    public float ShootDistance;
    public GameObject BulletPrefab;


    private float timeToShoot;
    
    private Transform playerTransform;
    private Transform enemyTransform;
    private NavMeshAgent agent;

    private GameObject rightBullet;
    private GameObject leftBullet;


    private void Awake()
    {
        playerTransform = GameManager.instance.PlayerTransform;
        enemyTransform = transform.parent.transform;
        agent = GetComponentInParent<NavMeshAgent>();
        timeToShoot = 0;
    
    }



    private void Update()
    {
        if (timeToShoot > Time.time) return;
        agent.isStopped = true;

        if (Vector3.Distance(playerTransform.position, enemyTransform.position) < ShootDistance)
        {
            EnemyShoot();

        }
        agent.isStopped = false;

        timeToShoot = Time.time + ShootCooldown;


    }


    private void EnemyShoot()
    {
        agent.velocity = Vector3.zero;
        enemyTransform.LookAt(playerTransform);

        rightBullet = Instantiate(BulletPrefab, enemyTransform.position, enemyTransform.rotation);
        leftBullet = Instantiate(BulletPrefab, enemyTransform.position, enemyTransform.rotation);

        rightBullet.GetComponent<SphereMovement>().direction = enemyTransform.right;
        leftBullet.GetComponent<SphereMovement>().direction = -enemyTransform.right;

    }

}

using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    
    public float NumberOfEnemies;
    public float TimeBetweenWaves;
    public float IncreaseNumberOfEnemiesBetweenWaves;

    public Transform[] spawnTransforms;
    public int Wave;

    public Transform ParentZombie;
    public Transform PartenBigZombie;


    private ObjectPool instance;
    private int randomSpawnPoint;
    private GameObject enemyGO;
    private int totalNumberOfEnemiesSpawned;
    
    private void Start()
    {
        Wave = 1;
        totalNumberOfEnemiesSpawned = 1;
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        yield return new WaitForSeconds(5f);
        instance = ObjectPool.Instance;
        StartCoroutine(SpawnWave());
    }


    IEnumerator SpawnWave()
    {
        for (int i = 0; i < NumberOfEnemies; i++)
        {
            if (totalNumberOfEnemiesSpawned % 10 == 0)
            {

                randomSpawnPoint = Random.Range(0, 3);
                enemyGO = instance.SpawnFromPool("BigEnemy", spawnTransforms[randomSpawnPoint].position, Quaternion.identity);
                enemyGO.GetComponent<EnemyMovement>().OnSpawn();
            }

            
            totalNumberOfEnemiesSpawned++;

            randomSpawnPoint = Random.Range(0, 3);
            enemyGO = instance.SpawnFromPool("Zombie", spawnTransforms[randomSpawnPoint].position, Quaternion.identity);
            enemyGO.GetComponent<EnemyMovement>().OnSpawn();
            yield return new WaitForSeconds(1f);
        }

        StartCoroutine(BetweenWaves());


    }

    private void UpdateWave()
    {
        Wave++;
        NumberOfEnemies = Mathf.RoundToInt( NumberOfEnemies * IncreaseNumberOfEnemiesBetweenWaves);
        GameManager.instance.CurrentZombieHealth *= GameManager.instance.healthScaling;
    }

    IEnumerator BetweenWaves()
    {
        UpdateWave();
        yield return new WaitForSeconds(TimeBetweenWaves);
        StartCoroutine(SpawnWave());
    }


}

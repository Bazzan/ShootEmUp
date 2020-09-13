using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    
    public float NumberOfEnemies;
    public float TimeBetweenWaves;
    public float IncreaseNumberOfEnemiesBetweenWaves;
    private ObjectPool instance;

    public Transform[] spawnTransforms;

    private int randomSpawnPoint;

    private void Start()
    {
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
            randomSpawnPoint = Random.Range(0, 3);
            instance.SpawnFromPool("Zombie", spawnTransforms[randomSpawnPoint].position, Quaternion.identity);

            yield return new WaitForSeconds(1f);
        }

        StartCoroutine(BetweenWaves());


    }

    private void UpdateWave()
    {
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

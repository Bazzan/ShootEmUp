using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform poolLocation;
    private Dictionary<string, Queue<GameObject>> PoolDictionary;
    [SerializeField] private List<Pool> Pools;

    [System.Serializable]
    public class Pool
    {
        public GameObject PoolPrefab;
        [SerializeField] public string PoolTag;
        public int PoolSize;
    }

    #region SingeltonInAwake
    public static ObjectPool Instance;
    //private WaveSpawner waveSpawner;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        else
        {
            Instance = this;
        }


    }
    #endregion


   

    
    private void Start()
    {
        poolLocation = transform;
        
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();


        foreach (Pool pool in Pools)
        {
            Queue<GameObject> queuePool = new Queue<GameObject>();

            for (int i = 0; i < pool.PoolSize; i++)
            {
                GameObject poolObject = Instantiate(pool.PoolPrefab);
                poolObject.SetActive(false);
                queuePool.Enqueue(poolObject);
                Debug.Log("poolTag -> " + pool.PoolTag + " added");

            }

            PoolDictionary.Add(pool.PoolTag, queuePool);
        }
    }

    public GameObject SpawnFromPool(string poolTag, Vector3 position, Quaternion rotation)
    {

        if (!PoolDictionary.ContainsKey(poolTag))
        {
            Debug.Log("ObjectPool dose not have the Pooltag in the dictionary ->" + poolTag);
            return null;

        }
        else if (PoolDictionary[poolTag].Count == 0) //if queue is empty
        {
            AddToEmptyQueue(poolTag);
        }

        GameObject objectToSpawn = PoolDictionary[poolTag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);


        return objectToSpawn;
    }

    public void EnQueueInPool(string poolTag, GameObject objectToPool)
    {

        if (!PoolDictionary.ContainsKey(poolTag))
        {
            Debug.Log("ObjectPool dose not have the Pooltag in the dictionary ->" + poolTag);
            return;
        }
        objectToPool.SetActive(false);
        objectToPool.transform.position = poolLocation.position;
        PoolDictionary[poolTag].Enqueue(objectToPool);


    }

    private void AddToEmptyQueue(string poolTag)
    {
        GameObject prefab = GetObjectToInstansiate(poolTag);
        for (int i = 0; i < 10; i++)
        {
            GameObject poolObject = Instantiate(prefab);
            poolObject.SetActive(false);
            PoolDictionary[poolTag].Enqueue(poolObject);
        }
        Debug.Log("10 new enemies instansiated of " + poolTag);
    }

    private GameObject GetObjectToInstansiate(string poolTag)
    {
        foreach (Pool pool in Pools)
        {
            if (pool.PoolTag.Equals(poolTag))
            {
                return pool.PoolPrefab;
            }
        }
        Debug.Log("Could not find the object with PoolTag: " + poolTag);
        return null;

    }


}


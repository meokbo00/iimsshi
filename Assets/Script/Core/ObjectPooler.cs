using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    [System.Serializable]
    public class Pool
    {
        public string tag;   // 유니크하지 않은 태그
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    private Dictionary<string, List<Queue<GameObject>>> poolDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Singleton 패턴을 따르기 위해, 중복된 인스턴스는 파괴
        }
    }

    private void Start()
    {
        poolDictionary = new Dictionary<string, List<Queue<GameObject>>>();

        foreach (Pool pool in pools)
        {
            if (!poolDictionary.ContainsKey(pool.tag))
            {
                poolDictionary[pool.tag] = new List<Queue<GameObject>>();
            }

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary[pool.tag].Add(objectPool);
            Debug.Log("Created pool for tag: " + pool.tag + " with size: " + pool.size);
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        if (poolDictionary.ContainsKey(tag))
        {
            List<Queue<GameObject>> pools = poolDictionary[tag];
            foreach (var pool in pools)
            {
                if (pool.Count > 0)
                {
                    GameObject objToSpawn = pool.Dequeue();
                    objToSpawn.SetActive(true);
                    pool.Enqueue(objToSpawn);
                    return objToSpawn;
                }
            }

            Debug.LogWarning("No available objects in any pool for tag " + tag + ". Consider expanding the pools.");
            return null;
        }
        else
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }
    }

    public void ReturnToPool(GameObject obj, string tag)
    {
        obj.SetActive(false);
        if (poolDictionary.ContainsKey(tag))
        {
            List<Queue<GameObject>> pools = poolDictionary[tag];
            foreach (var pool in pools)
            {
                if (pool.Count < pool.Peek().GetComponent<Pool>().size) // Check if the pool is not full
                {
                    pool.Enqueue(obj);
                    return;
                }
            }
            Debug.LogWarning("All pools for tag " + tag + " are full. Destroying object.");
            Destroy(obj);
        }
        else
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist. Destroying object.");
            Destroy(obj);
        }
    }
}

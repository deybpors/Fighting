using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public List<Pool> pools;
    [SerializeField] private int poolSize = 10;

    private Dictionary<string, Queue<GameObject>> poolsDictionary = new Dictionary<string, Queue<GameObject>>();

    public static ObjectPooler instance;
    [System.Serializable]
    public class Pool
    {
        [HideInInspector] public string id;
        public GameObject prefab;

        public Pool(GameObject prefab)
        {
            this.prefab = prefab;
            id = prefab.name;
        }
    }

    private void OnValidate()
    {
        foreach (var pool in pools)
        {
            pool.id = pool.prefab.name;
        }
    }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    void Start()
    {
        foreach(var pool in pools)
        {
            if (!poolsDictionary.ContainsKey(pool.id))
            {
                poolsDictionary.Add(pool.id, new Queue<GameObject>());
            }
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                poolsDictionary[pool.id].Enqueue(obj);
            }
        }
    }

    public Pool GetObject(Pool pool, Vector2 position, Quaternion rotation)
    {
        //if there is a object in the pools
        if (poolsDictionary.ContainsKey(pool.id))
        {
            if (poolsDictionary[pool.id].Count == 0)
            {
                // If the pool is empty, create a new object and add it to the pool
                GameObject obj = Instantiate(pool.prefab);
                poolsDictionary[pool.id].Enqueue(obj);
            }

            // Dequeue an object from the pool and return it
            GameObject pooledObj = poolsDictionary[pool.id].Dequeue();
            pooledObj.SetActive(true);
            pooledObj.transform.position = position;
            pooledObj.transform.rotation = rotation;
            return new Pool(pooledObj);
        }

        //if there is no object in the pools
        var id = pool.prefab.name;
        poolsDictionary.Add(id, new Queue<GameObject>());
        pool.id = id;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(pool.prefab);
            obj.SetActive(false);
            poolsDictionary[pool.id].Enqueue(obj);
        }

        return GetObject(pool, position, rotation);
    }

    public void ReturnObject(Pool pool)
    {
        // Reset the object's state and return it to the pool
        pool.prefab.SetActive(false);
        var id = pool.id.Split('(');
        poolsDictionary[id[0]].Enqueue(pool.prefab);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool
{
    private GameObject prefab;
    private List<GameObject> pool;
    public GameObjectPool(GameObject _prefab)
    {
        prefab = _prefab;
        pool = new List<GameObject>();
    }

    public GameObject GetObject()
    {
        if (pool.Count <= 0) return MonoBehaviour.Instantiate(prefab);

        GameObject objectFromPool = pool[0];
        pool.RemoveAt(0);
        objectFromPool.SetActive(true);
        return objectFromPool;
    }

    public void InsertObject(GameObject newValue)
    {
        newValue.SetActive(false);
        pool.Add(newValue);
        
    }
}

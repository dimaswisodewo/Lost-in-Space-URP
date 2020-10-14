using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    public List<GameObject> bulletPool = new List<GameObject>();
    public GameObject bulletToPool;
    public int amountBulletToPool;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        ObjectPooling(bulletToPool, bulletPool, amountBulletToPool);
    }

    public GameObject GetPooledBullet()
    {
        GameObject obj = GetPooledObject(bulletPool);
        return obj;
    }

    private GameObject GetPooledObject(List<GameObject> objectPool)
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                return objectPool[i];
            }
        }

        return null;
    }

    private void ObjectPooling(GameObject objectToPool, List<GameObject> objectPool, int amountToPool)
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(objectToPool);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
    }
}

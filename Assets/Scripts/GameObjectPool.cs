using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool {
    
    public GameObject ReferenceObject
    {
        get;
        private set;
    }

    public int PoolSize;

    private Queue<GameObject> objectQueue;

    public GameObjectPool(GameObject referenceObject, int poolSize)
    {
        ReferenceObject = referenceObject;
        PoolSize = poolSize;

        objectQueue = new Queue<GameObject>();
    }

    public GameObject CreateNewObject()
    {
        GameObject objectInstance = Object.Instantiate(ReferenceObject);

        objectQueue.Enqueue(objectInstance);

        if (objectQueue.Count > PoolSize)
        {
            Object.Destroy(objectQueue.Dequeue());
        }

        return objectInstance;
    }

    public void DestroyAll()
    {
        for (int i = 0; i < objectQueue.Count; i++)
        {
            Object.Destroy(objectQueue.Dequeue());
        }
    }

    public int CurrentPoolSize()
    {
        return objectQueue.Count;
    }
}
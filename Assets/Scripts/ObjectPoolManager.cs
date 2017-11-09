using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager {

    //object for locking
    private static object syncRoot = new System.Object();
    private static volatile ObjectPoolManager _current;

    public static ObjectPoolManager Current
    {
        get
        {
            if (_current == null)
            {
                lock(syncRoot)
                {
                    if(_current == null)
                    {
                        _current = new  ObjectPoolManager();
                    }
                }
            }
            return _current;
        }
    }

    private Dictionary<string, ObjectPooler> objectPools;

    private ObjectPoolManager()
    {
        this.objectPools = new Dictionary<string, ObjectPooler>();
    }

    /// <summary>
    /// Creates a new object pool of the objects you wish to pool.
    /// </summary>
    /// <param name="objectToPool"></param>
    /// <param name="startSize"></param>
    /// <param name="maxSize"></param>
    /// <param name="canGrow"></param>
    /// <returns></returns>
    public bool CreatePool(GameObject objToPool, int startSize, int maxSize, bool canGrow)
    {
        if(Current.objectPools.ContainsKey(objToPool.name))
        {
            return false;
        }
        else
        {
            //create a new pool
            ObjectPooler newPool = new ObjectPooler(objToPool, startSize, maxSize, canGrow);
            //add the pool to the dictionary
            Current.objectPools.Add(objToPool.name, newPool);
            return true;
        }
    }
    
    /// <summary>
    /// Get an object from the pool.
    /// </summary>
    /// <param name="objName"></param>
    /// <returns></returns>
    public GameObject GetObject(string objName)
    {
        //Find the right pool and return object.
        return Current.objectPools[objName].GetObject();
    }
}

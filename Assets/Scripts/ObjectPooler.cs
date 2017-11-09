using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler  {

    private List<GameObject> pooledObjects;
    private GameObject pooledObject;
    private int maxSize;
    private int startSize;
    private bool canGrow;

    private static ObjectPooler _current;
    private ObjectPooler() { }
    public static ObjectPooler Current
    {
        get
        {
            if (_current == null)
            {
                _current = new ObjectPooler();
            }
            return _current;
        }
    }

    public ObjectPooler(GameObject obj, int startSize, int maxSize, bool canGrow)
    {
        //instantiate new list for storing object pool
        pooledObjects = new List<GameObject>();
        
        //instatiate initial objects to fill pool
        for(int i = 0; i < startSize; i++)
        {
            GameObject newObj = GameObject.Instantiate(obj, Vector3.zero, Quaternion.identity) as GameObject;
            newObj.SetActive(false);
            pooledObjects.Add(newObj);
        }
        this.maxSize = maxSize;
        this.pooledObject = obj;
        this.startSize = startSize;
        this.canGrow = canGrow;
    }

    public GameObject GetObject()
    {
        //iterate through all pooled objects
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            //look for first inactive object
            if(pooledObjects[i].activeSelf == false)
            {
                //Set active
                //pooledObjects[i].SetActive(true);
                //return
                return pooledObjects[i];
            }
        }

        if(this.pooledObjects.Count < this.maxSize && this.canGrow)
        {
            GameObject newObj = GameObject.Instantiate(pooledObject, Vector3.zero, Quaternion.identity) as GameObject;
            newObj.SetActive(true);
            pooledObjects.Add(newObj);
            return newObj;
        }
        return null;
    }
}

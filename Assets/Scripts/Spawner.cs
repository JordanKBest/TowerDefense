using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public float startWait;
    public float spawnWait;
    public float waveWait;
    public int waveCount;
    GameObject enemy;
    public static Spawner Current;
    public bool isRunning = false;

    // Use this for initialization
    private void Awake()
    {
        if(Current == null)
        {
            Current = this;
        }
    }
    void Start () {
	}

    IEnumerator SpawnWaves()
    {
        isRunning = true;
        yield return new WaitForSeconds(startWait);
        while(true)
        {
            for(int i = 0; i < waveCount; i++)
            {
                enemy = ObjectPoolManager.Current.GetObject("Sheep");
                enemy.transform.position = this.transform.position;
                enemy.transform.rotation = this.transform.rotation;
                enemy.SetActive(true);
                yield return new WaitForSeconds(spawnWait);
            }
            break;
        }
        isRunning = false;
        StopSpawner();
    }

    public void StartSpawner()
    {
        if(!isRunning)
        {
            StartCoroutine(SpawnWaves());
        }
    }

    public void StopSpawner()
    {
        if(isRunning)
        {
            StopCoroutine(SpawnWaves());
        }
    }

    
}

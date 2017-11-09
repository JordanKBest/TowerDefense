using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {


    public GameObject cannonBall;
    public GameObject rocket;
    public GameObject sheep;
    public GameObject woolExplosion;

	void Start () {
        CreateObjectPools();
	}
	
    private void CreateObjectPools()
    {
        ObjectPoolManager.Current.CreatePool(this.cannonBall, 1, 5, true);
        ObjectPoolManager.Current.CreatePool(this.rocket, 1, 5, true);
        ObjectPoolManager.Current.CreatePool(this.sheep, 5, Spawner.Current.waveCount, true);
        ObjectPoolManager.Current.CreatePool(this.woolExplosion, 1, 5, true);
    }
}

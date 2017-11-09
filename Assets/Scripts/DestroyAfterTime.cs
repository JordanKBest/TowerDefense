using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

    public int waitTime;
	// Use this for initialization
	void OnEnable () {
        Invoke("Destroy", waitTime);
	}
	
	void Destroy () {
        gameObject.SetActive(false);
	}
}

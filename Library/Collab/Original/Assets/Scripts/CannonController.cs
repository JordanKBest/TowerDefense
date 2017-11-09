using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : TowerController {

    private GameObject cannonBall;
    Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = new Vector3(0, 0, -1);
        rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            //(cannonBall, transform.position - offset, transform.rotation);
            cannonBall = ObjectPoolManager.Current.GetObject("CannonBall");
            cannonBall.transform.position = transform.position - offset;
            cannonBall.transform.rotation = transform.rotation;
            cannonBall.SetActive(true);
        }
	}

    public override void Fire()
    {
        //Instantiate(cannonBall, transform.position - offset, transform.rotation);
        cannonBall = ObjectPoolManager.Current.GetObject("CannonBall");
        cannonBall.transform.position = transform.position - offset;
        cannonBall.transform.rotation = transform.rotation;
        cannonBall.SetActive(true);
    }
}

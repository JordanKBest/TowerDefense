using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : TowerController {

    private GameObject cannonBall;
    Vector3 offset;

	void Start ()
    {
        offset = new Vector3(0, 0, -1);
        rb = gameObject.GetComponent<Rigidbody2D>();
	}

    public override void Fire()
    {
        cannonBall = ObjectPoolManager.Current.GetObject("CannonBall");
        cannonBall.transform.position = transform.position - offset;
        cannonBall.transform.rotation = transform.rotation;
        cannonBall.SetActive(true);
    }
}

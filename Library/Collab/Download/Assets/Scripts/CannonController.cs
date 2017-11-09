using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : TowerController {

    public GameObject cannonBall;
    Vector3 offset;

	void Start ()
    {
        offset = new Vector3(0, 0, -1);
        rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(cannonBall, transform.position - offset, transform.rotation);
        }
	}

    public override void Fire()
    {
        Instantiate(cannonBall, transform.position - offset, transform.rotation);
    }
}

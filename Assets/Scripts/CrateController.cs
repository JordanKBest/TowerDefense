using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateController : ObstacleController {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
            EnemyController ec = collider.gameObject.GetComponent<EnemyController>();
            ec.currentSpeed *= 0.5f;
            ec.transform.localScale = new Vector3(0.21f, 0.21f);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
            EnemyController ec = collider.gameObject.GetComponent<EnemyController>();
            ec.currentSpeed = ec.moveSpeed;
            //ec.transform.localScale = new Vector3(0.18f, 0.18f);
            ec.transform.localScale = ec.baseScale;
        }
    }
}

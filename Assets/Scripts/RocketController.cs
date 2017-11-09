using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : TowerController {

    private GameObject rocket;
	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        LoadRocket();
    }

    public void LoadRocket()
    {
        rocket = ObjectPoolManager.Current.GetObject("Rocket");
        rocket.transform.parent = this.transform;
        rocket.transform.position = this.transform.position;
        rocket.transform.rotation = this.transform.rotation;
        rocket.SetActive(true);
    }
    public override void Fire()
    {
        if(rocket != null)
        {
            rocket.SendMessage("Fire");
            StartCoroutine(WaitForLoad());
        }
    }

    private IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(fireDelay/2);
        LoadRocket();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallFire : MonoBehaviour {

    public int destroyTime;
    Rigidbody2D rb;
    public int speed;

	void OnEnable ()
    {
        Invoke("Destroy", destroyTime);
        rb = GetComponent<Rigidbody2D>();
	}
    void Destroy()
    {
        this.gameObject.SetActive(false);
    }
    
    void Update ()
    {
        rb.transform.Translate(speed * Vector3.right * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SendMessage("TakeDamage", 1);
            Destroy();
        }
    }
}

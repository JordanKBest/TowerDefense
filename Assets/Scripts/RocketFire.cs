using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFire : MonoBehaviour
{

    public int destroyTime;
    Rigidbody2D rb;
    public int speed;
    bool isFiring = false;

    void OnEnable()
    {    
        rb = GetComponent<Rigidbody2D>();
    }

    void Fire()
    {
        isFiring = true;
        Invoke("Destroy", destroyTime);
    }

    void Destroy()
    {
        isFiring = false;
        this.gameObject.SetActive(false);      
    }

    void Update()
    {
        if(isFiring)
        {
            rb.transform.Translate(speed * Vector3.right * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SendMessage("TakeDamage", 2);
            Destroy();
        }
    }
}

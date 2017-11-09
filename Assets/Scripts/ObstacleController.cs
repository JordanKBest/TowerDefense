using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

    public int maxHealth = 10;
    private int currentHealth;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnEnable()
    {
        currentHealth = maxHealth;
        //randomize z rotation. does this even work or do i need to use euler angles?
        //do i even want this here or should it be on specific obstacles? could set a randomRotation bool also
        float random = Random.Range(0, 360);
        transform.rotation.Set(0, 0, random, 0);
    }

    //void OnTriggerStay2D(Collider2D collider)
    //{
    //    if(collider.tag == "Enemy")
    //    {
    //        Debug.Log("Sheep hit a box");
    //        TakeDamage();
    //        //collider.gameObject.SendMessage("Maneuver");
    //        ManeuverEnemy(collider.gameObject);
    //    }
    //}

    public virtual void ManeuverEnemy(GameObject enemy)
    {

    }

    void TakeDamage()
    {
        //can change to "damaged" sprite
        currentHealth -= 1;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}

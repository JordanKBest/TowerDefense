using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    Rigidbody2D rb;

    public GameObject Waypoints;
    public GameObject blood;
    [HideInInspector]
    public List<Vector3> points = new List<Vector3>();
    [HideInInspector]
    public int targetWaypointIndex = 0;

    public float moveSpeed = 4f;
    [HideInInspector]
    public float currentSpeed;
    public float turnSpeed = 8f;
    public int maxHealth = 2;
    [HideInInspector]
    public int currentHealth;
    public float bloodDuration = 3f;
    [HideInInspector]
    public float distanceRemaining;

    //[HideInInspector]
    public Vector3 baseScale;

    // Use this for initialization
    void Start ()
    {
        rb = gameObject.GetComponentInChildren<Rigidbody2D>();
        
        foreach (Transform point in Waypoints.transform)
        {
            points.Add(point.position);
        }
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        currentSpeed = moveSpeed;
        if (baseScale == null || baseScale == new Vector3(0, 0, 0))
        {
            baseScale = transform.localScale;
        }
        transform.localScale = baseScale;
        targetWaypointIndex = 0;
    }

    void Update ()
    {
        if (targetWaypointIndex < points.Count)
        {
            FollowPath();
        }
        else
        {
            gameObject.SetActive(false);
        }
	}

    void CalculateRemainingDistance()
    {
        if(targetWaypointIndex >= points.Count)
        {
            return;
        }

        float dist = 0.0f;
        dist += Vector2.Distance(transform.position, points[targetWaypointIndex]);

        for (int i = targetWaypointIndex; i < points.Count-1; i++)
        {
            dist += Vector2.Distance(points[i], points[i + 1]);
        }
        distanceRemaining = dist;
    }

    void FollowPath()
    {
        //turn to face next waypoint
        transform.right = Vector3.RotateTowards(transform.right, points[targetWaypointIndex] -
            transform.position, turnSpeed * Time.deltaTime, 0.0f);
        //move to the next waypoint
        transform.position = Vector3.MoveTowards(transform.position, points[targetWaypointIndex], currentSpeed * Time.deltaTime);
        //find next waypoint
        if(transform.position == points[targetWaypointIndex])
        {
            targetWaypointIndex++;
        }

        CalculateRemainingDistance();
    }

    public void TakeDamage(int damagePoints = 1)
    {
        if(currentHealth >= 1)
        {
            currentHealth -= damagePoints;
        }
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject splatter = ObjectPoolManager.Current.GetObject("WoolSplatter");
        splatter.transform.position = transform.position;
        splatter.transform.rotation = transform.rotation;
        splatter.SetActive(true);
        gameObject.SetActive(false);
    }
}

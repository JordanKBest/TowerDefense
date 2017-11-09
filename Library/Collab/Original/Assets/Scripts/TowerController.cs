using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TowerController : MonoBehaviour {

    public Rigidbody2D rb;
    public int speed;
    public float fireDelay = 1;

    private bool canFire = true;
    private GameObject currentTarget;

    public enum TargetType { First, Strongest, Weakest};
    public TargetType targetType = TargetType.First;

    private List<GameObject> enemiesInRange = new List<GameObject>();

	void Start () {
        rb = gameObject.GetComponentInChildren<Rigidbody2D>();
    }

    public void RotateToTarget(GameObject target)
    {
        Vector3 vectorToTarget = target.transform.position - rb.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, q, Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!enemiesInRange.Contains(collider.gameObject) && collider.GetComponent<EnemyController>() != null)
        {
            enemiesInRange.Add(collider.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (enemiesInRange.Contains(collider.gameObject))
        {
            enemiesInRange.Remove(collider.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Enemy")
        {
            SetTarget();
            //Rotate to enemy and start firing.
            RotateToTarget(currentTarget);
            TryFire();
        }
    }
    
    //find enemy that is furthest ahead, set as current target
    private void SetTarget()
    {
        if (enemiesInRange.Count == 0)
        {
            return;
        }
        #region Target First
        if (targetType == TargetType.First)
        {
            //the below can/should probably be simplified. There are way too many GetComponent calls
            //find the furthest waypoint being targeted by enemies
            int furthestWaypoint = enemiesInRange.Max(x => x.GetComponent<EnemyController>().targetWaypoint);
            //get each enemy going for that furthest waypoint
            var furthestEnemies = enemiesInRange.Where(x => x.GetComponent<EnemyController>().targetWaypoint == furthestWaypoint);

            //find the enemy that is closest to the next waypoint aka furthest ahead
            foreach (GameObject enemy in furthestEnemies)
            {
                if (Vector2.Distance(enemy.transform.position, enemy.GetComponent<EnemyController>().points[furthestWaypoint])
                    < Vector2.Distance(currentTarget.transform.position, currentTarget.GetComponent<EnemyController>().points[furthestWaypoint]))
                {
                    //target enemy that is furthest along path
                    currentTarget = enemy;
                }
            }
        }
        #endregion
        //TODO: make strong/weak focus first enemy with max/min health
        #region Target Strongest
        if (targetType == TargetType.Strongest)
        {
            //the below can/should probably be simplified. There are way too many GetComponent calls
            //find the furthest waypoint being targeted by enemies
            //int furthestWaypoint = enemiesInRange.Max(x => x.GetComponent<EnemyController>().targetWaypoint);
            //get each enemy going for that furthest waypoint
            //var furthestEnemies = enemiesInRange.Where(x => x.GetComponent<EnemyController>().targetWaypoint == furthestWaypoint);
            int highestHealth = enemiesInRange.Max(x => x.GetComponent<EnemyController>().health);

            //find the enemy that is closest to the next waypoint aka furthest ahead
            //foreach (GameObject enemy in furthestEnemies)
            //{
            //    if (Vector2.Distance(enemy.transform.position, enemy.GetComponent<EnemyController>().points[furthestWaypoint])
            //        < Vector2.Distance(currentTarget.transform.position, currentTarget.GetComponent<EnemyController>().points[furthestWaypoint]))
            //    {
            //        //target enemy that is furthest along path
            //        currentTarget = enemy;
            //    }
            //}

            //currently takes the first it finds with the highest health
            //should be improved to reliably shoot the leading enemy if multiple have the same health
            currentTarget = enemiesInRange.First(x => x.GetComponent<EnemyController>().health == highestHealth);
        }
        #endregion

        #region Target Weakest
        if (targetType == TargetType.Weakest)
        {
            //the below can/should probably be simplified. There are way too many GetComponent calls
            //find the furthest waypoint being targeted by enemies
            //int furthestWaypoint = enemiesInRange.Max(x => x.GetComponent<EnemyController>().targetWaypoint);
            //get each enemy going for that furthest waypoint
            //var furthestEnemies = enemiesInRange.Where(x => x.GetComponent<EnemyController>().targetWaypoint == furthestWaypoint);
            int lowestHealth = enemiesInRange.Min(x => x.GetComponent<EnemyController>().health);

            //find the enemy that is closest to the next waypoint aka furthest ahead
            //foreach (GameObject enemy in furthestEnemies)
            //{
            //    if (Vector2.Distance(enemy.transform.position, enemy.GetComponent<EnemyController>().points[furthestWaypoint])
            //        < Vector2.Distance(currentTarget.transform.position, currentTarget.GetComponent<EnemyController>().points[furthestWaypoint]))
            //    {
            //        //target enemy that is furthest along path
            //        currentTarget = enemy;
            //    }
            //}

            //currently takes the first it finds with the lowest health
            //should be improved to reliably shoot the leading enemy if multiple have the same health
            currentTarget = enemiesInRange.First(x => x.GetComponent<EnemyController>().health == lowestHealth);
        }
        #endregion
    }

    //this let's us keep the generic behavior while allowing Fire to be overridden in each tower
    private void TryFire()
    {
        if (canFire)
        {
            Fire();
            StartCoroutine(DelayFire());
        }
    }

    public virtual void Fire()
    {

    }

    private IEnumerator DelayFire()
    {
        canFire = false;
        yield return new WaitForSeconds(fireDelay);
        canFire = true;
    }

}

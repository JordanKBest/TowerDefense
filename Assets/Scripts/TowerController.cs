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

    public enum TargetType { First, Last, Strongest, Weakest};
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
            OrderEnemyList();
        }
    }

    private void OrderEnemyList()
    {
        var sortedList = enemiesInRange.OrderBy(x => x.GetComponent<EnemyController>().distanceRemaining);
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

        switch (targetType)
        {
            case TargetType.First:
                currentTarget = enemiesInRange.First();
                break;
            case TargetType.Last:
                currentTarget = enemiesInRange.Last();
                break;
            case TargetType.Strongest:
                int highestHealth = enemiesInRange.Max(x => x.GetComponent<EnemyController>().currentHealth);
                currentTarget = enemiesInRange.First(x => x.GetComponent<EnemyController>().currentHealth == highestHealth);
                break;
            case TargetType.Weakest:
            int lowestHealth = enemiesInRange.Min(x => x.GetComponent<EnemyController>().currentHealth);
            currentTarget = enemiesInRange.First(x => x.GetComponent<EnemyController>().currentHealth == lowestHealth);
            break;
        }
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

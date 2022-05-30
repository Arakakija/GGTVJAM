using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform patrolPoint;

    public GameObject[] limits;

    private int randomSpot;
    public Enemy enemyController;
    
    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        patrolPoint = GameObject.FindWithTag("Waypoint").transform;
        limits = GameObject.FindGameObjectsWithTag("Limit");
        patrolPoint.position = new Vector2(Random.Range(GetLimit("minX").position.x, GetLimit("maxX").position.x),Random.Range( GetLimit("minY").position.y, GetLimit("maxY").position.y));
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyController.isChasing) return;
        transform.position =
            Vector2.MoveTowards(transform.position, patrolPoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position,patrolPoint.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                patrolPoint.position = new Vector2(Random.Range(GetLimit("minX").position.x, GetLimit("maxX").position.x),
                    Random.Range(GetLimit("minY").position.y, GetLimit("maxY").position.y));
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    Transform GetLimit(string nameLimit)
    {
        foreach (var limit in limits)
        {
            if (limit.name.ToLower() == nameLimit.ToLower())
            {
                return limit.transform;
            }
        }
        return null;
    }
}

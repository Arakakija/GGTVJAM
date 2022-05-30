using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform patrolPoint;

    public Transform maxX;
    public Transform minX;   
    public Transform maxY;
    public Transform minY;
    
    private int randomSpot;

    
    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;

        patrolPoint.position = new Vector2(Random.Range(minX.position.x, maxX.position.x),Random.Range(minY.position.y, maxY.position.y));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =
            Vector2.MoveTowards(transform.position, patrolPoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position,patrolPoint.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                patrolPoint.position = new Vector2(Random.Range(minX.position.x, maxX.position.x),
                    Random.Range(minY.position.y, maxY.position.y));
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}

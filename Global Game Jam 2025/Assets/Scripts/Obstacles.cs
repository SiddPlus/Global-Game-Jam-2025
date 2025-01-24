using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public GameObject Spawn;

    private Rigidbody2D rb;

    public List<Transform> wayPoints;
    public float speed = 2f;
    public float nextWayPointReachDistance = 0.1f;

    public Transform nextWayPoint;
    public int wayPointIndex = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextWayPoint = wayPoints[wayPointIndex];
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 DirectionToNextWayPoint = (nextWayPoint.position - transform.position).normalized;

        float Distance = Vector2.Distance(nextWayPoint.position, transform.position);

        rb.velocity = DirectionToNextWayPoint * speed;

        if (Distance <= nextWayPointReachDistance)
        {
            wayPointIndex++;

            if (wayPointIndex >= wayPoints.Count)
            {
                wayPointIndex = 0;
            }

            nextWayPoint = wayPoints[wayPointIndex];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("End"))
        {
            gameObject.transform.position = Spawn.transform.position;

            wayPointIndex = 0;
            nextWayPoint = wayPoints[wayPointIndex];
        }
    }
}

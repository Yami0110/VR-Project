using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLoop : MonoBehaviour
{
    public List<Transform> points;
    public float speed = 2.0f;
    private int currentIndex = 0;

    void Update()
    {
        if (points.Count == 0) return;

        Transform targetPoint = points[currentIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentIndex = (currentIndex + 1) % points.Count;
        }
    }
}

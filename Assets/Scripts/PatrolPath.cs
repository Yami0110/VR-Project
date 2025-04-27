using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    public List<Transform> points;
    public float speed = 2.0f;
    private int currentIndex = 0;
    private int direction = 1;
    private Rigidbody rb;
    public float rotationSpeed = 100.0f;

    void Update()
    {
        if (points.Count == 0) return;

        Transform targetPoint = points[currentIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Vector3 direction2 = (targetPoint - transform.position).normalized;

        // float angle = Vector3.SignedAngle(transform.forward, direction2, Vector3.up);
        // Quaternion targetRotation = Quaternion.LookRotation(direction2);
        // rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentIndex += direction;

            if (currentIndex >= points.Count)
            {
                currentIndex = points.Count - 2;
                direction = -1;
            }
            else if (currentIndex < 0)
            {
                currentIndex = 1;
                direction = 1;
            }
        }
    }
}

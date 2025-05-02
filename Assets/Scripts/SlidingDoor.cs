using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public Transform openPosition;     // Cieľová pozícia (napr. prázdny objekt)
    public Transform closedPosition;   // Pôvodná pozícia (ďalší prázdny objekt)
    public float moveSpeed = 2f;

    private Vector3 targetPos;

    private void Start()
    {
        targetPos = closedPosition.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    public void OpenDoor()
    {
        targetPos = openPosition.position;
    }

    public void CloseDoor()
    {
        targetPos = closedPosition.position;
    }
}

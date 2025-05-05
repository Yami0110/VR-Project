using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPoint : MonoBehaviour
{
    public TaskManager objectiveManager;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            objectiveManager.CompleteObjective();
        }
    }
}

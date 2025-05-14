using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPoint : MonoBehaviour
{
    public TaskManager taskManager;   
    public int objectiveIndex;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            if (taskManager.IsCurrentObjective(objectiveIndex))
            {
                triggered = true;
                taskManager.CompleteObjective();
            }
        }
    }
}

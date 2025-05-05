using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSensor : MonoBehaviour
{
    public Animator doorAnimator;
    public float openDuration = 5f;
    public string keyTag = "KeyCard";

    private bool isOpen = false;

    private void OnTriggerEnter(Collider other)
    {Debug.Log("Karta priložená, otváram dvere!");
        if (!isOpen && other.CompareTag(keyTag))
        {
            Debug.Log("Karta priložená, otváram dvere!");
            isOpen = true;
            doorAnimator.SetBool("IsOpen", true);
            StartCoroutine(CloseDoorAfterDelay());
        }
    }

    private IEnumerator CloseDoorAfterDelay()
    {
        yield return new WaitForSeconds(openDuration);
        doorAnimator.SetBool("IsOpen", false);
        isOpen = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioSource>().Play();
    }
}

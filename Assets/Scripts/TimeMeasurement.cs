using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeMeasurement : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime;
    void Start()
    {
        startTime = Time.time;
    }
    void Update()
    {
        timerText.text = "Time: " + (Time.time - startTime);
    }
}

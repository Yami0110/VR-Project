using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class TaskManager : MonoBehaviour
{
    [TextArea]
    public List<string> objectiveTexts;   // Zoznam textov pre jednotlivé úlohy
    public TMP_Text objectiveUI;            // UI Text komponent pre zobrazenie

    private int currentObjectiveIndex = 0;

    private void Start()
    {
        ShowCurrentObjective();
    }

    public void CompleteObjective()
    {
        currentObjectiveIndex++;

        if (currentObjectiveIndex < objectiveTexts.Count)
        {
            ShowCurrentObjective();
        }
        else
        {
            objectiveUI.text = "Všetky úlohy splnené!";
        }
    }

    private void ShowCurrentObjective()
    {
        if (objectiveUI != null && currentObjectiveIndex < objectiveTexts.Count)
        {
            objectiveUI.text = objectiveTexts[currentObjectiveIndex];
        }
    }
}

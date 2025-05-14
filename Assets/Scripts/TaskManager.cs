using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class TaskManager : MonoBehaviour
{
    [TextArea]
    public List<string> objectiveTexts;   // Zoznam textov pre jednotlivé úlohy
    public GameObject gameClearPanel;
    public GameObject menuCanvas;
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
            objectiveUI.text = "";

            if (menuCanvas != null)
            {
                menuCanvas.SetActive(true); 
            }

            GameObject startPanel = GameObject.Find("StartPanel");
            GameObject resetPanel = GameObject.Find("ResetPanel");
            if (startPanel != null) startPanel.SetActive(false);
            if (resetPanel != null) resetPanel.SetActive(false);

            if (gameClearPanel != null)
            {
                gameClearPanel.SetActive(true); 
            }

            Time.timeScale = 0f;
        }
    }

    private void ShowCurrentObjective()
    {
        if (objectiveUI != null && currentObjectiveIndex < objectiveTexts.Count)
        {
            objectiveUI.text = objectiveTexts[currentObjectiveIndex];
        }
    }

    public bool IsCurrentObjective(int index)
    {
        return index == currentObjectiveIndex;
    }
}

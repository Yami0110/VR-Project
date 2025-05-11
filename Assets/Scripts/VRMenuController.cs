using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VRMenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject startPanel;
    public GameObject resetPanel;
    public GameObject gameOverPanel;

    public InputActionProperty toggleMenuAction;
    private bool isMenuVisible = true;

    void OnEnable()
    {
        toggleMenuAction.action.performed += ToggleMenu;
        toggleMenuAction.action.Enable();
    }

    void OnDisable()
    {
        toggleMenuAction.action.performed -= ToggleMenu;
        toggleMenuAction.action.Disable();
    }

    void Start()
    {
        ShowStartPanel();
        gameOverPanel.SetActive(false);
    }

    public void OnStartGameClicked()
    {
        Debug.Log("Start Game Clicked!");
        ShowResetPanel();
        HideMenu();
    }

    public void OnRestartGameClicked()
    {
        Debug.Log("Restart Game Clicked!");
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    void ToggleMenu(InputAction.CallbackContext context)
    {
        isMenuVisible = !isMenuVisible;
        menuCanvas.SetActive(isMenuVisible);
    }

    void ShowStartPanel()
    {
        startPanel.SetActive(true);
        resetPanel.SetActive(false);
    }

    void ShowResetPanel()
    {
        startPanel.SetActive(false);
        resetPanel.SetActive(true);
    }

    void HideMenu()
    {
        isMenuVisible = false;
        menuCanvas.SetActive(false);
    }

    public void ShowGameOver()
    {
        foreach (GameObject go in FindObjectsOfType<GameObject>())
        {
            if (go.CompareTag("Enemy"))
            {
                go.SetActive(false);
            }
        }

        menuCanvas.SetActive(true);
        startPanel.SetActive(false);
        resetPanel.SetActive(false);
        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }
}

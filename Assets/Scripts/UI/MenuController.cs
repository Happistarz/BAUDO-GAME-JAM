using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject background;
    public GameObject mainMenu;
    public SettingsController settingsMenu;

    public InputActionReference menuAction;

    private void OnEnable()
    {
        if (menuAction == null) return;
        menuAction.action.Enable();
        menuAction.action.performed += OpenMenu;
    }

    private void OnDisable()
    {
        if (menuAction == null) return;
        menuAction.action.performed -= OpenMenu;
        menuAction.action.Disable();
    }

    private void OpenMenu(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.OnUI) return;

        background.SetActive(true);
        mainMenu.SetActive(true);
        Time.timeScale = 0f; // Pause game time
        GameManager.Instance.OnUI = true;
        GameManager.Instance.ShowCursor();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("ActualGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        mainMenu.SetActive(false);
        settingsMenu.ToggleSettings(true);
    }

    public void BackToMainMenu()
    {
        settingsMenu.ToggleSettings(false);
        mainMenu.SetActive(true);
    }

    public void ExitGame()
    {
        GameManager.Instance.OnUI = false;
        Time.timeScale = 1f; // Resume game time
        SceneManager.LoadScene("Menu");
    }

    public void ResumeGame()
    {
        BackToMainMenu();
        background.SetActive(false);
        mainMenu.SetActive(false);
        Time.timeScale = 1f; // Resume game time
        GameManager.Instance.OnUI = false;
        GameManager.Instance.HideCursor();
    }
}
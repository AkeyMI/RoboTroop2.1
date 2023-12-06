using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Awake() => Application.targetFrameRate = 30;
    public void StartGame()
    {
        SceneManager.LoadScene("Lobby");
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void BackToGame()
    {
        FindObjectOfType<UiManager>().ClosePauseMenu();
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("Creditos");
    }
}

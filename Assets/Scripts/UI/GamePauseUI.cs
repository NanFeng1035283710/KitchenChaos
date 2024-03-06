using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GamePauseUI : MonoBehaviour
{

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button mainMenuButton;
    private void Awake()
    {
        resumeButton.onClick.AddListener(ResumeButton_OnClick);
        optionsButton.onClick.AddListener(OptionsButton_OnClick);
        mainMenuButton.onClick.AddListener(MainMenuButton_OnClick);
    }

    private void OptionsButton_OnClick()
    {
        QptionsUI.Instance.Show();
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;
        HIde();
    }

    private void ResumeButton_OnClick()
    {
        GameManager.Instance.TogglePauesGame();
    }

    private void MainMenuButton_OnClick()
    {
        SceneManager.LoadScene(Loader.Scene.MainMenuScene.ToString());
    }

    private void GameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        HIde();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void HIde()
    {
        gameObject.SetActive(false);
    }
}

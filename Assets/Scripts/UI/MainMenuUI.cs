using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 主菜单UI
/// </summary>
public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    private void Awake()
    {
        playButton.onClick.AddListener(PlayButtonOnClick);
        quitButton.onClick.AddListener(QuitButtonOnClick);
    }
    private void Start()
    {
        Time.timeScale = 1.0f;
    }
    private void PlayButtonOnClick()
    {
        Loader.Load(Loader.Scene.GameScene);
    }
    private void QuitButtonOnClick()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
  Application.Quit();
#endif

    }
}

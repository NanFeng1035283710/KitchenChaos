using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 倒计时UI
/// </summary>
public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TMP_Text countdown_Text;

    private void Start()
    {
        GameManager.Instance.OnStatechanged += GameManager_OnStatechanged;
        Hide();
    }

    private void Update()
    {
        countdown_Text.text = Mathf.Ceil(GameManager.Instance.GetCountdownToStartTimer()).ToString();
    }
    private void GameManager_OnStatechanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}

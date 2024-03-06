using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏计时UI
/// </summary>
public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;
    private void Update()
    {
        timerImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public event EventHandler OnStatechanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;
    private enum State
    {
        waitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }
    private State state;
    private float waitingToStartTimer = 0.2f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private bool isGamePaused;
    [SerializeField][Tooltip("游戏最大时长")] private float gamePlayingTimerMax = 10f;

    new private void Awake()
    {
        base.Awake();
        state = State.waitingToStart;
    }
    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (state == State.waitingToStart)
        {
            state = State.CountdownToStart;
            OnStatechanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauesGame();
    }
    private void Update()
    {
        switch (state)
        {
            case State.waitingToStart:
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStatechanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStatechanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
            default:
                break;
        }
    }


    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    /// <summary>
    /// 是否倒计时
    /// </summary>
    /// <returns></returns>
    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }
    /// <summary>
    /// 是否游戏结束
    /// </summary>
    /// <returns></returns>
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }
    public float GetGamePlayingTimerNormalized()
    {
        return 1 - gamePlayingTimer / gamePlayingTimerMax;
    }

    public void TogglePauesGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);
        }
    }
}

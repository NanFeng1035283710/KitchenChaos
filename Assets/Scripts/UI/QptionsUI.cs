using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 设置UI
/// </summary>
public class QptionsUI : Singleton <QptionsUI>
{
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private TMP_Text mopeUpText;
    [SerializeField] private TMP_Text mopeDownText;
    [SerializeField] private TMP_Text mopeLeftText;
    [SerializeField] private TMP_Text mopeRightText;


    private void Awake()
    {
        base.Awake();
        soundEffectsButton.onClick.AddListener(SoundEffectsButton_OnClick);
        closeButton.onClick.AddListener(CloseButton_OnClick);
        moveUpButton.onClick.AddListener(MoveUpButton_OnClick);
        moveDownButton.onClick.AddListener(MoveDownButton_OnClick);
        moveLeftButton.onClick.AddListener(MoveLeftButton_OnClick);
        moveRightButton.onClick.AddListener(MoveRightButton_OnClick);
       
        Hide();
    }
    private void MoveRightButton_OnClick()
    {
        GameInput.Instance.RebinBinding(GameInput.Binding.Move_Right, UpdateVisual);
    }

    private void MoveLeftButton_OnClick()
    {
        GameInput.Instance.RebinBinding(GameInput.Binding.Move_Left, UpdateVisual);
    }

    private void MoveDownButton_OnClick()
    {
        GameInput.Instance.RebinBinding(GameInput.Binding.Move_Down, UpdateVisual);
    }

    private void MoveUpButton_OnClick()
    {
        GameInput.Instance.RebinBinding(GameInput.Binding.Move_Up, UpdateVisual);
    }

   new  private void OnDestroy()
    {
        soundEffectsButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
        moveUpButton.onClick.RemoveAllListeners();
        moveDownButton.onClick.RemoveAllListeners();
        moveLeftButton.onClick.RemoveAllListeners();
        moveRightButton.onClick.RemoveAllListeners();
    }
    private void Start()
    {
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;
        UpdateVisual();
    }
    private void UpdateVisual()
    {
        mopeUpText.text = GameInput.Instance.GetBinbingText(GameInput.Binding.Move_Up);
        mopeDownText.text = GameInput.Instance.GetBinbingText(GameInput.Binding.Move_Down);
        mopeLeftText.text = GameInput.Instance.GetBinbingText(GameInput.Binding.Move_Left);
        mopeRightText.text = GameInput.Instance.GetBinbingText(GameInput.Binding.Move_Right);
    }
    private void GameManager_OnGameUnPaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void CloseButton_OnClick()
    {
        Hide();
    }

    private void SoundEffectsButton_OnClick()
    {
        SoundManager.Instance.ChangeVolume();
    }

    public void Show()
    {

        gameObject.SetActive(true);
    }
    public void Hide() {
        gameObject.SetActive(false);
    }
}

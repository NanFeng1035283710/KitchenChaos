using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
///  教程UI
/// </summary>
public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TMP_Text moveUpText;
    [SerializeField] private TMP_Text moveDownText;
    [SerializeField] private TMP_Text moveLeftText;
    [SerializeField] private TMP_Text moveRihtText;
    [SerializeField] private TMP_Text moveInteractText;
    [SerializeField] private TMP_Text moveActText;
    [SerializeField] private TMP_Text movePauseText;

    private void Start()
    {
        GameManager.Instance.OnStatechanged += GameManager_OnStatechanged;
        UpdataVisual();
        Show();
    }

    private void GameManager_OnStatechanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void UpdataVisual()
    {
        moveUpText.text = GameInput.Instance.GetBinbingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBinbingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBinbingText(GameInput.Binding.Move_Left);
        moveRihtText.text = GameInput.Instance.GetBinbingText(GameInput.Binding.Move_Right);
        moveInteractText.text = "E";
        moveActText.text = "F";
        movePauseText.text = "ESC";
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide() { gameObject.SetActive(false); }
}


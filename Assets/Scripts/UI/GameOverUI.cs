using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMP_Text recipeDelivered_Text;
    private void Start()
    {
        GameManager.Instance.OnStatechanged += GameManager_OnStatechanged;
        Hide();
    }
    private void GameManager_OnStatechanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();
            recipeDelivered_Text.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
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

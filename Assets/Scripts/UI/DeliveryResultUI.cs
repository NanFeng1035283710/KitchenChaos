using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 出餐结果UI
/// </summary>
public class DeliveryResultUI : MonoBehaviour
{
    private const string SWAY = "Sway";

    [SerializeField] DeliveryCounter deliveryCounter;
    [SerializeField] private GameObject success;
    [SerializeField] private GameObject failed;

    private Animator animator;
    private void Awake()
    {
            animator    = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        success.SetActive(false);
        failed.SetActive(false);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        success.SetActive(false);
        failed.SetActive(true);
        animator.SetTrigger(SWAY );
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        failed.SetActive(false);
        success.SetActive(true);
        animator.SetTrigger(SWAY);
    }
}

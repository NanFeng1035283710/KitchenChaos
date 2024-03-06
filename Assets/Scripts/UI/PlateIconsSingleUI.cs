using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 单个餐盘图标UI
/// </summary>
public class PlateIconsSingleUI : MonoBehaviour
{
    [SerializeField] private Image image;
    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    { 
        image.sprite =kitchenObjectSO.sprite;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 选中柜台视觉效果
/// 说明:
/// </summary>
public class SelectedCounterVisual : MonoBehaviour
{
    public BaseCounter baseCounter;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEnventArgs e)
    {
        gameObject.SetActive(e.selectedCounter == baseCounter);
    }

}

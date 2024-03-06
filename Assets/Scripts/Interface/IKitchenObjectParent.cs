using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 厨房物品持有者
/// </summary>
public interface IKitchenObjectParent
{
    /// <summary>
    /// 获取厨房物品的父级
    /// </summary>
    /// <returns></returns>
    public Transform GetKitchenObjectFollowTransform();
    /// <summary>
    /// 设置厨房物品
    /// </summary>
    /// <param name="kitchenObject"></param>
    public void SetKitchenObject(KitchenObject kitchenObject);

    /// <summary>
    /// 获取厨房物品
    /// </summary>
    /// <returns></returns>
    public KitchenObject GetKitchenObject();
    /// <summary>
    /// 清空父级拥有的食品
    /// </summary>
    public void ClearKiechenObject();
    /// <summary>
    /// 是否拥有厨房物品
    /// </summary>
    /// <returns></returns>
    public bool HasKitchenObject();
}

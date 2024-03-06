using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 盘子
/// </summary>
public class PlateKitChenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    /// <summary>
    /// 可装入盘子的SO
    /// </summary>
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSO_List;

    private List<KitchenObjectSO> kitchenObjectSO_List = new List<KitchenObjectSO>();

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validKitchenObjectSO_List.Contains(kitchenObjectSO))
        {
            return false;
        }
        if (kitchenObjectSO_List.Contains(kitchenObjectSO))
        {
            return false;
        }
        else
        {
            kitchenObjectSO_List.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs { kitchenObjectSO = kitchenObjectSO }); ;
            return true;
        }
    }
    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSO_List;
    }
}

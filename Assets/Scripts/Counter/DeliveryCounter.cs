using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 出餐柜台
/// </summary>
public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitChenObject plateKitChenObject))
            {
                DeliveryManager.Instance.DeliveryRecipe(plateKitChenObject);
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}

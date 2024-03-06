using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 空桌台
/// </summary>
public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //桌台上没东西 
            if (player.HasKitchenObject())
            {
                //玩家手上有东西  把玩家手上的东西父级设置成桌台
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //玩家手上没东西 
            }
        }
        else  //桌上有东西 
        {

            if (!player.HasKitchenObject())
            {
                //手上没东西  把东西拿到手上
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            else
            {
                // 手上有盘子 
                if (player.GetKitchenObject().TryGetPlate(out PlateKitChenObject plateKitChenObject))
                {
                    //把桌上的东西放到手中的盘子上
                    if (plateKitChenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        //删除桌上的东西
                        GetKitchenObject().DestroySelf();
                    }

                }
                else //手上有盘子意外的物品
                {
                    //桌上是盘子   
                    if (GetKitchenObject().TryGetPlate(out plateKitChenObject))
                    {
                        //放入盘中
                        if (plateKitChenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }

        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 容器柜台
/// </summary>
public class ContainerCounter : BaseCounter
{
    /// <summary>
    /// 玩家抓取物品之后调用的事件
    /// </summary>
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public override void Interact(Player player)
    {
        //玩家手上有
        if (player.HasKitchenObject())
        {
            //柜台有
            if (HasKitchenObject())
            {
                //柜台上是盘子  
                if (GetKitchenObject().TryGetPlate(out PlateKitChenObject plateKitchenObject))
                {
                    //把手上东西放到柜台盘子上
                    if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        //删除手上的东西
                        player.GetKitchenObject().DestroySelf();
                    }
                }
                else
                { //手上的是盘子
                    if (player.GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //把柜台东西放手上盘子里
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            //删除柜台的东西
                            GetKitchenObject().DestroySelf();
                        }
                    }
                }

            }
            else
            {
                //  柜台没有 放到柜台上
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            if (HasKitchenObject())
            {
                //如果桌上有 拿桌上的
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            else
            {
                // 桌台没有   生成一个拿到玩家手上
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            }

        }



    }
}

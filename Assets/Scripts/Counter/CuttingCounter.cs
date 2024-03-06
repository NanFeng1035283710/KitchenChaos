using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 切菜桌台
/// 备注: 食材的切割次数应该保存在食材本身  这样切到一半拿起来的话不用重新再切
/// </summary>
public class CuttingCounter : BaseCounter, IHasProgress
{
    public static event EventHandler OnAnyCut;
   new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    /// <summary>
    /// 切菜进度
    /// </summary>
    private int cuttingProgress;
    public override void Interact(Player player)
    {

        if (!HasKitchenObject())
        {
            //桌台上没东西 
            if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                //玩家手上有东西  把物品放到桌台
                player.GetKitchenObject().SetKitchenObjectParent(this);

                cuttingProgress = 0;
                CuttingRecipeSO outputCuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = (float)cuttingProgress / outputCuttingRecipeSO.cuttingProgressMAx
                });
            }
            else
            {
                //玩家手上没东西 
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                //桌上有东西 手上没东西  把东西拿到手上
                GetKitchenObject().SetKitchenObjectParent(player);
                cuttingProgress = 0;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = cuttingProgress
                });
            }
            else
            {
                //桌上有东西  手上有盘子 把桌上的东西放到手中的盘子上
                if (player.GetKitchenObject().TryGetPlate(out PlateKitChenObject plateKitchenObject))
                {
                    //把桌上的东西放到手中的盘子上
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        //删除桌上的东西
                        GetKitchenObject().DestroySelf();
                    }

                }
            }
        }
    }
    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            OnCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSO outputCuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / outputCuttingRecipeSO.cuttingProgressMAx
            });

            if (cuttingProgress >= outputCuttingRecipeSO.cuttingProgressMAx)
            {
                //桌台有东西  删除原物品
                GetKitchenObject().DestroySelf();
                //生成切片
                KitchenObject.SpawnKitchenObject(outputCuttingRecipeSO.output, this);
            }

        }
    }

    /// <summary>
    /// 是否属于食谱
    /// </summary>
    /// <param name="inputKitchenObjectSO"></param>
    /// <returns></returns>
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    /// <summary>
    /// 获取输出的食材
    /// </summary>
    /// <param name="inputKitchenObjectSO">输入的食材</param>
    /// <returns></returns>
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null ? cuttingRecipeSO.output : null;
    }

    /// <summary>
    /// 获取食谱
    /// </summary>
    /// <param name="inputKitchenObjectSO">输入的食材</param>
    /// <returns></returns>
    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var item in cuttingRecipeSOArray)
        {
            if (inputKitchenObjectSO == item.input)
            {
                return item;
            }
        }
        return null;
    }
}

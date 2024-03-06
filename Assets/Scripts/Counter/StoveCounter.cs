using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using static CuttingCounter;

/// <summary>
/// 炉子
/// </summary>
public class StoveCounter : BaseCounter, IHasProgress
{

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    /// <summary>
    /// 进度条变化时
    /// </summary>
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        /// <summary>
        /// 烹饪中
        /// </summary>
        Frying,
        /// <summary>
        /// 烹饪完成
        /// </summary>
        Fried,
        /// <summary>
        /// 糊了
        /// </summary>
        Burned,
    }

    [SerializeField] FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] BurningRecipeSO[] burningRecipeSOArray;
    /// <summary>
    /// 烹饪进度
    /// </summary>
    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;
    private State state;
    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Frying:
                fryingTimer += Time.deltaTime;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = (float)fryingTimer / fryingRecipeSO.fryingTimerMax
                });
                if (fryingTimer >= fryingRecipeSO.fryingTimerMax)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                    burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Fried;
                    burningTimer = 0;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });

                }
                break;
            case State.Fried:
                burningTimer += Time.deltaTime;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = (float)burningTimer / burningRecipeSO.BurningTimerMax
                });
                if (burningTimer >= burningRecipeSO.BurningTimerMax)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                    state = State.Burned;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f
                    });
                }
                break;
            case State.Burned:
                break;
            default:
                break;
        }
    }
    public override void Interact(Player player)
    {

        if (!HasKitchenObject())
        {
            //桌台上没东西  玩家有东西且符合桌台食谱
            if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                // 把物品放到桌台
                player.GetKitchenObject().SetKitchenObjectParent(this);
                fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                state = State.Frying;
                fryingTimer = 0;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = (float)fryingTimer / fryingRecipeSO.fryingTimerMax
                });
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                //桌上有东西 手上没东西  把东西拿到手上
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
            else
            {
                //桌上有东西  手上有盘子 把桌上的东西放到手中的盘子上
                if (player.GetKitchenObject().TryGetPlate(out PlateKitChenObject plateKitChenObject))
                {
                    //把桌上的东西放到手中的盘子上
                    if (plateKitChenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        //删除桌上的东西
                        GetKitchenObject().DestroySelf();
                    }
                    state = State.Idle;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f
                    });
                }
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
        FryingRecipeSO cuttingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    /// <summary>
    /// 获取输出的食材
    /// </summary>
    /// <param name="inputKitchenObjectSO">输入的食材</param>
    /// <returns></returns>
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO cuttingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null ? cuttingRecipeSO.output : null;
    }

    /// <summary>
    /// 获取食谱
    /// </summary>
    /// <param name="inputKitchenObjectSO">输入的食材</param>
    /// <returns></returns>
    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var item in fryingRecipeSOArray)
        {
            if (inputKitchenObjectSO == item.input)
            {
                return item;
            }
        }
        return null;
    }
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var item in burningRecipeSOArray)
        {
            if (inputKitchenObjectSO == item.input)
            {
                return item;
            }
        }
        return null;
    }

    public void AddContextAttribute(string name, string value, HelpKeywordType keywordType)
    {
        throw new NotImplementedException();
    }

    public void ClearContextAttributes()
    {
        throw new NotImplementedException();
    }

    public IHelpService CreateLocalContext(HelpContextType contextType)
    {
        throw new NotImplementedException();
    }

    public void RemoveContextAttribute(string name, string value)
    {
        throw new NotImplementedException();
    }

    public void RemoveLocalContext(IHelpService localContext)
    {
        throw new NotImplementedException();
    }

    public void ShowHelpFromKeyword(string helpKeyword)
    {
        throw new NotImplementedException();
    }

    public void ShowHelpFromUrl(string helpUrl)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 是否在烹饪
    /// </summary>
    public bool IsFried()
    {
        return state == State.Fried;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///出餐管理 
/// </summary>
public class DeliveryManager : Singleton<DeliveryManager>
{
    /// <summary>
    /// 菜谱生成
    /// </summary>
    public event EventHandler OnRecipeSpawned;
    /// <summary>
    /// 菜谱完成时
    /// </summary>
    public event EventHandler OnRecipeCompleted;
    /// <summary>
    /// 送餐成功
    /// </summary>
    public event EventHandler OnRecipeSuccess;
    /// <summary>
    /// 送餐失败
    /// </summary>
    public event EventHandler OnRecipeFailed;

    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSO_List = new List<RecipeSO>();
    /// <summary>
    /// 菜单生成计时
    /// </summary>
    private float spawnRecipeTimer;
    /// <summary>
    /// 菜单生成间隔
    /// </summary>
    private float spawnRecipeTimerMax = 4f;
    /// <summary>
    /// 菜单最大数量
    /// </summary>
    private float waitingRecipesMax = 4f;
    /// <summary>
    /// 成功送餐数量
    /// </summary>
    private int successfulRecipesAmount;
    private void Update()
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSO_List.Count < waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSO_List[UnityEngine.Random.Range(0, recipeListSO.recipeSO_List.Count)];
                waitingRecipeSO_List.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliveryRecipe(PlateKitChenObject plateKitChenObject)
    {
        for (int i = 0; i < waitingRecipeSO_List.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSO_List[i];
            //如果出餐菜谱的食材数量等于盘子中的食材数量
            if (waitingRecipeSO.kitchenObjectSO_List.Count == plateKitChenObject.GetKitchenObjectSOList().Count)
            {
                //盘子的食材是否匹配
                bool plateContentsMatchesRecipe = true;
                //遍历这个出餐菜谱  取出菜谱中的一个食材
                foreach (KitchenObjectSO recipeKitchenObjectSo in waitingRecipeSO.kitchenObjectSO_List)
                {
                    bool ingredientFound = false;
                    //遍历盘子中是否包含这个食材
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitChenObject.GetKitchenObjectSOList())
                    {
                        if (recipeKitchenObjectSo == plateKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        plateContentsMatchesRecipe = false;
                    }
                }
                if (plateContentsMatchesRecipe)
                {
                    successfulRecipesAmount++;
                    waitingRecipeSO_List.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }

        }
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        Debug.Log("失败");
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSO_List;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return successfulRecipesAmount;
    }
}

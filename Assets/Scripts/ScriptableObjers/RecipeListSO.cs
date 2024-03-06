using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
/// <summary>
/// 菜谱列表
/// </summary>
public class RecipeListSO : ScriptableObject
{
    public List<RecipeSO> recipeSO_List = new List<RecipeSO>();
}

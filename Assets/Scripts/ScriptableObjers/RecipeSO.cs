using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
/// <summary>
/// 菜谱
/// </summary>
public class RecipeSO : ScriptableObject
{
    public List<KitchenObjectSO> kitchenObjectSO_List;
    public string   recipeName;
}

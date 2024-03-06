using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
/// <summary>
/// 切割配方
/// </summary>
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    /// <summary>
    /// 可切次数
    /// </summary>
    public int cuttingProgressMAx;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
/// <summary>
/// 烹饪食材
/// </summary>
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float fryingTimerMax;
}


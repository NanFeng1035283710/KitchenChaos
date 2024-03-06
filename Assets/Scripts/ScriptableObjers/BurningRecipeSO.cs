using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
/// <summary>
/// 煮糊菜谱
/// </summary>
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float BurningTimerMax;
}

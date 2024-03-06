using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 盘子视觉
/// </summary>
public class PlateCompleteVisual : MonoBehaviour
{
    
   [Serializable] public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] private List<KitchenObjectSO_GameObject> KitchenObjectSOGameObject_List = new List<KitchenObjectSO_GameObject>();
    [SerializeField] private PlateKitChenObject plateKitChenObject;

    private void Start()
    {
        plateKitChenObject.OnIngredientAdded += PlateKitChenObject_OnIngredientAdded;
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in KitchenObjectSOGameObject_List)
        {
                kitchenObjectSO_GameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitChenObject_OnIngredientAdded(object sender, PlateKitChenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject  in KitchenObjectSOGameObject_List)
        {
            if (kitchenObjectSO_GameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectSO_GameObject.gameObject.SetActive(true);
            }
        }
        //e.kitchenObjectSO
    }
}

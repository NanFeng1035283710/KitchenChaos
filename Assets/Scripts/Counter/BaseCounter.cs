using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPpacedHera;
    public static void  ResetStaticData()
    {
        OnAnyObjectPpacedHera = null;
    }
    [SerializeField] protected Transform spawnPoint;
    protected KitchenObject kitchenObject;
    public virtual void Interact(Player player)
    {
        Debug.LogError("Interact");
    }
    public virtual void InteractAlternate(Player player)
    {
        Debug.LogError("InteractAlternate");
    }
    public Transform GetKitchenObjectFollowTransform() { return spawnPoint; }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnAnyObjectPpacedHera?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject() { return kitchenObject; }
    public void ClearKiechenObject() { kitchenObject = null; }
    public bool HasKitchenObject() { return kitchenObject != null; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 厨房物品
/// </summary>
public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;
    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }

    /// <summary>
    /// 设置厨房物品的父级
    /// </summary>
    /// <param name="kitchenObjectParent"></param>
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKiechenObject();
        }
        this.kitchenObjectParent = kitchenObjectParent;
        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("已有");
        }
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    /// <summary>
    /// 获取物品父级
    /// </summary>
    /// <returns></returns>
    public IKitchenObjectParent GetKitchenObjectParent() { return kitchenObjectParent; }

    /// <summary>
    /// 销毁自身  
    /// </summary>
    public void DestroySelf()
    {
        kitchenObjectParent.ClearKiechenObject();
        Destroy(gameObject);
    }
    /// <summary>
    /// 是否是盘子
    /// </summary>
    /// <param name="plateKitChenObject"></param>
    /// <returns></returns>
    public bool TryGetPlate(out PlateKitChenObject plateKitChenObject)
    {
        if (this is PlateKitChenObject)
        {
            plateKitChenObject = this as PlateKitChenObject;
            return true;
        }
        else
        {
            plateKitChenObject = null;
            return false;
        }
    }
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }

}

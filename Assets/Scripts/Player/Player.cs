using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 说明:角色控制
/// </summary>
public class Player : Singleton<Player>, IKitchenObjectParent
{
    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEnventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEnventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    /// <summary>
    /// 柜台层级
    /// </summary>
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform spawnPoint;

    /// <summary>
    /// 是否在移动
    /// </summary>
    private bool isWalking;
    /// <summary>
    /// 最后一次互动
    /// </summary>
    private Vector3 lastInteractDic;
    /// <summary>
    /// 交互距离
    /// </summary>
    private float interactDistance = 2f;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Start()
    {
        GameInput.Instance.OnInteractAction += Instance_OnInteractAction;
        GameInput.Instance.OnInteractAlternateAction += Instance_OnInteractAlternateAction;
    }

    private void Instance_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void Instance_OnInteractAction(object sender, EventArgs e)
    {
        selectedCounter?.Interact(this);
    }

    private void Update()
    {
        HandleMovement();
        if (GameManager.Instance.IsGamePlaying())
        {
            HandleInteractions();
        }

    }
    /// <summary>
    /// 处理移动
    /// </summary>
    private void HandleMovement()
    {
        //获取输入
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNoramlized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        //旋转 
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        //检测是否碰撞到物体
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = (moveDir.x < -.5f || moveDir.x > +0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = (moveDir.z < -.5f || moveDir.z > +0.5f) &&!Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }
        //移动
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;
        if (moveDir != Vector3.zero)
            lastInteractDic = moveDir;
    }
    private void HandleInteractions()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNoramlized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        if (moveDir != Vector3.zero)
            lastInteractDic = moveDir;
        if (Physics.Raycast(transform.position, lastInteractDic, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }

    }
    public bool IsWalking()
    {
        return isWalking;
    }
    /// <summary>
    /// 设置选中的柜台
    /// </summary>
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEnventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform() { return spawnPoint; }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject() { return kitchenObject; }
    public void ClearKiechenObject() { kitchenObject = null; }
    public bool HasKitchenObject() { return kitchenObject != null; }
}

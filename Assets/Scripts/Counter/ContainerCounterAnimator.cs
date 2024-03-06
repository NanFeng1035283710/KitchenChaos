using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 容器桌台动画控制
/// 说明:
/// </summary>
public class ContainerCounterAnimator : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;

    private Animator animator;
    private const string OPEN_CLOSE = "OpenClose";
    private void Start()
    {
        animator = GetComponent<Animator>();
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }
    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}

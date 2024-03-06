using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 烹饪糊了的闪光条脚本
/// </summary>
public class StoveBurnFlashingBarUI : MonoBehaviour
{
    private const string IS_FLASHING = "IsFlashing";
    [SerializeField] private StoveCounter stoveCounter;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        animator.SetBool(IS_FLASHING, false);
    }
    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        //烧糊进度大于0.5时展示警告ui
        float burnShowProgressAmount = 0.5f;
        //是不是已经煮好且进度条到0.5了
        bool show = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
        animator.SetBool(IS_FLASHING, show);
    }
}

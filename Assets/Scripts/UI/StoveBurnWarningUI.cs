using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炉子警告UI
/// </summary>
public class StoveBurnWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        Hide();
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        //烧糊进度大于0.5时展示警告ui
        float burnShowProgressAmount = 0.5f;
        //是不是已经煮好且进度条到0.5了
        bool show = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
        if (show) {

            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide() { gameObject.SetActive(false); }

}

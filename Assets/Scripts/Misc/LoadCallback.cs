using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景加载回调
/// </summary>
public class LoadCallback : MonoBehaviour
{
    private bool isFiretUpdata = true;
    private void Update()
    {
        if (isFiretUpdata)
        {
            isFiretUpdata = false;
            Loader.LoaderCallback();
        }
    }
}

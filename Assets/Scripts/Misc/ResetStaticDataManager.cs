using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 重置静态数据
/// </summary>
public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        BaseCounter.ResetStaticData();
        CuttingCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
    }
}

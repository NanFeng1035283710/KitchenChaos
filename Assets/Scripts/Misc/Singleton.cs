using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 说明:
/// </summary>
public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance { get { return instance; } }
    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this as T;
        }
    }
    public static bool IsInitialized
    {
        get { return instance != null; }
    }
    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
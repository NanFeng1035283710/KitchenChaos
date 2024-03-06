using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
/// <summary>
/// 
/// </summary>
public class AudioClipRefsSO : ScriptableObject
{
    /// <summary>
    /// 砍
    /// </summary>
    public AudioClip[] chop;
    /// <summary>
    /// 出餐失败
    /// </summary>
    public AudioClip[] deliveryFail;
    /// <summary>
    /// 出餐成功
    /// </summary>
    public AudioClip[] deliverySuccess;
    /// <summary>
    /// 脚步声
    /// </summary>
    public AudioClip[] footstep;
    /// <summary>
    /// 丢
    /// </summary>
    public AudioClip[] objectDrop;
    /// <summary>
    /// 拾取
    /// </summary>
    public AudioClip[] objectPickup;
    /// <summary>
    /// 炉子嘶嘶声
    /// </summary>
    public AudioClip stoveSizzle;
    /// <summary>
    /// 丢垃圾声
    /// </summary>
    public AudioClip[] trash;
    /// <summary>
    /// 警告声
    /// </summary>
    public AudioClip[] warning;
    
}

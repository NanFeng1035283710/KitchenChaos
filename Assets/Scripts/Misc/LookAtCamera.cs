using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 朝向相机
/// </summary>
public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        /// <summary>
        /// 看向相机
        /// </summary>
        LookAt,
        /// <summary>
        /// 看向相机反方向
        /// </summary>
        LookAtInverted,
        /// <summary>
        /// 朝向相机前方
        /// </summary>
        CameraForward,
        /// <summary>
        /// 朝向相机后方
        /// </summary>
        CameraBackward,
    }

    [SerializeField] private Mode mode;
    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                transform.LookAt(transform.position - Camera.main.transform.position);
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraBackward:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}

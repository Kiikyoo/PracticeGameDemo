using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
/// <summary>
/// 相机管理器
/// </summary>
public class CameraManager : SingleMonoBase<CameraManager>
{
    //CinemachineBrain组件
    public CinemachineBrain cmBrain;
    //自由相机
    public GameObject freeLookCamera;
    //自由相机的组件
    public CinemachineFreeLook freeLook;

    public void ResetFreeLookCamera()
    {
        freeLook.m_YAxis.Value = 0.5f;
        freeLook.m_XAxis.Value = PlayerController.INSTANCE.playerModel.transform.eulerAngles.y;
    }
}

using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
/// <summary>
/// ���������
/// </summary>
public class CameraManager : SingleMonoBase<CameraManager>
{
    //CinemachineBrain���
    public CinemachineBrain cmBrain;
    //�������
    public GameObject freeLookCamera;
    //������������
    public CinemachineFreeLook freeLook;

    public void ResetFreeLookCamera()
    {
        freeLook.m_YAxis.Value = 0.5f;
        freeLook.m_XAxis.Value = PlayerController.INSTANCE.playerModel.transform.eulerAngles.y;
    }
}

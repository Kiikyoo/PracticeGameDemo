using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
/// <summary>
/// ��Ҵ��п�ʼ״̬
/// </summary>
public class PlayerBigSkillStartState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        //�л��������ӽǾ�ͷ
        CameraManager.INSTANCE.cmBrain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut, 0f);
        //�ر������ӽǾ�ͷ
        CameraManager.INSTANCE.freeLookCamera.SetActive(false);
        //�򿪴��п�ʼ�ӽǾ�ͷ
        playerModel.bigSkillStartShot.SetActive(true);
        //���Ŷ���
        playerController.PlayAnimation("BigSkill_Start", 0f);
    }

    public override void Update()
    {
        base.Update();
        
        #region ��⶯�����Ž���
        if(IsAnimationEnd())
        {
            playerController.SwitchState(E_PlayerState.BigSkill);
            return;
        }    
        #endregion

    }
}

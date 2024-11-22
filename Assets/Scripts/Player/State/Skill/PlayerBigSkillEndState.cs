using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
/// <summary>
/// ��Ҵ��н���״̬
/// </summary>
public class PlayerBigSkillEndState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        //�л���ͷ
        //�رմ����ӽǾ�ͷ
        playerModel.bigSkillShot.SetActive(false);
        //�ָ������ӽǾ�ͷ
        CameraManager.INSTANCE.cmBrain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, 0.5f);
        //�������ɾ�ͷ

        CameraManager.INSTANCE.ResetFreeLookCamera();
        CameraManager.INSTANCE.freeLookCamera.SetActive(true);
        //���Ŷ���
        playerController.PlayAnimation("BigSkill_End", 0f);
    }


    public override void Update()
    {
        base.Update();

        #region ��⶯�����Ž���
        if (IsAnimationEnd())
        {
            playerController.SwitchState(E_PlayerState.Idle);
            return;
        }
        #endregion

        #region ��ⱼ��
        if (playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(E_PlayerState.Run);
            return;
        }
        #endregion

        #region �������
        if (playerController.inputSystem.Player.Evade.triggered)
        {
            //�л�������״̬
            playerController.SwitchState(E_PlayerState.Evade_Back);
            return;
        }
        #endregion

        #region ��⹥��
        if (playerController.inputSystem.Player.Fire.triggered)
        {
            //�л�����ͨ����״̬
            playerController.SwitchState(E_PlayerState.NormalAttack);
            return;
        }
        #endregion
    }
}

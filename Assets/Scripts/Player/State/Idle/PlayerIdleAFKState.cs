using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleAFKState : PlayerStateBase
{

    public override void Enter()
    {
        base.Enter();
        playerController.PlayAnimation("Idle_AFK");
    }

    public override void Update()
    {
        base.Update();
        //����������Ž������л��ش���״̬
        if (IsAnimationEnd())
        {
            statePlayingTime = 0;
            playerController.SwitchState(E_PlayerState.Idle);
            return;
        }

        #region ������
        if (playerController.inputSystem.Player.BigSkill.triggered)
        {
            //�������״̬
            playerController.SwitchState(E_PlayerState.BigSkillStart);
            return;
        }
        #endregion

        #region ����ƶ�
        if (playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(E_PlayerState.Walk);
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

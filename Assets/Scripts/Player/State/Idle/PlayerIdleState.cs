using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        playerController.PlayAnimation("Idle");

    }
    public override void Update()
    {
        base.Update();

        #region ���һ�
        if (statePlayingTime >= 7)
        {
            playerController.SwitchState(E_PlayerState.Idle_AFK);
            statePlayingTime = 0;
            return;
        }


        #endregion

        #region ������
        if (playerController.inputSystem.Player.BigSkill.triggered)
        {
            //�������״̬
            playerController.SwitchState(E_PlayerState.BigSkill_Start);
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

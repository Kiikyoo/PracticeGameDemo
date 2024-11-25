using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������ܽ���״̬
/// </summary>
public class PlayerEvadeEndState : PlayerStateBase
{

    public override void Enter()
    {
        base.Enter();

        #region �ж�ǰ������
        switch(playerModel.currentState)
        {
            case E_PlayerState.Evade_Front_End:
                playerController.PlayAnimation("Evade_Front_End");
                break;            
            case E_PlayerState.Evade_Back_End:
                playerController.PlayAnimation("Evade_Back_End");
                break;
        }

        #endregion
    }

    public override void Update()
    {
        base.Update();
        #region ����ƶ�����
        if (playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(E_PlayerState.Run);
            return;
        }
        #endregion

        #region �����Ƿ񲥷Ž���,����תΪ����״̬
        if (IsAnimationEnd())
        {
            playerController.SwitchState(E_PlayerState.Idle);
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

        #region ������
        if (playerController.inputSystem.Player.BigSkill.triggered)
        {
            //�������״̬
            playerController.SwitchState(E_PlayerState.BigSkillStart);
            return;
        }
        #endregion
    }


}


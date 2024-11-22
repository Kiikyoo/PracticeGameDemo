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
        switch(playerModel.State)
        {
            case PlayerState.Evade_Front:
                playerController.PlayAnimation("Evade_Front_End");
                break;            
            case PlayerState.Evade_Back:
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
            playerController.SwitchState(PlayerState.Run);
        }
        #endregion

        #region �����Ƿ񲥷Ž���,����תΪ����״̬
        if (IsAnimationEnd())
        {
            playerController.SwitchState(PlayerState.Idle);
        }
        #endregion

        #region ��⹥��
        if (playerController.inputSystem.Player.Fire.triggered)
        {
            //�л�����ͨ����״̬
            playerController.SwitchState(PlayerState.NormalAttack);
        }
        #endregion
    }


}


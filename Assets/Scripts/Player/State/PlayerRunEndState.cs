using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��Ҽ�ͣ״̬
/// </summary>
public class PlayerRunEndState : PlayerStateBase
{

    public override void Enter()
    {
        base.Enter();

        #region �ж����ҽ�
        switch(playerModel.moveFoot)
        {
            case MoveFoot.Left:

                playerController.PlayAnimation("Run_End_L",0.1f);

                break;
            case MoveFoot.Right:

                playerController.PlayAnimation("Run_End_R", 0.1f);

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

        #region �������
        if (playerController.inputSystem.Player.Evade.triggered)
        {
            //�л�������״̬
            playerController.SwitchState(PlayerState.Evade_Back);
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

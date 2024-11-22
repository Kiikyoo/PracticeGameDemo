using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvadeState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        #region �ж�ǰ������
        switch (playerModel.State)
        {
            case E_PlayerState.Run:
                playerController.PlayAnimation("Evade_Front");
                break;
            case E_PlayerState.Idle:
            case E_PlayerState.RunEnd:
            case E_PlayerState.NormalAttackEnd:
                playerController.PlayAnimation("Evade_Back");
                break;
        }
        #endregion
    }
    public override void Update()
    {
        base.Update();
        #region �����Ƿ񲥷Ž���,����תΪ���ܽ���״̬
        if (IsAnimationEnd())
        {
            playerController.SwitchState(E_PlayerState.EvadeEnd);
            return;
        }
        #endregion
    }
}

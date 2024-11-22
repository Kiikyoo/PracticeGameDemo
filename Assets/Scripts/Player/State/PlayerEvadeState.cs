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
            case PlayerState.Run:
                playerController.PlayAnimation("Evade_Front");
                break;
            case PlayerState.Idle:
            case PlayerState.RunEnd:
            case PlayerState.NormalAttackEnd:
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
            playerController.SwitchState(PlayerState.EvadeEnd);
        }
        #endregion
    }
}

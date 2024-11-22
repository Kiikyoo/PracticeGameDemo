using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvadeState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        #region 判断前后闪避
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
        #region 动画是否播放结束,是则转为闪避结束状态
        if (IsAnimationEnd())
        {
            playerController.SwitchState(PlayerState.EvadeEnd);
        }
        #endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnBackState : PlayerStateBase
{
    /// <summary>
    /// 转身状态
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        playerController.PlayAnimation("TurnBack", 0.1f);
    }



    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        #region 检测动画是否结束
        if(stateInfo.normalizedTime >= 1.0f && !playerModel.animator.IsInTransition(0))
        {
            playerController.SwitchState(PlayerState.Run);
        }

        #endregion
    }
}

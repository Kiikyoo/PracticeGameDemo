using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 转身状态
/// </summary>
public class PlayerTurnBackState : PlayerStateBase
{

    public override void Enter()
    {
        base.Enter();
        playerController.PlayAnimation("TurnBack", 0.1f);
    }



    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        #region 检测大招
        if (playerController.inputSystem.Player.BigSkill.triggered)
        {
            //切换到进入大招状态
            playerController.SwitchState(E_PlayerState.BigSkillStart);
            return;
        }
        #endregion

        #region 检测动画是否结束
        if (stateInfo.normalizedTime >= 1.0f && !playerModel.animator.IsInTransition(0))
        {
            playerController.SwitchState(E_PlayerState.Run);
            return;
        }

        #endregion
    }
}

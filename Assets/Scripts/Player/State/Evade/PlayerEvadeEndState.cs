using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家闪避结束状态
/// </summary>
public class PlayerEvadeEndState : PlayerStateBase
{

    public override void Enter()
    {
        base.Enter();

        #region 判断前后闪避
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
        #region 检测移动输入
        if (playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(E_PlayerState.Run);
            return;
        }
        #endregion

        #region 动画是否播放结束,是则转为待机状态
        if (IsAnimationEnd())
        {
            playerController.SwitchState(E_PlayerState.Idle);
            return;
        }
        #endregion

        #region 检测攻击
        if (playerController.inputSystem.Player.Fire.triggered)
        {
            //切换至闪避攻击状态
            playerController.SwitchState(E_PlayerState.NormalAttack);
            return;
        }
        #endregion

        #region 检测大招
        if (playerController.inputSystem.Player.BigSkill.triggered)
        {
            //进入大招状态
            playerController.SwitchState(E_PlayerState.BigSkill_Start);
            return;
        }
        #endregion
    }


}


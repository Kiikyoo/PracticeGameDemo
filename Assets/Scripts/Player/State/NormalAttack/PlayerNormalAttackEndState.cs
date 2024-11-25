using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 普通攻击后摇状态
/// </summary>
public class PlayerNormalAttackEndState : PlayerStateBase
{

    public override void Enter()
    {
        base.Enter();
        playerController.PlayAnimation($"Attack_Normal_{playerModel.skillConfig.currentNormalAttackIndex}_End");
    }

    public override void Update()
    {
        base.Update(); 
        #region 检测连击状态
        if(playerController.inputSystem.Player.Fire.triggered)
        {
            playerModel.skillConfig.currentNormalAttackIndex++;
            //若连招全部完成
            if (playerModel.skillConfig.currentNormalAttackIndex > playerModel.skillConfig.normalAttackDamageMultiple.Length)
            {
                //重新计算攻击段数
                playerModel.skillConfig.currentNormalAttackIndex = 1;
            }
            playerController.SwitchState(E_PlayerState.NormalAttack);
            return;
        }
        #endregion

        #region 检测移动
        if (playerController.inputMoveVec2 != Vector2.zero && statePlayingTime >= 0.4f)
        {
            playerController.SwitchState(E_PlayerState.Run);
            //重新计算攻击段数
            playerModel.skillConfig.currentNormalAttackIndex = 1;
        }
        #endregion

        #region 检测闪避
        if (playerController.inputSystem.Player.Evade.triggered)
        {
            //切换至闪避状态
            playerController.SwitchState(E_PlayerState.Evade_Front);
            //重新计算攻击段数
            playerModel.skillConfig.currentNormalAttackIndex = 1;
        }
        #endregion

        #region 检测动画播放结束
        if (IsAnimationEnd())
        {
            //切换至普通攻击后摇状态
            playerController.SwitchState(E_PlayerState.Idle);
            playerModel.skillConfig.currentNormalAttackIndex = 1;
        }
        #endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家普通攻击状态
/// </summary>
public class PlayerNormalAttackState : PlayerStateBase
{
    //判断是否进入下一段攻击连击
    private bool enterNextAttack;

    public override void Enter()
    {
        base.Enter();
        enterNextAttack = false;
        //播放普通攻击动画
        playerController.PlayAnimation($"Attack_Normal_{playerModel.skillConfig.currentNormalAttackIndex}");
    }

    public override void Update()
    {
        base.Update();

        #region 检测攻击输入
        if(stateInfo.normalizedTime >= 0.5f && playerController.inputSystem.Player.Fire.triggered)
        {
            enterNextAttack = true;
        }
        #endregion

        #region 检测是否进入下一段连击
        if (IsAnimationEnd())
        {
            if (enterNextAttack)
            {
                //直接进入下一段平A状态
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
            else
            {
                playerController.SwitchState(E_PlayerState.NormalAttackEnd);
                return;
            }
        }
        #endregion

        #region 检测大招
        if (playerController.inputSystem.Player.BigSkill.triggered && stateInfo.normalizedTime >= 0.5f)
        {
            //进入大招状态
            playerController.SwitchState(E_PlayerState.BigSkillStart);
            return;
        }
        #endregion
    }
}

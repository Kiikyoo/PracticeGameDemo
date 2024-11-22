using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家大招状态
/// </summary>
public class PlayerBigSkillState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        //关闭大招开始视角镜头
        playerModel.bigSkillStartShot.SetActive(false);
        //打开大招视角镜头
        playerModel.bigSkillShot.SetActive(true);


        playerController.PlayAnimation("BigSkill", 0f);
    }

    public override void Update()
    {
        base.Update();

        #region 检测动画播放结束
        if (IsAnimationEnd())
        {
            playerController.SwitchState(E_PlayerState.BigSkillEnd);
        }
        #endregion

    }
}

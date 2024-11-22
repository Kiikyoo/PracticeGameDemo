using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
/// <summary>
/// 玩家大招开始状态
/// </summary>
public class PlayerBigSkillStartState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        //切换至大招视角镜头
        CameraManager.INSTANCE.cmBrain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut, 0f);
        //关闭自由视角镜头
        CameraManager.INSTANCE.freeLookCamera.SetActive(false);
        //打开大招开始视角镜头
        playerModel.bigSkillStartShot.SetActive(true);
        //播放动画
        playerController.PlayAnimation("BigSkill_Start", 0f);
    }

    public override void Update()
    {
        base.Update();
        
        #region 检测动画播放结束
        if(IsAnimationEnd())
        {
            playerController.SwitchState(E_PlayerState.BigSkill);
            return;
        }    
        #endregion

    }
}

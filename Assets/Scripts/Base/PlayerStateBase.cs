using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家状态枚举
/// </summary>
public enum E_PlayerState
{
    Idle, Idle_AFK, Walk, Run, RunEnd, TurnBack, Evade_Front, Evade_Back, Evade_Front_End, Evade_Back_End, NormalAttack, NormalAttackEnd, BigSkillStart, BigSkill, BigSkillEnd
}
public class PlayerStateBase : StateBase
{

    //玩家控制器
    protected PlayerController playerController;
    //玩家模型
    protected PlayerModel playerModel;
    //动画信息
    protected AnimatorStateInfo stateInfo;
    //记录当前动画进入时间
    protected float statePlayingTime = 0;
    public override void Enter()
    {
        statePlayingTime = 0;
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
        //施加重力
        playerModel.characterController.Move(new Vector3(0, playerModel.gravity * Time.deltaTime, 0));
    }

    public override void Init(IStateMachineOwner owner)
    {
        playerController = (PlayerController)owner;
        playerModel = playerController.playerModel;
    }

    public override void LateUpdate()
    {

    }

    public override void UnInit()
    {
    }

    public override void Update()
    {
        //刷新动画状态信息
        stateInfo = playerModel.animator.GetCurrentAnimatorStateInfo(0);
        //动画进入时间开始计时
        statePlayingTime +=Time.deltaTime;
    }

    /// <summary>
    /// 判断动画是否结束
    /// </summary>
    public bool IsAnimationEnd()
    {
        return (stateInfo.normalizedTime >= 1.0f && !playerModel.animator.IsInTransition(0));
    }
}

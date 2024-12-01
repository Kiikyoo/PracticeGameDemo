using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���״̬ö��
/// </summary>
public enum E_PlayerState
{
    Idle, Idle_AFK, Walk, Run, RunEnd, TurnBack, Evade_Front, Evade_Back, Evade_Front_End, Evade_Back_End, NormalAttack, NormalAttackEnd, BigSkillStart, BigSkill, BigSkillEnd
}
public class PlayerStateBase : StateBase
{

    //��ҿ�����
    protected PlayerController playerController;
    //���ģ��
    protected PlayerModel playerModel;
    //������Ϣ
    protected AnimatorStateInfo stateInfo;
    //��¼��ǰ��������ʱ��
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
        //ʩ������
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
        //ˢ�¶���״̬��Ϣ
        stateInfo = playerModel.animator.GetCurrentAnimatorStateInfo(0);
        //��������ʱ�俪ʼ��ʱ
        statePlayingTime +=Time.deltaTime;
    }

    /// <summary>
    /// �ж϶����Ƿ����
    /// </summary>
    public bool IsAnimationEnd()
    {
        return (stateInfo.normalizedTime >= 1.0f && !playerModel.animator.IsInTransition(0));
    }
}

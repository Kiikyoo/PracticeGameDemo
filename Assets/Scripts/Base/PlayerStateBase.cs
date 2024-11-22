using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateBase : StateBase
{

    //��ҿ�����
    protected PlayerController playerController;
    //���ģ��
    protected PlayerModel playerModel;
    //������Ϣ
    protected AnimatorStateInfo stateInfo;
    //��¼��ǰ��������ʱ��
    protected float animationPlayTime = 0;
    public override void Enter()
    {
        animationPlayTime = 0;
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
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
        //ʩ������
        playerModel.characterController.Move(new Vector3(0, playerModel.gravity * Time.deltaTime, 0));
        //ˢ�¶���״̬��Ϣ
        stateInfo = playerModel.animator.GetCurrentAnimatorStateInfo(0);
        //��������ʱ�俪ʼ��ʱ
        animationPlayTime +=Time.deltaTime;
    }

    /// <summary>
    /// �ж϶����Ƿ����
    /// </summary>
    public bool IsAnimationEnd()
    {
        return (stateInfo.normalizedTime >= 1.0f && !playerModel.animator.IsInTransition(0));
    }
}

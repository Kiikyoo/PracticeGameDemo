using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ҿ�����
/// </summary>
public class PlayerController : SingleMonoBase<PlayerController>,IStateMachineOwner
{
    //����ϵͳ
    public InputSystem inputSystem;
    //����ƶ�����
    public Vector2 inputMoveVec2;

    //���ģ��
    public PlayerModel playerModel;
    //ת���ٶ�
    public float rotationSpeed = 8f;
    //״̬��
    private StateMachine stateMachine;

    private float evadeCD;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine(this);
        inputSystem = new InputSystem();
    }
    private void Start()
    {
        //�л�����״̬
        SwitchState(E_PlayerState.Idle);
        LockMouse();
    }
    /// <summary>
    /// �������������Ļ�м�
    /// </summary>
    private void LockMouse()
    {
        //�������м�
        Cursor.lockState = CursorLockMode.Locked;
        //���ع��
        Cursor.visible = false;
    }
    /// <summary>
    /// �л�״̬
    /// </summary>
    /// <param name="playerState"></param>
    public void SwitchState(E_PlayerState playerState)
    {
        switch (playerState)
        {
            case E_PlayerState.Idle:
                stateMachine.EnterState<PlayerIdleState>();
                break;
            case E_PlayerState.Run:
                stateMachine.EnterState<PlayerRunState>();
                break;         
            case E_PlayerState.RunEnd:
                stateMachine.EnterState<PlayerRunEndState>();
                break;
            case E_PlayerState.TurnBack:
                stateMachine.EnterState<PlayerTurnBackState>();
                break; 
            case E_PlayerState.Evade_Back:
            case E_PlayerState.Evade_Front:
                if (evadeCD != 1f)
                    return;
                stateMachine.EnterState<PlayerEvadeState>();
                evadeCD -= 1f;
                break;
            case E_PlayerState.EvadeEnd:
                stateMachine.EnterState<PlayerEvadeEndState>();
                break;
            case E_PlayerState.NormalAttack:
                stateMachine.EnterState<PlayerNormalAttackState>();
                break;            
            case E_PlayerState.NormalAttackEnd:
                stateMachine.EnterState<PlayerNormalAttackEndState>();
                break;
            case E_PlayerState.BigSkillStart:
                stateMachine.EnterState<PlayerBigSkillStartState>();
                break;
            case E_PlayerState.BigSkill:
                stateMachine.EnterState<PlayerBigSkillState>();
                break;
            case E_PlayerState.BigSkillEnd:
                stateMachine.EnterState<PlayerBigSkillEndState>();
                break;
        }
        playerModel.State = playerState;
    }
    /// <summary>
    /// ���Ŷ���
    /// </summary>
    /// <param name="animationName">��������</param>
    /// <param name="fixedTransitionDuration">����ʱ��</param>
    public void PlayAnimation(string animationName,float fixedTransitionDuration = 0.25f)
    {
        playerModel.animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration);
    }
    /// <summary>
    /// ���Ŷ���
    /// </summary>
    /// <param name="animationName">��������</param>
    /// <param name="fixedTransitionDuration">����ʱ��</param>
    /// <param name="fixedTimeOffset">������ʼƫ��ʱ��</param>
    public void PlayAnimation(string animationName,float fixedTransitionDuration,float fixedTimeOffset)
    {
        playerModel.animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration, 0, fixedTimeOffset);
    }

    private void Update()
    {
        //�����������
        inputMoveVec2 = inputSystem.Player.Move.ReadValue<Vector2>().normalized;

        if(evadeCD < 1f)
        {
            evadeCD += Time.deltaTime;
            if(evadeCD > 1f)
            {
                evadeCD = 1f;
            }
        }
    }

    private void OnEnable()
    {
        inputSystem.Enable();
    }
    private void OnDisable()
    {
        inputSystem.Disable();
    }

}

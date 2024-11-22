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
        SwitchState(PlayerState.Idle);
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
    public void SwitchState(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                stateMachine.EnterState<PlayerIdleState>();
                break;
            case PlayerState.Run:
                stateMachine.EnterState<PlayerRunState>();
                break;         
            case PlayerState.RunEnd:
                stateMachine.EnterState<PlayerRunEndState>();
                break;
            case PlayerState.TurnBack:
                stateMachine.EnterState<PlayerTurnBackState>();
                break; 
            case PlayerState.Evade_Back:
            case PlayerState.Evade_Front:
                if (evadeCD != 1f)
                    return;
                stateMachine.EnterState<PlayerEvadeState>();
                evadeCD -= 1f;
                break;
            case PlayerState.EvadeEnd:
                stateMachine.EnterState<PlayerEvadeEndState>();
                break;
            case PlayerState.NormalAttack:
                stateMachine.EnterState<PlayerNormalAttackState>();
                break;            
            case PlayerState.NormalAttackEnd:
                stateMachine.EnterState<PlayerNormalAttackEndState>();
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

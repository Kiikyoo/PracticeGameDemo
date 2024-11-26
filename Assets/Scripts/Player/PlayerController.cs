using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家控制器
/// </summary>
public class PlayerController : SingleMonoBase<PlayerController>,IStateMachineOwner
{
    //输入系统
    public InputSystem inputSystem;
    //玩家移动输入
    public Vector2 inputMoveVec2;

    //玩家模型
    public PlayerModel playerModel;
    //转向速度
    public float rotationSpeed = 8f;
    //状态机
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
        //切换待机状态
        SwitchState(E_PlayerState.Idle);
        LockMouse();
    }
    /// <summary>
    /// 将鼠标锁定至屏幕中间
    /// </summary>
    private void LockMouse()
    {
        //锁定至中间
        Cursor.lockState = CursorLockMode.Locked;
        //隐藏光标
        Cursor.visible = false;
    }
    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="playerState"></param>
    public void SwitchState(E_PlayerState playerState)
    {
        playerModel.currentState = playerState;
        switch (playerState)
        {
            case E_PlayerState.Idle:
                stateMachine.EnterState<PlayerIdleState>();
                break;
            case E_PlayerState.Idle_AFK:
                stateMachine.EnterState<PlayerIdleAFKState>();
                break;
            case E_PlayerState.Walk:
            case E_PlayerState.Run:
                stateMachine.EnterState<PlayerRunState>(true);
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
                {
                    return;
                }
                stateMachine.EnterState<PlayerEvadeState>();
                evadeCD = 0f;
                break;
            case E_PlayerState.Evade_Front_End:
            case E_PlayerState.Evade_Back_End:
                stateMachine.EnterState<PlayerEvadeEndState>();
                break;
            case E_PlayerState.NormalAttack:
                stateMachine.EnterState<PlayerNormalAttackState>(true);
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
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="animationName">动画名称</param>
    /// <param name="fixedTransitionDuration">过渡时间</param>
    public void PlayAnimation(string animationName,float fixedTransitionDuration = 0.25f)
    {
        playerModel.animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration);
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="animationName">动画名称</param>
    /// <param name="fixedTransitionDuration">过渡时间</param>
    /// <param name="fixedTimeOffset">动画起始偏移时间</param>
    public void PlayAnimation(string animationName,float fixedTransitionDuration,float fixedTimeOffset)
    {
        playerModel.animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration, 0, fixedTimeOffset);
    }

    private void Update()
    {
        //更新玩家输入
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

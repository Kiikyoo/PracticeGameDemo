using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IStateMachineOwner { }
/// <summary>
/// 有限状态机
/// </summary>
public class StateMachine
{
    //当前状态
    private StateBase currentState;
    //是否包含当前状态
    public bool HasState { get => currentState != null; }
    //宿主
    private IStateMachineOwner owner;
    //状态字典
    private Dictionary<Type, StateBase> stateDic = new Dictionary<Type, StateBase>();
    public StateMachine(IStateMachineOwner owner)
    {
        Init(owner);
    }
    //有限状态机的初始化
    public void Init(IStateMachineOwner owner)
    {
        this.owner = owner;
    }
    /// <summary>
    /// 进入状态
    /// </summary>
    /// <typeparam name="T">状态类型</typeparam>
    /// <typeparam name="reLoadState">是否刷新状态</typeparam>
    public void EnterState<T>(bool reLoadState = false)where T : StateBase, new()
    {
        //如果状态一致则不切换状态
        if (HasState && currentState.GetType() == typeof(T) && !reLoadState)
        {
            return;
        }   
        #region 结束当前状态
        if(HasState)
        {
            ExitCurrentState();
        }
        #endregion

        #region 进入新状态
        currentState = LoadState<T>();
        EnterCurrentState();
        #endregion
    }
    /// <summary>
    /// 创建新状态
    /// </summary>
    /// <typeparam name="T">状态类型</typeparam>
    /// <returns></returns>
    private StateBase LoadState<T>() where T : StateBase, new()
    {
        //获取状态类型
        Type stateType = typeof(T);
        //如果字典不存在该状态，创建新状态并保存至字典
        if(!stateDic.TryGetValue(stateType, out StateBase state))
        {
            state = new T();
            state.Init(owner);
            stateDic.Add(stateType, state);
        }
        return state;
    }

    private void EnterCurrentState()
    {
        currentState.Enter();
        MonoManager.INSTANCE.AddUpdateAction(currentState.Update);
        MonoManager.INSTANCE.AddFixedUpdateAction(currentState.FixedUpdate);
        MonoManager.INSTANCE.AddLateUpdateAction(currentState.LateUpdate);
    }
    private void ExitCurrentState()
    {
        currentState.Exit();
        MonoManager.INSTANCE.RemoveUpdateAction(currentState.Update);
        MonoManager.INSTANCE.RemoveFixedUpdateAction(currentState.FixedUpdate);
        MonoManager.INSTANCE.RemoveLateUpdateAction(currentState.LateUpdate);
    }
/// <summary>
/// 停止运作
/// </summary>
    public void Stop()
    {
        ExitCurrentState();
        foreach (var item in stateDic.Values)
        {
            item.UnInit(); 
        }
        stateDic.Clear();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 状态基类
/// </summary>
public abstract class StateBase
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="owner"></param>
    public abstract void Init(IStateMachineOwner owner);
    /// <summary>
    /// 释放资源
    /// </summary>
    public abstract void UnInit();
    //进入状态
    public abstract void Enter();
    //结束状态
    public abstract void Exit();
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void LateUpdate();

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Update托管执行器
/// </summary>
public class MonoManager : SingleMonoBase<MonoManager>
{
    public Action updateAction;
    public Action fixedUpdateAction;
    public Action lateUpdateAction;
    /// <summary>
    /// 添加Update任务
    /// </summary>
    /// <param name="task"></param>
    public void AddUpdateAction(Action task)
    {
        updateAction += task;
    }
    /// <summary>
    /// 移除Update任务
    /// </summary>
    /// <param name="task"></param>
    public void RemoveUpdateAction(Action task)
    {
        updateAction -= task;
    }
    /// <summary>
    /// 添加FixedUpdate任务
    /// </summary>
    /// <param name="task"></param>
    public void AddFixedUpdateAction(Action task)
    {
        fixedUpdateAction += task;
    }
    /// <summary>
    /// 移除FixedUpdate任务
    /// </summary>
    /// <param name="task"></param>
    public void RemoveFixedUpdateAction(Action task)
    {
        fixedUpdateAction -= task;
    }

    /// <summary>
    /// 添加LateUpdate任务
    /// </summary>
    /// <param name="task"></param>
    public void AddLateUpdateAction(Action task)
    {
        lateUpdateAction += task;
    }
    /// <summary>
    /// 移除Update任务
    /// </summary>
    /// <param name="task"></param>
    public void RemoveLateUpdateAction(Action task)
    {
        lateUpdateAction -= task;
    }


    void Update()
    {
        updateAction?.Invoke();
    }
    void FixedUpdate()
    {
        fixedUpdateAction?.Invoke();
    }
    void LateUpdate()
    {
        lateUpdateAction?.Invoke();
    }
}

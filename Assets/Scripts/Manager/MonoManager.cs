using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Update�й�ִ����
/// </summary>
public class MonoManager : SingleMonoBase<MonoManager>
{
    public Action updateAction;
    public Action fixedUpdateAction;
    public Action lateUpdateAction;
    /// <summary>
    /// ���Update����
    /// </summary>
    /// <param name="task"></param>
    public void AddUpdateAction(Action task)
    {
        updateAction += task;
    }
    /// <summary>
    /// �Ƴ�Update����
    /// </summary>
    /// <param name="task"></param>
    public void RemoveUpdateAction(Action task)
    {
        updateAction -= task;
    }
    /// <summary>
    /// ���FixedUpdate����
    /// </summary>
    /// <param name="task"></param>
    public void AddFixedUpdateAction(Action task)
    {
        fixedUpdateAction += task;
    }
    /// <summary>
    /// �Ƴ�FixedUpdate����
    /// </summary>
    /// <param name="task"></param>
    public void RemoveFixedUpdateAction(Action task)
    {
        fixedUpdateAction -= task;
    }

    /// <summary>
    /// ���LateUpdate����
    /// </summary>
    /// <param name="task"></param>
    public void AddLateUpdateAction(Action task)
    {
        lateUpdateAction += task;
    }
    /// <summary>
    /// �Ƴ�Update����
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

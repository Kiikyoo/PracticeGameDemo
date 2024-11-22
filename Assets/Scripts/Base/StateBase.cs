using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ״̬����
/// </summary>
public abstract class StateBase
{
    /// <summary>
    /// ��ʼ��
    /// </summary>
    /// <param name="owner"></param>
    public abstract void Init(IStateMachineOwner owner);
    /// <summary>
    /// �ͷ���Դ
    /// </summary>
    public abstract void UnInit();
    //����״̬
    public abstract void Enter();
    //����״̬
    public abstract void Exit();
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void LateUpdate();

}

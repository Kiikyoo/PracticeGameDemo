using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ͨ����״̬
/// </summary>
public class PlayerNormalAttackState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        //������ͨ��������
        playerController.PlayAnimation($"Attack_Normal_{playerModel.skillConfig.currentNormalAttackIndex}");
    }

    public override void Update()
    {
        base.Update();

        #region ��⶯���Ƿ����
        if (IsAnimationEnd())
        {
            playerController.SwitchState(E_PlayerState.NormalAttackEnd);
            return;
        }
        #endregion
    }
}

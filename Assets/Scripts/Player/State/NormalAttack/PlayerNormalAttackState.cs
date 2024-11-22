using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Íæ¼ÒÆÕÍ¨¹¥»÷×´Ì¬
/// </summary>
public class PlayerNormalAttackState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        //²¥·ÅÆÕÍ¨¹¥»÷¶¯»­
        playerController.PlayAnimation($"Attack_Normal_{playerModel.skillConfig.currentNormalAttackIndex}");
    }

    public override void Update()
    {
        base.Update();

        #region ¼ì²â¶¯»­ÊÇ·ñ½áÊø
        if (IsAnimationEnd())
        {
            playerController.SwitchState(E_PlayerState.NormalAttackEnd);
        }
        #endregion
    }
}

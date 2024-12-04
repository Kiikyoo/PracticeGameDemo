using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvadeAttackEnd : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        //²¥·ÅÉÁ±Ü¹¥»÷½áÊø¶¯»­
        playerController.PlayAnimation("Evade_Attack_End", 0.1f);
    }


    public override void Update()
    {
        base.Update();

        #region ¼ì²â´ý»ú
        if (IsAnimationEnd())
        {
            playerController.SwitchState(E_PlayerState.Idle);
            return;
        }
        #endregion

        #region ¼ì²â´óÕÐ
        if (playerController.inputSystem.Player.BigSkill.triggered)
        {
            //ÇÐ»»µ½½øÈë´óÕÐ×´Ì¬
            playerController.SwitchState(E_PlayerState.BigSkill_Start);
            return;
        }
        #endregion

        #region ¼ì²â¹¥»÷
        if (playerController.inputSystem.Player.Fire.triggered)
        {
            //ÇÐ»»ÖÁÆÕÍ¨¹¥»÷×´Ì¬
            playerController.SwitchState(E_PlayerState.NormalAttack);
            return;
        }
        #endregion

        #region ¼ì²âÉÁ±Ü
        if (playerController.inputSystem.Player.Evade.triggered)
        {
            //ÇÐ»»ÖÁÉÁ±Ü×´Ì¬
            playerController.SwitchState(E_PlayerState.Evade_Front);
            return;
        }
        #endregion

        #region ¼ì²âÒÆ¶¯
        if (playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(E_PlayerState.Run);
            return;
        }
        #endregion
    }
}

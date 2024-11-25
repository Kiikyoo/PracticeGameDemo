using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleAFKState : PlayerStateBase
{

    public override void Enter()
    {
        base.Enter();
        playerController.PlayAnimation("Idle_AFK");
    }

    public override void Update()
    {
        base.Update();
        //Èç¹û¶¯»­²¥·Å½áÊø£¬ÇÐ»»»Ø´ý»ú×´Ì¬
        if (IsAnimationEnd())
        {
            statePlayingTime = 0;
            playerController.SwitchState(E_PlayerState.Idle);
            return;
        }

        #region ¼ì²â´óÕÐ
        if (playerController.inputSystem.Player.BigSkill.triggered)
        {
            //½øÈë´óÕÐ×´Ì¬
            playerController.SwitchState(E_PlayerState.BigSkillStart);
            return;
        }
        #endregion

        #region ¼ì²âÒÆ¶¯
        if (playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(E_PlayerState.Walk);
            return;
        }
        #endregion

        #region ¼ì²âÉÁ±Ü
        if (playerController.inputSystem.Player.Evade.triggered)
        {
            //ÇÐ»»ÖÁÉÁ±Ü×´Ì¬
            playerController.SwitchState(E_PlayerState.Evade_Back);
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
    }
}

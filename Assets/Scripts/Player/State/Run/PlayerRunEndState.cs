using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Íæ¼Ò¼±Í£×´Ì¬
/// </summary>
public class PlayerRunEndState : PlayerStateBase
{

    public override void Enter()
    {
        base.Enter();

        #region ÅÐ¶Ï×óÓÒ½Å
        switch(playerModel.moveFoot)
        {
            case MoveFoot.Left:

                playerController.PlayAnimation("Run_End_L",0.1f);

                break;
            case MoveFoot.Right:

                playerController.PlayAnimation("Run_End_R", 0.1f);

                break;

        }
        #endregion
    }

    public override void Update()
    {
        base.Update();

        #region ¼ì²â´óÕÐ
        if (playerController.inputSystem.Player.BigSkill.triggered)
        {
            //½øÈë´óÕÐ×´Ì¬
            playerController.SwitchState(E_PlayerState.BigSkillStart);
            return;
        }
        #endregion


        #region ¼ì²âÒÆ¶¯ÊäÈë
        if (playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(E_PlayerState.Run);
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

        #region ¶¯»­ÊÇ·ñ²¥·Å½áÊø,ÊÇÔò×ªÎª´ý»ú×´Ì¬
        if (IsAnimationEnd())
        {
            playerController.SwitchState(E_PlayerState.Idle);
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

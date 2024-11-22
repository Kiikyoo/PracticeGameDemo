using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        playerController.PlayAnimation("Idle");

    }
    public override void Update()
    {
        base.Update();

        #region ºÏ≤‚¥Û’–
        if (playerController.inputSystem.Player.BigSkill.triggered)
        {
            //Ω¯»Î¥Û’–◊¥Ã¨
            playerController.SwitchState(E_PlayerState.BigSkillStart);
            return;
        }
        #endregion

        #region ºÏ≤‚±º≈‹
        if (playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(E_PlayerState.Run);
            return;
        }
        #endregion

        #region ºÏ≤‚…¡±‹
        if (playerController.inputSystem.Player.Evade.triggered)
        {
            //«–ªª÷¡…¡±‹◊¥Ã¨
            playerController.SwitchState(E_PlayerState.Evade_Back);
            return;
        }
        #endregion

        #region ºÏ≤‚π•ª˜
        if (playerController.inputSystem.Player.Fire.triggered)
        {
            //«–ªª÷¡∆’Õ®π•ª˜◊¥Ã¨
            playerController.SwitchState(E_PlayerState.NormalAttack);
            return;
        }
        #endregion
    }
}

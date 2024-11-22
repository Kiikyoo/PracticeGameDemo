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

        #region ºÏ≤‚±º≈‹
        if(playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(PlayerState.Run);
        }
        #endregion

        #region ºÏ≤‚…¡±‹
        if (playerController.inputSystem.Player.Evade.triggered)
        {
            //«–ªª÷¡…¡±‹◊¥Ã¨
            playerController.SwitchState(PlayerState.Evade_Back);
        }
        #endregion

        #region ºÏ≤‚π•ª˜
        if (playerController.inputSystem.Player.Fire.triggered)
        {
            //«–ªª÷¡∆’Õ®π•ª˜◊¥Ã¨
            playerController.SwitchState(PlayerState.NormalAttack);
        }
        #endregion
    }
}

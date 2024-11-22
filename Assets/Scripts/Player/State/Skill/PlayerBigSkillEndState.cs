using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
/// <summary>
/// ÕÊº“¥Û’–Ω· ¯◊¥Ã¨
/// </summary>
public class PlayerBigSkillEndState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        //«–ªªæµÕ∑
        //πÿ±’¥Û’– ”Ω«æµÕ∑
        playerModel.bigSkillShot.SetActive(false);
        //ª÷∏¥◊‘”… ”Ω«æµÕ∑
        CameraManager.INSTANCE.cmBrain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, 0.5f);
        //º§ªÓ◊‘”…æµÕ∑

        CameraManager.INSTANCE.ResetFreeLookCamera();
        CameraManager.INSTANCE.freeLookCamera.SetActive(true);
        //≤•∑≈∂Øª≠
        playerController.PlayAnimation("BigSkill_End", 0f);
    }


    public override void Update()
    {
        base.Update();

        #region ºÏ≤‚∂Øª≠≤•∑≈Ω· ¯
        if (IsAnimationEnd())
        {
            playerController.SwitchState(E_PlayerState.Idle);
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

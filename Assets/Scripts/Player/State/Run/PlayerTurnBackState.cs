using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ת��״̬
/// </summary>
public class PlayerTurnBackState : PlayerStateBase
{

    public override void Enter()
    {
        base.Enter();
        playerController.PlayAnimation("TurnBack", 0.1f);
    }



    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        #region ������
        if (playerController.inputSystem.Player.BigSkill.triggered)
        {
            //�л����������״̬
            playerController.SwitchState(E_PlayerState.BigSkillStart);
            return;
        }
        #endregion

        #region ��⶯���Ƿ����
        if (stateInfo.normalizedTime >= 1.0f && !playerModel.animator.IsInTransition(0))
        {
            playerController.SwitchState(E_PlayerState.Run);
            return;
        }

        #endregion
    }
}

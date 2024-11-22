using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnBackState : PlayerStateBase
{
    /// <summary>
    /// ת��״̬
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        playerController.PlayAnimation("TurnBack", 0.1f);
    }



    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        #region ��⶯���Ƿ����
        if(stateInfo.normalizedTime >= 1.0f && !playerModel.animator.IsInTransition(0))
        {
            playerController.SwitchState(PlayerState.Run);
        }

        #endregion
    }
}

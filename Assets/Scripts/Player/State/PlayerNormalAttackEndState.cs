using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ͨ������ҡ״̬
/// </summary>
public class PlayerNormalAttackEndState : PlayerStateBase
{

    public override void Enter()
    {
        base.Enter();
        playerController.PlayAnimation($"Attack_Normal_{playerModel.skillConfig.currentNormalAttackIndex}_End");
    }

    public override void Update()
    {
        base.Update(); 
        #region �������״̬
        if(playerController.inputSystem.Player.Fire.triggered)
        {
            playerModel.skillConfig.currentNormalAttackIndex++;
            //������ȫ�����
            if (playerModel.skillConfig.currentNormalAttackIndex > playerModel.skillConfig.normalAttackDamageMultiple.Length)
            {
                //���¼��㹥������
                playerModel.skillConfig.currentNormalAttackIndex = 1;
            }
            playerController.SwitchState(PlayerState.NormalAttack);
            return;
        }
        #endregion

        #region ����ƶ�
        if (playerController.inputMoveVec2 != Vector2.zero && animationPlayTime >= 0.4f)
        {
            playerController.SwitchState(PlayerState.Run);
            //���¼��㹥������
            playerModel.skillConfig.currentNormalAttackIndex = 1;
        }
        #endregion

        #region �������
        if (playerController.inputSystem.Player.Evade.triggered)
        {
            //�л�������״̬
            playerController.SwitchState(PlayerState.Evade_Front);
            //���¼��㹥������
            playerModel.skillConfig.currentNormalAttackIndex = 1;
        }
        #endregion

        #region ��⶯�����Ž���
        if (IsAnimationEnd())
        {
            //�л�����ͨ������ҡ״̬
            playerController.SwitchState(PlayerState.Idle);
            playerModel.skillConfig.currentNormalAttackIndex = 1;
        }
        #endregion
    }
}

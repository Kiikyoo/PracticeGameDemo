using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ͨ����״̬
/// </summary>
public class PlayerNormalAttackState : PlayerStateBase
{
    //�ж��Ƿ������һ�ι�������
    private bool enterNextAttack;

    public override void Enter()
    {
        base.Enter();
        enterNextAttack = false;
        //������ͨ��������
        playerController.PlayAnimation($"Attack_Normal_{playerModel.skillConfig.currentNormalAttackIndex}");
    }

    public override void Update()
    {
        base.Update();

        #region ��⹥������
        if(stateInfo.normalizedTime >= 0.5f && playerController.inputSystem.Player.Fire.triggered)
        {
            enterNextAttack = true;
        }
        #endregion

        #region ����Ƿ������һ������
        if (IsAnimationEnd())
        {
            if (enterNextAttack)
            {
                //ֱ�ӽ�����һ��ƽA״̬
                playerModel.skillConfig.currentNormalAttackIndex++;
                //������ȫ�����
                if (playerModel.skillConfig.currentNormalAttackIndex > playerModel.skillConfig.normalAttackDamageMultiple.Length)
                {
                    //���¼��㹥������
                    playerModel.skillConfig.currentNormalAttackIndex = 1;
                }
                playerController.SwitchState(E_PlayerState.NormalAttack);
                return;
            }
            else
            {
                playerController.SwitchState(E_PlayerState.NormalAttackEnd);
                return;
            }
        }
        #endregion

        #region ������
        if (playerController.inputSystem.Player.BigSkill.triggered && stateInfo.normalizedTime >= 0.5f)
        {
            //�������״̬
            playerController.SwitchState(E_PlayerState.BigSkillStart);
            return;
        }
        #endregion
    }
}

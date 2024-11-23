using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��Ҵ���״̬
/// </summary>
public class PlayerBigSkillState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        //�رմ��п�ʼ�ӽǾ�ͷ
        playerModel.bigSkillStartShot.SetActive(false);
        //�򿪴����ӽǾ�ͷ
        playerModel.bigSkillShot.SetActive(true);
        //�ر��������ý�ɫ��Ծ����
        //playerModel.gravity = 0f;

        playerController.PlayAnimation("BigSkill", 0f);
    }

    public override void Update()
    {
        base.Update();

        #region ��⶯�����Ž���
        if (IsAnimationEnd())
        {

            playerController.SwitchState(E_PlayerState.BigSkillEnd);
            return;
        }
        #endregion

    }
}

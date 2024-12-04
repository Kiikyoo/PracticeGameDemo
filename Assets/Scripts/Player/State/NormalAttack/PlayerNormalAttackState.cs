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

        #region �����������
        GameObject targetEnemy = null;
        //��ʼ��������˾��룬Ĭ�����ֵ
        float minDistance = Mathf.Infinity;
        //��������ӵ�б�ǩ�ĵ���
        foreach(string tag in playerController.enemyTagList)
        {
            //��ȡ������ӵ�б�ǩ�ĵ�������
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
            foreach(GameObject enemy in enemies)
            {
                //������˾���
                float distance = Vector3.Distance(playerModel.transform.position, enemy.transform.position);
                //����ҵ�����������ˣ��л�Ŀ�����
                if(distance < minDistance)
                {
                    targetEnemy = enemy;
                    minDistance = distance;
                }
            }
        }
        //����Χ�ڴ��ڵ���
        if(targetEnemy != null && minDistance < 5f)
        {
            //���������������˵ķ���
            Vector3 direction = (targetEnemy.transform.position - playerModel.transform.position).normalized;
            //���ģ�ͳ���÷���
            playerModel.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        }
        #endregion
        
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
                playerController.SwitchState(E_PlayerState.NormalAttack_End);
                return;
            }
        }
        #endregion

        #region ������
        if (playerController.inputSystem.Player.BigSkill.triggered && stateInfo.normalizedTime >= 0.5f)
        {
            //�������״̬
            playerController.SwitchState(E_PlayerState.BigSkill_Start);
            return;
        }
        #endregion

    }
}

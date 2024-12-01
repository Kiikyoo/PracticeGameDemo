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

        //�����������
        #region �����������
        GameObject targetEnemy = null;
        //��ʼ��������˾��룬Ĭ�����ֵ
        float minDistance = Mathf.Infinity;
        //��������ӵ�б�ǩ�ĵ���
        foreach (string tag in playerController.enemyTagList)
        {
            //��ȡ������ӵ�б�ǩ�ĵ�������
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject enemy in enemies)
            {
                //������˾���
                float distance = Vector3.Distance(playerModel.transform.position, enemy.transform.position);
                //����ҵ�����������ˣ��л�Ŀ�����
                if (distance < minDistance)
                {
                    targetEnemy = enemy;
                    minDistance = distance;
                }
            }
        }
        //����Χ�ڴ��ڵ���
        if (targetEnemy != null && minDistance < 7f)
        {
            //���������������˵ķ���
            Vector3 direction = (targetEnemy.transform.position - playerModel.transform.position).normalized;
            //���ģ�ͳ���÷���
            playerModel.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        }
        #endregion

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

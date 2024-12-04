using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvadeAttack : PlayerStateBase
{
    public override void Enter()
    {

        base.Enter();

        #region 锁定最近敌人
        GameObject targetEnemy = null;
        //初始化最近敌人距离，默认最大值
        float minDistance = Mathf.Infinity;
        //遍历所有拥有标签的敌人
        foreach (string tag in playerController.enemyTagList)
        {
            //获取场景中拥有标签的敌人数组
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject enemy in enemies)
            {
                //计算敌人距离
                float distance = Vector3.Distance(playerModel.transform.position, enemy.transform.position);
                //如果找到更近距离敌人，切换目标敌人
                if (distance < minDistance)
                {
                    targetEnemy = enemy;
                    minDistance = distance;
                }
            }
        }
        //若范围内存在敌人
        if (targetEnemy != null && minDistance < 5f)
        {
            //计算玩家与最近敌人的方向
            Vector3 direction = (targetEnemy.transform.position - playerModel.transform.position).normalized;
            //玩家模型朝向该方向
            playerModel.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        }
        #endregion

        //播放闪避攻击动画
        playerController.PlayAnimation("Evade_Attack");
    }

    public override void Update()
    {
        base.Update();

        #region 检测大招
        if (playerController.inputSystem.Player.BigSkill.triggered && stateInfo.normalizedTime >= 0.5f)
        {
            //进入大招状态
            playerController.SwitchState(E_PlayerState.BigSkill_Start);
            return;
        }
        #endregion

        #region 动画是否播放结束,是则转为闪避攻击结束状态
        if (IsAnimationEnd())
        {
            playerController.SwitchState(E_PlayerState.EvadeAttack_End);
            return;
        }
        #endregion

        #region 检测闪避
        if (playerController.inputSystem.Player.Evade.triggered)
        {
            //切换至闪避状态
            playerController.SwitchState(E_PlayerState.Evade_Front);
            //重新计算攻击段数
            playerModel.skillConfig.currentNormalAttackIndex = 1;
            return;
        }
        #endregion





    }
}

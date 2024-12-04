using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家普通攻击状态
/// </summary>
public class PlayerNormalAttackState : PlayerStateBase
{
    //判断是否进入下一段攻击连击
    private bool enterNextAttack;

    public override void Enter()
    {
        base.Enter();
        enterNextAttack = false;

        #region 锁定最近敌人
        GameObject targetEnemy = null;
        //初始化最近敌人距离，默认最大值
        float minDistance = Mathf.Infinity;
        //遍历所有拥有标签的敌人
        foreach(string tag in playerController.enemyTagList)
        {
            //获取场景中拥有标签的敌人数组
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
            foreach(GameObject enemy in enemies)
            {
                //计算敌人距离
                float distance = Vector3.Distance(playerModel.transform.position, enemy.transform.position);
                //如果找到更近距离敌人，切换目标敌人
                if(distance < minDistance)
                {
                    targetEnemy = enemy;
                    minDistance = distance;
                }
            }
        }
        //若范围内存在敌人
        if(targetEnemy != null && minDistance < 5f)
        {
            //计算玩家与最近敌人的方向
            Vector3 direction = (targetEnemy.transform.position - playerModel.transform.position).normalized;
            //玩家模型朝向该方向
            playerModel.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        }
        #endregion
        
        //播放普通攻击动画
        playerController.PlayAnimation($"Attack_Normal_{playerModel.skillConfig.currentNormalAttackIndex}");
    }

    public override void Update()
    {
        base.Update();

        #region 检测攻击输入
        if(stateInfo.normalizedTime >= 0.5f && playerController.inputSystem.Player.Fire.triggered)
        {
            enterNextAttack = true;
        }
        #endregion

        #region 检测是否进入下一段连击
        if (IsAnimationEnd())
        {
            if (enterNextAttack)
            {
                //直接进入下一段平A状态
                playerModel.skillConfig.currentNormalAttackIndex++;
                //若连招全部完成
                if (playerModel.skillConfig.currentNormalAttackIndex > playerModel.skillConfig.normalAttackDamageMultiple.Length)
                {
                    //重新计算攻击段数
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

        #region 检测大招
        if (playerController.inputSystem.Player.BigSkill.triggered && stateInfo.normalizedTime >= 0.5f)
        {
            //进入大招状态
            playerController.SwitchState(E_PlayerState.BigSkill_Start);
            return;
        }
        #endregion

    }
}

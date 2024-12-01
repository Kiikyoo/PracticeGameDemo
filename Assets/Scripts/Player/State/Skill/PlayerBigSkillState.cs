using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家大招状态
/// </summary>
public class PlayerBigSkillState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        //关闭大招开始视角镜头
        playerModel.bigSkillStartShot.SetActive(false);

        //打开大招视角镜头
        playerModel.bigSkillShot.SetActive(true);

        //锁定最近敌人
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
        if (targetEnemy != null && minDistance < 7f)
        {
            //计算玩家与最近敌人的方向
            Vector3 direction = (targetEnemy.transform.position - playerModel.transform.position).normalized;
            //玩家模型朝向该方向
            playerModel.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        }
        #endregion

        playerController.PlayAnimation("BigSkill", 0f);
    }

    public override void Update()
    {
        base.Update();

        #region 检测动画播放结束
        if (IsAnimationEnd())
        {

            playerController.SwitchState(E_PlayerState.BigSkillEnd);
            return;
        }
        #endregion
    }
}

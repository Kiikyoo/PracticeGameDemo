using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 武器控制器
/// </summary>
public class WeaponController : MonoBehaviour
{
    //武器碰撞体
    public Collider hitCollider;
    //敌人标签列表
    public List<string> enemyTagList;
    //单次攻击时敌人受击列表
    public List<IHurt> enemyHurtList = new List<IHurt>();
    //命中事件
    private Action<IHurt> onHItAction;
    public void Init(List<string> enemyTagList, Action<IHurt> onHItAction)
    {
        StopHit();
        this.enemyTagList = enemyTagList;
        this.onHItAction = onHItAction;
    }
    //开启武器碰撞体触发器
    public void StartHit()
    {
        hitCollider.enabled = true;
    }
    //关闭武器碰撞体触发器
    public void StopHit()
    {
        hitCollider.enabled = false;
        enemyHurtList.Clear();
    }

    private void OnTriggerStay(Collider other)
    {
        //检测攻击对象标签是否为敌人
        if(enemyTagList.Contains(other.tag))
        {
            IHurt enemy = other.GetComponent<IHurt>();
            //如果本次攻击已经攻击过该敌人，则不再产生二次攻击
            if(enemy != null && !enemyHurtList.Contains(enemy))
            {
                //记录攻击到的敌人
                enemyHurtList.Add(enemy);
                #region 让敌人受到伤害
                //触发命中事件
                onHItAction.Invoke(enemy);
                #endregion
            }
            else if(enemy == null)
            {
                Debug.LogError($"该受击对象{other.name}不包含IHurt接口");
            }
        }
    }
}

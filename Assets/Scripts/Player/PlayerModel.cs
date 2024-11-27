using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//急停动画移动脚
public enum MoveFoot
{
    Right,Left
}
/// <summary>
/// 玩家模型
/// </summary>
public class PlayerModel : MonoBehaviour,IHurt
{
    //动画控制器
    public Animator animator;
    //当前状态
    public E_PlayerState currentState;
    //角色控制器
    public CharacterController characterController;
    //移动脚枚举
    public MoveFoot moveFoot = MoveFoot.Right;
    //重力
    //public float gravity = -9.8f;
    //技能配置文件
    public SkillConfig skillConfig;
    //大招开始镜头
    public GameObject bigSkillStartShot;
    //大招镜头
    public GameObject bigSkillShot;
    //武器列表
    public WeaponController[] weapons;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    //开启武器碰撞体触发器
    public void StartHit(int weaponIndex)
    {
        weapons[weaponIndex].StartHit();
    }
    //关闭武器碰撞体触发器
    public void StopHit(int weaponIndex)
    {
        weapons[weaponIndex].StopHit();
    }

    //初始化武器
    public void Init(List<string> enemyTagList)
    {
        //将所有武器载入武器数组中
        for(int i = 0; i < weapons.Length; i++)
        {
            weapons[i].Init(enemyTagList, OnHit);
        }
    }

    /// <summary>
    /// 命中事件
    /// </summary>
    private void OnHit(IHurt enemy)
    {
        Debug.Log(((Component)enemy).name);
    }

    //迈出左脚
    public void SetOutLeftFoot()
    {
        moveFoot = MoveFoot.Left;
    }    
    //迈出右脚
    public void SetOutRightFoot()
    {
        moveFoot = MoveFoot.Right;
    }
    private void OnDisable()
    {
        //重置普通攻击段数
        skillConfig.currentNormalAttackIndex = 1;
    }
}

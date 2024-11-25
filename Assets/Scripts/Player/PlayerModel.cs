using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//急停动画移动脚
public enum MoveFoot
{
    Right,Left
}
public class PlayerModel : MonoBehaviour
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家移动状态
/// </summary>
public class PlayerRunState : PlayerStateBase
{
    private Camera mainCamera;
    public override void Enter()
    {
        base.Enter();
        mainCamera = Camera.main;
        #region 判断迈出左右脚
        switch (playerModel.currentState)
        {
            case E_PlayerState.Walk:
                switch (playerModel.moveFoot)
                {
                    case MoveFoot.Right:
                        playerController.PlayAnimation("Walk", 0.125f, 0);
                        playerModel.moveFoot = MoveFoot.Left;
                        break;
                    case MoveFoot.Left:
                        playerController.PlayAnimation("Walk", 0.125f, 0.6f);
                        playerModel.moveFoot = MoveFoot.Right;
                        break;
                }
                break;
            case E_PlayerState.Run:
                switch (playerModel.moveFoot)
                {
                    case MoveFoot.Right:
                        playerController.PlayAnimation("Run", 0.125f, 0);
                        playerModel.moveFoot = MoveFoot.Left;
                        break;
                    case MoveFoot.Left:
                        playerController.PlayAnimation("Run", 0.125f, 0.5f);
                        playerModel.moveFoot = MoveFoot.Right;
                        break;
                }
                break;
        }
        #endregion
    }

    public override void Update()
    {
        base.Update();
        #region 检测大招
        if (playerController.inputSystem.Player.BigSkill.triggered)
        {
            //进入大招状态
            playerController.SwitchState(E_PlayerState.BigSkill_Start);
            return;
        }
        #endregion

        #region 检测攻击
        if (playerController.inputSystem.Player.Fire.triggered)
        {
            //切换至普通攻击状态
            playerController.SwitchState(E_PlayerState.NormalAttack);
            return;
        }
        #endregion

        #region 检测闪避
        if (playerController.inputSystem.Player.Evade.triggered)
        {
            //切换至闪避状态
            playerController.SwitchState(E_PlayerState.Evade_Front);
            return;
        }
        #endregion

        #region 检测并处理移动方向
        else
        {
            Vector3 inputMoveVec3 = new Vector3(playerController.inputMoveVec2.x, 0, playerController.inputMoveVec2.y);
            //获取相机的旋转轴Y
            float cameraAxisY = mainCamera.transform.rotation.eulerAngles.y;
            //四元数乘向量
            Vector3 targetDic = Quaternion.Euler(0, cameraAxisY, 0) * inputMoveVec3;
            Quaternion targetQua = Quaternion.LookRotation(targetDic);
            //计算旋转角度，若在区间内切换至转身状态，否则慢转人物模型
            float angles = Mathf.Abs(targetQua.eulerAngles.y - playerModel.transform.rotation.eulerAngles.y);
            if (angles >= 145f && angles <= 215f)
            {
                //切换至转身状态
                playerController.SwitchState(E_PlayerState.TurnBack);
                return;
            }
            else
            {
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, targetQua, Time.deltaTime * playerController.rotationSpeed);
            }
        }
        #endregion

        #region 检测待机
        if (playerController.inputMoveVec2 == Vector2.zero)
        {
            playerController.SwitchState(E_PlayerState.RunEnd);
            return;
        }
        #endregion



        #region 检测慢跑升级
        if (playerModel.currentState == E_PlayerState.Walk && statePlayingTime > 2f)
        {
            //切换到疾跑状态
            playerController.SwitchState(E_PlayerState.Run);
            return;
        }
        #endregion
    }
}

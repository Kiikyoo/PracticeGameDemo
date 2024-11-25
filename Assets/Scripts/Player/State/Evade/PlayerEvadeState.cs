using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvadeState : PlayerStateBase
{
    private Camera mainCamera;
    public override void Enter()
    {
        base.Enter();
        mainCamera = Camera.main;
        #region 没有移动输入下的闪避
        //如果没有移动输入
        if (playerController.inputMoveVec2 == Vector2.zero)
        {
            #region 判断前后闪避
            switch (playerModel.currentState)
            {
                case E_PlayerState.Evade_Front:
                    playerController.PlayAnimation("Evade_Front");
                    break;
                case E_PlayerState.Evade_Back:
                    playerController.PlayAnimation("Evade_Back");
                    break;
            }
            #endregion
            return;
        }
        #endregion

        #region 有移动输入下的闪避，检测并处理移动方向
        else
        {
            //获取移动方向向量
            Vector3 inputMoveVec3 = new Vector3(playerController.inputMoveVec2.x, 0, playerController.inputMoveVec2.y);
            //获取相机的旋转轴Y
            float cameraAxisY = mainCamera.transform.rotation.eulerAngles.y;
            //四元数乘向量
            Vector3 targetDic = Quaternion.Euler(0, cameraAxisY, 0) * inputMoveVec3;
            Quaternion targetQua = Quaternion.LookRotation(targetDic);
            //计算旋转角度，若在区间内则向后闪避，否则向前闪避
            float angles = Mathf.Abs(targetQua.eulerAngles.y - playerModel.transform.rotation.eulerAngles.y);
            if (angles > 145f && angles < 215f )
            {
                //切换至向后闪避状态
                playerController.PlayAnimation("Evade_Back");
                return;
            }
            else
            {
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, targetQua, Time.deltaTime * 1000f);
                playerController.PlayAnimation("Evade_Front");
                return;
            }
        }
        #endregion
    }

    public override void Update()
    {
        base.Update();
        #region 动画是否播放结束,是则转为闪避结束状态
        if (IsAnimationEnd())
        {
            switch (playerModel.currentState)
            {
                case E_PlayerState.Evade_Front:
                    if(playerController.inputSystem.Player.Evade.IsPressed())
                    {
                        playerController.SwitchState(E_PlayerState.Run);
                        return;
                    }
                    else
                    {
                        playerController.SwitchState(E_PlayerState.Evade_Front_End);
                        return;
                    }
                case E_PlayerState.Evade_Back:
                    playerController.SwitchState(E_PlayerState.Evade_Back_End);
                    break;
            }
            return;
        }
        #endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerStateBase
{
    private Camera mainCamera;
    public override void Enter()
    {
        base.Enter();
        mainCamera = Camera.main;
        #region �ж��������ҽ�
        switch (playerModel.moveFoot)
        {
            case MoveFoot.Right:
                playerModel.moveFoot = MoveFoot.Left;
                playerController.PlayAnimation("Run",0.25f, 0);
                break;             
            case MoveFoot.Left:
                playerController.PlayAnimation("Run",0.25f,0.5f);
                playerModel.moveFoot = MoveFoot.Right;
                break; 
        }
        #endregion
    }

    public override void Update()
    {
        base.Update();

        #region ��⹥��
        if (playerController.inputSystem.Player.Fire.triggered)
        {
            //�л�����ͨ����״̬
            playerController.SwitchState(PlayerState.NormalAttack);
        }
        #endregion

        #region �������
        if (playerController.inputSystem.Player.Evade.IsPressed())
        {
            //�л�������״̬
            playerController.SwitchState(PlayerState.Evade_Front);
        }
        #endregion

        #region ������
        if (playerController.inputMoveVec2 == Vector2.zero)
        {
            playerController.SwitchState(PlayerState.RunEnd);
        }
        #endregion

        #region ��Ⲣ�����ƶ�����
        else
        {
            
            Vector3 inputMoveVec3 = new Vector3(playerController.inputMoveVec2.x, 0, playerController.inputMoveVec2.y);
            //��ȡ�������ת��Y
            float cameraAxisY = mainCamera.transform.rotation.eulerAngles.y;
            //��Ԫ��������
            Vector3 targetDic = Quaternion.Euler(0, cameraAxisY, 0) * inputMoveVec3;
            Quaternion targetQua = Quaternion.LookRotation(targetDic);
            //������ת�Ƕȣ������������л���ת��״̬��������ת����ģ��
            float angles = Mathf.Abs(targetQua.eulerAngles.y - playerModel.transform.rotation.eulerAngles.y);
            if(angles > 145f && angles < 215f && playerModel.State == PlayerState.Run)
            {
                //�л���ת��״̬
                playerController.SwitchState(PlayerState.TurnBack);
            }
            else
            {
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, targetQua, Time.deltaTime * playerController.rotationSpeed);
            }
        }
        #endregion


    }

}

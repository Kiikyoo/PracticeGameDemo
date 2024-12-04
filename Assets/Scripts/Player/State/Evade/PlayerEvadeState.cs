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
        #region û���ƶ������µ�����
        //���û���ƶ�����
        if (playerController.inputMoveVec2 == Vector2.zero)
        {
            #region �ж�ǰ������
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

        #region ���ƶ������µ����ܣ���Ⲣ�����ƶ�����
        else
        {
            //��ȡ�ƶ���������
            Vector3 inputMoveVec3 = new Vector3(playerController.inputMoveVec2.x, 0, playerController.inputMoveVec2.y);
            //��ȡ�������ת��Y
            float cameraAxisY = mainCamera.transform.rotation.eulerAngles.y;
            //��Ԫ��������
            Vector3 targetDic = Quaternion.Euler(0, cameraAxisY, 0) * inputMoveVec3;
            Quaternion targetQua = Quaternion.LookRotation(targetDic);
            //������ת�Ƕȣ�������������������ܣ�������ǰ����
            float angles = Mathf.Abs(targetQua.eulerAngles.y - playerModel.transform.rotation.eulerAngles.y);
            if (angles > 145f && angles < 215f )
            {
                //�л����������״̬
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

        #region ������ܹ���
        if (playerController.inputSystem.Player.Fire.triggered && stateInfo.normalizedTime >= 0.3f)
        {
            //�л������ܹ���״̬
            playerController.SwitchState(E_PlayerState.EvadeAttack);
            return;
        }
        #endregion

        #region �����Ƿ񲥷Ž���,����תΪ���ܽ���״̬
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

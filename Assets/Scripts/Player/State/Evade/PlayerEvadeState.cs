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
        #region ������
        //���û���ƶ�����
        if (playerController.inputMoveVec2 == Vector2.zero)
        {
            #region �ж�ǰ������
            switch (playerModel.State)
            {
                case E_PlayerState.Run:
                    playerController.PlayAnimation("Evade_Front");
                    break;
                case E_PlayerState.Idle:
                case E_PlayerState.RunEnd:
                case E_PlayerState.NormalAttackEnd:
                case E_PlayerState.BigSkillEnd:
                    playerController.PlayAnimation("Evade_Back");
                    break;

            }
            #endregion
            return;
        }
        #endregion

        #region ��Ⲣ�����ƶ�����
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
        #region �����Ƿ񲥷Ž���,����תΪ���ܽ���״̬
        if (IsAnimationEnd())
        {
            playerController.SwitchState(E_PlayerState.EvadeEnd);
            return;
        }
        #endregion
    }
}

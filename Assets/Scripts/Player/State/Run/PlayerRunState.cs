using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����ƶ�״̬
/// </summary>
public class PlayerRunState : PlayerStateBase
{
    private Camera mainCamera;
    public override void Enter()
    {
        base.Enter();
        mainCamera = Camera.main;
        #region �ж��������ҽ�
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
        #region ������
        if (playerController.inputSystem.Player.BigSkill.triggered)
        {
            //�������״̬
            playerController.SwitchState(E_PlayerState.BigSkill_Start);
            return;
        }
        #endregion

        #region ��⹥��
        if (playerController.inputSystem.Player.Fire.triggered)
        {
            //�л�����ͨ����״̬
            playerController.SwitchState(E_PlayerState.NormalAttack);
            return;
        }
        #endregion

        #region �������
        if (playerController.inputSystem.Player.Evade.triggered)
        {
            //�л�������״̬
            playerController.SwitchState(E_PlayerState.Evade_Front);
            return;
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
            if (angles >= 145f && angles <= 215f)
            {
                //�л���ת��״̬
                playerController.SwitchState(E_PlayerState.TurnBack);
                return;
            }
            else
            {
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, targetQua, Time.deltaTime * playerController.rotationSpeed);
            }
        }
        #endregion

        #region ������
        if (playerController.inputMoveVec2 == Vector2.zero)
        {
            playerController.SwitchState(E_PlayerState.RunEnd);
            return;
        }
        #endregion



        #region �����������
        if (playerModel.currentState == E_PlayerState.Walk && statePlayingTime > 2f)
        {
            //�л�������״̬
            playerController.SwitchState(E_PlayerState.Run);
            return;
        }
        #endregion
    }
}

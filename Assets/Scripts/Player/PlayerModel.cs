using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���״̬ö��
/// </summary>
public enum PlayerState
{
    Idle, Run, RunEnd, TurnBack, Evade_Front, Evade_Back, EvadeEnd, NormalAttack, NormalAttackEnd
}
//��ͣ�����ƶ���
public enum MoveFoot
{
    Right,Left
}
public class PlayerModel : MonoBehaviour
{
    //����������
    public Animator animator;
    //���״̬
    public PlayerState State;
    //��ɫ������
    public CharacterController characterController;
    //�ƶ���ö��
    public MoveFoot moveFoot = MoveFoot.Right;
    //����
    public float gravity = -9.8f;
    //���������ļ�
    public SkillConfig skillConfig;

    //�������
    public void SetOutLeftFoot()
    {
        moveFoot = MoveFoot.Left;
    }    
    //�����ҽ�
    public void SetOutRightFoot()
    {
        moveFoot = MoveFoot.Right;
    }
}

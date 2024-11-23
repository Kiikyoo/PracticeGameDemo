using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���״̬ö��
/// </summary>

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
    public E_PlayerState State;
    //��ɫ������
    public CharacterController characterController;
    //�ƶ���ö��
    public MoveFoot moveFoot = MoveFoot.Right;
    //����
    //public float gravity = -9.8f;
    //���������ļ�
    public SkillConfig skillConfig;
    //���п�ʼ��ͷ
    public GameObject bigSkillStartShot;
    //���о�ͷ
    public GameObject bigSkillShot;

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

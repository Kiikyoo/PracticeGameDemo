using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ͣ�����ƶ���
public enum MoveFoot
{
    Right,Left
}
/// <summary>
/// ���ģ��
/// </summary>
public class PlayerModel : MonoBehaviour,IHurt
{
    //����������
    public Animator animator;
    //��ǰ״̬
    public E_PlayerState currentState;
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
    //�����б�
    public WeaponController[] weapons;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    //����������ײ�崥����
    public void StartHit(int weaponIndex)
    {
        weapons[weaponIndex].StartHit();
    }
    //�ر�������ײ�崥����
    public void StopHit(int weaponIndex)
    {
        weapons[weaponIndex].StopHit();
    }

    //��ʼ������
    public void Init(List<string> enemyTagList)
    {
        //������������������������
        for(int i = 0; i < weapons.Length; i++)
        {
            weapons[i].Init(enemyTagList, OnHit);
        }
    }

    /// <summary>
    /// �����¼�
    /// </summary>
    private void OnHit(IHurt enemy)
    {
        Debug.Log(((Component)enemy).name);
    }

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
    private void OnDisable()
    {
        //������ͨ��������
        skillConfig.currentNormalAttackIndex = 1;
    }
}

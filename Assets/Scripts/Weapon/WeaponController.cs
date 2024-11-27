using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����������
/// </summary>
public class WeaponController : MonoBehaviour
{
    //������ײ��
    public Collider hitCollider;
    //���˱�ǩ�б�
    public List<string> enemyTagList;
    //���ι���ʱ�����ܻ��б�
    public List<IHurt> enemyHurtList = new List<IHurt>();
    //�����¼�
    private Action<IHurt> onHItAction;
    public void Init(List<string> enemyTagList, Action<IHurt> onHItAction)
    {
        StopHit();
        this.enemyTagList = enemyTagList;
        this.onHItAction = onHItAction;
    }
    //����������ײ�崥����
    public void StartHit()
    {
        hitCollider.enabled = true;
    }
    //�ر�������ײ�崥����
    public void StopHit()
    {
        hitCollider.enabled = false;
        enemyHurtList.Clear();
    }

    private void OnTriggerStay(Collider other)
    {
        //��⹥�������ǩ�Ƿ�Ϊ����
        if(enemyTagList.Contains(other.tag))
        {
            IHurt enemy = other.GetComponent<IHurt>();
            //������ι����Ѿ��������õ��ˣ����ٲ������ι���
            if(enemy != null && !enemyHurtList.Contains(enemy))
            {
                //��¼�������ĵ���
                enemyHurtList.Add(enemy);
                #region �õ����ܵ��˺�
                //���������¼�
                onHItAction.Invoke(enemy);
                #endregion
            }
            else if(enemy == null)
            {
                Debug.LogError($"���ܻ�����{other.name}������IHurt�ӿ�");
            }
        }
    }
}

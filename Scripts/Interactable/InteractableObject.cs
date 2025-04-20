using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractableObject : MonoBehaviour//���п��Խ��������嶼���Թ��ظýű�
{
    private NavMeshAgent playerAgent;
    private bool haveInteracted = false;//�Ƿ񽻻���
    public void OnClick(NavMeshAgent playerAgent)//����������Ҫ����һ������
    {
        //TODD
        //InteractableObject��ʾ�������壬����NPCObject,PickableObject,��NPC�ɽ��жԻ�����Pickable�ɽ��м���Ľ�������
        //����OnClick��⵽�������λ��
        //Ȼ�����Player�����λ�ã�GoHere
        //Ȼ����н���Interact
        this.playerAgent = playerAgent;
        playerAgent.stoppingDistance = 2;//��ʾ�ھ���ɽ�����������ʱֹͣ�˶����ܼ��ٶ�ֹͣӰ��

        playerAgent.SetDestination(transform.position);//��PlayerAgent��Ŀ��λ������Ϊ��ǰ��Ϸ�����λ��
        //PlayerAgent:��һ��NavMeshAgent���͵ı�����ͨ�����ڿ�����Ϸ�����ڵ�������NavMesh�ϵ��ƶ�
        //SetDestination��Vector3 target):��NavMeshAgent��ķ�������������AI��Ŀ��λ��
        //transform.position:�ǵ�ǰ��Ϸ�����λ�ã��������꣩
        haveInteracted = false;//ÿ�ε����֮��Ҫ���ý���λ��
        //Interact();


    }
    private void Update()
    {
        if(playerAgent != null&&haveInteracted==false&&playerAgent.pathPending==false)//pathPendingΪtrueʱ����ʾ·�����ڼ��㣬pathPendingΪfalseʱ����ʾ·���������
        {
            if(playerAgent.remainingDistance <= 2)//remainingDistance��ʾʣ����پ��뵽��Ŀ��λ��
            {
                Interact();//�������Ŀ��λ�ã����Դ���Interact
                haveInteracted=true;//�������Ͳ��ü����ˣ���Լ����
            }
        }

    }
    protected virtual void Interact()
    {
        print("Interactiong with Interactable Object.");
        //NPC����Ҫ���collider

    }
}

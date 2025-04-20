using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractableObject : MonoBehaviour//所有可以交互的物体都可以挂载该脚本
{
    private NavMeshAgent playerAgent;
    private bool haveInteracted = false;//是否交互过
    public void OnClick(NavMeshAgent playerAgent)//代表它自身要进行一个交互
    {
        //TODD
        //InteractableObject表示交互物体，包括NPCObject,PickableObject,对NPC可进行对话，对Pickable可进行捡起的交互动作
        //首先OnClick检测到鼠标点击的位置
        //然后控制Player走向该位置，GoHere
        //然后进行交互Interact
        this.playerAgent = playerAgent;
        playerAgent.stoppingDistance = 2;//表示在距离可交互物体两米时停止运动，受加速度停止影响

        playerAgent.SetDestination(transform.position);//把PlayerAgent的目标位置设置为当前游戏对象的位置
        //PlayerAgent:是一个NavMeshAgent类型的变量，通常用于控制游戏对象在导航网格NavMesh上的移动
        //SetDestination（Vector3 target):是NavMeshAgent类的方法，用于设置AI的目标位置
        //transform.position:是当前游戏对象的位置（世界坐标）
        haveInteracted = false;//每次点击完之后，要重置交互位置
        //Interact();


    }
    private void Update()
    {
        if(playerAgent != null&&haveInteracted==false&&playerAgent.pathPending==false)//pathPending为true时，表示路径正在计算，pathPending为false时，表示路径计算完成
        {
            if(playerAgent.remainingDistance <= 2)//remainingDistance表示剩余多少距离到达目标位置
            {
                Interact();//如果到达目标位置，可以触发Interact
                haveInteracted=true;//交互过就不用计算了，节约性能
            }
        }

    }
    protected virtual void Interact()
    {
        print("Interactiong with Interactable Object.");
        //NPC身上要添加collider

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickableObject : InteractableObject
{
    public ItemSO itemSO;//��ʾ������һ��Item����������ʱ��֪������Item
    protected override void Interact()
    {
        //print("Interacting with pickableobject!!");
        //��������Ȱ������٣�Ȼ���ڱ��������һ��
        Destroy(this.gameObject);
        InventoryManager.Instance.AddItem(itemSO);

    }
}

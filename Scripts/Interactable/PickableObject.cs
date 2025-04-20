using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickableObject : InteractableObject
{
    public ItemSO itemSO;//表示它是哪一个Item，捡起物体时可知道它的Item
    protected override void Interact()
    {
        //print("Interacting with pickableobject!!");
        //捡起就是先把他销毁，然后在背包里面放一个
        Destroy(this.gameObject);
        InventoryManager.Instance.AddItem(itemSO);

    }
}

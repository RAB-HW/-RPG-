using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notebook1_Ainavigation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //让NPC变成障碍物的两种方法
        //1)NavMesh Modfier Volume:右键AI，添加这个，一般是用于控制一大片障碍物
        //2)Nav Mesh Obstacle:add Component中，一般用于添加单个障碍物，要勾选Carve，跟随物体动态移动
        //3）单独移除某个物体：NavMeshModifier，add Component中，把Mode设为Remove object
        //4）某给物体身上不需要生成导航，比如房子，NavMeshModifier，add Component中把Area Type设置为Not Walkable

        //控制主角移动
        //Nav Mesh Agent:add Component中
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class notebook2_射线检测 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Physics.Raycast(Ray ray,out RaycastHit hit)
        //参数一：射线对象，包含起点和方向
        //参数二：输出参数，存储射线检测的结果，如碰撞点，碰撞物体
        //返回值：true：射线与场景中的碰撞体相交，false：射线未与任何碰撞体相交

        //Ray:是一个结构体，表示一条射线，包含两个属性：
        //1）origin：射线的起点（Vector3）
        //2）direction:射线的方向（Vector3，通常需要归一化）

        //out RaycastHit hit:是一个结构体，存储射线检测的结果
         //        常用属性：
         //point：射线与碰撞体的交点（Vector3 类型）。
         //normal：交点处的法线方向（Vector3 类型）。
         //distance：射线起点到交点的距离（float 类型）。
         //collider：射线击中的碰撞体（Collider 类型）。
         //transform：射线击中的物体的 Transform 组件。
    }

    // Update is called once per frame
    void Update()
    {
        #region 一个简单的射线检测示例
        // 创建一条从摄像机发射到鼠标位置的射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 检测射线是否与场景中的碰撞体相交
        if (Physics.Raycast(ray, out hit))
        {
            // 输出碰撞点的位置
            Debug.Log("Hit point: " + hit.point);

            // 输出碰撞物体的名称
            Debug.Log("Hit object: " + hit.collider.gameObject.name);

            // 在碰撞点绘制一个红色小球
            Debug.DrawLine(ray.origin, hit.point, UnityEngine.Color.red);
        }


        #endregion
        #region 鼠标点击检测
        if (Input.GetMouseButtonDown(0)) // 左键点击
        {
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit2;
            if (Physics.Raycast(ray2, out hit2))
            {
                Debug.Log("Clicked on: " + hit2.collider.gameObject.name);
            }
        }
        #endregion
    }
    #region 子弹射击检测
    void Shoot()
    {
        Ray ray3 = new Ray(transform.position, transform.forward);
        RaycastHit hit3;
        if (Physics.Raycast(ray3, out hit3, 100f)) // 检测100米内的碰撞
        {
            Debug.Log("Hit target: " + hit3.collider.gameObject.name);
        }
    }
    #endregion
}

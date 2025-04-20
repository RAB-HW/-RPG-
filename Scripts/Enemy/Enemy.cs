using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class Enemy : MonoBehaviour
{
    //正常状态-战斗状态
    //1.移动
    //2.休息
    //状态码
    public enum EnemyState
    {
        NormalState,
        FightingState,
        MovingState,//MovingState,RestingState是属于NormalState的状态
        RestingState,
    }

    private EnemyState state=EnemyState.NormalState;//给一个正常状态
    private EnemyState childState=EnemyState.RestingState;//把子状态设置为休息状态，也就是默认状态
    private NavMeshAgent enemyAgent;

    public float restTime = 2;
    private float restTimer = 0;

    public int HP = 100;

    public int exp = 20;//杀死一个敌人可以获得20经验值
    private void Start()
    {
        enemyAgent= GetComponent<NavMeshAgent>();
    }

    #region 被抛弃的携程
    //IEnumerator NormalState()
    //{
    //    //在移动和休息之间进行切换
    //    while (true)
    //    {
    //        Vector3 randomPosition = FindRandomPosition();
    //        enemyAgent.SetDestination(randomPosition);//移动到目标位置
    //        while(enemyAgent.)

    //    }
    //}
    #endregion
    private void Update()
    {
        if(state==EnemyState.NormalState)//表示当前敌人处于正常状态
        {
            if(childState == EnemyState.RestingState)
            {
                restTimer += Time.deltaTime;//计时器累加，记录敌人已经休息了多久
                if(restTimer > restTime)//如果休息时间超过设定的restTime
                {
                    Vector3 randomPosition=FindRandomPosition();//设置一个随机目标点
                    enemyAgent.SetDestination(randomPosition);//让敌人朝该点移动
                    childState= EnemyState.MovingState; 
                }
            }else if(childState == EnemyState.MovingState)
            {
                if (enemyAgent.remainingDistance <= 0)//判断敌人是否到达目标位置
                {
                    restTimer = 0;//重置计时器
                    childState = EnemyState.RestingState;//切换回休息状态
                }
                
            }
        }

        ////测试代码
        //if(Input.GetKeyUp(KeyCode.Space))
        //{
        //    TakeDamage(30);
        //}
    }
    Vector3 FindRandomPosition()
    {
        Vector3 randomDir=new Vector3(Random.Range(-1,1f),0,Random.Range(-1,1f));//在xz轴随机生成一个值
        return transform.position+randomDir.normalized*Random.Range(2, 5);

    }
    public void TakeDamage(int damage)//受到伤害掉血量
    {

        HP -= damage;
        if(HP <= 0)
        {
            Die();
        }
    }
    //处理敌人死亡的方法
    private void Die()
    {
        //防止生成的物体跟当前的物体发生碰撞
        GetComponent<Collider>().enabled = false;//禁用
        int count = 4;
        //int count = Random.Range(0, 4);//随机爆0,1,2,3个装备，0，4，不包含最大值
        for (int i = 0; i < count; i++)//循环生成count个随机物品
        {
            SpawnPickableItem();
        }
        //死亡之前触发事件
        EventManager.EnemyDied(this);
        Destroy(this.gameObject);//血量不足，死亡，销毁这个游戏物体
    }
    private void SpawnPickableItem()
    {
        //从物品数据库中随机获取一个物品（ItemSO）
        ItemSO item = ItemDBManager.Instance.GetRandomItem();
        //在敌人死亡位置生成该物品的GameObject（Prefab)
        GameObject go = GameObject.Instantiate(item.prefab, transform.position, Quaternion.identity);//Quaternion.identity表示无旋转，默认朝向
        #region Instantiate用法
        //GameObject.Instantiate 是用于 动态创建（克隆）游戏对象（GameObject） 的核心方法，
        //常用于实例化预制体
        //最简形式（生成在原点，无旋转）
        //GameObject newObj=Instantiate(originalPrefab);
        //完整参数（指定位置和旋转）
        //GameObject newObj=Instantiate(originalPrefab,position,rotation);
        //可选：指定父物体
        //GameObject newObj=Instantiate(originalPrefab,position,rotation,parentTransform);

        #endregion
        //由于敌人被杀死后，掉落的标枪在一秒后会消失，无法将其捡起，
        //因此给能够交互的标枪和射出去的物体标枪设置不同的tag
        go.tag = Tag.INTERACTABLE;
        //由于敌人被杀死后，掉落的镰刀位置不在敌人原位置，
        //是因为镰刀动画特效导致的，所以在此地禁用镰刀动画特效
        Animator anim = go.GetComponent<Animator>();//得到它的动画特效
        if (anim != null)//判断这个物体是不是武器，有anim才是武器，没有就不是
        {
            anim.enabled = false;
        }

        //P36

        PickableObject po = go.AddComponent<PickableObject>();//当每次实例化出来一个可捡起的物体时，给它挂载一个PickableObject脚本,并把这个组件作为一个对象返回
        po.itemSO = item;//再把前面随机生成一个的item索引放在这个组建的itemSO上
        Collider collider= go.GetComponent<Collider>();
        if(collider != null)
        {
            collider.enabled = true;//把weapon中的Mesh Collider启用
            collider.isTrigger = false;//把Mesh Collider中的isTrigger禁用
        }
        Rigidbody rgd = go.GetComponent<Rigidbody>();
        if (rgd != null)
        {
            rgd.isKinematic = false;//这样就可以跟Weapon发生碰撞
            rgd.useGravity = true;//并且碰撞受重力影响
        }
    }
}

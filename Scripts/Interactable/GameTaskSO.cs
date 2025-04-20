using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
//任务系统
//1.等待任务Waiting
//2.执行任务中Executing
//3.完成任务Completed
//4.结束任务End
public enum GameTaskState
{
    Waiting,
    Executing,
    Completed,
    End
}
[CreateAssetMenu()]
public class GameTaskSO :ScriptableObject
{ 
   public GameTaskState State;
    public string[] diague;
    public ItemSO startReward;
    public ItemSO endReward;

    public int enemyCountNeed =3;
    public int currentEnemyCount =0;
    //public GameTask(string[] diague)
    //{
    //    this.diague = diague;
    //}

    public void Start()
    {
        currentEnemyCount = 0;
        State = GameTaskState.Executing;
        EventManager.OnEnemyDied += OnEnemyDied;
        
    }

    //注册敌人死亡的事件中心
    private void OnEnemyDied(Enemy enemy)
    {
        if (State == GameTaskState.Completed)
        {
            return;
        }
        currentEnemyCount++;
        if(currentEnemyCount>=enemyCountNeed)
        {
            State = GameTaskState.Completed;
            MessageUI.instance.Show("任务已完成！请前去领取奖励！");

        }

    }

    public void End()
    {
        State=GameTaskState.End;
        EventManager.OnEnemyDied-=OnEnemyDied;//在事件结束后取消注册
        
    }
}

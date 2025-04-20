using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
//����ϵͳ
//1.�ȴ�����Waiting
//2.ִ��������Executing
//3.�������Completed
//4.��������End
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

    //ע������������¼�����
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
            MessageUI.instance.Show("��������ɣ���ǰȥ��ȡ������");

        }

    }

    public void End()
    {
        State=GameTaskState.End;
        EventManager.OnEnemyDied-=OnEnemyDied;//���¼�������ȡ��ע��
        
    }
}

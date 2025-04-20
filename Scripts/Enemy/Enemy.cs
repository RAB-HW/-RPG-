using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class Enemy : MonoBehaviour
{
    //����״̬-ս��״̬
    //1.�ƶ�
    //2.��Ϣ
    //״̬��
    public enum EnemyState
    {
        NormalState,
        FightingState,
        MovingState,//MovingState,RestingState������NormalState��״̬
        RestingState,
    }

    private EnemyState state=EnemyState.NormalState;//��һ������״̬
    private EnemyState childState=EnemyState.RestingState;//����״̬����Ϊ��Ϣ״̬��Ҳ����Ĭ��״̬
    private NavMeshAgent enemyAgent;

    public float restTime = 2;
    private float restTimer = 0;

    public int HP = 100;

    public int exp = 20;//ɱ��һ�����˿��Ի��20����ֵ
    private void Start()
    {
        enemyAgent= GetComponent<NavMeshAgent>();
    }

    #region ��������Я��
    //IEnumerator NormalState()
    //{
    //    //���ƶ�����Ϣ֮������л�
    //    while (true)
    //    {
    //        Vector3 randomPosition = FindRandomPosition();
    //        enemyAgent.SetDestination(randomPosition);//�ƶ���Ŀ��λ��
    //        while(enemyAgent.)

    //    }
    //}
    #endregion
    private void Update()
    {
        if(state==EnemyState.NormalState)//��ʾ��ǰ���˴�������״̬
        {
            if(childState == EnemyState.RestingState)
            {
                restTimer += Time.deltaTime;//��ʱ���ۼӣ���¼�����Ѿ���Ϣ�˶��
                if(restTimer > restTime)//�����Ϣʱ�䳬���趨��restTime
                {
                    Vector3 randomPosition=FindRandomPosition();//����һ�����Ŀ���
                    enemyAgent.SetDestination(randomPosition);//�õ��˳��õ��ƶ�
                    childState= EnemyState.MovingState; 
                }
            }else if(childState == EnemyState.MovingState)
            {
                if (enemyAgent.remainingDistance <= 0)//�жϵ����Ƿ񵽴�Ŀ��λ��
                {
                    restTimer = 0;//���ü�ʱ��
                    childState = EnemyState.RestingState;//�л�����Ϣ״̬
                }
                
            }
        }

        ////���Դ���
        //if(Input.GetKeyUp(KeyCode.Space))
        //{
        //    TakeDamage(30);
        //}
    }
    Vector3 FindRandomPosition()
    {
        Vector3 randomDir=new Vector3(Random.Range(-1,1f),0,Random.Range(-1,1f));//��xz���������һ��ֵ
        return transform.position+randomDir.normalized*Random.Range(2, 5);

    }
    public void TakeDamage(int damage)//�ܵ��˺���Ѫ��
    {

        HP -= damage;
        if(HP <= 0)
        {
            Die();
        }
    }
    //������������ķ���
    private void Die()
    {
        //��ֹ���ɵ��������ǰ�����巢����ײ
        GetComponent<Collider>().enabled = false;//����
        int count = 4;
        //int count = Random.Range(0, 4);//�����0,1,2,3��װ����0��4�����������ֵ
        for (int i = 0; i < count; i++)//ѭ������count�������Ʒ
        {
            SpawnPickableItem();
        }
        //����֮ǰ�����¼�
        EventManager.EnemyDied(this);
        Destroy(this.gameObject);//Ѫ�����㣬���������������Ϸ����
    }
    private void SpawnPickableItem()
    {
        //����Ʒ���ݿ��������ȡһ����Ʒ��ItemSO��
        ItemSO item = ItemDBManager.Instance.GetRandomItem();
        //�ڵ�������λ�����ɸ���Ʒ��GameObject��Prefab)
        GameObject go = GameObject.Instantiate(item.prefab, transform.position, Quaternion.identity);//Quaternion.identity��ʾ����ת��Ĭ�ϳ���
        #region Instantiate�÷�
        //GameObject.Instantiate ������ ��̬��������¡����Ϸ����GameObject�� �ĺ��ķ�����
        //������ʵ����Ԥ����
        //�����ʽ��������ԭ�㣬����ת��
        //GameObject newObj=Instantiate(originalPrefab);
        //����������ָ��λ�ú���ת��
        //GameObject newObj=Instantiate(originalPrefab,position,rotation);
        //��ѡ��ָ��������
        //GameObject newObj=Instantiate(originalPrefab,position,rotation,parentTransform);

        #endregion
        //���ڵ��˱�ɱ���󣬵���ı�ǹ��һ������ʧ���޷��������
        //��˸��ܹ������ı�ǹ�����ȥ�������ǹ���ò�ͬ��tag
        go.tag = Tag.INTERACTABLE;
        //���ڵ��˱�ɱ���󣬵��������λ�ò��ڵ���ԭλ�ã�
        //����Ϊ����������Ч���µģ������ڴ˵ؽ�������������Ч
        Animator anim = go.GetComponent<Animator>();//�õ����Ķ�����Ч
        if (anim != null)//�ж���������ǲ�����������anim����������û�оͲ���
        {
            anim.enabled = false;
        }

        //P36

        PickableObject po = go.AddComponent<PickableObject>();//��ÿ��ʵ��������һ���ɼ��������ʱ����������һ��PickableObject�ű�,������������Ϊһ�����󷵻�
        po.itemSO = item;//�ٰ�ǰ���������һ����item������������齨��itemSO��
        Collider collider= go.GetComponent<Collider>();
        if(collider != null)
        {
            collider.enabled = true;//��weapon�е�Mesh Collider����
            collider.isTrigger = false;//��Mesh Collider�е�isTrigger����
        }
        Rigidbody rgd = go.GetComponent<Rigidbody>();
        if (rgd != null)
        {
            rgd.isKinematic = false;//�����Ϳ��Ը�Weapon������ײ
            rgd.useGravity = true;//������ײ������Ӱ��
        }
    }
}

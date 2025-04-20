using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour//����ʵ�ֵ��˵�����������
{
    public GameObject enemyPrefab;//���˵�Ԥ������קһ��
    public float spawnTime;//��ǰ��ʱ��
    private float spawnTimer;//���ɵ��˵ļ��ʱ��

    // Start is called before the first frame update
    void Start()
    {
            SpawnEnemy();//����һ������

    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTime <spawnTimer)
        {
            spawnTimer = 0;//���ü�ʱ��
            SpawnEnemy();//����һ������
        }
    }
    void SpawnEnemy()
    {
        //��������λ��ʵ����һ������
        GameObject.Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}

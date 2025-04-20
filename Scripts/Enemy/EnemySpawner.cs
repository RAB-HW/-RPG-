using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour//用于实现敌人的周期性生成
{
    public GameObject enemyPrefab;//敌人的预制体拖拽一下
    public float spawnTime;//当前计时器
    private float spawnTimer;//生成敌人的间隔时间

    // Start is called before the first frame update
    void Start()
    {
            SpawnEnemy();//生成一个敌人

    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTime <spawnTimer)
        {
            spawnTimer = 0;//重置计时器
            SpawnEnemy();//生成一个敌人
        }
    }
    void SpawnEnemy()
    {
        //再生成器位置实例化一个敌人
        GameObject.Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}

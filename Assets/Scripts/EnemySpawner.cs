using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public Enemy[] prefabs;
    private IObjectPool<Enemy>[] enemyPool;
    public SpawnData[] spawnDatas;

    public float spawnTimer = 0f;
    private int level = 0;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        
        enemyPool = new IObjectPool<Enemy>[prefabs.Length];
        for (int i = 0; i < prefabs.Length; ++i)
        {
            var index = i;
            enemyPool[index] = new ObjectPool<Enemy>(
                () => 
                {
                    var enemy = Instantiate(prefabs[index]);
                    enemy.SetManagePool(enemyPool[index]);
                    return enemy;
                },
                Get, Release, Destroy, maxSize: 30);
        }

    }
    private void Update()
    {
        spawnTimer += Time.deltaTime;
        level = Mathf.FloorToInt(GameManager.instance.gameSec/10f);
        if (spawnTimer > spawnDatas[level].spawnTime)
        {
            spawnTimer = 0f;
            SpawnEnemy();
            //enemyPool[level].Get();
        }

    }
    public void SpawnEnemy()
    {
        var enemy = enemyPool[0].Get();
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.Init(spawnDatas[level]);
    }

    public Enemy Get(int index)
    {
        return enemyPool[index].Get();
    }

    public void Get(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }
    public void Release(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }
    public void Destroy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }

}
[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
    public float damage;
}

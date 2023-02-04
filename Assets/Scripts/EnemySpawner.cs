using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public Enemy[] prefabs;
    private IObjectPool<Enemy>[] enemyPool;

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
        level = Mathf.FloorToInt(GameManager.instance.gameTime/10);
        if(spawnTimer>0.2)
        {
            enemyPool[Random.Range(0,prefabs.Length)].Get();
            spawnTimer = 0f;
        }

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

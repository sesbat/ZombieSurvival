using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;
    public IObjectPool<Bullet> pool;

    public void Init(float damage, int per)
    {
        this.damage = damage;
        this.per = per;
    }
    public void SetManagePool(IObjectPool<Bullet>pool)
    {
        this.pool = pool;
    }
}

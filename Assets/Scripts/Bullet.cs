using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public int id;
    public int prefabId;
    public int count;
    public float speed;
    public float damage;
    public int per;
    public IObjectPool<Bullet> pool;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per,Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if (rigid!=null&&per > -1)
        {
            rigid.velocity = dir*15f;
        }
    }
    public void SetManagePool(IObjectPool<Bullet>pool)
    {
        this.pool = pool;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1) 
            return;

        per--;
        if(per == -1)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}

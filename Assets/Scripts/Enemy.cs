using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animatorControllers;
    public Rigidbody2D target;

    private Rigidbody2D enemyRigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private IObjectPool<Enemy> managePool;

    private bool isLive;

    // Start is called before the first frame update
    private void Awake()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!isLive)
            return;

        Vector2 dirVec = (target.position - enemyRigidBody.position);
        Vector2 move = dirVec.normalized * speed * Time.fixedDeltaTime;

        enemyRigidBody.MovePosition(enemyRigidBody.position + move);
        enemyRigidBody.velocity = Vector2.zero;
    }
    private void LateUpdate()
    {
        if (!isLive)
            return;
        spriteRenderer.flipX = target.position.x < 0;
    }
    public void OnEnable()
    {
        target = GameManager.instance.
            player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }
    public void Init(SpawnData data)
    {
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
        animator.runtimeAnimatorController = animatorControllers[data.spriteType];
    }
    public void SetManagePool(IObjectPool<Enemy> pool)
    {
        this.managePool = pool;
    }
    public void DestroyEnemy()
    {
        managePool.Release(this);
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        health -= collision.GetComponent<Bullet>().damage;

        if(health>0)
        {

        }
        else
        {
            Dead();
        }
    }
    public void Dead()
    {
        gameObject.SetActive(false);
    }
}

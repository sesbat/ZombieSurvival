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
    public float hitKnockBack;
    public Rigidbody2D target;

    private Rigidbody2D enemyRigidBody;
    private Collider2D enemyCol;
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
        enemyCol = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
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
        enemyCol.enabled = true;
        enemyRigidBody.simulated = true;
        spriteRenderer.sortingOrder = 2;
        animator.SetBool("Dead", false);
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
        if (!collision.CompareTag("Bullet")||!isLive)
            return;

        StartCoroutine(KnockBack());

        health -= collision.GetComponent<Bullet>().damage;

        if(health>0)
        {
            animator.SetTrigger("Hit");
        }
        else
        {
            enemyRigidBody.simulated = false;
            enemyCol.enabled = false;
            isLive = false;
            spriteRenderer.sortingOrder = 1;
            animator.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }
    private IEnumerator KnockBack()
    {
        yield return new WaitForFixedUpdate();


        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 hitVec = (transform.position - playerPos).normalized;
        enemyRigidBody.AddForce(hitVec * 3f,ForceMode2D.Force);
    }
    public void Dead()
    {
        gameObject.SetActive(false);
    }
}

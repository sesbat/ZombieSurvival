using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public Rigidbody2D target;

    private Rigidbody2D enemyRigidBody;
    private SpriteRenderer spriteRenderer;

    private bool isLive = true;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
}

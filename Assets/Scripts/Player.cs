using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody2D playerRigidBody;
    private Collider2D playerCollider;
    private SpriteRenderer spriteRenderer;
    private Animator playerAnimator;

    public Slider hpBar;
    public float hp;
    public float maxHp;

    public Scanner scanner;
    public Vector2 inputVec;

    public float speed = 5f;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        playerCollider = GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        hpBar.gameObject.SetActive(true);
        hp = maxHp;
        hpBar.value = hp / maxHp;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        if (hp > 0)
        {
            hp-=collision.GetComponent<Enemy>().damage;
        }
        else
        {
            playerAnimator.SetTrigger("Dead");
        }
    }
    private void Update()
    {
        hpBar.value = hp / maxHp;
    }
    private void LateUpdate()
    {
        playerAnimator.SetFloat("Speed", inputVec.magnitude);
        if(inputVec.x !=0)
        {
            spriteRenderer.flipX = inputVec.x < 0;
        }
    }
    private void FixedUpdate()
    {
        Vector2 newVec = inputVec * speed * Time.fixedDeltaTime;
        playerRigidBody.MovePosition(playerRigidBody.position + newVec);

    }
    public void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
    public void OnDead()
    {
        playerAnimator.SetTrigger("Dead");
    }
}

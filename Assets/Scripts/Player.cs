using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody2D playerRigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator playerAnimator;

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
    }
    private void Update()
    {
        
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
}

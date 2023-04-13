using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed =5f;

    private Vector2 inputVector;
    private PlayerInput playerInput;
    private Rigidbody2D rb;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(inputVector.x * walkSpeed, rb.velocity.y) *Time.fixedDeltaTime;
    }
    private void Update()
    {
        inputVector = playerInput.GetInputVector();
    }
   
}

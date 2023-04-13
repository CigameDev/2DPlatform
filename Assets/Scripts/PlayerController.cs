using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed =5f;
    [SerializeField] private float runSpeed = 10f;
    private Vector2 inputVector;
    private PlayerInput playerInput;
    private Rigidbody2D rb;
    private Animator animator;
    private float currentSpeed
    {
        get
        {
            if (_isMoving)
            {
                if (_isRunning)
                {
                    return runSpeed;
                }
                else return walkSpeed;
            }
            else return 0;
        }
    }
    private bool _isMoving = false;
    public bool IsMoving
    {
        get { return _isMoving; }
        set 
        {   _isMoving = value;
            animator.SetBool(AnimationString.IS_MOVING, value);
        }
    }
    private bool _isRunning = false;
    public bool IsRunning
    {
        get { return _isRunning; }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationString.IS_RUNNING, value);
        }
    }    

    private bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get{ return _isFacingRight; }
        set
        {
            if(_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1,1);
            }   
           
            _isFacingRight = value;
        }
    }    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(inputVector.x * currentSpeed, rb.velocity.y) *Time.fixedDeltaTime;
    }
    private void Update()
    {
        inputVector = playerInput.GetInputVector();
    }

    public void SetFacingRight(Vector2 moveDir)
    {
        if(moveDir.x >0 && _isFacingRight == false)
        {
            IsFacingRight=true;
        }   
        else if(moveDir.x <0 && _isFacingRight == true)
        {
            IsFacingRight = false;
        }    
    }    
   
}

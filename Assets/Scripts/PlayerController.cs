using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    private InputSystem inputSystem = null;

    [SerializeField] private float walkSpeed =5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float airWalkSpeed = 3f;
    [SerializeField] private float jumpImpulse =10f;
    private Vector2 inputVector;
    private Rigidbody2D rb;
    private Animator animator;
    private TouchingDirections touchingDirections;
    private float currentSpeed
    {
        get
        {
            if (_isMoving && !touchingDirections.IsOnWall)
            {
                if (touchingDirections.IsGround)
                {
                    if (_isRunning)
                    {
                        return runSpeed;
                    }
                    else
                    {

                        return walkSpeed;
                    }
                }
                else
                {
                    return airWalkSpeed;
                }    
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
        inputSystem = new InputSystem();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }
    private void OnEnable()
    {
        inputSystem.Enable();
        inputSystem.Player.Move.performed += Movement_performed;
        inputSystem.Player.Move.canceled += Movement_canceled;
        inputSystem.Player.Run.performed += Run_performed;
        inputSystem.Player.Run.canceled += Run_canceled;
        inputSystem.Player.Jump.performed += Jump_performed;
        inputSystem.Player.Jump.canceled += Jump_canceled;
        inputSystem.Player.Attack.performed += Attack_performed;
        inputSystem.Player.Attack.canceled += Attack_canceled;
    }

   

    private void OnDisable()
    {
        inputSystem.Player.Move.performed -= Movement_performed;
        inputSystem.Player.Move.canceled -= Movement_canceled;
        inputSystem.Player.Run.performed -= Run_performed;
        inputSystem.Player.Run.canceled -= Run_canceled;
        inputSystem.Player.Jump.performed -= Jump_performed;
        inputSystem.Player.Jump.canceled -= Jump_canceled;
        inputSystem.Player.Attack.performed -= Attack_performed;
        inputSystem.Player.Attack.canceled -= Attack_canceled;
        inputSystem.Dispose();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(inputVector.x * currentSpeed, rb.velocity.y);
        animator.SetFloat(AnimationString.Y_VELOCITY, rb.velocity.y);
    }
   

    public void SetFacingRight(Vector2 moveDir)
    {
        if(moveDir.x > 0 && _isFacingRight == false)
        {
            IsFacingRight=true;
        }   
        else if(moveDir.x < 0 && _isFacingRight == true)
        {
            IsFacingRight = false;
        }    
    }    

    private void Movement_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        inputVector = Vector2.zero;
        IsMoving = false;
    }

    private void Movement_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        inputVector = obj.ReadValue<Vector2>();
        IsMoving = true;
        SetFacingRight(inputVector);
    }
    private void Run_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        IsRunning = false;
    }

    private void Run_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        IsRunning = true;
    }
    private void Jump_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (touchingDirections.IsGround)
        {
            animator.SetTrigger(AnimationString.JUMP_TRIGGER);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    private void Attack_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        animator.SetTrigger(AnimationString.ATTACK_TRIGGER);
    }
}

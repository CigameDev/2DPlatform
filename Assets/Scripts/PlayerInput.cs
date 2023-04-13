using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private InputSystem inputSystem = null;
    private Vector2 moveVector;
    private PlayerController playerController;
    private void Awake()
    {
        inputSystem = new InputSystem();
        playerController = GetComponent<PlayerController>();
    }
    private void OnEnable()
    {
        inputSystem.Enable();
        inputSystem.Player.Move.performed += Movement_performed;
        inputSystem.Player.Move.canceled += Movement_canceled;
        inputSystem.Player.Run.performed += Run_performed;
        inputSystem.Player.Run.canceled += Run_canceled;
    }

    private void Run_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        playerController.IsRunning = false;
    }

    private void Run_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        playerController.IsRunning = true;
    }

    private void OnDisable()
    {
        //inputSystem.Disable();
        inputSystem.Player.Move.performed -= Movement_performed;
        inputSystem.Player.Move.canceled -= Movement_canceled;
        inputSystem.Player.Run.performed -= Run_performed;
        inputSystem.Player.Run.canceled -= Run_canceled;
        inputSystem.Dispose();
    }
    private void Movement_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        moveVector = Vector2.zero;
        playerController.IsMoving = false;
    }

    private void Movement_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        moveVector = obj.ReadValue<Vector2>();
        playerController.IsMoving = true;
        playerController.SetFacingRight(moveVector);
    }

   
   
    public Vector2 GetInputVector()
    {
        return moveVector;
    }    
  
}

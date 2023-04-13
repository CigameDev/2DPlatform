using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private InputSystem inputSystem = null;
    private Vector2 moveVector;

    private void Awake()
    {
        inputSystem = new InputSystem();
    }
    private void OnEnable()
    {
        inputSystem.Enable();
        inputSystem.Player.Move.performed += Movement_performed;
        inputSystem.Player.Move.canceled += Movement_canceled;
    }

  
    private void OnDisable()
    {
        inputSystem.Disable();
        inputSystem.Player.Move.performed -= Movement_performed;
        inputSystem.Player.Move.canceled -= Movement_canceled;
    }
    private void Movement_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        moveVector = Vector2.zero;
    }

    private void Movement_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        moveVector = obj.ReadValue<Vector2>();
    }

   

    public Vector2 GetInputVector()
    {
        return moveVector;
    }    
}

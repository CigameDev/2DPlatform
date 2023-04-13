using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private CustomInput input = null;
    private Vector2 moveVector;

    private void Awake()
    {
        input = new CustomInput();
    }
    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += Movement_performed;
        input.Player.Movement.canceled += Movement_canceled;
    }

  
    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= Movement_performed;
        input.Player.Movement.canceled -= Movement_canceled;
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

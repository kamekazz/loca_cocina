using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    PlayerActionsControls playerInputActions;
    void Awake()
    {
        playerInputActions = new PlayerActionsControls();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_Performed;
    }

    private void Interact_Performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;
        return inputVector;
    }


}

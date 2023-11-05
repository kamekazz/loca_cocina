using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movedSpeedField = 7f;
    [SerializeField] Animator animatorCP;
    bool isWalking;
    void Update()
    {
        Vector2 inputVector = GetComponent<GameInput>().GetMovementVector();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        isWalking = moveDir != Vector3.zero;
        transform.position += moveDir * Time.deltaTime * movedSpeedField;
        float _rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * _rotateSpeed);
    }

    public bool GetIsWalking()
    {
        return isWalking;
    }

}

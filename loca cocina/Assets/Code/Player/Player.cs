using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movedSpeedField = 7f;

    bool isWalking;
    void Update()
    {
        HandheldMove();
    }

    private void HandheldMove()
    {
        Vector2 _inputVector = GetComponent<GameInput>().GetMovementVector();
        Vector3 _moveDir = new Vector3(_inputVector.x, 0f, _inputVector.y);

        float _moveDistance = movedSpeedField * Time.deltaTime;
        float _playerRadius = .7f;
        float _playerHeight = 2f;
        bool _canMove = !Physics.CapsuleCast(
                                                transform.position,
                                                transform.position + Vector3.up * _playerHeight,
                                                _playerRadius,
                                                _moveDir,
                                                _moveDistance
                                            );
        if (!_canMove)
        {
            //can not move towards moveDir

            //attempt only x movement
            Vector3 _moveDirX = new Vector3(_moveDir.x, 0, 0).normalized;
            _canMove = !Physics.CapsuleCast(
                                                transform.position,
                                                transform.position + Vector3.up * _playerHeight,
                                                _playerRadius,
                                                _moveDirX,
                                                _moveDistance
                                            );
            if (_canMove)
            {
                //Can move only on the X
                _moveDir = _moveDirX;
            }
            else
            {
                // cannot move ony on the X


                // Attempt only Z movement
                Vector3 _moveDirZ = new Vector3(0, 0, _moveDir.z).normalized;
                _canMove = !Physics.CapsuleCast(
                                                    transform.position,
                                                    transform.position + Vector3.up * _playerHeight,
                                                    _playerRadius,
                                                    _moveDirZ,
                                                    _moveDistance
                                                );

                if (_canMove)
                {
                    //Can move only on the Z
                    _moveDir = _moveDirZ;
                }
                else
                {
                    //can't move in any direction
                }
            }

        }
        if (_canMove)
        {
            transform.position += _moveDir * Time.deltaTime * movedSpeedField;
        }
        isWalking = _moveDir != Vector3.zero;
        float _rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, _moveDir, Time.deltaTime * _rotateSpeed);
    }

    public bool GetIsWalking()
    {
        return isWalking;
    }

}

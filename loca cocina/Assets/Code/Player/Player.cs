using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }
    [SerializeField] float movedSpeedField = 7f;
    [SerializeField] LayerMask countersLayerMask;
    GameInput gameInputSP;
    ClearCounter selectedCounterSP;


    bool isWalking;
    Vector3 lastInteractDir;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }


    void Start()
    {
        gameInputSP = GetComponent<GameInput>();
        gameInputSP.OnInteractAction += GameInput_OnInteract_Action;
    }

    void Update()
    {
        HandheldMove();
        HandleInteractions();
    }

    private void GameInput_OnInteract_Action(object sender, EventArgs e)
    {
        if (selectedCounterSP != null)
        {
            selectedCounterSP.Interact();
        }
    }

    void HandleInteractions()
    {
        Vector2 _inputVector = gameInputSP.GetMovementVector();
        Vector3 _moveDir = new Vector3(_inputVector.x, 0f, _inputVector.y);
        if (_moveDir != Vector3.zero)
        {
            lastInteractDir = _moveDir;
        }
        float _interactDistance = 1f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit _raycastHit, _interactDistance, countersLayerMask))
        {
            if (_raycastHit.transform.TryGetComponent(out ClearCounter clearCounterSP))
            {
                if (clearCounterSP != selectedCounterSP)
                {
                    SetSelectedCounter(clearCounterSP);
                }
            }
            else
            {


                SetSelectedCounter(null);
            }
        }
        else
        {


            SetSelectedCounter(null);
        }


    }

    private void SetSelectedCounter(ClearCounter _selectedCounter)
    {
        selectedCounterSP = _selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = _selectedCounter
        });
    }

    private void HandheldMove()
    {
        Vector2 _inputVector = gameInputSP.GetMovementVector();
        Vector3 _moveDir = new Vector3(_inputVector.x, 0f, _inputVector.y);

        float _moveDistance = movedSpeedField * Time.deltaTime;
        float _playerRadius = .7f;
        float _playerHeight = 2f;
        _moveDir = MyCollisionDetection(_moveDir, _moveDistance, _playerRadius, _playerHeight);
        isWalking = _moveDir != Vector3.zero;
        float _rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, _moveDir, Time.deltaTime * _rotateSpeed);
    }

    private Vector3 MyCollisionDetection(Vector3 _moveDir, float _moveDistance, float _playerRadius, float _playerHeight)
    {
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

        return _moveDir;
    }

    public bool GetIsWalking()
    {
        return isWalking;
    }


}

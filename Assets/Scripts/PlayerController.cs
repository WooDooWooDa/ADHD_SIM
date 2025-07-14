using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Camera PlayerCamera;
    public CharacterController Controller;
    public InputActionAsset actionAsset;
    
    public float speed = 5f;
    public float lookSpeed = 0.1f;

    private InputAction moveAction;
    private InputAction lookAction;

    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _lookAmt;
    private float _rotationX = 0;
    private float _xRotationLimit = 85f;
    private bool _canMove = true;
    
    public void OnEnable()
    {
        actionAsset.FindActionMap("Player").Enable();
    }

    public void OnDisable()
    {
        actionAsset.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Move();
        Look();
    }

    private void Move()
    {
        _moveDirection = moveAction.ReadValue<Vector2>();
        if (!_canMove) return;
        
        var forward = transform.forward;
        var right = PlayerCamera.transform.right;
        
        _moveDirection = forward * _moveDirection.y + right * _moveDirection.x;
        
        Controller.Move(_moveDirection * (speed * Time.deltaTime));
    }

    private void Look()
    {
        _lookAmt = lookAction.ReadValue<Vector2>();
        _rotationX -= _lookAmt.y * lookSpeed;
        _rotationX = Mathf.Clamp(_rotationX, -_xRotationLimit, _xRotationLimit);
        PlayerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, _lookAmt.x * lookSpeed, 0);
    }
}

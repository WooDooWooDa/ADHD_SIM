using System;
using DefaultNamespace.Widgets;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public Camera PlayerCamera;
    public CharacterController Controller;
    public AudioSource footSource;
    public AudioClip[] footClips;
    public InputActionAsset actionAsset;

    public float _footStepTimeThreshold = 0.5f;
    private float _footStepTime = 0f;
    
    public bool canMove
    {
        get => _canMove; set => _canMove = value; 
    }
    public float speed = 5f;
    public float lookSpeed = 0.1f;

    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _pauseAction;

    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _lookAmt;
    private float _rotationX = 0;
    private float _xRotationLimit = 85f;
    private bool _canMove = false;
    private PauseWidget _pauseWidget;

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
        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
        _pauseWidget = FindFirstObjectByType<PauseWidget>();
        _pauseWidget.player = this;
        _pauseAction = InputSystem.actions.FindAction("Pause");
        _pauseAction.performed += OnPauseAction;
    }

    private void OnPauseAction(InputAction.CallbackContext obj)
    {
        if (!_pauseWidget.isVisible) 
            _pauseWidget.Show();
        else
            _pauseWidget.Hide();
    }

    private void Update()
    {
        Move();
        Look();
    }

    private void Move()
    {
        _moveDirection = _moveAction.ReadValue<Vector2>();
        if (!_canMove) return;
        
        var forward = transform.forward;
        var right = PlayerCamera.transform.right;
        
        _moveDirection = forward * _moveDirection.y + right * _moveDirection.x;

        if (_moveDirection != Vector3.zero)
        {
            _footStepTime += Time.deltaTime;
            if (_footStepTime >= _footStepTimeThreshold)
            {
                _footStepTime = 0f;
                footSource.clip = footClips[Random.Range(0,footClips.Length)];
                footSource.Play();
            }
        }
        else
        {
            _footStepTime = 0f;
        }
        
        if (!Controller.isGrounded) _moveDirection.y += Physics.gravity.y;
        
        Controller.Move(_moveDirection * (speed * Time.deltaTime));
    }

    private void Look()
    {
        if (!_canMove) return;
        
        _lookAmt = _lookAction.ReadValue<Vector2>();
        _rotationX -= _lookAmt.y * lookSpeed;
        _rotationX = Mathf.Clamp(_rotationX, -_xRotationLimit, _xRotationLimit);
        PlayerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, _lookAmt.x * lookSpeed, 0);
    }
}

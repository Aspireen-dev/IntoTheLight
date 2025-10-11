using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Serialized fields

    [SerializeField] private Player _player;
    [SerializeField] private InputActionReference _dragAction;

    #endregion Serialized fields

    #region Private variables

    private bool _isDragging = false;
    private Vector2 _startDraggingPosition;

    #endregion Private variables

    #region Singleton pattern

    public static InputManager Instance { get; private set; }

    #endregion Singleton pattern

    private void Awake()
    {
        // Ensure only one instance of InputManager exists (Singleton pattern)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        _dragAction.action.started += OnDragStarted;
        _dragAction.action.canceled += OnDragCanceled;
    }

    private void OnDisable()
    {
        _dragAction.action.started -= OnDragStarted;
        _dragAction.action.canceled -= OnDragCanceled;
    }

    private void Update()
    {
        if (_isDragging && _dragAction.action.IsPressed())
        {
            _player.Aim(_startDraggingPosition, GetInputPosition());
        }
    }

    private void OnDragStarted(InputAction.CallbackContext context)
    {
        _isDragging = true;
        _startDraggingPosition = GetInputPosition();
        _player.StartAiming();
    }

    private void OnDragCanceled(InputAction.CallbackContext context)
    {
        _isDragging = false;
        _startDraggingPosition = Vector2.zero;
        _player.StopAiming();
    }

    private Vector2 GetInputPosition()
    {
        // Check for mouse input
        if (Mouse.current.leftButton.isPressed)
        {
            return Mouse.current.position.ReadValue();
        }
        // Check for touch input
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            return Touchscreen.current.primaryTouch.position.ReadValue();
        }
        // Default return value if no input is detected
        return Vector2.zero;
    }
}

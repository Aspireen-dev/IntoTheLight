using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    private PlayerInputActions _playerInputActions;

    public static InputManager Instance { get; private set; }

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Attack.performed += Attack_Performed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Attack_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (_player != null)
        {
            _player.Launch();
        }
    }
}

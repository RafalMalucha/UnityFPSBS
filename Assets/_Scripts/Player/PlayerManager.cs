using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private InputActionAsset _inputManager;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private int _health;
    
    //--------------------------------------------------
    private InputAction _attack;
    private InputAction _move;
    private InputAction _look;
    private InputAction _jump;
    private InputAction _dash;


    private void OnEnable() 
    {
        _inputManager.FindActionMap("Player").Enable();
    }

    private void OnDisable() 
    {
        _inputManager.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _characterController = GetComponent<CharacterController>();

        _inputManager.FindActionMap("Player").Enable();

        _attack = InputSystem.actions.FindAction("Attack");
        _move = InputSystem.actions.FindAction("Move");
        _look = InputSystem.actions.FindAction("Look");
        _jump = InputSystem.actions.FindAction("Jump");
        _dash = InputSystem.actions.FindAction("Dash");
    }

    
    void Update()
    {
        
    }

    public PlayerInventory GetPlayerInventory()
    {
        return _playerInventory;
    }

    public Camera GetMainCamera()
    {
        return _mainCamera;
    }

    public CharacterController GetCharacterController()
    {
        return _characterController;
    }

    public InputAction GetAttackInputAction()
    {
        return _attack;
    }

    public InputAction GetMoveInputAction()
    {
        return _move;
    }

    public InputAction GetLookInputAction()
    {
        return _look;
    }

    public InputAction GetJumpInputAction()
    {
        return _jump;
    }

    public InputAction GetDashInputAction()
    {
        return _dash;
    }
}

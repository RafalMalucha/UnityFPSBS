using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerManager : MonoBehaviour
{

    //Singleton
    public static PlayerManager Instance;

    [SerializeField] private PlayerRaycast _playerRaycast;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private PlayerInteract _playerInteract;
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
    private InputAction _use;


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
        Instance = this;

        Cursor.lockState = CursorLockMode.Locked;

        _characterController = GetComponent<CharacterController>();

        _inputManager.FindActionMap("Player").Enable();

        _attack = InputSystem.actions.FindAction("Attack");
        _move = InputSystem.actions.FindAction("Move");
        _look = InputSystem.actions.FindAction("Look");
        _jump = InputSystem.actions.FindAction("Jump");
        _dash = InputSystem.actions.FindAction("Dash");
        _use = InputSystem.actions.FindAction("Use");
    }

    
    void Update()
    {
        
    }

    public PlayerRaycast GetPlayerRaycast()
    {
        return _playerRaycast;
    }

    public PlayerInventory GetPlayerInventory()
    {
        return _playerInventory;
    }

    public PlayerInteract GetPlayerInteract()
    {
        return _playerInteract;
    }

    public PlayerMovement GetPlayerMovement()
    {
        return _playerMovement;
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

    public InputAction GetUseInputAction()
    {
        return _use;
    }
}

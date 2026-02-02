using System.Collections;
using NUnit.Framework;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.XR;
public class PlayerMovement : MonoBehaviour
{

    public InputActionAsset InputManager;
    [SerializeField] private CharacterController _characterController;
    //private Rigidbody _rigidbody;
    [SerializeField] public Transform cameraHolder;
    // ------------------------------------------
    private InputAction _move;
    private InputAction _look;
    private InputAction _jump;
    private InputAction _dash;
    // ------------------------------------------
    public float baseSpeed = 10.0f; 
    public float maxSpeed = 10.0f; 
    public float gravity = -20.0f;
    public float jumpHeight = 1.5f; 
    public float jumpDuration = 0.65f; 
    public float dashDistance = 20.0f; 
    public float dashCooldown = 1.0f; 
    public float sensitivity = 1.0f;
    // ------------------------------------------
    private Vector2 _moveAmt;
    private Vector2 _lookAmt;
    // ------------------------------------------
    private Vector3 currentVelocity = Vector3.zero;
    private bool isGrounded;
    private bool isJumping = false;
    private float jumpStartTime;
    private float initialYPosition;
    private Vector3 retainedVelocity;
    private float lastDashTime;

    private float camera_xRotation = 0f;
    private float calculatedMoveSpeedX = 0.0f;
    private float calculatedMoveSpeedY = 0.0f;

    private void OnEnable() 
    {
        InputManager.FindActionMap("Player").Enable();
    }

    private void OnDisable() 
    {
        InputManager.FindActionMap("Player").Disable();
    }

    private void Awake() 
    {
        _move = InputSystem.actions.FindAction("Move");
        _look = InputSystem.actions.FindAction("Look");
        _jump = InputSystem.actions.FindAction("Jump");
        _dash = InputSystem.actions.FindAction("Dash");

        _characterController = GetComponent<CharacterController>();
        //this.transform.position.y = 0f;
        //_rigidbody = GetComponent<Rigidbody>();
    }

    private void Update() 
    {
        if (_jump.WasPressedThisFrame()) 
        {
            Jump();
        }

        HandleLook();
        HandleMovement();
    }

    private void HandleLook()
    {
        _lookAmt = _look.ReadValue<Vector2>();

        float horizontalLook = _lookAmt.x * 666.66f * sensitivity * Time.deltaTime * 0.1f;
        float verticalLook = _lookAmt.y * 666.66f * sensitivity * Time.deltaTime * 0.1f;

        camera_xRotation -= verticalLook;
        camera_xRotation = Mathf.Clamp(camera_xRotation, -90f, 90f);
        
        cameraHolder.transform.localRotation = Quaternion.Euler(camera_xRotation, 0f, 0f);
        
        transform.Rotate(Vector3.up * horizontalLook);
    }

    private void HandleMovement()
    {
        _moveAmt = _move.ReadValue<Vector2>();
        //Debug.Log(_moveAmt);

        isGrounded = _characterController.isGrounded;
        //Debug.Log(isGrounded);

        if (isGrounded && currentVelocity.y < 0)
        {
            currentVelocity.y = -2f;
        }

        if(_moveAmt[0] != 0)
        {
            Debug.Log("move X");
            CalculateMoveSpeedX();
            Debug.Log(calculatedMoveSpeedX);
        }

        if(_moveAmt[0] == 0)
        {
            Debug.Log("stop X");
            GradualyReduceCalculatedSpeedX();
            Debug.Log(calculatedMoveSpeedX);
        }

        if(_moveAmt[1] != 0)
        {
            Debug.Log("move Y");
            CalculateMoveSpeedY();
            Debug.Log(calculatedMoveSpeedY);
        }

        if(_moveAmt[1] == 0)
        {
            Debug.Log("stop Y");
            GradualyReduceCalculatedSpeedY();
            Debug.Log(calculatedMoveSpeedY);
        }

        //Vector3 move = transform.right * math.abs(_moveAmt.x) * calculatedMoveSpeedX * 0.001f + transform.forward * math.abs(_moveAmt.y) * calculatedMoveSpeedY * 0.001f;
        Vector3 move = transform.right * calculatedMoveSpeedX * 0.001f + transform.forward * calculatedMoveSpeedY * 0.001f;
        _characterController.Move(move * baseSpeed * Time.deltaTime);

        currentVelocity.y += gravity * Time.deltaTime;
        _characterController.Move(currentVelocity * Time.deltaTime);

    }

    private void CalculateMoveSpeedX()
    {
        calculatedMoveSpeedX += _moveAmt[0] * 10.0f;

        if (calculatedMoveSpeedX > 600.0f)
        {
            calculatedMoveSpeedX = 600.0f;
        }
        if (calculatedMoveSpeedX < -600.0f)
        {
            calculatedMoveSpeedX = -600.0f;
        }
    }

    private void CalculateMoveSpeedY()
    {
        calculatedMoveSpeedY += _moveAmt[1] * 10.0f;

        if (calculatedMoveSpeedY > 600.0f)
        {
            calculatedMoveSpeedY = 600.0f;
        }
        if (calculatedMoveSpeedY < -600.0f)
        {
            calculatedMoveSpeedY = -600.0f;
        }
    }

    private void GradualyReduceCalculatedSpeedX()
    {
        if(calculatedMoveSpeedX > 3.0f)
        {
            calculatedMoveSpeedX -= 3.0f;
        }
        if(calculatedMoveSpeedX < -3.0f)
        {
            calculatedMoveSpeedX += 3.0f;
        }
        if(calculatedMoveSpeedX <= 3.0f && calculatedMoveSpeedX >= -3.0f)
        {
            calculatedMoveSpeedX = 0.0f;
        }
    }

    private void GradualyReduceCalculatedSpeedY()
    {
        if(calculatedMoveSpeedY > 3.0f)
        {
            calculatedMoveSpeedY -= 3.0f;
        }
        if(calculatedMoveSpeedY < -3.0f)
        {
            calculatedMoveSpeedY += 3.0f;
        }
        if(calculatedMoveSpeedY <= 3.0f && calculatedMoveSpeedY >= -3.0f)
        { 
            calculatedMoveSpeedY = 0.0f;
        }
    }

    private void Jump()
    {
        Debug.Log("dupa");
        Debug.Log(_characterController.isGrounded);
    }
}

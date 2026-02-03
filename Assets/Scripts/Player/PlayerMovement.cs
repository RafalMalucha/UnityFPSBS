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
    public float dashDistance = 10.0f; 
    public float dashCooldown = 5.0f; 
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
        Cursor.lockState = CursorLockMode.Locked;

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
        HandleLook();
        HandleMovement();
    }

    private void HandleLook()
    {
        _lookAmt = _look.ReadValue<Vector2>();

        float horizontalLook = _lookAmt.x * 666.66f * sensitivity * Time.deltaTime * 0.05f;
        float verticalLook = _lookAmt.y * 666.66f * sensitivity * Time.deltaTime * 0.05f;

        camera_xRotation -= verticalLook;
        camera_xRotation = Mathf.Clamp(camera_xRotation, -90f, 90f);
        
        cameraHolder.transform.localRotation = Quaternion.Euler(camera_xRotation, 0f, 0f);
        
        transform.Rotate(Vector3.up * horizontalLook);
    }

    private void HandleMovement()
    {
        _moveAmt = _move.ReadValue<Vector2>();
        Vector3 direction = new Vector3(_moveAmt.x, 0, _moveAmt.y);
        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }

        isGrounded = _characterController.isGrounded;
        //Debug.Log(isGrounded);

        if (isGrounded)
        {
            currentVelocity.y = -1.25f;

            if(_moveAmt[0] != 0)
            {
                CalculateMoveSpeedX();
            }

            if(_moveAmt[0] == 0)
            {
                GradualyReduceCalculatedSpeedX();
            }

            if(_moveAmt[1] != 0)
            {
                CalculateMoveSpeedY();
            }

            if(_moveAmt[1] == 0)
            {
                GradualyReduceCalculatedSpeedY();
            }
        }

        if (_jump.WasPressedThisFrame() && isGrounded) 
        {
            isJumping = true;
            jumpStartTime = Time.time;
            initialYPosition = transform.position.y;
        }

        if(isJumping)
        {
            float elapsedTime = Time.time - jumpStartTime;
            if (elapsedTime < jumpDuration)
            {
                float jumpProgress = elapsedTime / jumpDuration;
                float yOffset = Mathf.Sin(jumpProgress * Mathf.PI) * jumpHeight;
                currentVelocity.y = (initialYPosition + yOffset - transform.position.y) / Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        //Vector3 move = transform.right * math.abs(_moveAmt.x) * calculatedMoveSpeedX * 0.001f + transform.forward * math.abs(_moveAmt.y) * calculatedMoveSpeedY * 0.001f;
        Vector3 move = transform.right * calculatedMoveSpeedX * 0.001f + transform.forward * calculatedMoveSpeedY * 0.001f;
        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        if (_dash.WasPressedThisFrame())
        {
            Debug.Log("dash");
            // lastDashTime = Time.time;

            // float dashStartTime = Time.time;
            // Vector3 dashStartPosition = transform.position;
            // Vector3 dashEndPosition = dashStartPosition + direction.normalized * dashDistance;
            // Debug.Log(lastDashTime);
            // Debug.Log(dashStartTime);
            // Debug.Log(dashStartPosition);
            // Debug.Log(dashEndPosition);

            StartCoroutine(Dash(move));
        }

        _characterController.Move(move * baseSpeed * Time.deltaTime);

        currentVelocity.y += gravity * Time.deltaTime;
        _characterController.Move(currentVelocity * Time.deltaTime);

    }

    private void CalculateMoveSpeedX()
    {
        calculatedMoveSpeedX += _moveAmt[0] * 25.0f;

        if (calculatedMoveSpeedX > 1000.0f)
        {
            calculatedMoveSpeedX = 1000.0f;
        }
        if (calculatedMoveSpeedX < -1000.0f)
        {
            calculatedMoveSpeedX = -1000.0f;
        }
    }

    private void CalculateMoveSpeedY()
    {
        calculatedMoveSpeedY += _moveAmt[1] * 25.0f;

        if (calculatedMoveSpeedY > 1000.0f)
        {
            calculatedMoveSpeedY = 1000.0f;
        }
        if (calculatedMoveSpeedY < -1000.0f)
        {
            calculatedMoveSpeedY = -1000.0f;
        }
    }

    private void GradualyReduceCalculatedSpeedX()
    {
        if(calculatedMoveSpeedX > 15.0f)
        {
            calculatedMoveSpeedX -= 15.0f;
        }
        if(calculatedMoveSpeedX < -15.0f)
        {
            calculatedMoveSpeedX += 15.0f;
        }
        if(calculatedMoveSpeedX <= 15.0f && calculatedMoveSpeedX >= -15.0f)
        {
            calculatedMoveSpeedX = 0.0f;
        }
    }

    private void GradualyReduceCalculatedSpeedY()
    {
        if(calculatedMoveSpeedY > 15.0f)
        {
            calculatedMoveSpeedY -= 15.0f;
        }
        if(calculatedMoveSpeedY < -15.0f)
        {
            calculatedMoveSpeedY += 15.0f;
        }
        if(calculatedMoveSpeedY <= 15.0f && calculatedMoveSpeedY >= -15.0f)
        { 
            calculatedMoveSpeedY = 0.0f;
        }
    }

    IEnumerator Dash(Vector3 direction)
    {
        lastDashTime = Time.time;

        float dashStartTime = Time.time;
        Vector3 dashStartPosition = transform.position;
        Vector3 dashEndPosition = dashStartPosition + direction.normalized * dashDistance;

        Debug.Log(lastDashTime);
        Debug.Log(dashStartTime);
        Debug.Log(dashStartPosition);
        Debug.Log(dashEndPosition);


        while (Time.time < dashStartTime + 0.1f) // Adjusted dash duration to 0.2s
        {
            Vector3 dashPosition = Vector3.Lerp(dashStartPosition, dashEndPosition, (Time.time - dashStartTime) / 0.1f);
            _characterController.Move(dashPosition - transform.position);

            if (_characterController.collisionFlags == CollisionFlags.Sides || _characterController.collisionFlags == CollisionFlags.Above)
            {
                break;
            }

            yield return null;
        }
    }
}

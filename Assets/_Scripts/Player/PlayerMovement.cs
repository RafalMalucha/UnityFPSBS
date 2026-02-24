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

    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private CharacterController _characterController;
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
    private Vector2 _moveAmount;
    private Vector2 _lookAmount;
    // ------------------------------------------
    private Vector3 currentVelocity = Vector3.zero;
    // private bool isGrounded;
    private bool isJumping = false;
    private float jumpStartTime;
    private float initialYPosition;
    Vector3 move;
    private float lastDashTime;

    private float camera_xRotation = 0f;
    private float calculatedMoveSpeedX = 0.0f;
    private float calculatedMoveSpeedY = 0.0f;

    private void Awake() 
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update() 
    {
        HandleLook();
        HandleMovement();
    }

    private void HandleLook()
    {
        _lookAmount = _playerManager.GetLookInputAction().ReadValue<Vector2>();

        float horizontalLook = _lookAmount.x * 666.66f * sensitivity * Time.deltaTime * 0.05f;
        float verticalLook = _lookAmount.y * 666.66f * sensitivity * Time.deltaTime * 0.05f;

        camera_xRotation -= verticalLook;
        camera_xRotation = Mathf.Clamp(camera_xRotation, -90f, 90f);
        
        _playerManager.GetMainCamera().transform.localRotation = Quaternion.Euler(camera_xRotation, 0f, 0f);
        
        transform.Rotate(Vector3.up * horizontalLook);
    }

    private void HandleMovement()
    {
        _moveAmount = _playerManager.GetMoveInputAction().ReadValue<Vector2>();
        Vector3 direction = new Vector3(_moveAmount.x, 0, _moveAmount.y);
        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }

        //isGrounded = _characterController.isGrounded;

        if (_playerManager.GetCharacterController().isGrounded)
        {
            currentVelocity.y = -1.25f;

            if(_moveAmount[0] != 0)
            {
                CalculateMoveSpeedX();
            }

            if(_moveAmount[0] == 0)
            {
                GradualyReduceCalculatedSpeedX();
            }

            if(_moveAmount[1] != 0)
            {
                CalculateMoveSpeedY();
            }

            if(_moveAmount[1] == 0)
            {
                GradualyReduceCalculatedSpeedY();
            }
        }

        if (_playerManager.GetJumpInputAction().WasPressedThisFrame() && _playerManager.GetCharacterController().isGrounded) 
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
        move = transform.right * calculatedMoveSpeedX * 0.001f + transform.forward * calculatedMoveSpeedY * 0.001f;
        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        if (_playerManager.GetDashInputAction().WasPressedThisFrame())
        {
            StartCoroutine(Dash(move));
        }

        _characterController.Move(move * baseSpeed * Time.deltaTime);

        currentVelocity.y += gravity * Time.deltaTime;
        _characterController.Move(currentVelocity * Time.deltaTime);

    }

    private void CalculateMoveSpeedX()
    {
        calculatedMoveSpeedX += _moveAmount[0] * 10.0f;

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
        calculatedMoveSpeedY += _moveAmount[1] * 10.0f;

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

    public float GetCurrentSpeed()
    {
        return Mathf.RoundToInt(move.magnitude * 100);
    }
}

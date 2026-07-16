using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    // ------------------------------------------
    public float baseSpeed = 10.0f; 
    public float maxSpeed = 10.0f; 
    public float gravity = -20.0f;
    public float jumpHeight = 1.5f; 
    public float jumpDuration = 0.65f; 
    public float dashDistance = 10.0f; 
    public float dashCooldown = 0.75f; 
    public float sensitivity = 0.5f;
    // ------------------------------------------
    private Vector2 _moveAmount;
    private Vector2 _lookAmount;
    // ------------------------------------------
    private Vector3 currentVelocity = Vector3.zero;
    private bool isJumping = false;
    private bool isMonkeyBarJumping = false;
    private bool isJumpingFloorBounce = false;
    private float jumpStartTime;
    private float initialYPosition;
    Vector3 move;
    private float lastDashTime;

    private float camera_xRotation = 0f;
    private float calculatedMoveSpeedX = 0.0f;
    private float calculatedMoveSpeedY = 0.0f;
    private float currentJumpHeight;
    private float currentJumpDuration;

    private void Awake() 
    {
        lastDashTime = Time.time;
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

        if (_playerManager.GetCharacterController().isGrounded)
        {
            currentVelocity.y = -1.25f;

            if(_moveAmount[0] != 0)
            {
                CalculateMoveSpeedX(1.0f);
            }

            if(_moveAmount[0] == 0)
            {
                GradualyReduceCalculatedSpeedX();
            }

            if(_moveAmount[1] != 0)
            {
                CalculateMoveSpeedY(1.0f);
            }

            if(_moveAmount[1] == 0)
            {
                GradualyReduceCalculatedSpeedY();
            }
        }

        if (!_playerManager.GetCharacterController().isGrounded)
        {
            if(_moveAmount[0] != 0)
            {
                CalculateMoveSpeedX(0.1f);
            }

            if(_moveAmount[1] != 0)
            {
                CalculateMoveSpeedY(0.1f);
            }

        }

        if (_playerManager.GetJumpInputAction().WasPressedThisFrame() && _playerManager.GetCharacterController().isGrounded) 
        {
            isJumping = true;
            jumpStartTime = Time.time;
            initialYPosition = transform.position.y;
            currentJumpHeight = jumpHeight;
            currentJumpDuration = jumpDuration;
        }

        if (isMonkeyBarJumping) 
        {
            isJumping = true;
            jumpStartTime = Time.time;
            initialYPosition = transform.position.y;
            currentJumpHeight = jumpHeight;
            currentJumpDuration = jumpDuration;
            isMonkeyBarJumping = false;
        }

        if (isJumpingFloorBounce)
        {
            isJumping = true;
            jumpStartTime = Time.time;
            initialYPosition = transform.position.y;
            currentJumpHeight = jumpHeight;
            currentJumpDuration = jumpDuration;
            isJumpingFloorBounce = false;
        }

        if(isJumping)
        {
            float elapsedTime = Time.time - jumpStartTime;
            if (elapsedTime < currentJumpDuration)
            {
                float jumpProgress = elapsedTime / currentJumpDuration;
                float yOffset = Mathf.Sin(jumpProgress * Mathf.PI) * currentJumpHeight;
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
            if(Time.time > lastDashTime + dashCooldown)
            {
                StartCoroutine(Dash(move));
            }
        }

        _playerManager.GetCharacterController().Move(move * baseSpeed * Time.deltaTime);

        currentVelocity.y += gravity * Time.deltaTime;
        _playerManager.GetCharacterController().Move(currentVelocity * Time.deltaTime);

    }

    private void CalculateMoveSpeedX(float modifier)
    {
        calculatedMoveSpeedX += _moveAmount[0] * 25.0f * modifier;

        if (calculatedMoveSpeedX > 1000.0f)
        {
            calculatedMoveSpeedX = 1000.0f;
        }
        if (calculatedMoveSpeedX < -1000.0f)
        {
            calculatedMoveSpeedX = -1000.0f;
        }
    }

    private void CalculateMoveSpeedY(float modifier)
    {
        calculatedMoveSpeedY += _moveAmount[1] * 25.0f * modifier;

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
        if(calculatedMoveSpeedX > 50.0f)
        {
            calculatedMoveSpeedX -= 50.0f;
        }
        if(calculatedMoveSpeedX < -50.0f)
        {
            calculatedMoveSpeedX += 50.0f;
        }
        if(calculatedMoveSpeedX <= 50.0f && calculatedMoveSpeedX >= -50.0f)
        {
            calculatedMoveSpeedX = 0.0f;
        }
    }

    private void GradualyReduceCalculatedSpeedY()
    {
        if(calculatedMoveSpeedY > 50.0f)
        {
            calculatedMoveSpeedY -= 50.0f;
        }
        if(calculatedMoveSpeedY < -50.0f)
        {
            calculatedMoveSpeedY += 50.0f;
        }
        if(calculatedMoveSpeedY <= 50.0f && calculatedMoveSpeedY >= -50.0f)
        { 
            calculatedMoveSpeedY = 0.0f;
        }
    }

    public void SetNewPlayerPosition(Vector3 newPosition)
    {
        currentVelocity = Vector3.zero;
        move = Vector3.zero;

        _playerManager.GetCharacterController().enabled = false;
        transform.position = newPosition;
        _playerManager.GetCharacterController().enabled = true;
    }

    public IEnumerator MonkeyBar()
    {
        _playerManager.GetCharacterController().enabled = false;
        Vector3 savedPlayerPosition = _playerManager.transform.position;

        while(true)
        {
            if(_playerManager.GetJumpInputAction().WasPressedThisFrame())
            {
                SetNewPlayerPosition(savedPlayerPosition);
                _playerManager.GetCharacterController().enabled = true;
                isMonkeyBarJumping = true;
                break;
            }

            yield return null;
        }
    }

    IEnumerator Dash(Vector3 direction)
    {
        lastDashTime = Time.time;

        float dashStartTime = Time.time;
        Vector3 dashStartPosition = transform.position;
        Vector3 dashEndPosition = dashStartPosition + direction.normalized * dashDistance;

        if(lastDashTime + dashCooldown > dashStartTime)
        {
            while (Time.time < dashStartTime + 0.1f)
            {
                Vector3 dashPosition = Vector3.Lerp(dashStartPosition, dashEndPosition, (Time.time - dashStartTime) / 0.1f);
                _playerManager.GetCharacterController().Move(dashPosition - transform.position);

                if (_playerManager.GetCharacterController().collisionFlags == CollisionFlags.Sides || _playerManager.GetCharacterController().collisionFlags == CollisionFlags.Above)
                {
                    break;
                }

                yield return null;
            }
        }
        
    }

    public float GetCurrentSpeed()
    {
        return Mathf.RoundToInt(move.magnitude * 100);
    }

    public Vector3 GetCurrentVelocity()
    {
        return currentVelocity;
    }

    public void SetIsJumpingFloorBounce(bool newValue)
    {
        isJumpingFloorBounce = newValue;
    }
}

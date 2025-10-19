using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference lookAction;
    
    [Header("Movement")]
    public float moveSpeed = 5f;
    public Camera playerCamera;
    
    [Header("Look")]
    public float mouseSensitivity = 150f;
    public float maxPitch = 80f;
    
    [Header("Jump")]
    public float jumpHeight = 2f;
    public float gravity = 9.81f;
    public float jumpBufferTime = 0.2f;
    
    [Header("FOV")]
    public float fovLerpSpeed = 5f;
    [Range(0f, 1f)] public float airborneFovMultiplier = 0.2f;
    
    [Header("Footsteps")]
    public AudioSource footstepAudioSource;
    public AudioClip[] footstepClips;
    
    private float _baseFov = 60f;
    private CharacterController _controller;
    private Vector3 _currentVelocity;
    private float _pitch;
    private float _jumpBufferCounter;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _baseFov = playerCamera.fieldOfView;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
        lookAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
        lookAction.action.Disable();
    }

    private void Update()
    {
        if (GameManager.Instance.OnUI)
        {
            _currentVelocity.x = 0f;
            _currentVelocity.z = 0f;
            footstepAudioSource.Stop();
            
            if (!_controller.isGrounded)
                _currentVelocity.y -= gravity * Time.deltaTime;
            
            _controller.Move(_currentVelocity * Time.deltaTime);
            return;
        }
        
        HandleMovement();
        HandleLook();
        
        HandleJumpAndGravity();
        HandleFootsteps();
        
        _controller.Move(_currentVelocity * Time.deltaTime);
        
        var horizontalSpeed = new Vector3(_controller.velocity.x, 0, _controller.velocity.z).magnitude;
        var airMultiplier = _controller.isGrounded ? 1f : airborneFovMultiplier;
        var targetFov = _baseFov + (horizontalSpeed / moveSpeed) * 10f * airMultiplier;
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFov, Time.deltaTime * fovLerpSpeed);
    }

    private void HandleLook()
    {
        var look = lookAction.action.ReadValue<Vector2>();
        var mouseX = look.x * mouseSensitivity * Time.deltaTime;
        var mouseY = look.y * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);
        
        _pitch -= mouseY;
        _pitch = Mathf.Clamp(_pitch, -maxPitch, maxPitch);
        playerCamera.transform.localRotation = Quaternion.Euler(_pitch, 0, 0);
    }

    private void HandleMovement()
    {
        var movementInput = moveAction.action.ReadValue<Vector2>();
        
        var move = transform.right * movementInput.x + transform.forward * movementInput.y;
        _currentVelocity.x = move.x * moveSpeed;
        _currentVelocity.z = move.z * moveSpeed;
    }
    
    private void HandleJumpAndGravity()
    {
        if (_jumpBufferCounter > 0f)
            _jumpBufferCounter -= Time.deltaTime;
        
        if (jumpAction.action.triggered)
            _jumpBufferCounter = jumpBufferTime;
        
        if (!_controller.isGrounded) 
        {
            _currentVelocity.y -= gravity * Time.deltaTime;
            return;
        }

        if (!(_jumpBufferCounter > 0f)) return;
        
        _currentVelocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        _jumpBufferCounter = 0f;
    }

    private void HandleFootsteps()
    {
        if (!_controller.isGrounded || _controller.velocity.magnitude < 0.1f) return;

        if (footstepAudioSource.isPlaying) return;
        
        footstepAudioSource.PlayOneShot(footstepClips[Random.Range(0, footstepClips.Length)]);
        footstepAudioSource.pitch = Random.Range(0.9f, 1.1f);
        footstepAudioSource.panStereo = Random.Range(-0.2f, 0.2f);
    }
}
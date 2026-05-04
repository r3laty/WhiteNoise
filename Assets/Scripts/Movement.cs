using UnityEngine;
public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody _rb;
    private Vector2 _input;
    private bool _isJumped;
    private bool _jumpHeld;
    private bool _isGrounded;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundLayer);
    }
    private void FixedUpdate()
    {
        Walking();
        Jump();
    }
    private void Walking()
    {
        Vector3 move = transform.right * _input.x + transform.forward * _input.y;

        Vector3 walk = new Vector3(move.x * walkSpeed * Time.fixedDeltaTime, _rb.linearVelocity.y, move.z * walkSpeed* Time.fixedDeltaTime);

        _rb.linearVelocity = walk;
    }
    private void Jump()
    {
        if (_isJumped && _isGrounded)
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z); // сброс Y
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("JUMPED");
            _isJumped = false;
        }

        if (_rb.linearVelocity.y < 0)
        {
            _rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (_rb.linearVelocity.y > 0 && !_jumpHeld)
        {
            _rb.linearVelocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }
    public void GetWalk(Vector2 input)
    {
        _input = input;
    }
    public void GetJump(bool isPressed)
    {
        if (isPressed)
        {
            _isJumped = true;
            _jumpHeld = true;
        }
        else
        {
            _isJumped = false;
            _jumpHeld = false;
        }

    }
}

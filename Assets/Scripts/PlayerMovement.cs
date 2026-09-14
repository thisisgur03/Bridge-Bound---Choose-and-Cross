using Unity.Collections.Tests.CoreCLR.TestJobs;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movespeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpForce;

    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;

    private Rigidbody rigidbody;
    private Vector2 moveInput;
    private bool isGrounded;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
    }

    private void Start()
    {
        
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
    }

    private void Update()
    {
        moveInput = moveAction.action.ReadValue<Vector2>();

        if(jumpAction.action.WasPerformedThisFrame() && isGrounded)
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();

        rigidbody.angularVelocity = Vector3.zero;
    }

    private void Move()
    {
        Vector3 velocity = rigidbody.linearVelocity;

        velocity.x = moveInput.x * movespeed;
        velocity.z = moveInput.y * movespeed;

        rigidbody.linearVelocity = velocity;
    }

    private void Rotate()
    {
        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y);

        if (direction.sqrMagnitude < 0.01f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        rigidbody.MoveRotation(Quaternion.Slerp(rigidbody.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
    }
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
public class BallPlayer : MonoBehaviour
{
    [SerializeField, Range(0, 50)] float moveForce = 3f;
    [SerializeField, Range(0, 50)] float jumpForce = 3f;
    [SerializeField] Transform view;

    [Header("Ground Collision")]
    [SerializeField, Range(0, 5)] float rayLength = 1;
    [SerializeField] LayerMask groundLayer = Physics.AllLayers;

    Rigidbody rb;
    Vector2 moveInput;


    InputAction moveAction;
    InputAction jumpAction;

    void Awake()
    {
        //if view isn't set, get main camera
        view ??= Camera.main.transform;

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        TryGetComponent<Rigidbody>(out rb);
    }


    private void OnEnable()
    {
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        jumpAction.started += OnJump;
    }
    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        jumpAction.started -= OnJump;
    }
    private void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * rayLength, Color.red);
    }
    void FixedUpdate()
    {
        //convert controller space to world space
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
        //convert movement to view space
        movement = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up) * movement;
        rb.AddForce(movement * moveForce, ForceMode.Force);
    }
    private void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }
    private void OnJump(InputAction.CallbackContext ctx)
    {
        if (OnGround())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void Start()
    {

    }

    bool OnGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, rayLength, groundLayer);
    }


}

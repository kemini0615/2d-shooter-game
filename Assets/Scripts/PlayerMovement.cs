using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    private Vector2 moveDirection;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    private void Update()
    {
        SetMoveDirection();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void SetMoveDirection()
    {
        moveDirection = playerInputActions.Movement.Move.ReadValue<Vector2>().normalized;
    }

    private void MovePlayer()
    {
        rb.MovePosition(rb.position + moveDirection * (speed * Time.fixedDeltaTime));
    }
}

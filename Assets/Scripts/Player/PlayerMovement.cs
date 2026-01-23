using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private Rigidbody2D rb;
    private SpriteRenderer playerSpriteRenderer;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuraion = 0.3f;
    [SerializeField] private float alpha = 0.1f;

    private Vector2 moveDirection;
    public Vector2 MoveDirection => moveDirection;
    private float currentSpeed;
    private bool canDash;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        currentSpeed = moveSpeed;
        canDash = true;
        // Button 타입 액션에서는 입력 이벤트에 핸들러를 등록한다.
        playerInputActions.Movement.Dash.performed += ctx => Dash();
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
        Flip();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    /// <summary>
    /// 유저 입력에 따라 이동 방향을 설정한다.
    /// </summary>
    private void SetMoveDirection()
    {
        // Value 타입 액션에서는 입력값을 출력값으로 변환한다.
        moveDirection = playerInputActions.Movement.Move.ReadValue<Vector2>().normalized;
    }

    /// <summary>
    /// 설정한 이동 방향으로 플레이어를 움직인다.
    /// </summary>
    private void MovePlayer()
    {
        // 콜리전(Collision)과 트리거(Trigger) 이벤트는 발생하지만, 다른 물리 법칙(예: 중력, 마찰, 관성 등)은 무시한다.
        rb.MovePosition(rb.position + moveDirection * (currentSpeed * Time.fixedDeltaTime));
    }

    /// <summary>
    /// 대시 이동할 때 플레이어의 투명도를 변경한다.
    /// </summary>
    /// <param name="alpha">투명도</param>
    private void SetPlayerAlpha(float alpha)
    {
        Color initialColor = playerSpriteRenderer.color;
        Color newColor = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
        playerSpriteRenderer.color = newColor;
    }

    /// <summary>
    /// 대시가 가능한 상태라면 대시 이동한다.
    /// </summary>
    private void Dash()
    {
        if (!canDash)
            return;

        StartCoroutine(DashCoroutine(dashDuraion));
    }

    /// <summary>
    /// 대시 지속 시간 동안 플레이어의 투명도를 낮추고 스피드를 높인다.
    /// 대시 지속 시간이 끝나면 플레이어의 투명도와 스피드를 되돌린다.
    /// </summary>
    /// <returns>코루틴</returns>
    private IEnumerator DashCoroutine(float duration)
    {
        canDash = false;
        currentSpeed = dashSpeed;
        SetPlayerAlpha(alpha);

        yield return new WaitForSeconds(duration);

        currentSpeed = moveSpeed;
        SetPlayerAlpha(1f);
        canDash = true;
    }

    /// <summary>
    /// 플레이어 이동 방향에 따라 플립한다.
    /// </summary>
    private void Flip()
    {
        if (moveDirection.x == 0f)
            return;
        
        playerSpriteRenderer.flipX = !(moveDirection.x > 0f);
    }
}

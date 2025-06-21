using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;               // �������� ��������
    public float jumpForce = 10f;              // ���� ������
    public Transform groundCheck;              // ����� ��� �������� �����
    public float groundCheckRadius = 0.2f;     // ������ ��������
    public LayerMask groundLayer;              // ��� ��������� �����

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // �������� ��������
        float moveX = Input.GetAxisRaw("Horizontal");
        moveInput = new Vector2(moveX, 0).normalized;

        // �������� ������� �����
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // ������
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        // ���������� ������ groundCheck � ���������
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}


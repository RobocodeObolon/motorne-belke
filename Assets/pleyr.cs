using System.Collections.Specialized;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI; // �� ������ ���������� ��� UI

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;               // �������� ��������
    public float jumpForce = 10f;              // ���� ������
    public Transform groundCheck;              // ����� ��� �������� �����
    public float groundCheckRadius = 0.2f;     // ������ ��������
    public LayerMask groundLayer;              // ��� ��������� �����

    public Text coinText;                      // UI ����� ��� ����������� �����
    public AudioClip coinSound;                // ���� ��� ����� ������

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;
    private int coinCount = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateCoinUI();
    }

    private void Update()
    {
        // �������� ��������
        float moveX = Input.GetAxisRaw("Horizontal");
        moveInput = new Vector2(moveX, 0).normalized;
        transform.Translate(moveInput * moveSpeed * Time.deltaTime);

        // �������� ������� �����
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // ������
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            coinCount++;
            UpdateCoinUI();

            if (coinSound != null)
                AudioSource.PlayClipAtPoint(coinSound, transform.position);

            Destroy(collision.gameObject);
        }
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }
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



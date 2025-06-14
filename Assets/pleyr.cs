using System.Collections.Specialized;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI; // Не забудь подключить для UI

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;               // Скорость движения
    public float jumpForce = 10f;              // Сила прыжка
    public Transform groundCheck;              // Точка для проверки земли
    public float groundCheckRadius = 0.2f;     // Радиус проверки
    public LayerMask groundLayer;              // Что считается землёй

    public Text coinText;                      // UI текст для отображения монет
    public AudioClip coinSound;                // Звук при сборе монеты

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
        // Получаем движение
        float moveX = Input.GetAxisRaw("Horizontal");
        moveInput = new Vector2(moveX, 0).normalized;
        transform.Translate(moveInput * moveSpeed * Time.deltaTime);

        // Проверка касания земли
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Прыжок
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
        // Отображаем радиус groundCheck в редакторе
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}




using UnityEngine;
using UnityEngine.SceneManagement; // Для перезапуска уровня

public class FollowPlayer : MonoBehaviour
{
    public Transform player;      // Ссылка на игрока
    public float moveSpeed = 3f;  // Скорость следования

    void Update()
    {
        if (player != null)
        {
            // Рассчитываем направление к игроку
            Vector2 direction = (player.position - transform.position).normalized;

            // Двигаем врага к игроку
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Игрок пойман!");

            // Способ 1: Уничтожить игрока
            // Destroy(other.gameObject);

            // Способ 2: Перезапустить сцену
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}



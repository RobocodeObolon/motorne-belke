
using UnityEngine;
using UnityEngine.SceneManagement; // ��� ����������� ������

public class FollowPlayer : MonoBehaviour
{
    public Transform player;      // ������ �� ������
    public float moveSpeed = 3f;  // �������� ����������

    void Update()
    {
        if (player != null)
        {
            // ������������ ����������� � ������
            Vector2 direction = (player.position - transform.position).normalized;

            // ������� ����� � ������
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("����� ������!");

            // ������ 1: ���������� ������
            // Destroy(other.gameObject);

            // ������ 2: ������������� �����
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}



using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public int coinCount = 0;
    public Text coinText; // ��������� � ���������� UI Text ��� ����������� �����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            coinCount++;
            UpdateCoinUI();
            Destroy(other.gameObject);
        }
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }
    }
}

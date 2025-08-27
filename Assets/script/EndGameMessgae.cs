using UnityEngine;
using UnityEngine.UI;

public class EndGameHandler : MonoBehaviour
{
    [SerializeField] private Text endGameMessage; // Text UI để hiển thị thông báo

    private void Start()
    {
        // Ẩn thông báo khi bắt đầu
        if (endGameMessage != null)
        {
            endGameMessage.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Hiển thị thông báo
            if (endGameMessage != null)
            {
                endGameMessage.gameObject.SetActive(true);
                endGameMessage.text = "Trò chơi đã kết thúc và cám ơn các bạn đã chơi nó. mong bạn đã có những trải nghiệm tuyệt vời cùng với trò chơi này. Trò chơi sẽ được cập nhật trong tương lai với với nhiều nhân vật mới, quái vật mới hay mở rộng trên các nền tảng khác.";
            }
        }
    }
}

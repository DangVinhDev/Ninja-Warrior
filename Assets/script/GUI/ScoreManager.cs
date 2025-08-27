using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    [Header("Floating Text")]
    public GameObject floatingTextPrefab;  // Prefab TextMeshPro có gắn FloatingText.cs

    private int currentScore = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    public void AddScore(int amount) // +điểm
    {
        currentScore += amount;
        if (scoreText != null) scoreText.text = "Quái đã hạ: " + currentScore;
    }

    public void SpawnFloatingScore(Vector3 worldPos, int amount, Color color) //spawn điểm bay lên ngẫu nhiên
    {
        if (floatingTextPrefab == null) return;
        var go = Instantiate(floatingTextPrefab, worldPos, Quaternion.identity);
        var ft = go.GetComponent<FloatingText>();
        if (ft != null) ft.SetText("+" + amount, color);
    }
}

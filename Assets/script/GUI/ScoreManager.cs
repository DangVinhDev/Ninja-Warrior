using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    [Header("Floating Text UI")]
    public Canvas uiCanvas;                        // Canvas UI (Screen Space - Overlay)
    public RectTransform floatingParent;          // Optional: một Panel để chứa các text (nếu để trống sẽ dùng canvas root)
    public GameObject floatingTextUIPrefab;       // Prefab có TextMeshProUGUI + FloatingTextUI.cs

    private int currentScore = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        if (scoreText != null) scoreText.text = "Score: " + currentScore;
    }

    // Spawn floating UI theo vị trí world
    public void SpawnFloatingScoreUI(Vector3 worldPos, int amount, Color color)
    {
        if (uiCanvas == null || floatingTextUIPrefab == null) return;

        // World -> Screen
        Vector2 screen = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPos);

        // Screen -> Canvas local (Overlay => camera = null)
        RectTransform root = floatingParent != null ? floatingParent : uiCanvas.transform as RectTransform;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            root, screen, null, out Vector2 localPt
        );

        GameObject go = Instantiate(floatingTextUIPrefab, root);
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.anchoredPosition = localPt;

        var ft = go.GetComponent<FloatingTextUI>();
        if (ft != null) ft.SetText("+" + amount, color);
    }
}

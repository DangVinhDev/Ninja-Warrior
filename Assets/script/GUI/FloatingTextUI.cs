using TMPro;
using UnityEngine;

public class FloatingTextUI : MonoBehaviour
{
    [Header("Motion")]
    public float lifetime = 1.2f;
    public float upSpeed = 60f;                 // px/giây (UI đơn vị pixel)
    public Vector2 horizontalDrift = new Vector2(20f, 45f); // px/giây, random trái/phải
    public float wobbleFrequency = 6f;
    public float wobbleAmplitude = 6f;          // px

    [Header("Visual")]
    public Vector2 startScale = new Vector2(0.9f, 1.1f);
    public AnimationCurve alphaCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    private TextMeshProUGUI tmp;
    private RectTransform rt;
    private float t;
    private float dir;
    private float hDrift;
    private float baseScale;
    private Vector2 startAnchoredPos;

    void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        rt = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        t = 0f;
        startAnchoredPos = rt.anchoredPosition;

        dir = Random.value < 0.5f ? -1f : 1f;
        hDrift = Random.Range(horizontalDrift.x, horizontalDrift.y) * dir;

        baseScale = Random.Range(startScale.x, startScale.y);
        rt.localScale = Vector3.one * baseScale;
    }

    void Update()
    {
        t += Time.deltaTime;
        float n = Mathf.Clamp01(t / lifetime);

        float up = upSpeed * t;
        float wobble = Mathf.Sin(t * wobbleFrequency) * wobbleAmplitude;

        rt.anchoredPosition = startAnchoredPos + new Vector2(hDrift * t + wobble, up);

        var c = tmp.color;
        c.a = alphaCurve.Evaluate(n);
        tmp.color = c;

        if (t >= lifetime) Destroy(gameObject);
    }

    public void SetText(string text, Color color)
    {
        tmp.text = text;
        tmp.color = color;
    }
}

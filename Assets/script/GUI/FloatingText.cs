using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [Header("Motion")]
    public float lifetime = 1.2f;                  // time tồn tại
    public float baseUpSpeed = 1.8f;               // tốc độ bay thẳng
    public Vector2 horizontalDrift = new Vector2(0.1f, 1f); // lệch ngang (min,max)
    public float wobbleFrequency = 6f;             // tần số lắc
    public float wobbleAmplitude = 0.05f;          // biên độ lắc

    [Header("Visual")]
    public Vector2 startScale = new Vector2(0.9f, 1.1f);      // random scale
    public AnimationCurve alphaCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    private TextMeshPro tmp;
    private float t;
    private float dir;       // hướng ngang: -1 trái +1 phải
    private float hDrift;    // lực trượt ngang
    private float baseScale;
    private Vector3 origin;
    private Camera cam;

    void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
        cam = Camera.main;
    }

    void OnEnable()
    {
        t = 0f;
        origin = transform.position;

        // random hướng & độ lệch ngang
        dir = Random.value < 0.5f ? -1f : 1f;
        hDrift = Random.Range(horizontalDrift.x, horizontalDrift.y) * dir;

        // random scale ban đầu
        baseScale = Random.Range(startScale.x, startScale.y);
        transform.localScale = Vector3.one * baseScale;
    }

    void Update()
    {
        t += Time.deltaTime;
        float normalized = Mathf.Clamp01(t / lifetime);

        // vị trí: lên + trôi ngang + lắc lư nhẹ
        float up = baseUpSpeed * t;
        float wobble = Mathf.Sin(t * wobbleFrequency) * wobbleAmplitude;
        Vector3 pos = origin + new Vector3(hDrift * t + wobble, up, 0f);
        transform.position = pos;

        // billboard: chữ luôn nhìn về camera (dùng TextMeshPro 3D)
        if (cam != null) transform.forward = cam.transform.forward;

        // mờ dần theo curve
        Color c = tmp.color;
        c.a = alphaCurve.Evaluate(normalized);
        tmp.color = c;

        if (t >= lifetime)
            Destroy(gameObject);
    }

    public void SetText(string text, Color color)
    {
        tmp.text = text;
        tmp.color = color;
    }
}

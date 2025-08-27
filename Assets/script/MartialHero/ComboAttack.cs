using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    Animator ani;

    public int combo;
    public int comboNumber;

    public bool attacking;

    public float comboTiming;

    public float comboTempo;

    [SerializeField] private LayerMask enemyLayer; // Layer của quái vật
    [SerializeField] private float attackRange = 1f; // Phạm vi tấn công
    [SerializeField] private int attackDamage = 20; // Sát thương tấn công
    private int facingDirection = 1; // Hướng mà người chơi đang nhìn, 1 = phải, -1 = trái

    // Thêm biến tham chiếu đến AudioManager
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        if (ani == null)
        {
            Debug.LogError("Animator component is missing on this GameObject");
        }
        combo = 1;
        comboTiming = 0.5f;
        comboTempo = comboTiming;
        comboNumber = 2;

        // Tìm và gán tham chiếu đến AudioManager
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager is missing in the scene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Combo();
        // Cập nhật hướng nhân vật đang nhìn
        if (transform.localScale.x > 0)
        {
            facingDirection = 1;
        }
        else if (transform.localScale.x < 0)
        {
            facingDirection = -1;
        }
    }

    public void Combo()
    {
        // Giảm combo temp theo thời gian khung hình time.deltatime
        comboTempo -= Time.deltaTime;

        // Nếu có lệnh tấn công và comboTempo < 0, thực hiện combo đầu tiên
        if (Input.GetKeyDown(KeyCode.Q) && comboTempo < 0)
        {
            // Bật trạng thái tấn công
            attacking = true;

            // Kích hoạt animation tấn công
            if (ani != null)
            {
                ani.SetTrigger("Attack" + combo);
            }
            else
            {
                Debug.LogWarning("Animator is null, cannot set trigger");
            }

            // Tấn công quái vật nếu nằm trong tầm
            Attack();

            // Phát âm thanh tấn công
            if (audioManager != null)
            {
                audioManager.PlaySFX(audioManager.attackClip);
            }

            // Reset comboTempo
            comboTempo = comboTiming;
        }
        // Nếu có lệnh tấn công và trong thời gian cho phép để kích hoạt combo tiếp theo
        else if (Input.GetKeyDown(KeyCode.Q) && comboTempo > 0 && comboTempo < 0.5f)
        {
            // Bật trạng thái tấn công
            attacking = true;

            // Tăng giá trị biến đếm combo và kiểm tra xem đã vượt giới hạn combo chưa
            combo++;

            // Nếu đã đạt giới hạn combo thì set combo về 1
            if (combo > comboNumber)
            {
                combo = 1;
            }

            // Kích hoạt animation tấn công tiếp theo
            if (ani != null)
            {
                ani.SetTrigger("Attack" + combo);
            }
            else
            {
                Debug.LogWarning("Animator is null, cannot set trigger");
            }

            // Tấn công quái vật nếu nằm trong tầm
            Attack();

            // Phát âm thanh tấn công
            if (audioManager != null)
            {
                audioManager.PlaySFX(audioManager.attackClip);
            }

            // Thiết lập lại giá trị của combo timing
            comboTempo = comboTiming;
        }
        // Nếu không có lệnh tấn công nào được thực hiện và comboTempo < 0, tắt trạng thái tấn công
        else if (comboTempo < 0 && !Input.GetKeyDown(KeyCode.Q))
        {
            attacking = false;
        }

        // Nếu comboTempo bé hơn 0, thì reset giá trị combo về 1 để bắt đầu thực hiện combo tới
        if (comboTempo < 0)
        {
            combo = 1;
        }
    }

    private void Attack()
    {
        // Kiểm tra các đối tượng trong phạm vi tấn công
        Vector2 attackDirection = new Vector2(facingDirection, 0); // Hướng tấn công

        // Tạo nửa hình tròn bằng cách sử dụng Physics2D.OverlapCircleAll
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        // Gây sát thương cho từng đối tượng trong nửa hình tròn phía trước
        foreach (Collider2D enemy in hitEnemies)
        {
            Vector2 directionToEnemy = (enemy.transform.position - transform.position).normalized;

            // Kiểm tra xem quái vật có nằm trong nửa hình tròn phía trước không
            if (Vector2.Dot(attackDirection, directionToEnemy) > 0)
            {
                IDamageable damageable = enemy.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(attackDamage);
                }
                else
                {
                    Debug.LogWarning("IDamageable component is missing on enemy");
                }
            }
        }
    }

    // Vẽ phạm vi tấn công trong Scene view để kiểm tra
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

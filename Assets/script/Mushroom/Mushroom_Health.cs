using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mushroom_Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public HealthBar healthBar;
    private Animator anim;
    private EnemyPatrol enemyPatrol; // Đổi từ Mushroom_Patrol sang EnemyPatrol
    public UnityEvent OnDeath;

    private void OnEnable()
    {
        OnDeath.AddListener(Death);
    }

    private void OnDisable()
    {
        OnDeath.RemoveListener(Death);
    }

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponent<EnemyPatrol>();

        // Cập nhật thanh máu nếu có
        if (healthBar != null)
        {
            healthBar.UpdateBar(currentHealth, maxHealth, "HP");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDeath.Invoke();
        }
        else
        {
            if (anim != null)
            {
                anim.SetTrigger("takehit"); // Kích hoạt animation bị đánh
            }
        }

        // Cập nhật thanh máu nếu có
        if (healthBar != null)
        {
            healthBar.UpdateBar(currentHealth, maxHealth, "HP");
        }
    }

    private void Death()
    {
        if (anim != null)
        {
            anim.SetTrigger("die"); // Kích hoạt animation chết
        }

        // Vô hiệu hóa EnemyPatrol
        if (enemyPatrol != null)
        {
            Destroy(enemyPatrol);
        }

        // Thêm logic để xử lý khi quái vật chết, ví dụ: hủy đối tượng sau một thời gian
        Destroy(gameObject, 0.5f); // Hủy đối tượng sau 0.5 giây
    }
}

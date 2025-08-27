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
    private EnemyPatrol enemyPatrol;
    public UnityEvent OnDeath;

    [Header("Score Settings")]
    public int scoreOnDeath = 100;

    [Header("Floating Text")]
    public Vector3 floatingOffset = new Vector3(0f, 0.6f, 0f); // vị trí lệch khi spawn chữ

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

        if (healthBar != null)
            healthBar.UpdateBar(currentHealth, maxHealth, "HP");
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
            if (anim != null) anim.SetTrigger("takehit");
        }

        if (healthBar != null)
            healthBar.UpdateBar(currentHealth, maxHealth, "HP");
    }

    private void Death()
    {
        if (anim != null) anim.SetTrigger("die");
        if (enemyPatrol != null) Destroy(enemyPatrol);

        // Cộng điểm vs hiện floating text
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(scoreOnDeath);
            ScoreManager.Instance.SpawnFloatingScore(transform.position + floatingOffset, scoreOnDeath, Color.yellow);
        }

        Destroy(gameObject, 0.1f);
    }
}

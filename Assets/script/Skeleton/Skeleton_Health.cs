using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Skeleton_Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public HealthBar healthBar;
    private Animator anim;
    private EnemyPatrol enemyPatrol;
    public UnityEvent OnDeath;

    [SerializeField] private float shieldChance = 0.25f; // Xác suất chống đỡ

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
        {
            healthBar.UpdateBar(currentHealth, maxHealth, "HP");
        }
    }

    public void TakeDamage(int damage)
    {
        if (Random.value < shieldChance)
        {
            // Chống đỡ thành công
            if (anim != null)
            {
                anim.SetTrigger("Shield");
            }
            return;
        }

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
                anim.SetTrigger("takehit");
            }
        }

        if (healthBar != null)
        {
            healthBar.UpdateBar(currentHealth, maxHealth, "HP");
        }
    }

    private void Death()
    {
        if (anim != null)
        {
            anim.SetTrigger("die");
        }

        if (enemyPatrol != null)
        {
            Destroy(enemyPatrol);
        }

        Destroy(gameObject, 0.5f);
    }
}

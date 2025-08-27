using UnityEngine;
using UnityEngine.Events;

public class Hero_Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public HealthBar healthBar;
    private Animator anim;
    public UnityEvent OnDeath;

    public GameManagerScript gameManager;

    private void OnEnable()
    {
        OnDeath.AddListener(Death);
    }

    private void OnDisable()
    {
        OnDeath.RemoveListener(Death);
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
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
            anim.SetTrigger("takehit");
        }

        healthBar.UpdateBar(currentHealth, maxHealth, "HP");
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.UpdateBar(currentHealth, maxHealth, "HP");
    }

    private void Death()
    {
        anim.SetTrigger("die");
        gameObject.SetActive(false);
        gameManager.gameOver();
    }
}

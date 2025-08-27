using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float collideDistance;
    [SerializeField] private int damage;
    [SerializeField] private float criticalChance = 0.2f; // Xác suất chí mạng
    [SerializeField] private float criticalMultiplier = 1.2f; // Hệ số sát thương chí mạng
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private Hero_Health playerHealth;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("Attack1");
            }
        }
        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * collideDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, transform.right, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Hero_Health>();
        return hit.collider != null;
    }

    // Hàm gây sát thương cho người chơi, được gọi trong Animation Event
    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            if (Random.value < criticalChance)
            {
                playerHealth.TakeDamage(Mathf.RoundToInt(damage * criticalMultiplier));
            }
            else
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    // Vẽ phạm vi tấn công trong Scene view để kiểm tra
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * collideDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}

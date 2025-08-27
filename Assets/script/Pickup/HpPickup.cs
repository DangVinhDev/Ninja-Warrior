using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPickup : MonoBehaviour
{
    public int healthAmount = 10; // Lượng máu hồi khi va chạm

    void OnTriggerEnter2D(Collider2D other)
    {
        Hero_Health heroHealth = other.GetComponent<Hero_Health>();
        if (heroHealth != null)
        {
            heroHealth.Heal(healthAmount);
            Destroy(gameObject); // Hủy đối tượng sau khi đã hồi máu
        }
    }
}

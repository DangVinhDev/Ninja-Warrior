using System.Collections;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    public int maxMana = 50;
    public int currentMana;
    public float manaRegenRate = 7f; // Thời gian hồi mana
    public int manaRegenAmount = 5; // Số lượng mana hồi mỗi lần

    private Coroutine regenCoroutine;
    public ManaBar manaBar; // Thanh mana

    void Start()
    {
        currentMana = maxMana;
        manaBar.UpdateBar(currentMana, maxMana, "Mana");
        regenCoroutine = StartCoroutine(RegenerateMana());
    }

    public bool UseMana(int amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            manaBar.UpdateBar(currentMana, maxMana, "Mana");
            return true;
        }
        else
        {
            Debug.Log("Not enough mana!");
            return false;
        }
    }

    private IEnumerator RegenerateMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(manaRegenRate);
            currentMana = Mathf.Min(currentMana + manaRegenAmount, maxMana);
            manaBar.UpdateBar(currentMana, maxMana, "Mana");
        }
    }
}

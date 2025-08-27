using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Image bar;

    public void UpdateBar(int value, int maxValue, string text = null)
    {
        if (text == null)
        {
            healthText.text = value.ToString() + " / " + maxValue.ToString();
        }
        else
        {
            healthText.text = text + ": " + value.ToString() + " / " + maxValue.ToString();
        }
        bar.fillAmount = (float)value / (float)maxValue;
    }
}

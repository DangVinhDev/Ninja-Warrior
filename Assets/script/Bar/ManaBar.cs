using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public TextMeshProUGUI manaText;
    public Image bar;

    public void UpdateBar(int value, int maxValue, string text = null)
    {
        if (text == null)
        {
            manaText.text = value.ToString() + " / " + maxValue.ToString();
        }
        else
        {
            manaText.text = text + ": " + value.ToString() + " / " + maxValue.ToString();
        }
        bar.fillAmount = (float)value / (float)maxValue;
    }
}

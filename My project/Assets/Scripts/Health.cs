using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public float health;
    public TextMeshPro healthText;
    public Slider healthBar;
    public Image healthBarFill;
    public Color maxHealthColor;
    public Color midHealthColor;
    public Color lowHealthColor;

    void TakeDamage(float damage)
    {
        health -= damage;
        healthText.text = health.ToString();
        healthBar.value = health;

        if (health < 75f)
        {
            healthText.color = maxHealthColor;
            healthBarFill.color = maxHealthColor;
        }
        else if (health < 25f)
        {
            healthText.color = midHealthColor;
            healthBarFill.color = midHealthColor;
        }
        else
        {
            healthText.color = lowHealthColor;
            healthBarFill.color = lowHealthColor;
        }
    }
}

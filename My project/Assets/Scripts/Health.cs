using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public Score score;
    public float health;
    public TextMeshPro healthText;
    public Slider healthBar;
    public Image healthBarFill;
    public Color maxHealthColor;
    public Color midHealthColor;
    public Color lowHealthColor;
    public GameObject DeathScreen;

    public void Damage(float damage)
    {
        health -= damage;

        if (health <= 0f)
        {
            score.SetHighScore();
            DeathScreen.SetActive(true);
            Destroy(gameObject);
        }

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

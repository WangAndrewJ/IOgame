using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    private Score score;
    public float health;
    public TextMeshProUGUI healthText;
    public Slider healthBar;
    public Image healthBarFill;
    public Color maxHealthColor;
    public Color midHealthColor;
    public Color lowHealthColor;
    public GameObject Player;
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private Ctr ctr;

    private void Start()
    {
        score = GetComponent<Score>();
        spawner = FindObjectOfType<EnemySpawner>();
    }

    public void Damage(float damage)
    {
        health -= damage;

        if (health > 100)
        {
            health = 100;
        }

        if (health <= 0f)
        {
            spawner.players.Remove(transform);
            spawner.listOfCtr.Remove(ctr);
            spawner.UpdateScore();
            score.SetHighScore();
            Destroy(Player);
            SceneManager.LoadScene(1);
        }

        healthText.text = Mathf.Round(health).ToString();
        healthBar.value = health;

        if (health > 75f)
        {
            healthText.color = maxHealthColor;
            healthBarFill.color = maxHealthColor;
        }
        else if (health > 25f)
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

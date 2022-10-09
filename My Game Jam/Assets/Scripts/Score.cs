using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score = 0;
    float damageOverTime;
    public float damageOverTimeMultiplier = 0.01f;
    Health health;

    private void Start()
    {
        health = GetComponent<Health>();

        try
        {
            PlayerPrefs.GetInt("Highscore");
        }
        catch
        {
            PlayerPrefs.SetInt("Highscore", 0);
        }
    }

    public void SetHighScore()
    {
        if (score > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }

    public void AddScore(int addedScore)
    {
        score += addedScore;
        damageOverTime = score * damageOverTimeMultiplier;
    }

    private void Update()
    {
        health.Damage(damageOverTime * Time.deltaTime);
    }
}

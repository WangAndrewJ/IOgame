using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScore : MonoBehaviour
{
    public int score = 0;
    float damageOverTime;
    public float damageOverTimeMultiplier = 0.01f;
    private Ctr ctr;

    private void Start()
    {
        ctr = GetComponent<Ctr>();
    }

    public void AddScore(int addedScore)
    {
        score += addedScore;
        damageOverTime = score * damageOverTimeMultiplier;
    }

    private void Update()
    {
        Debug.Log(damageOverTime);
        ctr.aiHealth -= damageOverTime * Time.deltaTime;
    }
}

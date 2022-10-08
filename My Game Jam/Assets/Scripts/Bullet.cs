using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public Health health;
    [SerializeField] private Rigidbody2D rb2d;
    public GameObject shooter;
    public EnemyScore score;
    public EnemySpawner spawner;

    private void Start()
    {
        health = FindObjectOfType<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != shooter)
        {
            Debug.Log(collision.gameObject.name);

            try
            {
                Ctr ctr = collision.GetComponent<Ctr>();

                if (ctr.isPlayer)
                {
                    health.Damage(damage);
                }
                else
                {
                    if (ctr.aiHealth - damage <= 0f)
                    {
                        score.AddScore(1);
                        spawner.players.Remove(ctr.gameObject.transform);
                        Destroy(ctr.gameObject);
                        Debug.LogWarning(ctr.gameObject);
                        Debug.LogWarning("Killed AI");
                    }
                    else
                    {
                        ctr.aiHealth -= damage;
                        Debug.LogWarning("Did Damage");
                    }
                }
            }
            catch
            {
                Debug.Log("Hit Something Else");
            }
        }

        Destroy(gameObject);
    }

    public void AddForce(Vector3 direction, ForceMode2D forceMode2D)
    {
        rb2d.AddForce(direction, forceMode2D);
    }
}

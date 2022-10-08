using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public Health health;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private LayerMask playerLayer;

    private void Start()
    {
        health = FindObjectOfType<Health>();
        StartCoroutine(DestroySelf());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            Debug.Log("Hit");
            health.Damage(damage);
        }
    }

    private IEnumerator DestroySelf()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }

    public void AddForce(Vector3 direction, ForceMode2D forceMode2D)
    {
        rb2d.AddForce(direction, forceMode2D);
    }
}

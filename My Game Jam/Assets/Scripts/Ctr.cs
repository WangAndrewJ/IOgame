using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ctr : MonoBehaviour
{
    Vector2 Velocity;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb2d;
    Quaternion rotation;
    [SerializeField] GameObject player;
    [SerializeField] GameObject BulletPrefab;
    GameObject Bullet;
    [SerializeField] GameObject FirePoint;
    [SerializeField] int force;
    [SerializeField] Health healthScript;
    [SerializeField] Score scoreScript;
    [SerializeField] float aiHealth;
    [SerializeField] bool isPlayer;
    [SerializeField] int gainOnKill;
    [SerializeField] float maxDistance;
    [SerializeField] float aiShootingDistance;
    private float distanceFromPlayer;
    public float enemyDamage;

    // Start is called before the first frame update
    void Start()
    {
        healthScript.Damage(0);
        scoreScript.AddScore(1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPlayer)
        {
            Vector3 diff = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
            diff.Normalize();
            float rotation = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotation);
            rb2d.velocity = Velocity * Time.deltaTime;
        }
        else
        {
            Vector3 diff = player.transform.position - transform.position;
            diff.Normalize();
            float rotation = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotation);

            if (aiHealth <= 0)
            {
                healthScript.Damage(-gainOnKill);
                scoreScript.AddScore(1);
                Destroy(gameObject);
            }

            distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);

            if (distanceFromPlayer > maxDistance)
            {
                rb2d.velocity = -Vector2.MoveTowards(player.transform.position, transform.position, speed * Time.fixedDeltaTime);
            }
            else
            {
                rb2d.velocity = Vector3.zero;
            }

            if (!isPlayer && distanceFromPlayer <= aiShootingDistance)
            {
                Bullet = Instantiate(BulletPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
                Bullet enemyBullet = Bullet.GetComponent<Bullet>();
                enemyBullet.damage = enemyDamage;
                enemyBullet.health = healthScript;
                enemyBullet.AddForce(FirePoint.transform.up * force, ForceMode2D.Impulse);
            }
        }
    }

    void OnMove(InputValue Move)
    {
        if (isPlayer)
        {
            Velocity = new Vector2(Move.Get<Vector2>().x * speed, Move.Get<Vector2>().y * speed);
        }
    }

    void OnFire()
    {
        if (isPlayer)
        {
            Bullet = Instantiate(BulletPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
            Rigidbody2D br = Bullet.GetComponent<Rigidbody2D>();
            br.AddForce(FirePoint.transform.up * force, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isPlayer)
        {
            var go = other.gameObject;
            Destroy(go);
            aiHealth -= 20;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using TMPro;

public class Ctr : MonoBehaviour
{
    Vector2 Velocity;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb2d;
    Quaternion rotation;
    [SerializeField] GameObject BulletPrefab;
    GameObject Bullet;
    [SerializeField] GameObject FirePoint;
    [SerializeField] int force;
    public Health healthScript;
    public Score scoreScript;
    public float aiHealth;
    public bool isPlayer;
    [SerializeField] int gainOnKill;
    [SerializeField] float maxDistance;
    [SerializeField] float aiShootingDistance;
    private float distanceFromPlayer;
    public float enemyDamage;
    public float fireRate;
    private float nextTimeToFire;
    [SerializeField] private EnemyScore aiScore;
    public EnemySpawner spawner;
    Transform closestTarget;
    public string username;
    public TextMeshProUGUI usernameText;
    [SerializeField] private RectTransform usernameTextRectTransform;

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
            usernameText.text = username;
        }
        else
        {
            try
            {
                var closestTargets = spawner.players.OrderBy(o => (o.transform.position - transform.position).sqrMagnitude);

                closestTarget = closestTargets.Skip(1).Take(1).FirstOrDefault();
                Vector3 diff = closestTarget.transform.position - transform.position;
                diff.Normalize();
                float rotation = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rotation);

                if (aiHealth <= 0)
                {
                    healthScript.Damage(-gainOnKill);
                    scoreScript.AddScore(1);
                    spawner.players.Remove(transform);
                    Destroy(gameObject);
                }

                distanceFromPlayer = Vector3.Distance(closestTarget.transform.position, transform.position);

                if (distanceFromPlayer > maxDistance)
                {
                    rb2d.velocity = transform.right * speed / 50;
                    Debug.Log(rb2d.velocity);
                }
                else
                {
                    rb2d.velocity = Vector3.zero;
                }

                if (!isPlayer && distanceFromPlayer <= aiShootingDistance && Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1f / fireRate;
                    Bullet = Instantiate(BulletPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
                    Bullet enemyBullet = Bullet.GetComponent<Bullet>();
                    enemyBullet.damage = enemyDamage;
                    enemyBullet.health = healthScript;
                    enemyBullet.shooter = gameObject;
                    enemyBullet.score = aiScore;
                    enemyBullet.spawner = spawner;
                    enemyBullet.AddForce(FirePoint.transform.up * force, ForceMode2D.Impulse);
                }
            }
            catch
            {
                Debug.Log("Everyones Dead!");
            }
        }

        try
        {
            usernameTextRectTransform.rotation = Quaternion.identity;
        }
        catch { }
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
        if (!isPlayer && other.tag == "Player Bullet")
        {
            var go = other.gameObject;
            Destroy(go);
            aiHealth -= 20;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ctr : MonoBehaviour
{
    Vector2 Velocity;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb2d;
    Quaternion rotaion;
    [SerializeField] GameObject player;
    [SerializeField] GameObject BulletPrefab;
    GameObject Bullet;
    [SerializeField] GameObject FirePoint;
    [SerializeField] int force;
    Health healthScript;
    Score scoreScript;
    [SerializeField] float aiHealth;
    [SerializeField] bool isPlayer;
    [SerializeField] int gainOnKill;
    // Start is called before the first frame update
    void Start()
    {
        healthScript = GameObject.Find("/Health Script").GetComponent<Health>();
        healthScript.Damage(0);
        scoreScript = GameObject.Find("/Health Script").GetComponent<Score>();
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
        if (aiHealth <= 0 && !isPlayer)
        {
            healthScript.Damage(-gainOnKill);
            scoreScript.AddScore(1);
            Destroy(this.gameObject);
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
        if (isPlayer)
        {
            var go = other.gameObject;
            Destroy(go);
            healthScript.Damage(20);
        }
        else
        {
            var go = other.gameObject;
            Destroy(go);
            aiHealth -= 20;
        }
    }
}

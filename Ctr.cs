using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ctr : MonoBehaviour
{
    Vector2 Velocity;
    [SerializeField] float speed;
    [SerializeField]Rigidbody2D rb2d;
    Quaternion rotaion;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 diff=Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue())-transform.position;
        diff.Normalize();
        float rotation=Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation=Quaternion.Euler(0f, 0f, rotation);      
        rb2d.velocity=Velocity;
    }
    void OnMove(InputValue Move){
        Velocity= new Vector2(Move.Get<Vector2>().x*speed,Move.Get<Vector2>().y*speed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    GameObject Bullet;
    [SerializeField]GameObject BulletPrefab;
    [SerializeField]GameObject FirePoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnFire(){
        Bullet=Instantiate(BulletPrefab,FirePoint.transform.position ,FirePoint.transform.rotation);
        Rigidbody2D br=Bullet.GetComponent<Rigidbody2D>();
        br.AddForce(FirePoint.transform.up*10,ForceMode2D.Impulse);
    }
}

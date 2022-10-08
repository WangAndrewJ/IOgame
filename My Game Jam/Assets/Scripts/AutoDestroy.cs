using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float timeAlive = 2f;

    private void Start()
    {
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeAlive);
            Destroy(gameObject);
        }
    }
}

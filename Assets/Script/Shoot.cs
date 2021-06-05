using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;

    public float delay;

    //public float bSpeed;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine(ShotDelay());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StopAllCoroutines();
        }
    }

    IEnumerator ShotDelay()
    {
        yield return new WaitForSecondsRealtime(delay);
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
        StartCoroutine(ShotDelay());
        yield return null;
    }
}

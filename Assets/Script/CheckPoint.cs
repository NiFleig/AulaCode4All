using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public CircleCollider2D checkCollider;

    void Start()
    {
        checkCollider = gameObject.GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.GetComponent<Collider2D>().tag == "Player")
        {
            GameMaster.gm.checkpoints.Add(transform);

            gameObject.transform.parent.DetachChildren();

            checkCollider.enabled = false;
        }
    }
}

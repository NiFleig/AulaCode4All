using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPowerUp : MonoBehaviour
{
    public PowerUp powerToEnable;
    public PlayerState state;

    public SpriteRenderer sprite;
    public CircleCollider2D puCollider;

    void Start()
    {
        sprite.sprite = powerToEnable.sprite;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(powerToEnable.powerName == "Bear")
            {
                state.bearEquip = true;
            }
            else if(powerToEnable.powerName == "Chicken")
            {
                state.chickenEquip = true;
            }
            else if(powerToEnable.powerName == "Monkey")
            {
                state.monkeyEquip = true;
            }
            else if(powerToEnable.powerName == "Fish")
            {
                state.fishEquip = true;
            }
            StartCoroutine(EffectDelay());
        }
        Debug.Log("debug");
        //StartCoroutine(EffectDelay());
        
    }

    IEnumerator EffectDelay()
    {
        sprite = GetComponent<SpriteRenderer>();
        puCollider = GetComponent<CircleCollider2D>();

        sprite.enabled = false;
        puCollider.enabled = false;

        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}

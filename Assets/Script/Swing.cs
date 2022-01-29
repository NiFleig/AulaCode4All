﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    public float range = 20f;

    public float swingSpeed = 5;

    public Player player;
    public LayerMask layer;
    public Transform playerTransform;
    public GameObject swingPrefab;
    public bool isHooked; 
    [HideInInspector]
    public Transform swingPosition;

    private GameObject currentSwingObject;

    public float swingJumpX = 5;
    public float swingJumpY = 10;
    

    void Start()
    {
        
    }

    void Update()
    {
        if(player.currentState != Player.states.death)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                if(!isHooked)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, AimDirection(), range, layer);

                    if(hit.collider != null)
                    {
                        print(hit.collider.name);
                        isHooked = true;
                        currentSwingObject = InstantiateSwingObject(hit.point);
                        swingPosition = currentSwingObject.transform.Find("SwingPivot"); 
                        swingPosition.transform.position = playerTransform.position;
                    }
                }else
                {
                    var swingObjectVelocity = currentSwingObject.transform.Find("SwingPivot").GetComponent<Rigidbody2D>().velocity;
                    player.velocity = new Vector3(swingObjectVelocity.x * swingJumpX,1 * swingJumpY,0);

                    isHooked = false;
                    Destroy(currentSwingObject);
                }
            }
        }else
        {
            if(currentSwingObject != null)
            {
               Destroy(currentSwingObject); 
            }
        }
    
        Debug.DrawRay(transform.position, AimDirection(), Color.red);
    }

    GameObject InstantiateSwingObject(Vector2 position)
    {
        return Instantiate(swingPrefab, position, Quaternion.identity);
    }

    Vector3 AimDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return (mousePos - transform.position).normalized;
    }
}

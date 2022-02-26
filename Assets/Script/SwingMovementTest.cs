using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingMovementTest : MonoBehaviour
{
    public Transform PlayerTransform;
    public Rigidbody2D rb; 

    public float movementForce;
    public float startMovementForce;

    public float MinAngle = 60;
    public float MaxAngle = 120;

    public float HInput;


    // Start is called before the first frame update
    void Start()
    {
        startMovementForce = movementForce;
        PlayerTransform = GameObject.Find("Player").transform;
        print(PlayerTransform.name);
    }

    // Update is called once per frame
    void Update()
    {
        HInput = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        Vector2 direction = rb.velocity.normalized;
        //print(direction.);

        //var currentAngle = Vector2.Angle(transform.parent.position, PlayerTransform.position);
        //print("Fixed Update "+ transform.parent.name);
        Vector2 dir = (transform.parent.position - PlayerTransform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        print(angle);

        if (angle > MinAngle && angle < MaxAngle)
        {
            movementForce = startMovementForce;

            if (Input.GetKey(KeyCode.D))
            {
                if (direction.x > 0)
                    rb.AddForce(direction * (movementForce), ForceMode2D.Force);
                if (direction.x < 0)
                    rb.AddForce(direction * (-movementForce), ForceMode2D.Force);

            }
            if (Input.GetKey(KeyCode.A))
            {
                if (direction.x > 0)
                    rb.AddForce(direction * (-movementForce), ForceMode2D.Force);
                if (direction.x < 0)
                    rb.AddForce(direction * (movementForce), ForceMode2D.Force);
            }
        }
        else
        {
            //rb.AddForce(direction * (movementForce), ForceMode2D.Force); 
            //rb.AddForce(-(direction * movementForce), ForceMode2D.Force);

            movementForce = 0;
        }
        
        //print(HInput);
        //if(HInput != 0)
            //rb.velocity = new Vector2(HInput * movementForce, rb.velocity.y); 
    }
}

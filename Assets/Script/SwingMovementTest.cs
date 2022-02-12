using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingMovementTest : MonoBehaviour
{
    public Rigidbody2D rb; 
    public float movementForce;

    public float HInput;


    // Start is called before the first frame update
    void Start()
    {
        
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

        if (Input.GetKey(KeyCode.D))
        {
            if(direction.x > 0)
                rb.AddForce(direction * (movementForce), ForceMode2D.Force);
            if(direction.x < 0)
                rb.AddForce(direction * (-movementForce), ForceMode2D.Force);
            
        }
        if (Input.GetKey(KeyCode.A))
        {
            if(direction.x > 0)
                rb.AddForce(direction * (-movementForce), ForceMode2D.Force);
            if(direction.x < 0)    
                rb.AddForce(direction * (movementForce), ForceMode2D.Force);
        }
        
        //print(HInput);
        //if(HInput != 0)
            //rb.velocity = new Vector2(HInput * movementForce, rb.velocity.y); 
    }
}

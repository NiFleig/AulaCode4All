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
        if (Input.GetKeyDown(KeyCode.D))
            rb.AddForce(new Vector2(1 * movementForce, 0), ForceMode2D.Force);
        if(Input.GetKeyDown(KeyCode.A))
            rb.AddForce(new Vector2(-1 * movementForce, 0), ForceMode2D.Force);
        
        //print(HInput);
        //if(HInput != 0)
            //rb.velocity = new Vector2(HInput * movementForce, rb.velocity.y); 
    }
}

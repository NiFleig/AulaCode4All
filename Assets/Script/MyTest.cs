using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTest : MonoBehaviour
{
    public GameObject rocket;
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Raycastor();
        }

        Vector2 direction = (
            Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)) - transform.position).normalized;

        Debug.DrawRay(transform.position, direction, Color.red);
    }

    void Raycastor()
    {
        Vector2 direction = (transform.position - Input.mousePosition).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 5f);
        
    }

    Vector3 MouseDirection()
    {
        Vector3 direction;
        direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        direction.z = 0;
        direction.Normalize();
        return direction;
    }
}

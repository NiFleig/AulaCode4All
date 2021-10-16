using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    public float range = 20f;

    public LayerMask layer;

    public GameObject swing;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, AimDirection(), range, layer);

            if(hit.collider != null)
            {
                print(hit.collider.name);
                InstantiateSwingObject(hit.point);
            }
        }
        Debug.DrawRay(transform.position, AimDirection(), Color.red);
    }

    void InstantiateSwingObject(Vector2 position)
    {
        Instantiate(swing, position, Quaternion.identity);
    }

    Vector3 AimDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return (mousePos - transform.position).normalized;
    }
}

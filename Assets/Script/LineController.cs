using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public LineRenderer lr;
    public Transform child;
    // Start is called before the first frame update
    void Start()
    {
        lr.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(1, new Vector3(child.position.x, child.position.y, 0));
    }
}

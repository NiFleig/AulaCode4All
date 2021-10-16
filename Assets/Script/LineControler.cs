using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineControler : MonoBehaviour
{

    public LineRenderer lr;
    public Transform child;

    void Start()
    {
        lr.SetPosition(0,transform.position);
    }

    void Update()
    {
        lr.SetPosition(1, child.position);
    }
}

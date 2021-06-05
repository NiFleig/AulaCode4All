using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject destroyed;
    public void DestroyAction()
    {
        Instantiate(destroyed, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

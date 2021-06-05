using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject turretBase;

   // void Awake()
   // {

  //  }

    void Update()
    {
        if(turretBase == null)
        {
            Destroy(gameObject);
        }
    }
}

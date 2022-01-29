using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    public CharacterController controller;
    public Player player;

    public bool jump = false;
    public bool djump = false;

    void Start()
    {
        player = GetComponentInParent<Player>();
        controller = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        if(controller.info.down == false)
        {
            jump = true;
        }else
        {
            jump = false;
            djump = false;
        }
    }
}

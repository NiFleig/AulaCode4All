using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    public Player player;
    public CharacterController controller2D;

    public bool wSlide = false;
    public float wSlideSpeed = 2f;
    public float wSlideTime = .25f;
    public float wSlideStop;

    public int wDirection;

    public Vector2 wJump; //sem direção
    public Vector2 wHop;  //mesma
    public Vector2 wLeap; //oposto

    void Start()
    {
        player = GetComponentInParent<Player>();
        controller2D = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        wDirection = (controller2D.info.left) ? -1 : 1;

        if((controller2D.info.left || controller2D.info.right) && ! controller2D.info.down && player.velocity.y != 0)
        {
            wSlide = true;

            if(player.velocity.y < -wSlideSpeed)
            {
                player.velocity.y = -wSlideSpeed;
            }

            if(wSlideStop > 0)
            {
                player.smoothVelocity = 0;
                player.velocity.x = 0;

                if(player.input.x != wDirection && player.input.x != 0)
                {
                    wSlideStop -= Time.deltaTime;
                }else
                {
                    wSlideStop = wSlideTime;
                }
            }else
            {
                wSlideStop = wSlideTime;
            }          
        }else
        {
            wSlide = false;
        }
    }
}

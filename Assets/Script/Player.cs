using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CharacterController))]
public class Player : MonoBehaviour
{
    CharacterController controller2D;
    public WallJump wallJump;
    public DoubleJump doubleJump;
    public Attack attack;
    public WallClimb wallClimb;
    private PlayerState playerState;

    public Vector3 velocity;
    public Vector2 input;

    public float hSpeed;
    float vSpeed;
    float grav;

    public float jumpHeight = 4f;
    public float timeToFall = .4f;

    public float smoothVelocity;

    float airAcceleration = .2f;
    float groundAcceleration = .1f;

    public enum states {bear, chicken, monkey, fish, death, pause}

    public states currentState;

    public bool pause;

    public AudioSource jump;

    Animator[] animatorList;

    public Animator playerAnim;
    public Animator mainAnim;
    
    void Start()
    {
        controller2D = GetComponent<CharacterController>();
        playerState = GetComponent<PlayerState>();

        //wallJump = GetComponentInChildren<WallJump>();
        //doubleJump = GetComponentInChildren<DoubleJump>();
        //attack = GetComponentInChildren<Attack>();

        grav = -(jumpHeight * 2) / Mathf.Pow(timeToFall, 2);
        vSpeed = Mathf.Abs(grav) * timeToFall;

        pause = false;

        animatorList = new Animator[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            animatorList[i] = transform.GetChild(i).GetComponent<Animator>();
        }

        playerAnim = animatorList[0];
        mainAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if(controller2D.info.up || controller2D.info.down)
        {
            velocity.y = 0;
        }

        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(controller2D.info.down)
            {
                velocity.y = vSpeed;
                jump.Play();
            }else if(!controller2D.info.down && doubleJump.jump && !doubleJump.djump)
            {
                velocity.y = vSpeed;
                doubleJump.djump = true;
                jump.Play();

                playerAnim.SetBool("DoubleJump", true);
            }

            if(wallJump.wSlide)
            {
                if(input.x == 0)
                {
                    velocity.x = -wallJump.wDirection * wallJump.wJump.x;
                    velocity.y = wallJump.wJump.y;
                    jump.Play();
                    playerAnim.SetBool("WallJump", true);
                }else if(input.x == wallJump.wDirection)
                {
                    velocity.x = -wallJump.wDirection * wallJump.wHop.x;
                    velocity.y = wallJump.wHop.y;
                    jump.Play();
                    playerAnim.SetBool("WallJump", true);
                }else
                {
                    velocity.x = -wallJump.wDirection * wallJump.wLeap.x;
                    velocity.y = wallJump.wLeap.y;
                    jump.Play();
                    playerAnim.SetBool("WallJump", true);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.RightShift))
        {
            attack.Action();

            playerAnim.SetTrigger("Attacking");
        }

        if(Input.GetKey(KeyCode.RightShift) && wallClimb.wClimb)
        {
            velocity = new Vector2(velocity.x, 0);

            float wSpeed = input.y > 0 ? wallClimb.wClimbSpeed : 1;

            velocity = new Vector2(velocity.x, input.y * (hSpeed * wSpeed));

            playerAnim.SetBool("Climbing", true);
        }else
        {
            playerAnim.SetBool("Climbing", false);
        }

        if(Input.GetKeyDown(KeyCode.Alpha1) && playerState.bearEquip == true)
        {
            currentState = states.bear;
            playerAnim = animatorList[0];

            wallJump.wSlide = false;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && playerState.chickenEquip == true)
        {
            currentState = states.chicken;
            playerAnim = animatorList[1];

            wallJump.wSlide = false;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3) && playerState.monkeyEquip == true)
        {
            currentState = states.monkey;
            playerAnim = animatorList[2];
        }
        if(Input.GetKeyDown(KeyCode.Alpha4) && playerState.fishEquip == true)
        {
            currentState = states.fish;
            playerAnim = animatorList[3];

            wallJump.wSlide = false;
        }

        float targetVeloX = input.x * hSpeed;

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVeloX, ref smoothVelocity, (controller2D.info.down) ? groundAcceleration : airAcceleration);
        velocity.y += grav * Time.deltaTime;
        controller2D.Move(velocity * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            pause = !pause;
        }
    }
}

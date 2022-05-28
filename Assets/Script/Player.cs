using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CharacterController))]
public class Player : MonoBehaviour
{
    public CharacterController controller2D;
    public WallJump wallJump;
    public DoubleJump doubleJump;
    public Attack attack;
    public WallClimb wallClimb;
    public Swing swing;

    public Vector3 SwingSpotOffset;
    
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

    public enum states {naked, bear, chicken, monkey, fish, frog, death, pause}

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

        if(currentState != states.death)
        {
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

                //TODO
                if (swing.isHooked && controller2D.info.down)
                {
                    swing.distanceJoint2D.distance -= 7;
                }
            }

            if(Input.GetKeyDown(KeyCode.RightShift) && currentState == Player.states.bear)
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

            if(swing.isHooked == false)
            {
                if(Input.GetKeyDown(KeyCode.Alpha1) && playerState.bearEquip == true)
                {
                    currentState = states.bear;
                    playerAnim = animatorList[1];

                    wallJump.wSlide = false;
                }
                if(Input.GetKeyDown(KeyCode.Alpha2) && playerState.chickenEquip == true)
                {
                    currentState = states.chicken;
                    playerAnim = animatorList[2];

                    wallJump.wSlide = false;
                }
                if(Input.GetKeyDown(KeyCode.Alpha3) && playerState.monkeyEquip == true)
                {
                    currentState = states.monkey;
                    playerAnim = animatorList[3];
                }
                if(Input.GetKeyDown(KeyCode.Alpha4) && playerState.fishEquip == true)
                {
                    currentState = states.fish;
                    playerAnim = animatorList[4];

                    wallJump.wSlide = false;
                }
                if(Input.GetKeyDown(KeyCode.Alpha5) && playerState.frogEquip == true)
            {
                currentState = states.frog;
                playerAnim = animatorList[5];

                wallJump.wSlide = false;
                }
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

                //TODO
                if (swing.isHooked && controller2D.info.down)
                {
                    swing.distanceJoint2D.distance -= 7;
                }
            }

            if(Input.GetKeyDown(KeyCode.RightShift) && currentState == Player.states.bear)
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
                playerAnim = animatorList[1];

                wallJump.wSlide = false;
            }
            if(Input.GetKeyDown(KeyCode.Alpha2) && playerState.chickenEquip == true)
            {
                currentState = states.chicken;
                playerAnim = animatorList[2];

                wallJump.wSlide = false;
            }
            if(Input.GetKeyDown(KeyCode.Alpha3) && playerState.monkeyEquip == true)
            {
                currentState = states.monkey;
                playerAnim = animatorList[3];
            }
            if(Input.GetKeyDown(KeyCode.Alpha4) && playerState.fishEquip == true)
            {
                currentState = states.fish;
                playerAnim = animatorList[4];

                wallJump.wSlide = false;
            }

            float targetVeloX = input.x * hSpeed;

            velocity.x = Mathf.SmoothDamp(velocity.x, targetVeloX, ref smoothVelocity, (controller2D.info.down) ? groundAcceleration : airAcceleration);
            velocity.y += grav * Time.deltaTime;

            if (!swing.isHooked)
                controller2D.Move(velocity * Time.deltaTime);
            else
            {
                if(swing.swingPosition != null)
                {
                    //if (!controller2D.info.down)
                    if (swing.ShouldSwing)
                    {
                        //Movimento de balan�o
                        if (Vector2.Distance(transform.position, swing.swingPosition.position) > .25f)
                        {
                            var direction = (swing.swingPosition.position - transform.position).normalized;
                            controller2D.Move(direction * swing.SwingSpeed * Time.deltaTime, false);

                            transform.position = Vector3.Lerp(transform.position, swing.swingPosition.position + SwingSpotOffset, swing.SwingSpeed * Time.deltaTime);
                        }
                    }
                    else
                    {
                        //Movimento de andar (normal)
                        swing.distanceJoint2D.distance = Vector3.Distance(transform.position, swing.currentSwingObject.transform.position);
                        controller2D.Move(velocity * Time.deltaTime);
                        swing.swingPosition.transform.position = transform.position - SwingSpotOffset;
                        swing.swingPosition.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    }
                } 
            }

            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                pause = !pause;
            }
        }
    }

}

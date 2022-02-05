using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class CharacterController : MonoBehaviour
{
    BoxCollider2D collider;
    RayCastOrigins rayOrigins;
    public int hRay = 3;
    public int vRay = 5;

    float hSpacing;
    float vSpacing;

    const float rayOffSet = 0.02f;

    public LayerMask collisionMask;

    private bool facingR = true;

    public CollisionInfo info;

    public Player player;
    public PlayerState state;

    public bool EnableInvincibility;

    void Start()
    {
        player = GetComponent<Player>();
        state = GetComponent<PlayerState>();

        collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();

        info.direction = 1;
    }

    public void Move(Vector3 velocity)
    {
        UpdateRaycastOrigins();
        info.ResetInfo();

        if(velocity.x != 0)
        {
            info.direction = (int)Mathf.Sign(velocity.x);

            if(velocity.x > 0 && !facingR)
            {
                Flip();
            } else if(velocity.x < 0 && facingR)
            {
                Flip();
            }

            player.playerAnim.SetFloat("Speed", Mathf.Abs(velocity.x));
        }else
        {
            player.playerAnim.SetFloat("Speed", Mathf.Abs(velocity.x));
        }

        HorizontalCollision(ref velocity);
        
        if(velocity.y != 0)
        {
            VerticalCollision(ref velocity);
            player.playerAnim.SetFloat("VSpeed", velocity.y);
        }

        if(!info.down)
        {
            player.playerAnim.SetBool("Ground", false);
        }else
        {
            player.playerAnim.SetBool("Ground", true);

            player.playerAnim.SetBool("DoubleJump", false);
            player.playerAnim.SetBool("WallJump", false);
        }

        if(info.right || info.left)
        {
            player.playerAnim.SetBool("TouchWall", true);
            player.playerAnim.SetBool("WallJump", false);
        }else
        {
            player.playerAnim.SetBool("TouchWall", false);
        }

        transform.Translate(velocity);
    }

    void HorizontalCollision(ref Vector3 velocity)
    {
        float directionX = info.direction;

        float rayLenght = Mathf.Abs(velocity.x) + rayOffSet;

        if(Mathf.Abs(velocity.x) < rayOffSet)
        {
            rayLenght = rayOffSet * 2;
        }

        for(int i = 0; i < hRay; i++)
        {
            Vector2 originPoint = (directionX == -1) ? rayOrigins.botLeft : rayOrigins.botRight;
            originPoint += Vector2.up * (hSpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(originPoint, Vector2.right * directionX, rayLenght, collisionMask);

            Debug.DrawRay(originPoint, Vector2.right * -1, Color.blue);

            if(hit)
            {
                velocity.x = (hit.distance - rayOffSet) * directionX;

                rayLenght = hit.distance;

                info.left = directionX == -1;
                info.right = directionX == 1;
            }
        }
    }

     void VerticalCollision(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);

        float rayLenght = Mathf.Abs(velocity.y) + rayOffSet;

        for(int i = 0; i < vRay; i++)
        {
            Vector2 originPoint = (directionY == -1) ? rayOrigins.botLeft : rayOrigins.topLeft;
            originPoint += Vector2.right * (vSpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(originPoint, Vector2.up * directionY, rayLenght, collisionMask);

            Debug.DrawRay(originPoint, Vector2.up * -1, Color.blue);

            if(hit)
            {
                velocity.y = (hit.distance - rayOffSet) * directionY;

                rayLenght = hit.distance;

                info.up = directionY == 1;
                info.down = directionY == -1;
            }
        }
    }

    void UpdateRaycastOrigins()
    {
        //medir as "pontas" do colisor e subtrair por um valor mínimo pra que eles saíamde dentro do personagem
        Bounds colBounds = collider.bounds;
        
        //possibilita que funcione mesmo quando o player está no chão
        colBounds.Expand(rayOffSet * -2);

        //4 pontas do collider
        rayOrigins.topLeft = new Vector2(colBounds.min.x, colBounds.max.y);
        rayOrigins.topRight = new Vector2(colBounds.max.x, colBounds.max.y);
        rayOrigins.botLeft = new Vector2(colBounds.min.x, colBounds.min.y);
        rayOrigins.botRight = new Vector2(colBounds.max.x, colBounds.min.y);
    }

    void CalculateRaySpacing()
    {
        Bounds colBounds = collider.bounds;
        colBounds.Expand(rayOffSet * -2);

        hRay = Mathf.Clamp(hRay, 2, int.MaxValue);
        vRay = Mathf.Clamp(vRay, 2, int.MaxValue);

        hSpacing = colBounds.size.y / (hRay - 1);
        vSpacing = colBounds.size.x / (vRay - 1);
    }

    private void Flip()
    {
        facingR = !facingR;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Obstacle" && EnableInvincibility == false)
        {
            StartCoroutine(state.Death());
            player.currentState = Player.states.death;
            player.playerAnim.SetBool("IsDead", true);
            player.mainAnim.SetBool("IsDead", true);
        }
    }

    struct RayCastOrigins
    {
        public Vector2 topLeft;
        public Vector2 topRight;
        public Vector2 botLeft;
        public Vector2 botRight;
    }

    public struct CollisionInfo
    {
        public bool left;
        public bool right;
        public bool up;
        public bool down;

        public int direction;

        public void ResetInfo()
        {
            left = false;
            right = false;
            up = false;
            down = false;
        }
    }
}

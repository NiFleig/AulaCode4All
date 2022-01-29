using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClimb : MonoBehaviour
{
    public Player player;

    public LayerMask climbLayer;

    public bool wClimb;
    public float wClimbSpeed;

    private bool wSliding = false;
    public float wSlideSpeed;


    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    void Update()
    {
        wClimb = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(.2f,.9f), 0f, climbLayer);

        if(wClimb)
        {
            wSliding = true;

            if(player.velocity.y < -wSlideSpeed)
            {
                player.velocity.y = -wSlideSpeed;
            }
        }
    }

    void DrawGizmoSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(.2f,.9f));
    }
}

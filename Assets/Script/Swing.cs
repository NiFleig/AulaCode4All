using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    public DistanceJoint2D distanceJoint2D;
    public float ChangeDistanceSpeed;

    public Player player;
    public Transform playerTransform;
    public GameObject SwingPrefab;
    public float Range;
    public LayerMask LayerMask;

    public float SwingSpeed = 10;
    public bool isHooked;

    public float swingJumpForceX = 5;
    public float swingJumpForceY = 30;

    public float minPointDistance = 10;

    [HideInInspector]
    public Transform swingPosition;

    private GameObject currentSwingObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player.currentState != Player.states.death)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (!isHooked)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, AimDirection(), Range, LayerMask);
                    if (hit.collider != null)
                    {
                        print(hit.collider.name);
                        float hitPointDistance = Vector2.Distance(hit.point, playerTransform.position);

                        if (hit.point.y > playerTransform.position.y && hitPointDistance > minPointDistance)
                        {
                            currentSwingObject = InstantiateSwingObject(hit.point);
                            isHooked = true;
                            swingPosition = currentSwingObject.transform.Find("SwingPivot");
                            swingPosition.transform.position = playerTransform.position;
                        }
                    }
                }
                else
                {
                    var swingObjectVelocity = currentSwingObject.transform.Find("SwingPivot").GetComponent<Rigidbody2D>().velocity;
                    player.velocity = new Vector3(swingObjectVelocity.x * swingJumpForceX, 1 * swingJumpForceY, 0);

                    isHooked = false;
                    Destroy(currentSwingObject);
                }
            }

            if (Input.mouseScrollDelta.y != 0)
            {
                distanceJoint2D = currentSwingObject.GetComponent<DistanceJoint2D>();
                distanceJoint2D.distance += Input.mouseScrollDelta.normalized.y * ChangeDistanceSpeed;
            }
        }
        else
        {
            if (currentSwingObject != null)
                Destroy(currentSwingObject);
        }

        Debug.DrawRay(transform.position, AimDirection() * Range, Color.red);
        Debug.DrawRay(transform.position, AimDirection() * minPointDistance, Color.cyan);
    }

    GameObject InstantiateSwingObject(Vector2 position)
    {
        return Instantiate(SwingPrefab, position, Quaternion.identity);
    }


    Vector3 AimDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return (mousePos - transform.position).normalized;
    }
}

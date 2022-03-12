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
    public float aimRange = 20;

    public float maxRopeRange = 5;
    public float minRopeRange = 2;

    public LayerMask LayerMask;

    public float SwingSpeed = 10;
    public bool isHooked;

    public float swingJumpForceX = 5;
    public float swingJumpForceY = 30;

    public float minPointDistance = 10;

    //[HideInInspector]
    public Transform swingPosition;

    private GameObject currentSwingObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (isHooked)
        //{
        //    Vector2 distance = SwingPrefab.transform.position.normalized - swingPosition.position.normalized;
        //    print(Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg);
        //}
            //print(Vector2.SignedAngle(SwingPrefab.transform.position, swingPosition.position));

        if (player.currentState != Player.states.death)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (!isHooked)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, AimDirection(), aimRange, LayerMask);
                    if (hit.collider != null)
                    {
                        print(hit.collider.name);
                        float hitPointDistance = Vector2.Distance(hit.point, playerTransform.position);

                        if (hit.point.y > playerTransform.position.y && hitPointDistance > minPointDistance)
                        {
                            currentSwingObject = InstantiateSwingObject(hit.point);
                            distanceJoint2D = currentSwingObject.GetComponent<DistanceJoint2D>();
                            distanceJoint2D.distance = hitPointDistance;
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
            if (isHooked)
            {
                //if para qnd o tamanho da corda for maior que X, voltar para o máximo
                if (distanceJoint2D != null)
                {   
                    if(distanceJoint2D.distance > minRopeRange)
                    {
                        if (distanceJoint2D.distance < maxRopeRange)
                        {
                            print(distanceJoint2D.distance);

                            if (Input.mouseScrollDelta.y != 0)
                            {
                                distanceJoint2D.distance += Input.mouseScrollDelta.normalized.y * ChangeDistanceSpeed;
                            }
                        }
                        else
                        {
                            distanceJoint2D.distance -= 0.1f * Time.deltaTime;
                        }
                    }
                    else
                    {
                        distanceJoint2D.distance += 0.1f * Time.deltaTime;
                    }
                }
            }
        }
        else
        {
            if (currentSwingObject != null)
                Destroy(currentSwingObject);
        }

        Debug.DrawRay(transform.position, AimDirection() * aimRange, Color.red);
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

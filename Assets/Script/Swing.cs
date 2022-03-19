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
    public float currentRopeRange = 0;
    public float playerDistanceFromHookSpot = 0;

    public LayerMask LayerMask;

    public float SwingSpeed = 10;
    public bool isHooked;

    public float swingJumpForceX = 5;
    public float swingJumpForceY = 30;

    public float minPointDistance = 10;

    public bool ShouldSwing;

    //[HideInInspector]
    public Transform swingPosition;

    public GameObject currentSwingObject;

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
                            currentRopeRange = hitPointDistance;
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
                playerDistanceFromHookSpot = Vector3.Distance(player.transform.position - player.SwingSpotOffset, currentSwingObject.transform.position);

                print($"Real Rope Distance = {playerDistanceFromHookSpot} | Desired Rope Distance = {currentRopeRange}");
                
                //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, player.jumpHeight, LayerMask);
                //Debug.DrawRay(transform.position, Vector3.down * player.jumpHeight, Color.white);

                if (player.controller2D.info.down)
                    ShouldSwing = false;

                //if(Input.mouseScrollDelta.y != 0)
                if (currentRopeRange < playerDistanceFromHookSpot)
                    ShouldSwing = true;


                //if para qnd o tamanho da corda for maior que X, voltar para o máximo
                if (distanceJoint2D != null)
                {   
                    if(distanceJoint2D.distance > minRopeRange)
                    {
                        if (distanceJoint2D.distance < maxRopeRange)
                        {
                            //Dentro do Range de Distancia (min e max), então pode aumentar/diminuir a distancia da corda
                            if (Input.mouseScrollDelta.y != 0)
                            {
                                currentRopeRange = Mathf.Clamp(currentRopeRange += Input.mouseScrollDelta.normalized.y * ChangeDistanceSpeed, minRopeRange, maxRopeRange);

                                distanceJoint2D.distance += Input.mouseScrollDelta.normalized.y * ChangeDistanceSpeed;
                            }
                        }
                        else
                        {
                            //Tentando aumentar a corda além do máximo.
                            //Impedir que isso aconteca diminuindo o valor da distancia
                            distanceJoint2D.distance -= 0.1f;
                        }
                    }
                    else
                    {
                        //Tentando diminuir a corda além do mínimo.
                        //Impedir que isso aconteca aumentando o valor da distancia
                        distanceJoint2D.distance += 0.1f;
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

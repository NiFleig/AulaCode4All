using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera vCam;

    public bool followPlayer = false;

    private Transform player;

    private void Start()
    {
        vCam = transform.parent.GetComponentInChildren<CinemachineVirtualCamera>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    public void TriggerCamera()
    {
        CinemachineVirtualCamera currentCamera = (CinemachineVirtualCamera)Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera;

        if(followPlayer == true)
        {
            vCam.Follow = player;
        }
    }
}

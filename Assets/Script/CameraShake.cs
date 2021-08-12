using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public float duration;
    public float force;
    public float elapsedTime;

    private CinemachineVirtualCamera shakeCam;
    public CinemachineVirtualCamera backToCam;

    private void Awake()
    {
      shakeCam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
      if(elapsedTime > 0)
      {
        elapsedTime -= Time.deltaTime;
      }else
      {
        CinemachineBasicMultiChannelPerlin perlin = shakeCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        perlin.m_AmplitudeGain = 0;
        changePriority();
      }
    }

    public void Shake()
    {
      CinemachineBasicMultiChannelPerlin perlin = shakeCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

      perlin.m_AmplitudeGain = force;
      elapsedTime = duration;
    }

    public IEnumerator changePriority()
    {
      backToCam.Priority = 15;
      shakeCam.Priority = 1;

      return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration;
    public float force;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public void Shake()
    {
      Vector3 currentPosition = transform.localPosition;

      float elapsedTime = 0f;

      while(elapsedTime < duration)
      {
        float shakeX = Random.Range(minX, maxX)*force;
        float shakeY = Random.Range(minY, maxY)*force;

        transform.localPosition = new Vector3(shakeX, shakeY, currentPosition.z);

        elapsedTime += Time.deltaTime;

        Debug.Log("shaking");
      }

      transform.localPosition = currentPosition;
      Debug.Log("Stop Shaking");
    }
}

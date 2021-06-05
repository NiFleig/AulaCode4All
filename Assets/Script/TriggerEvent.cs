using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
   public string tagName;

   public UnityEngine.Events.UnityEvent OnTriggerEnter;
   public UnityEngine.Events.UnityEvent OnTriggerStay;
   public UnityEngine.Events.UnityEvent OnTriggerExit;

   void OnTriggerEnter2D(Collider2D collision)
   {
       if(collision.gameObject.tag == tagName)
       {
           OnTriggerEnter.Invoke();
           Debug.Log("cam");
       }
   }

   void OnTriggerStay2D(Collider2D collision)
   {
       if(collision.gameObject.tag == tagName)
       {
           OnTriggerStay.Invoke();
       }
   }

   void OnTriggerExit2D(Collider2D collision)
   {
       if(collision.gameObject.tag == tagName)
       {
           OnTriggerExit.Invoke();
       }
   }
}

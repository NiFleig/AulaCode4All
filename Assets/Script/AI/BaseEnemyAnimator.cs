using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BaseEnemyAI))]
public class BaseEnemyAnimator : MonoBehaviour
{
    public Animator Anim;
    public BaseEnemyAI EnemyAI;


    void Awake()
    {
        if(Anim == null)
        {
            Anim = GetComponent<Animator>();
        }
        if(EnemyAI == null)
        {
            EnemyAI = GetComponent<BaseEnemyAI>();
        }
    }

    void Start()
    {

    }


    void Update()
    {
        switch(EnemyAI.currentState)
        {
            case EnemyState.Idle:
                Anim.SetTrigger("Idle");
                break;
            case EnemyState.Attention:
                Anim.SetTrigger("Attention");
                break;
            case EnemyState.Combat:
                Anim.SetTrigger("Combat");
                break;
        }
    }
}

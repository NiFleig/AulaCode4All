using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EnemyState
{
    Idle,
    Attention,
    Combat
}

[RequireComponent(typeof(SpriteRenderer))]
public abstract class BaseEnemyAI : MonoBehaviour
{
    [Header("State Variables")]
    public EnemyState currentState;

    [Header("Sensor Variables")]
    public float ViewDistance;
    public float AttackDistance;
    public Transform PlayerTransform;
    public Vector3 TargetPosition;

    [Header("Events")]
    public UnityEvent OnEnterCombatStage;
    public UnityEvent OnExitCombatStage;

    [Header("Components")]
    public SpriteRenderer SpriteRenderer;


    [Header("Misc")]
    private Vector3 lastFramePosition;
    
    void Start()
    {
    }

    void Update()
    {
        ChangeState();

      //  IsMoving();
    }
    
    public void IsMoving()
    {
        float distanceFromLastPosition = Vector3.Distance(lastFramePosition, transform.position);

        if (distanceFromLastPosition != 0)
            SpriteRenderer.flipX = distanceFromLastPosition < 0 ? false : true;

        lastFramePosition = transform.position;
    }

    protected virtual bool IsAttentionDistance()
    {
        return Vector3.Distance(transform.position, PlayerTransform.position) < ViewDistance;
    }

    protected virtual bool IsCombatDistance()
    {
        return Vector3.Distance(transform.position, PlayerTransform.position) < AttackDistance;
    }

    public abstract void Attack();

    private void ChangeState()
    {
        if(IsCombatDistance())
        {
            currentState = EnemyState.Combat;
            OnCombat();
        }
        else if (IsAttentionDistance())
        {
            currentState = EnemyState.Attention;
            OnAttention();
        }
        else
        {
            currentState = EnemyState.Idle;
            OnIdle();
        }
    }

    protected virtual void OnIdle()
    {
        print("BaseEnemyAi est� parada.");
    }

    protected virtual void OnAttention()
    {
        print("BaseEnemyAi est� em aten��o.");
    }

    protected virtual void OnCombat()
    {
        print("BaseEnemyAi est� em combate.");
        Attack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, ViewDistance);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackDistance);
    }
}
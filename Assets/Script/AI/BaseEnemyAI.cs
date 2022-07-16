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
    public EnemyState currentState;

    public float ViewDistance;

    public float AttackDistance;

    public Transform TargetTransform;

    public UnityEvent OnEnterCombatStage;
    public UnityEvent OnExitCombatStage;

    public SpriteRenderer SpriteRenderer;
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
        return Vector3.Distance(transform.position, TargetTransform.position) < ViewDistance;
    }

    protected virtual bool IsCombatDistance()
    {
        return Vector3.Distance(transform.position, TargetTransform.position) < AttackDistance;
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

    protected virtual void OnCombat()
    {
        print("BaseEnemyAi est� em combate.");
        Attack();
    }

    protected virtual void OnAttention()
    {
        print("BaseEnemyAi est� em aten��o.");
    }

    protected virtual void OnIdle()
    {
        print("BaseEnemyAi est� parada.");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, ViewDistance);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackDistance);
    }
}
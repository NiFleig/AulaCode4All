using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAI : BaseEnemyAI
{
    private Vector3 StartPosition;
    private float lastAttackTimer;

    [Space(10)]
    public BeeMovementStats movementStats;
    public BeePatrolStats patrolStats;
    public BeeCombatStats combatStats;

    private void Start()
    {
        StartPosition = transform.position;
    }

    protected override void OnIdle()
    {
        base.OnIdle();

        transform.position += (TargetPosition - transform.position).normalized * movementStats.FlyPatrolSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, TargetPosition) < 0.1f)
        {
            SelectRandomTargetPosition(transform.position);
        }

        if (Vector3.Distance(transform.position, StartPosition) > patrolStats.maxDistanceFromStart)
        {
            SelectRandomTargetPosition(StartPosition);
        }
    }

    private void SelectRandomTargetPosition(Vector3 referencePoint)
    {
        var pos = referencePoint;

        TargetPosition = new Vector3(
            UnityEngine.Random.Range(pos.x - patrolStats.minPatrolDistance, pos.x + patrolStats.maxPatrolDistance),
            UnityEngine.Random.Range(pos.y - patrolStats.minPatrolDistance, pos.y + patrolStats.maxPatrolDistance),
            0);
    }

    protected override void OnAttention()
    {
        print("Eu sou uma abelha comum e em atenção.");

        if (currentState == EnemyState.Attention)
            transform.position += (PlayerTransform.position - transform.position).normalized * movementStats.FlyChaseSpeed * Time.deltaTime;
            //transform.position = Vector3.MoveTowards(transform.position, PlayerTransform.position, 0.001f);
    }

    protected override void OnCombat()
    {
        AttackByRate();
    }

    public void AttackByRate()
    {
        lastAttackTimer -= Time.deltaTime;
        
        if (lastAttackTimer <= 0)
        {
            lastAttackTimer = combatStats.AttackRateInSeconds;
            Attack();
        }
    }

    public override void Attack()
    {
        if (combatStats.playerStateComponent == null)
            combatStats.playerStateComponent = PlayerTransform.gameObject.GetComponent<PlayerState>();

        if (combatStats.playerStateComponent != null)
            combatStats.playerStateComponent.ApplyDamage(combatStats.AttackDamage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(StartPosition, patrolStats.maxDistanceFromStart);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, ViewDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackDistance);

        Gizmos.color = Color.white;
        Gizmos.DrawSphere(TargetPosition, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.name);

        if (collision != null)
        {
            SelectRandomTargetPosition(transform.position);
        }
    }
}

[Serializable]
public class BeeMovementStats
{
    public float FlyPatrolSpeed;
    public float FlyChaseSpeed;
}

[Serializable]
public class BeePatrolStats
{
    public float minPatrolDistance;
    public float maxPatrolDistance;
    public float maxDistanceFromStart;
}

[Serializable]
public class BeeCombatStats
{
    public PlayerState playerStateComponent;
    public int AttackDamage;
    
    public float AttackRateInSeconds;
}
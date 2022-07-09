using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAI : BaseEnemyAI
{
    public float FlySpeed;
    public int AttackDamage;

    public float AttackRateInSeconds;
    private float lastAttackTimer;

    protected override void OnAttention()
    {
        print("Eu sou uma abelha comum e em atenção.");

        if (currentState == EnemyState.Attention)
            transform.position += (TargetTransform.position - transform.position).normalized * FlySpeed * Time.deltaTime;
            //transform.position = Vector3.MoveTowards(transform.position, PlayerTransform.position, 0.001f);
    }

    protected override void OnCombat()
    {
        AttackByRate();
    }

    public override void Attack()
    {
        var playerState = TargetTransform.gameObject.GetComponent<PlayerState>();

        print(playerState);

        if (playerState != null)
            playerState.UpdateHP(AttackDamage);
    }

    public void AttackByRate()
    {
        lastAttackTimer -= Time.deltaTime;
        
        if (lastAttackTimer <= 0)
        {
            lastAttackTimer = AttackRateInSeconds;
            Attack();
        }
    }
}
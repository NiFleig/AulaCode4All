using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgressiveBee : BaseEnemyAI
{
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnAttention()
    {
        //base.OnAttention();
        print("Eu sou uma abelha agressiva e em atenção.");
    }
}

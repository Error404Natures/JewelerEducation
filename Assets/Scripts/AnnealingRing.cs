using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnealingRing : BaseHeat
{
    private Animator animator;

    protected override bool CorrectState() => StateManager.solderingSubState == SolderingSubState.HeatingRing;

    protected override void SubStateChanged() => StateManager.instance.SolderingSubStateComplete();

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject != targetObj || !CorrectState() || !BurnerController.isHeating) return;

        animator = targetObj.GetComponent<Animator>();
        animator.speed = 1.0f;
        animator.SetTrigger("isAnnealing");
        ProcessTrigger(other);
    }
}

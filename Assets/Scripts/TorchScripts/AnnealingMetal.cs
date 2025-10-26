using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnealingMetal : BaseHeat
{
    private Animator animator;

    protected override bool CorrectState() => StateManager.rollingSubState == RollingSubState.AnnealingMetal;
    protected override void SubStateChanged() => StateManager.instance.RollingSubStateComplete();

    private void Start()
    {
        animator = targetObj.GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == targetObj && 
            CorrectState() && BurnerController.isHeating)
        {
            animator.speed = 1;
            animator.SetTrigger("isAnnealing");
            ProcessTrigger(other);
        }
        else if (!BurnerController.isHeating && CorrectState())
        {
            animator.speed = 0;
        }
    }
}

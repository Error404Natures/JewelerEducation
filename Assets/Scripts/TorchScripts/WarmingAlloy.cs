using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmingAlloy : BaseHeat
{
    private Animator animator;

    private void Start()
    {
        animator = targetObj.GetComponent<Animator>();
    }

    protected override bool CorrectState() => StateManager.meltingSubState == MeltingSubState.WarmingAlloy;
    protected override void SubStateChanged() => StateManager.instance.MeltingSubStateComplete();

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == targetObj && CorrectState() 
            && BurnerController.isHeating)
        {
            animator.speed = 1;
            animator.SetTrigger("isFilling");
            ProcessTrigger(other);
        }
        else if (!BurnerController.isHeating)
        {
            animator.speed = 0;
        }

    }
}

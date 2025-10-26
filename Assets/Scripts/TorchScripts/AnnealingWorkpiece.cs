using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnealingWorkpiece : BaseHeat
{
    private Animator animator;

    private void Start()
    {
        animator =  targetObj.GetComponent<Animator>();
    }

    protected override bool CorrectState() => StateManager.cuttingSubState == CuttingSubState.AnnealingWorkpiece;

    protected override void SubStateChanged() => StateManager.instance.CuttingSubStateComplete();


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == targetObj && BurnerController.isHeating)
        {
            animator.SetTrigger("Annealing");
            ProcessTrigger(other);
        }
    }
}

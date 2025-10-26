using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DippingWorkpieceCitric : BaseProcess
{
    private Animator animator;
    protected override void CompleteSubState() => StateManager.instance.CuttingSubStateComplete();

    protected override bool IsCorrectState() => StateManager.cuttingSubState == CuttingSubState.Dipping—itric;

    // Start is called before the first frame update
    void Start()
    {
        useTimer = true;
        isEffects = true;

        animator = silver.GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == silver && IsCorrectState())
        {
            animator.SetTrigger("Cooling");
            ProcessTrigger(other);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DippingRingCitric : BaseProcess
{
    private Animator animator;

    protected override void CompleteSubState() => StateManager.instance.SolderingSubStateComplete();

    protected override bool IsCorrectState() => StateManager.solderingSubState == SolderingSubState.DippingCitric;

    private void Start()
    {
        animator = silver.GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == silver && IsCorrectState())
        {
            animator.SetTrigger("isCool");
            ProcessTrigger(other);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DippingRingFlux : BaseProcess
{
    private void Start()
    {
        useTimer = false;
        isEffects = false;
    }

    protected override void CompleteSubState() =>  StateManager.instance.SolderingSubStateComplete();

    protected override bool IsCorrectState() => StateManager.solderingSubState == SolderingSubState.DippingFlux;

    private void OnTriggerStay(Collider other) => ProcessTrigger(other);
}

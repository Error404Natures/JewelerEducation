using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DipingWorkpieceFlux : BaseProcess
{
    protected override bool IsCorrectState() => StateManager.cuttingSubState == CuttingSubState.DippingFlux;

    protected override void CompleteSubState() => StateManager.instance.CuttingSubStateComplete();

    // Start is called before the first frame update
    void Start()
    {
        isEffects = false;
        useTimer = false;
    }

    private void OnTriggerStay(Collider other) => ProcessTrigger(other);
}

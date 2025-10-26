using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlushingRing : BaseProcess
{
    protected override void CompleteSubState() => StateManager.instance.SolderingSubStateComplete();

    protected override bool IsCorrectState() => StateManager.solderingSubState == SolderingSubState.FlushingWater;

    private void OnTriggerEnter(Collider other)
    {
        ProcessTrigger(other);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmingTigel : BaseHeat
{   
    protected override bool CorrectState() => StateManager.meltingSubState == MeltingSubState.HeatingCrucible;

    protected override void SubStateChanged() => StateManager.instance.MeltingSubStateComplete();

    private void OnTriggerStay(Collider other) => ProcessTrigger(other);
}

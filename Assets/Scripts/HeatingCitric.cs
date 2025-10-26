using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeatingCitric : BaseHeat
{
    protected override bool CorrectState() => StateManager.solderingSubState == SolderingSubState.HeatingWorkpieceCitricAcid;

    protected override void SubStateChanged() => StateManager.instance.SolderingSubStateComplete();

    private void OnTriggerStay(Collider other)
    {
        ProcessTrigger(other);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DippingFlux : BaseProcess
{
    private void Start()
    {
        isEffects = false;
        useTimer = false;
    }

    protected override bool IsCorrectState() => 
        StateManager.rollingSubState == RollingSubState.DippingFlux;

    protected override void CompleteSubState() => 
        StateManager.instance.RollingSubStateComplete();
   
    private void OnTriggerStay(Collider other) => ProcessTrigger(other);


}

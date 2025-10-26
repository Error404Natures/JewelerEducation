using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlushingWater : BaseProcess
{
    private void Start()
    {
        isEffects = false;
        useTimer = false;
    }

    protected override bool IsCorrectState() =>
        StateManager.rollingSubState == RollingSubState.RinsingWater;

    protected override void CompleteSubState() =>
        StateManager.instance.RollingSubStateComplete();

    private void OnTriggerEnter(Collider other) => ProcessTrigger(other);
}

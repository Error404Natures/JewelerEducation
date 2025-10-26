using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlushingWorkpiece : BaseProcess
{
    protected override void CompleteSubState() => StateManager.instance.CuttingSubStateComplete();

    protected override bool IsCorrectState() => StateManager.cuttingSubState == CuttingSubState.RinsingWater;

    // Start is called before the first frame update
    void Start()
    {
        isEffects = false;
        useTimer = false;
    }

    private void OnTriggerEnter(Collider other) => ProcessTrigger(other);
}

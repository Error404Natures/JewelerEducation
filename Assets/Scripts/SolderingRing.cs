using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SolderingRing : BaseHeat
{
    public GameObject zoneSolder;
    protected override bool CorrectState() => StateManager.solderingSubState == SolderingSubState.HeatingWorkpieceSolder;
    protected override void SubStateChanged() => StateManager.instance.SolderingSubStateComplete();

    private void Start()
    {
        StateManager.OnSolderingSubStateChanged += OnSolderingChanged;
    }

    private void OnSolderingChanged(SolderingSubState state)
    {
        if (state == SolderingSubState.HeatingWorkpieceSolder)
        {
            zoneSolder.SetActive(true);
        }
        else
        {
            zoneSolder.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (BurnerController.isHeating && CheckedContact.isContact)
        {
            ProcessTrigger(other);
        }
    }

}

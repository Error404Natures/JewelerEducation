using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class WaitingCooldown : MonoBehaviour
{
    public GameObject meltedSilver;
    public Transform spawnPosition;

    private void Start()
    {
        StateManager.OnMeltingSubStateChanged += OnMeltingStateChanged;
    }

    private void OnMeltingStateChanged(MeltingSubState state)
    {
        if (state == MeltingSubState.WaitingForCooling)
        {
            meltedSilver.SetActive(true);

            meltedSilver.transform.position = spawnPosition.position;
            meltedSilver.transform.rotation = spawnPosition.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (StateManager.meltingSubState != MeltingSubState.WaitingForCooling) return;

        if (other.gameObject == meltedSilver.gameObject)
        {
            meltedSilver.GetComponent<Throwable>().enabled = false;
            meltedSilver.GetComponent<Interactable>().enabled = false;

            StateManager.instance.MeltingSubStateComplete();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanding : MonoBehaviour
{
    public GameObject ring;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ring && StateManager.grindingSubState == GrindingSubState.Sanding)
        {
            StateManager.instance.GrindingSubStateComplete();
        }
    }
}

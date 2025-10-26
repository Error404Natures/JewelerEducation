using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PutBoart : MonoBehaviour
{
    public GameObject ring;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ring && StateManager.solderingSubState == SolderingSubState.PutBoardRing)
        {
            StateManager.instance.SolderingSubStateComplete();
        }
        else if (other.CompareTag("Solder") && StateManager.solderingSubState == SolderingSubState.PutBoardSolder)
        {
            StateManager.instance.SolderingSubStateComplete();
        }
    }
}

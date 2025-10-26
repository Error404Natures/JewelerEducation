using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSilverPieces : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PiecesSilver"))
        {
            if (StateManager.meltingSubState == MeltingSubState.AddingMetal)
            {
                StateManager.instance.MeltingSubStateComplete();
                Debug.Log("Переходим к прогреву сплава до n t ");
            }

            if (StateManager.meltingSubState == MeltingSubState.CrucibeIntoTheMold)
                collision.gameObject.SetActive(false);
              ////collision.gameObject, Random.Range(0.1f, 2f));

        }
        
    }
}

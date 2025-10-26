using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckedContact : MonoBehaviour
{
    public static bool isContact = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Solder"))
        {
            isContact = true;
            Debug.Log($"isContact: {isContact}");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Solder"))
        {
            isContact = false;
            Debug.Log($"isContact: {isContact}");
        }
    }
}

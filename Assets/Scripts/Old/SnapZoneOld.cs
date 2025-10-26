using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapZoneOld : MonoBehaviour
{
    public Transform snapPoint;
    private bool objectSnapped = false;

    private void OnTriggerEnter(Collider other)
    {
        if (objectSnapped)return;

        if (other.gameObject.CompareTag("Workpiece"))
        {
            other.transform.position = snapPoint.transform.position;
            other.transform.rotation = snapPoint.transform.rotation;

            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            objectSnapped = true;
        }
    }
    private void OnTriggerExit(Collider other)
    { 

        if (other.CompareTag("Player"))
        {
            objectSnapped = false;

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }
}

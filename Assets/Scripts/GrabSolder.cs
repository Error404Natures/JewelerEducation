using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabSolder : MonoBehaviour
{
    public GameObject point;

    private GameObject solder;
    private Rigidbody rb;
    private bool isFix = false;

    private void Start()
    {
        StateManager.OnSolderingSubStateChanged += OnSolderSubStateChanged;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Solder") && StateManager.solderingSubState == SolderingSubState.GrabSolder)
        {
            rb = other.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;

            isFix = true;
            solder = other.gameObject;

            StateManager.instance.SolderingSubStateComplete();
        }
    }

    private void Update()
    {
        if (!isFix) return;

        solder.transform.position = point.transform.position;
        solder.transform.rotation = point.transform.rotation;
    }
    private void OnSolderSubStateChanged(SolderingSubState state)
    {
        if (state == SolderingSubState.DippingCitric && isFix)
        {
            solder.SetActive(false);
        }
    }
}

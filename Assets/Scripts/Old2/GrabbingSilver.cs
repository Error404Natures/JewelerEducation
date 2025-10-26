using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingSilver : MonoBehaviour
{
    public event Action OnGrabingSilver;

    public GameObject silverSoldering;
    public GameObject fixPoint;

    public static bool isFixed = false;

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject == silverSoldering && StateManager.currentState == State.GrabingSilver)
        {
            silverSoldering.transform.position = fixPoint.transform.position;
            silverSoldering.transform.rotation = fixPoint.transform.rotation;

            silverSoldering.GetComponent<BoxCollider>().enabled = false;

            isFixed = true;

            OnGrabingSilver.Invoke();

            silverSoldering.transform.SetParent(fixPoint.transform, true);
        }
    }

    private void Update()
    {
        if (isFixed)
        {
            silverSoldering.transform.position = fixPoint.transform.position;
            silverSoldering.transform.rotation = fixPoint.transform.rotation;
        }
    }
}

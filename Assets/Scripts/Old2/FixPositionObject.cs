using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class FixPositionObject : MonoBehaviour
{
    
    public string tagGameObject;
    public Transform pointToSet;
    public GameObject gameObjectSet;
    public static bool isFixed = false;

    private void Update()
    {
        if (isFixed)
        {
            gameObjectSet.transform.position = pointToSet.position;
            gameObjectSet.transform.rotation = pointToSet.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagGameObject))
        {
            isFixed = true;

            Rigidbody rb = gameObjectSet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }

            gameObjectSet.GetComponent<Throwable>().enabled = false;
            gameObjectSet.GetComponent<Interactable>().enabled = false;
            gameObjectSet.GetComponent<BoxCollider>().enabled = false;

            gameObjectSet.transform.position = transform.position;
            gameObjectSet.transform.rotation = transform.rotation;
            gameObjectSet.transform.SetParent(pointToSet, true);
        }
    }
}

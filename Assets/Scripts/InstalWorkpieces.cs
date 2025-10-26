using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstalWorkpieces : MonoBehaviour
{
    public GameObject silver;
    public GameObject pointFix;
    private bool isFix = false;

    private Rigidbody rbSilver;

    // Start is called before the first frame update
    void Start()
    {
        StateManager.OnMainStateChanged += OnMainStateChanged;
        pointFix.SetActive(false);
    }

    private void OnMainStateChanged(MainState bendingState)
    {
        if (bendingState == MainState.BendingTheWorkpiece)
        {
            pointFix.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == silver && CorrectState() && !isFix)
        {
            pointFix.SetActive(false);
            rbSilver = silver.GetComponent<Rigidbody>();
            rbSilver.velocity = Vector3.zero;
            rbSilver.isKinematic = true;

            isFix = true;
            StateManager.instance.BendingSubStateComplete();
        }
    }

    private void Update()
    {
        if (isFix)
        {
            silver.transform.SetPositionAndRotation(pointFix.transform.position, pointFix.transform.rotation);
            //silver.transform.SetParent(transform.parent, true);
        }
    }

    private bool CorrectState() => StateManager.bendingSubstate == BendingSubstate.installTheWorkpiece;
}

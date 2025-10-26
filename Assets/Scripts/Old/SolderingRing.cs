using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolderingRing1 : MonoBehaviour
{
    public GameObject burner;

    // Start is called before the first frame update
    void Start()
    {
        TorchTreatment torchTreatment = burner.GetComponent<TorchTreatment>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

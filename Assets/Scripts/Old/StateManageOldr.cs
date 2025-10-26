using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateManagerOld : MonoBehaviour
{
     
    public static StateManagerOld instance;

    public GameObject[] modelsState;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        modelsState[1].gameObject.SetActive(false);
    }

    public void UpdateState()
    {
        modelsState[1].transform.position = modelsState[0].transform.position;

        Destroy(modelsState[0]);
        modelsState[1].gameObject.SetActive(true);
    }
}

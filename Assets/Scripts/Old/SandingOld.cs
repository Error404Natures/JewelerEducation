using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SandingOld : MonoBehaviour
{
    public GameObject[] prefabs;

    public bool isSanding = false;

    private GameObject currentObj;



    private void Start()
    {
        currentObj = prefabs[0];
        currentObj.SetActive(true);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Sanding")
        {
            isSanding = true;

            currentObj.SetActive(false);
            StartCoroutine(SandingProcessing());



            

            Debug.Log($"{currentObj}");

            Debug.Log("Мы в if");
        }
    }
    private void OnCollisionExit(Collision collision)
    {

        isSanding = false ;
        Debug.Log("isSanding = false");
    }

    private IEnumerator SandingProcessing()
    {
        float timer =  5f;

        while (true) 
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;

                currentObj = prefabs[1];
                currentObj.SetActive(true);
            }
        }
        
    }
}

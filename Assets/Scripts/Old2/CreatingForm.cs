using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatingForm : MonoBehaviour
{
    public event Action OnRemovingFromMold;

   // public ParticleSystem particleSystem;

    public GameObject silver;
    public GameObject tigel;
    public GameObject ingus;

    public float pouiringTime = 3f;

    private void Start()
    {
       // particleSystem.Stop(); 
    }

    private void OnTriggerStay(Collider other)
    {
       // if (other.gameObject == ingus.gameObject 
          //  && StateManager.currentState == State.PouringIntoMold)
        {
          //  if (pouiringTime < 0) particleSystem.Stop();

            pouiringTime -= Time.deltaTime;

            //particleSystem.Play();

            if (pouiringTime < 0)
            {
                silver.SetActive(false);
               // particleSystem.Stop();
                OnRemovingFromMold?.Invoke();
                Debug.Log("Выплавка формы завершена");;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == ingus.gameObject)
        {
           // particleSystem.Stop();
        }
    }
}

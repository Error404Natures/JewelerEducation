using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrucibeIntoTheMold : MonoBehaviour
{
    public GameObject IntoMold;

    public ParticleSystem fusedParcticles;
    public Animator animator;

    public float timer = 2.4f;

    private void Start()
    {
        fusedParcticles.Stop();
        animator = GetComponent<Animator>();
        fusedParcticles.enableEmission = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == IntoMold.gameObject
            && StateManager.meltingSubState == MeltingSubState.CrucibeIntoTheMold)
        {
            timer -= Time.deltaTime;

            fusedParcticles.enableEmission = true;
            fusedParcticles.Play();

            animator.GetComponent<Animator>().speed = 1;
            animator.SetTrigger("isPouring");
            
            if (timer <= 0)
            {
                StateManager.instance.MeltingSubStateComplete();
                fusedParcticles.Stop();
                fusedParcticles.enableEmission = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == IntoMold.gameObject)
        {
            fusedParcticles.Stop();
            animator.GetComponent<Animator>().speed = 0;
        }
    }
}

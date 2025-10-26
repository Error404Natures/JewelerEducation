using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AddBorax : MonoBehaviour
{
    public ParticleSystem particle;

    private void Start()
    {
        particle.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Burax"))
        {
            if (StateManager.meltingSubState == MeltingSubState.AddingBorax)
            {
                StateManager.instance.MeltingSubStateComplete();
                Debug.Log("Переходим к добавлению серебра");
            }

            particle.Play();

            collision.gameObject.SetActive(false);

            ////Destroy(collision.gameObject, Random.Range(0.1f, 3f));
        }
        else
        {
            particle.Stop();
        }
    }
}
